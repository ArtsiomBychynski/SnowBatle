using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnowBattle
{
    public partial class GameForm : Form
    {
        private int iMouseBufX, iMouseBufY;//координаты мышки
        private int iSnowX = 0, iSnowY = 0;//координаты снежка
        private int iPlayerBufX, iPlayerBufY, iPlayerTempX, iPlayerTempY;//координаты игрока
        private bool FlagMouseMoveForm = false;// активность перемещение формы 
        private Bitmap canvasBitmap;
        private Graphics canvasGr;
        string state1 = "Go", state2 = "Go", state3 = "Go";//состояние игрока
        bool flagClose = false;
        bool numberState1, numberState2, numberState3;
        bool flagState1 = false, flagState2 = false, flagState3 = false;
        Point[] personPoint = new Point[3];
        private int PersonNumber;
        LoadForm loadForm;
        public GameForm(LoadForm loadForm)
        {
            //инициализация формы
            InitializeComponent();
            this.loadForm = loadForm;
            FormBorderStyle = FormBorderStyle.None;
            this.Height = 530;
            this.Width = 800;
            this.BackgroundImage = Properties.Resources.Map;
            pictureBoxCanvas.Width = this.Size.Width;
            pictureBoxCanvas.Height = this.Size.Height;
            pictureBoxCanvas.BackColor = Color.Transparent;
            pictureBoxClose.BackColor = Color.Transparent;
            pictureBoxClose.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxClose.Image = Properties.Resources.Close;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //начальное положение всех элементов
            canvasBitmap = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
            canvasGr = Graphics.FromImage(canvasBitmap);
            canvasGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            timerStart.Start();
            timerUpdate.Stop();
            timerSnow.Stop();
            StartPlayerPosition();

        }

        //позиция игроков начальная
        private void StartPlayerPosition()
        {
            
            loadForm.client.ClientNewPozition();
            canvasGr.DrawImageUnscaled(Properties.Resources.Go, 500, 380);
            personPoint[0] = new Point(500, 380);
            canvasGr.DrawImageUnscaled(Properties.Resources.Go, 620, 350);
            personPoint[1] = new Point(620, 350);
            canvasGr.DrawImageUnscaled(Properties.Resources.Go, 610, 250);
            personPoint[2] = new Point(610, 250);
            DrawPlayer();
        }

        //закрытие формы
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            loadForm.client.startGame = false;
            loadForm.client.ip = loadForm.client.ipAdrBuf;
            DialogResult = DialogResult.OK;
            loadForm.Visible = false;
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:// управление формой
                    FlagMouseMoveForm = true;
                    break;
            }
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:// управление формой
                    FlagMouseMoveForm = false;
                    break;
            }
        }
        ///////////////////////////////////

        private void pictureBoxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // управление формой
                if (FlagMouseMoveForm)
                {
                    iMouseBufX = e.X;
                    iMouseBufY = e.Y;
                }
                //выбор персоны
                bool flagTemp = false;
                PersonNumber = 0;
                foreach (var tempPicture in personPoint)
                {
                    PersonNumber++;
                    if (e.X >= tempPicture.X && e.X <= tempPicture.X + 60 && e.Y >= tempPicture.Y && e.Y <= tempPicture.Y + 80)
                    {
                        flagTemp = true;
                        break;
                    }
                }
                if (!flagTemp)
                    PersonNumber = 0;
                iPlayerBufX = e.X;
                iPlayerBufY = e.Y;
                if (iPlayerBufX + iPlayerBufY > 550 && PersonNumber != 0)
                {
                    iPlayerTempX = personPoint[PersonNumber - 1].X ;
                    iPlayerTempY = personPoint[PersonNumber - 1].Y ;
                    switch (PersonNumber)
                    {
                        case 1:
                            if (state1 != "Killed")
                            if (!flagState1)
                            {
                                state1 = "Throw";
                                loadForm.client.SendTo("State1:" + state1);
                            }
                            break;
                        case 2:
                            if (state2 != "Killed")
                            if (!flagState2)
                            {
                                state2 = "Throw";
                                loadForm.client.SendTo("State2:" + state2);
                            }
                            break;
                        case 3:
                            if (state3 != "Killed")
                            if (!flagState3)
                            {
                                state3 = "Throw";
                                loadForm.client.SendTo("State3:" + state3);
                            }
                            break;
                    }
                    DrawPlayer();
                }
            }
        }
        //отрисовка позиции игроков
        private void DrawPlayer()
        {
            canvasGr.Clear(Color.Transparent);
            if (!flagClose && loadForm.client.state1 == "Killed" && loadForm.client.state2 == "Killed" && loadForm.client.state3 == "Killed")
            {
                flagClose = true;
                MessageBox.Show("Вы выиграли!", "Поздравляем");
                state1 = "Win";
                state2 = "Win";
                state3 = "Win";
            }
            loadForm.client.SendTo("State1:" + state1);
            loadForm.client.SendTo("State2:" + state2);
            loadForm.client.SendTo("State3:" + state3);
            DrawUpdate();
            switch (PersonNumber)
            {
                case 0:
                    switch (state1)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[0]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[0]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[0]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[0]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[0]);
                            break;
                    }
                    switch (state2)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[1]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[1]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[1]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[1]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[1]);
                            break;
                    }
                    switch (state3)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[2]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[2]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[2]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[2]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[2]);
                            break;
                    }
                    break;
                case 1:
                    
                    switch (state2)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[1]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[1]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[1]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[1]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[1]);
                            break;
                    }
                    switch (state3)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[2]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[2]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[2]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[2]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[2]);
                            break;
                    }
                    switch (state1)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, iPlayerTempX, iPlayerTempY);
                            break;
                    }
                    break;
                case 2:
                    
                    switch (state1)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[0]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[0]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[0]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[0]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[0]);
                            break;
                    }
                    switch (state3)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[2]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[2]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[2]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[2]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[2]);
                            break;
                    }
                    switch (state2)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, iPlayerTempX, iPlayerTempY);
                            break;
                    }
                    break;
                case 3:
                    
                    switch (state2)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[1]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[1]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[1]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[1]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[1]);
                            break;
                    }
                    switch (state1)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, personPoint[0]);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, personPoint[0]);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, personPoint[0]);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, personPoint[0]);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, personPoint[2]);
                            break;
                    }
                    switch (state3)
                    {
                        case "Go":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Go, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Throw":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Throw, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Hit":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Hit, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Killed":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Killed, iPlayerTempX, iPlayerTempY);
                            break;
                        case "Win":
                            canvasGr.DrawImageUnscaled(Properties.Resources.Win, iPlayerTempX, iPlayerTempY);
                            break;
                    }
                    break;
            }
            if (loadForm.client.state1 != "Killed" && loadForm.client.personPoint[0].X <= iSnowX && loadForm.client.personPoint[0].X + 40 >= iSnowX && loadForm.client.personPoint[0].Y + 5 <= iSnowY && loadForm.client.personPoint[0].Y + 40 >= iSnowY)
            {
                iSnowX = -30;
                iSnowY = -30;
            }
            if (loadForm.client.state2 != "Killed" && loadForm.client.personPoint[1].X <= iSnowX && loadForm.client.personPoint[1].X + 40 >= iSnowX && loadForm.client.personPoint[1].Y + 5 <= iSnowY && loadForm.client.personPoint[1].Y + 40 >= iSnowY)
            {
                iSnowX = -30;
                iSnowY = -30;
            }
            if (loadForm.client.state3 != "Killed" && loadForm.client.personPoint[2].X <= iSnowX && loadForm.client.personPoint[2].X + 40 >= iSnowX && loadForm.client.personPoint[2].Y + 5 <= iSnowY && loadForm.client.personPoint[2].Y + 40 >= iSnowY)
            {
                iSnowX = -30;
                iSnowY = -30;
            }
            canvasGr.DrawImageUnscaled(Properties.Resources.snowballTwo, iSnowX, iSnowY);
            pictureBoxCanvas.Image = canvasBitmap;
        }
        //команда зеленых отрисовка
        private void DrawUpdate()
        {
            switch (loadForm.client.state1)
            {
                case "Go":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Go_, loadForm.client.personPoint[0]);
                    break;
                case "Throw":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Throw_, loadForm.client.personPoint[0]);
                    break;
                case "Hit":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Hit_, loadForm.client.personPoint[0]);
                    break;
                case "Killed":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Killed_, loadForm.client.personPoint[0]);
                    break;
                case "Win":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Win_, loadForm.client.personPoint[0]);
                    break;
            }
            switch (loadForm.client.state2)
            {
                case "Go":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Go_, loadForm.client.personPoint[1]);
                    break;
                case "Throw":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Throw_, loadForm.client.personPoint[1]);
                    break;
                case "Hit":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Hit_, loadForm.client.personPoint[1]);
                    break;
                case "Killed":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Killed_, loadForm.client.personPoint[1]);
                    break;
                case "Win":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Win_, loadForm.client.personPoint[1]);
                    break;
            }
            switch (loadForm.client.state3)
            {
                case "Go":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Go_, loadForm.client.personPoint[2]);
                    break;
                case "Throw":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Throw_, loadForm.client.personPoint[2]);
                    break;
                case "Hit":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Hit_, loadForm.client.personPoint[2]);
                    break;
                case "Killed":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Killed_, loadForm.client.personPoint[2]);
                    break;
                case "Win":
                    canvasGr.DrawImageUnscaled(Properties.Resources.Win_, loadForm.client.personPoint[2]);
                    break;
            }
            if ( personPoint[0].X <= loadForm.client.iSnowX && personPoint[0].X + 40 >= loadForm.client.iSnowX && personPoint[0].Y + 5 <= loadForm.client.iSnowY && personPoint[0].Y + 40 >= loadForm.client.iSnowY)
            {
                loadForm.client.iSnowX = -30;
                loadForm.client.iSnowY = -30;
                flagState1 = true;
                if (numberState1)
                {
                    state1 = "Killed";
                }
                else
                {
                    state1 = "Hit";
                    timerState1.Start();
                }
                loadForm.client.SendTo("State1:" + state1);
            }
            if ( personPoint[1].X <= loadForm.client.iSnowX && personPoint[1].X + 40 >= loadForm.client.iSnowX && personPoint[1].Y + 5 <= loadForm.client.iSnowY && personPoint[1].Y + 40 >= loadForm.client.iSnowY)
            {
                loadForm.client.iSnowX = -30;
                loadForm.client.iSnowY = -30;
                flagState2 = true;
                if (numberState2)
                {
                    state2 = "Killed";
                }
                else
                {
                    state2 = "Hit";
                    timerState2.Start();
                }
                loadForm.client.SendTo("State2:" + state2);
            }
            if ( personPoint[2].X <= loadForm.client.iSnowX && personPoint[2].X + 40 >= loadForm.client.iSnowX && personPoint[2].Y + 5 <= loadForm.client.iSnowY && personPoint[2].Y + 40 >= loadForm.client.iSnowY)
            {
                loadForm.client.iSnowX = -30;
                loadForm.client.iSnowY = -30;
                flagState3 = true;
                if (numberState3)
                {
                    state3 = "Killed";
                }
                else
                {
                    state3 = "Hit";
                    timerState3.Start();
                }
                loadForm.client.SendTo("State3:" + state3);
            }
            canvasGr.DrawImageUnscaled(Properties.Resources.snowballTwo, loadForm.client.iSnowX, loadForm.client.iSnowY);
            
 
        }
        //обновление игры по таймеру
        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            DrawPlayer();
        }

        private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // управление формой
                if (FlagMouseMoveForm)
                {
                    this.Location = new Point(this.Location.X + (e.X - iMouseBufX), this.Location.Y + (e.Y - iMouseBufY));
                    return;
                }
                if (PersonNumber != 0)
                {
                    //управление персоной
                    if (personPoint[PersonNumber - 1].X + (e.X - iPlayerBufX) + personPoint[PersonNumber - 1].Y + (e.Y - iPlayerBufY) > 550)
                    {

                        switch (PersonNumber)
                        {
                            case 1:
                                if(state1 != "Killed")
                                if (!flagState1 )
                                {
                                    iPlayerTempX = personPoint[PersonNumber - 1].X + (e.X - iPlayerBufX);
                                    iPlayerTempY = personPoint[PersonNumber - 1].Y + (e.Y - iPlayerBufY);
                                }
                                else
                                {
                                    iPlayerTempX = personPoint[PersonNumber - 1].X;
                                    iPlayerTempY = personPoint[PersonNumber - 1].Y;
                                }
                                loadForm.client.SendTo("XY1:" + iPlayerTempX + "/" + iPlayerTempY);
                                break;
                            case 2:
                                if (state2 != "Killed")
                                if (!flagState2)
                                {
                                    iPlayerTempX = personPoint[PersonNumber - 1].X + (e.X - iPlayerBufX);
                                    iPlayerTempY = personPoint[PersonNumber - 1].Y + (e.Y - iPlayerBufY);
                                }
                                else
                                {
                                    iPlayerTempX = personPoint[PersonNumber - 1].X;
                                    iPlayerTempY = personPoint[PersonNumber - 1].Y;
                                }
                                loadForm.client.SendTo("XY2:" + iPlayerTempX + "/" + iPlayerTempY);
                                break;
                            case 3:
                                if (state3 != "Killed")
                                if (!flagState3)
                                {
                                    iPlayerTempX = personPoint[PersonNumber - 1].X + (e.X - iPlayerBufX);
                                    iPlayerTempY = personPoint[PersonNumber - 1].Y + (e.Y - iPlayerBufY);
                                }
                                else
                                {
                                    iPlayerTempX = personPoint[PersonNumber - 1].X;
                                    iPlayerTempY = personPoint[PersonNumber - 1].Y;
                                }
                                loadForm.client.SendTo("XY3:" + iPlayerTempX + "/" + iPlayerTempY);
                                break;

                        }
                        DrawPlayer();
                    }
                }
            }
        }
        private void pictureBoxCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            // управление формой
            FlagMouseMoveForm = false;
            //управление персоной
            if(PersonNumber != 0)
            {
                    personPoint[PersonNumber - 1] = new Point(iPlayerTempX , iPlayerTempY);
                    switch (PersonNumber)
                    {
                        case 1:
                            if (!flagState1)
                            {
                                state1 = "Go";
                                loadForm.client.SendTo("State1:" + state1);
                                iSnowX = personPoint[PersonNumber - 1].X;
                                iSnowY = personPoint[PersonNumber - 1].Y;
                                timerSnow.Start();
                            }
                            break;
                        case 2:
                            if (!flagState2)
                            {
                                state2 = "Go";
                                loadForm.client.SendTo("State2:" + state2);
                                iSnowX = personPoint[PersonNumber - 1].X;
                                iSnowY = personPoint[PersonNumber - 1].Y;
                                timerSnow.Start();
                            }
                            break;
                        case 3:
                            if (!flagState3)
                            {
                                state3 = "Go";
                                loadForm.client.SendTo("State3:" + state3);
                                iSnowX = personPoint[PersonNumber - 1].X;
                                iSnowY = personPoint[PersonNumber - 1].Y;
                                timerSnow.Start();
                            }
                            break;
                    }
                    DrawPlayer();
                    PersonNumber = 0;
            }
             
        }
        ///////////////////////////////////
        private void pictureBoxClose_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBoxClose.Image = Properties.Resources.CloseSecond;
        }
        private void pictureBoxClose_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBoxClose.Image = Properties.Resources.Close;
            if (DialogResult.Yes == MessageBox.Show("Вы действительно хотите покинуть игру?", "Внимание!", MessageBoxButtons.YesNo))
            {
                this.Close();
            }
        }
        private void timerStart_Tick(object sender, EventArgs e)
        {
            if (loadForm.client.startGame)
            {
                canvasBitmap = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
                canvasGr = Graphics.FromImage(canvasBitmap);
                canvasGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                StartPlayerPosition();
                timerUpdate.Start();
                timerStart.Stop();
            }
        }
        //координаты снежка
        private void timerSnow_Tick(object sender, EventArgs e)
        {
            iSnowX -= 15;
            iSnowY -= 5;
            if (iSnowX <= -30 || iSnowY <= -30)
            {
                timerSnow.Stop();
            }
            loadForm.client.SendTo("XYSnow:" + iSnowX + "/" + iSnowY);
            DrawPlayer();
        }
        //обновление состояний игроков 
        private void timerState1_Tick(object sender, EventArgs e)
        {
            flagState1 = false;
            state1 = "Go";
            loadForm.client.SendTo("State1:" + state1);
            timerState1.Stop();
            numberState1 = true;
            DrawPlayer();
        }

        private void timerState2_Tick(object sender, EventArgs e)
        {
            flagState2 = false;
            state2 = "Go";
            loadForm.client.SendTo("State2:" + state2);
            timerState2.Stop();
            numberState2 = true;
            DrawPlayer();
        }

        private void timerState3_Tick(object sender, EventArgs e)
        {
            flagState3 = false;
            state3 = "Go";
            loadForm.client.SendTo("State3:" + state3);
            timerState3.Stop();
            numberState3 = true;
            DrawPlayer();
        }




 
       

    }
}
