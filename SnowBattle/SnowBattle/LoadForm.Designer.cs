namespace SnowBattle
{
    partial class LoadForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxSnowLoad = new System.Windows.Forms.PictureBox();
            this.pictureBoxCloseLoad = new System.Windows.Forms.PictureBox();
            this.pictureBoxLoginAndPass = new System.Windows.Forms.PictureBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.listBoxLoadStaticPlayer = new System.Windows.Forms.ListBox();
            this.pictureBoxRefresh = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlay = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnowLoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCloseLoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoginAndPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxSnowLoad
            // 
            this.pictureBoxSnowLoad.Location = new System.Drawing.Point(52, 33);
            this.pictureBoxSnowLoad.Name = "pictureBoxSnowLoad";
            this.pictureBoxSnowLoad.Size = new System.Drawing.Size(439, 50);
            this.pictureBoxSnowLoad.TabIndex = 1;
            this.pictureBoxSnowLoad.TabStop = false;
            // 
            // pictureBoxCloseLoad
            // 
            this.pictureBoxCloseLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxCloseLoad.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCloseLoad.Name = "pictureBoxCloseLoad";
            this.pictureBoxCloseLoad.Size = new System.Drawing.Size(31, 27);
            this.pictureBoxCloseLoad.TabIndex = 0;
            this.pictureBoxCloseLoad.TabStop = false;
            this.pictureBoxCloseLoad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCloseLoad_MouseDown);
            this.pictureBoxCloseLoad.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCloseLoad_MouseUp);
            // 
            // pictureBoxLoginAndPass
            // 
            this.pictureBoxLoginAndPass.Location = new System.Drawing.Point(141, 113);
            this.pictureBoxLoginAndPass.Name = "pictureBoxLoginAndPass";
            this.pictureBoxLoginAndPass.Size = new System.Drawing.Size(271, 50);
            this.pictureBoxLoginAndPass.TabIndex = 2;
            this.pictureBoxLoginAndPass.TabStop = false;
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(263, 118);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(135, 20);
            this.textBoxLogin.TabIndex = 3;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Location = new System.Drawing.Point(230, 156);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 4;
            this.buttonEnter.Text = "Войти";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // listBoxLoadStaticPlayer
            // 
            this.listBoxLoadStaticPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.listBoxLoadStaticPlayer.FormattingEnabled = true;
            this.listBoxLoadStaticPlayer.Location = new System.Drawing.Point(52, 99);
            this.listBoxLoadStaticPlayer.Name = "listBoxLoadStaticPlayer";
            this.listBoxLoadStaticPlayer.Size = new System.Drawing.Size(439, 121);
            this.listBoxLoadStaticPlayer.TabIndex = 5;
            // 
            // pictureBoxRefresh
            // 
            this.pictureBoxRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxRefresh.Location = new System.Drawing.Point(52, 226);
            this.pictureBoxRefresh.Name = "pictureBoxRefresh";
            this.pictureBoxRefresh.Size = new System.Drawing.Size(30, 25);
            this.pictureBoxRefresh.TabIndex = 6;
            this.pictureBoxRefresh.TabStop = false;
            this.pictureBoxRefresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxRefresh_MouseDown);
            this.pictureBoxRefresh.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxRefresh_MouseUp);
            // 
            // pictureBoxPlay
            // 
            this.pictureBoxPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPlay.Location = new System.Drawing.Point(99, 226);
            this.pictureBoxPlay.Name = "pictureBoxPlay";
            this.pictureBoxPlay.Size = new System.Drawing.Size(121, 25);
            this.pictureBoxPlay.TabIndex = 7;
            this.pictureBoxPlay.TabStop = false;
            this.pictureBoxPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlay_MouseDown);
            this.pictureBoxPlay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxPlay_MouseUp);
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 261);
            this.Controls.Add(this.pictureBoxPlay);
            this.Controls.Add(this.pictureBoxRefresh);
            this.Controls.Add(this.listBoxLoadStaticPlayer);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.pictureBoxLoginAndPass);
            this.Controls.Add(this.pictureBoxSnowLoad);
            this.Controls.Add(this.pictureBoxCloseLoad);
            this.Name = "LoadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LoadForm_FormClosed);
            this.Load += new System.EventHandler(this.LoadForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSnowLoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCloseLoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoginAndPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCloseLoad;
        private System.Windows.Forms.PictureBox pictureBoxSnowLoad;
        private System.Windows.Forms.PictureBox pictureBoxLoginAndPass;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.PictureBox pictureBoxRefresh;
        private System.Windows.Forms.PictureBox pictureBoxPlay;
        public System.Windows.Forms.ListBox listBoxLoadStaticPlayer;
    }
}

