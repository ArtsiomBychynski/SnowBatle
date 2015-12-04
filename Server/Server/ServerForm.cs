using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class ServerForm : Form
    {
        //структура дапнных пользователя
        public struct UserOnline
        {
            public string ipAdr;
            public string userName;
            public string status;
        }
        public List<UserOnline> listUser = new List<UserOnline>();//список игроков
        public Socket mySocket;
        Thread start;
        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            //новый поток с сервером
            start = new Thread(Start);
            start.Start();
        }

        private void Start()
        {
            int size;// размер данных полученных
            byte[] data = new byte[1024];
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //слушаем порт 900
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 9000);
            mySocket.Bind(ipEndPoint);
            IPEndPoint senders = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(senders);
            List<UserOnline> listUserTemp;
            while (true)
            {
                try
                {
                    data = new byte[1024];
                    size = mySocket.ReceiveFrom(data, ref remote);
                    string message = Encoding.Default.GetString(data, 0, size);
                    textBoxShow.BeginInvoke(AcceptDelegate, new object[] { remote.ToString() + ":   " + message, textBoxShow });
                    string messageBuf = message;
                    message = message.Remove(message.IndexOf(':'));
                    switch (message)
                    {
                        case "StartProgram"://отсылаем данные сервера
                            data = Encoding.Default.GetBytes("<OK>StartProgram:");
                            mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            break;
                        case "Login"://регистрация игрока
                            var buf = new UserOnline();
                            buf.ipAdr = remote.ToString();
                            buf.userName = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            buf.status = "";
                            bool flagTemp = false;
                            int i = 1;
                            foreach (var temp in listUser)
                            {
                                if (buf.ipAdr == temp.ipAdr)
                                {
                                    flagTemp = true;
                                    break;
                                }
                                if (buf.userName == temp.userName)
                                {

                                    buf.userName = buf.userName + i.ToString();
                                    i++;
                                }
                            }
                            if (!flagTemp)
                                listUser.Add(buf);
                            data = Encoding.Default.GetBytes("<OK>" + messageBuf);
                            mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            break;
                        case "Update"://обновление данных о играх
                            string messageUpdate = "Создать новую игру";
                            foreach (var temp in listUser)
                            {
                                if (remote.ToString() != temp.ipAdr && temp.status == "Ожидает игрока")
                                {
                                    messageUpdate += " /" + temp.userName + ":  " + temp.status;
                                }
                            }
                            data = Encoding.Default.GetBytes("<OK>" + messageBuf + messageUpdate);
                            mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            break;
                        case "Invitation"://создание новой игры
                            data = Encoding.Default.GetBytes("<OK>" + messageBuf);
                            mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            listUserTemp = new List<UserOnline>();
                            foreach(var temp in listUser)
                            {
                                if (temp.ipAdr == remote.ToString())
                                {
                                    UserOnline tempUser = new UserOnline();
                                    tempUser.ipAdr = temp.ipAdr;
                                    tempUser.userName = temp.userName;
                                    tempUser.status = "Ожидает игрока";
                                    listUserTemp.Add(tempUser);
                                }
                                else
                                    listUserTemp.Add(temp);
                            }
                            listUser = listUserTemp;
                            break;
                        case "Join"://присоеденится к текущей игре
                            bool flagUser = false;
                            string ipPlayer = null;
                            listUserTemp = new List<UserOnline>();
                            foreach(var temp in listUser)
                            {
                                string str = messageBuf.Remove(0, messageBuf.IndexOf(":") + 1);
                                if (temp.userName == str.Remove(str.IndexOf(":")))
                                {
                                    flagUser = true;
                                    UserOnline tempUser = new UserOnline();
                                    tempUser.ipAdr = temp.ipAdr;
                                    ipPlayer = temp.ipAdr;
                                    tempUser.userName = temp.userName;
                                    tempUser.status = "";
                                    listUserTemp.Add(tempUser);
                                }
                                else
                                    listUserTemp.Add(temp);
                            }
                            listUser = listUserTemp;
                            if (flagUser)
                            {
                                data = Encoding.Default.GetBytes("<OK>" + messageBuf + '/' + ipPlayer.Remove(ipPlayer.IndexOf(':')));
                                mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            }
                            else
                            {
                                data = Encoding.Default.GetBytes("<NO>" + messageBuf);
                                mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            }
                            break;
                        case "EndGame"://игра закончена
                            data = Encoding.Default.GetBytes("<OK>" + messageBuf);
                            mySocket.SendTo(data, data.Length, SocketFlags.None, _get(remote.ToString()));
                            listUserTemp = new List<UserOnline>();
                            foreach(var temp in listUser)
                            {
                                if (temp.ipAdr == remote.ToString())
                                {
                                    UserOnline tempUser = new UserOnline();
                                    tempUser.ipAdr = temp.ipAdr;
                                    tempUser.userName = temp.userName;
                                    tempUser.status = "";
                                    listUserTemp.Add(tempUser);
                                }
                                else
                                    listUserTemp.Add(temp);
                            }
                            listUser = listUserTemp;
                            break;
                        case "CloseProgram"://закрыие программы
                            data = Encoding.Default.GetBytes("<OK>" + messageBuf);
                            string ipTemp = remote.ToString();
                            List<UserOnline> listUserBuf = new List<UserOnline>();
                            foreach (var temp in listUser)
                            {
                                if (temp.ipAdr != ipTemp)
                                {
                                    listUserBuf.Add(temp);
                                }
                            }
                            listUser = listUserBuf;
                            break;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Server: "+ex.Message);
                }
            }
        }

        //конечная точка для отсылки данных
        private EndPoint _get(string text)
        {
            string host = text.Remove(text.IndexOf(":"), text.Length - text.IndexOf(":"));
            IPAddress hostIPAddress = IPAddress.Parse(host);
            IPEndPoint hostIPEndPoint = new IPEndPoint(hostIPAddress, 9001);
            return (EndPoint)(hostIPEndPoint);
        }

        //вывод сообщений на форму для кантроля данных
        delegate void SendMsg(String text, TextBox textBox);
        SendMsg AcceptDelegate = (String Text, TextBox textBox) =>
        {
            textBox.Text += Text + "\r\n";
        };


        private void ServerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //завершение потока 
            start.Abort();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }


    }
}
