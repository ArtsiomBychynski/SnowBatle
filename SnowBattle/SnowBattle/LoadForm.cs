using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowBattle
{
    public partial class LoadForm : Form
    {
        public Client client = new Client();
        Thread client_Start;
        Thread client_Start_Game;
        public LoadForm()
        {
            InitializeComponent();
            //устанавливка настроек формы 
            this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = this.BackColor;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 550;
            this.Height = 320;
            this.BackgroundImage = Properties.Resources.Map;
            //запуск сервера для прослушки сети 
            client_Start = new Thread(client.Start);
            client_Start.Start();
            client_Start_Game = new Thread(client.StartGameServer);
            client_Start_Game.Start();
            //поиск сервера в сети 
            client.ScanLocal();
        }

        private void LoadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //закрыие программы и всех потоков
            SendTo("CloseProgram:");
            client_Start.Abort();
            client_Start_Game.Abort();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            //настройки формы
            pictureBoxCloseLoad.BackColor = Color.Transparent;
            pictureBoxCloseLoad.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCloseLoad.Image = Properties.Resources.Close;
            pictureBoxSnowLoad.BackColor = Color.Transparent;
            pictureBoxSnowLoad.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxSnowLoad.Image = Properties.Resources.Snow;
            pictureBoxLoginAndPass.BackColor = Color.Transparent;
            pictureBoxLoginAndPass.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLoginAndPass.Image = Properties.Resources.login;
            pictureBoxRefresh.BackColor = Color.Transparent;
            pictureBoxRefresh.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxRefresh.Image = Properties.Resources.refresh;
            pictureBoxRefresh.Visible = false;
            pictureBoxPlay.BackColor = Color.Transparent;
            pictureBoxPlay.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPlay.Image = Properties.Resources.Start;
            pictureBoxPlay.Visible = false;
            listBoxLoadStaticPlayer.Visible = false;
            listBoxLoadStaticPlayer.MultiColumn = true;
        }

        private void pictureBoxCloseLoad_MouseDown(object sender, MouseEventArgs e)
        {
            //отображение иконки close
            pictureBoxCloseLoad.Image = Properties.Resources.CloseSecond;
        }

        private void pictureBoxCloseLoad_MouseUp(object sender, MouseEventArgs e)
        {
            //выход из программы
            pictureBoxCloseLoad.Image = Properties.Resources.Close;
            if (DialogResult.Yes == MessageBox.Show("Вы действительно хотите выйти?", "Внимание!", MessageBoxButtons.YesNo))
            {
                this.Close();
            }
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            //отправка данных на сервер
            if (textBoxLogin.Text != "")
            {
                if (client.ip == null)
                {
                    MessageBox.Show("Сервер не найден! Пожалуйста проверьте подключение к локальной сети и перезагрузите программу.", "Ошибка сети");
                    this.Close();
                }
                else
                {
                    if (!SendTo("Login:" + textBoxLogin.Text))
                    {
                        MessageBox.Show("Сервер не отвечает! Пожалуйста повторите запрос позже.", "Ошибка сети");
                        this.Close();
                        return;
                    }
                    pictureBoxLoginAndPass.Visible = false;
                    textBoxLogin.Visible = false;
                    buttonEnter.Visible = false;
                    client.loadForm = this;
                    listBoxLoadStaticPlayer.Visible = true;
                    pictureBoxPlay.Visible = true;
                    pictureBoxRefresh.Visible = true;
                    client.SendTo("Update:");
                }
            }
            else
            {
                MessageBox.Show("Введите имя пользователя !", "Ошибка ввода данных");
            }
        }

        //метод отправки с проверкой доставки сообщения 
        private bool SendTo(string text)
        {
            int number = 0;
            while (number != 5)
            {
                client.SendTo(text);
                Thread.Sleep(300);
                if (client.messageIn == "<OK>" + text || client.messageIn == "<NO>" + text)
                    return true;
                number++;
            }
            return false;
        }

        private void pictureBoxRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxRefresh.Image = Properties.Resources.refreshOne;
            client.SendTo("Update:");
        }

        private void pictureBoxRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxRefresh.Image = Properties.Resources.refresh;
        }

        private void pictureBoxPlay_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxPlay.Image = Properties.Resources.StartOne;
        }

        //создание или присоединение уже к готовой игре
        private void pictureBoxPlay_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxPlay.Image = Properties.Resources.Start;
            GameForm gameForm;
            if (listBoxLoadStaticPlayer.SelectedIndex > 0)
            {
                //присоидениться к игре 
                if (!SendTo("Join:" + listBoxLoadStaticPlayer.SelectedItem))
                {
                    MessageBox.Show("Сервер не отвечает! Пожалуйста повторите запрос позже.", "Ошибка сети");
                    return;
                }
                if (client.messageIn != "<NO>" + listBoxLoadStaticPlayer.SelectedItem)
                {
                    
                    client.ipAdrBuf = client.ip;
                    client.ip = client.ipAdrPlayer;
                    client.port = 9005;
                    if (!SendTo("GetIn:"))
                    {
                        MessageBox.Show("Сервер не отвечает! Пожалуйста повторите запрос позже.", "Ошибка сети");
                        return;
                    }

                    this.Visible = false;
                    gameForm = new GameForm(this);
                    if (gameForm.ShowDialog() == DialogResult.Yes)
                    {
                        if (!SendTo("EndGame:"))
                        {
                            MessageBox.Show("Сервер не отвечает! Пожалуйста повторите запрос позже.", "Ошибка сети");
                            return;
                        }
                        
                        
                    }
                    this.Close();
                    gameForm.Close();
                    this.Visible = false;
                    client.ip = client.ipAdrBuf;
                    client.port = 9000;
                    client.startGame = false;
                }
                else
                {
                    client.SendTo("Update:");
                }
            }
            else
                if (listBoxLoadStaticPlayer.SelectedIndex == 0)
                {
                    //создать игру
                    if (!SendTo("Invitation:" + textBoxLogin.Text))
                    {
                        MessageBox.Show("Сервер не отвечает! Пожалуйста повторите запрос позже.", "Ошибка сети");
                        return;
                    }
                    client.port = 9001;
                    this.Visible = false;
                    gameForm = new GameForm(this);
                    if (gameForm.ShowDialog() == DialogResult.Yes)
                    {
                        if (!SendTo("EndGame:"))
                        {
                            MessageBox.Show("Сервер не отвечает! Пожалуйста повторите запрос позже.", "Ошибка сети");
                            return;
                        }
                        
                    }
                    this.Close();
                    this.Visible = false;
                    gameForm.Close();
                    client.ip = client.ipAdrBuf;
                    client.port = 9000;
                    client.startGame = false;
                }
        }



       
    }
}
