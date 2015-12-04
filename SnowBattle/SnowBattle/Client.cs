using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowBattle
{
    public class Client
    {
        Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Socket mySocketGame = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        int size;// размер данных полученных

        byte[] data = new byte[1024];
        public int port = 9000;
        public LoadForm loadForm;
        public bool startGame = false;
        public string ipAdrPlayer = null;
        public string ipAdrBuf = null;
        public string ip = null;
        public int iSnowX = -20, iSnowY = -20;
        public string messageIn { get; set; }

        public string state1 = "Go", state2 = "Go", state3 = "Go";
        public Point[] personPoint = new Point[3];
        public void ClientNewPozition()
        {
            state1 = "Go";
            state2 = "Go";
            state3 = "Go";
            iSnowX = -20; 
            iSnowY = -20;
            startGame = false;
            personPoint[0] = new Point ( 800 - 60 - 500, 530 - 60 - 380 );
            personPoint[1] = new Point ( 800 - 60 - 620, 530 - 60 - 350 );
            personPoint[2] = new Point ( 800 - 60 - 610, 530 - 60 - 250 );
        }
        public void ScanLocal()
        {
            ScanLocal scan = new ScanLocal();
            scan.Start();
        }
        public void Start()
        {
            int x, y;
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 9001);
            mySocket.Bind(ipEndPoint);
            IPEndPoint senders = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(senders);
            while (true)
            {
                try
                {
                    byte[] dataIn = new byte[1024];
                    size = mySocket.ReceiveFrom(dataIn, ref remote);
                    string message = Encoding.Default.GetString(dataIn, 0, size);
                    string messageBuf = message;
                    message = message.Remove(message.IndexOf(':'));
                    switch (message)
                    {
                        case "<OK>StartProgram":
                            ip = remote.ToString().Remove(remote.ToString().IndexOf(":"), remote.ToString().Length - remote.ToString().IndexOf(":"));
                            break;
                        case "<OK>Login":
                            messageIn = messageBuf;
                            break;
                        case "<OK>Update":
                            loadForm.listBoxLoadStaticPlayer.BeginInvoke(AcceptDelegateList, new object[] { messageBuf.Remove(0, messageBuf.IndexOf(':') + 1), loadForm.listBoxLoadStaticPlayer });
                            break;
                        case "<OK>Invitation":
                            messageIn = messageBuf;
                            break;
                        case "<OK>EndGame":
                            messageIn = messageBuf;
                            break;
                        case "<OK>Join":
                            ipAdrPlayer = messageBuf.Remove(0, messageBuf.IndexOf('/') + 1);
                            messageIn = messageBuf.Remove(messageBuf.IndexOf('/'));
                            break;
                        case "<NO>Join":
                            messageIn = messageBuf;
                            break;
                        case "<OK>CloseProgram":
                            messageIn = messageBuf;
                            break;
                        case "<OK>GetIn":
                            messageIn = messageBuf;
                            startGame = true;
                            break;
                        case "State1":
                            if (port != 9001)
                            {
                                state1 = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            }
                            break;
                        case "State2":
                            if (port != 9001)
                            {
                                state2 = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            }
                            break;
                        case "State3":
                            if (port != 9001)
                            {
                                state3 = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            }
                            break;
                        case "XY1":
                            if (port != 9001)
                            {
                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                x = Convert.ToInt32( messageBuf.Remove(messageBuf.IndexOf('/')));
                                y = Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                                personPoint[0] = new Point(740 - x, 470 - y);
                                
                            }
                            break;
                        case "XY2":
                            if (port != 9001)
                            {
                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                x = Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                y = Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                                personPoint[1] = new Point(740 - x, 470 - y);
                            }
                            break;
                        case "XY3":
                            if (port != 9001)
                            {
                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                x = Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                y = Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                                personPoint[2] = new Point(740 - x, 470 - y);
                            }
                            break;
                        case "XYSnow":
                            if (port != 9001)
                            {
                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                iSnowX = 800 - Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                iSnowY = 530 - Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                            }
                            break;
                 
                    }
                }
                catch (System.Exception ex)
                {
                     MessageBox.Show("Client:"+ex.Message);
                }
            }
        }


        public void StartGameServer()
        {
            int x, y;
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 9005);
            mySocketGame.Bind(ipEndPoint);
            IPEndPoint senders = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(senders);
            while (true)
            {
                try
                {
                    byte[] dataIn = new byte[1024];
                    size = mySocketGame.ReceiveFrom(dataIn, ref remote);
                    string message = Encoding.Default.GetString(dataIn, 0, size);
                    string messageBuf = message;
                    message = message.Remove(message.IndexOf(':'));
                    switch (message)
                    {
                        case "GetIn":
                            ipAdrBuf = ip;
                            ip = remote.ToString().Remove(remote.ToString().IndexOf(":"), remote.ToString().Length - remote.ToString().IndexOf(":"));
                            startGame = true;
                            SendTo("<OK>" + messageBuf);
                            break;
                        case "State1":

                                state1 = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            
                            break;
                        case "State2":

                                state2 = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            
                            break;
                        case "State3":

                                state3 = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                            
                            break;
                        case "XY1":

                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                x = Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                y = Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                                personPoint[0] = new Point(740 - x, 470 - y);

                            break;
                        case "XY2":

                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                x = Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                y = Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                                personPoint[1] = new Point(740 - x, 470 - y);
                            
                            break;
                        case "XY3":

                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                x = Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                y = Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                                personPoint[2] = new Point(740 - x, 470 - y);
                            
                            break;
                        case "XYSnow":
                                messageBuf = messageBuf.Remove(0, messageBuf.IndexOf(':') + 1);
                                iSnowX = 800 - Convert.ToInt32(messageBuf.Remove(messageBuf.IndexOf('/')));
                                iSnowY = 530 - Convert.ToInt32(messageBuf.Remove(0, messageBuf.IndexOf('/') + 1));
                            break;

                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("ClientTwo:" + ex.Message);
                }
            }
        }
       
        delegate void SendMsgList(String Text, ListBox Rtb);
        SendMsgList AcceptDelegateList = (String Text, ListBox Rtb) =>
        {
            Rtb.Items.Clear();
                while (Text.IndexOf("/") != -1)
                {
                    Rtb.Items.Add(Text.Remove(Text.IndexOf('/')));
                    Text = Text.Remove(0, Text.IndexOf("/") + 1);
                }
                Rtb.Items.Add(Text);
        };
        public void SendTo(string text)
        {
            if (ip != null)
            {
                data = Encoding.Default.GetBytes(text);
                mySocket.SendTo(data, data.Length, SocketFlags.None, _get());
            }
        }

        private EndPoint _get()
        {
            IPAddress hostIPAddress = IPAddress.Parse(ip);
            IPEndPoint hostIPEndPoint = new IPEndPoint(hostIPAddress, port);
            return (EndPoint)(hostIPEndPoint);
        }
    }
}
