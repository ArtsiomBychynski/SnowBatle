namespace SnowBattle
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerUpdate = new System.Windows.Forms.Timer(this.components);
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.timerSnow = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.pictureBoxCanvas = new System.Windows.Forms.PictureBox();
            this.timerState1 = new System.Windows.Forms.Timer(this.components);
            this.timerState2 = new System.Windows.Forms.Timer(this.components);
            this.timerState3 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanvas)).BeginInit();
            this.SuspendLayout();
            // 
            // timerUpdate
            // 
            this.timerUpdate.Interval = 10;
            this.timerUpdate.Tick += new System.EventHandler(this.timerUpdate_Tick);
            // 
            // timerStart
            // 
            this.timerStart.Interval = 50;
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // timerSnow
            // 
            this.timerSnow.Interval = 10;
            this.timerSnow.Tick += new System.EventHandler(this.timerSnow_Tick);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxClose.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(31, 27);
            this.pictureBoxClose.TabIndex = 1;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxClose_MouseDown);
            this.pictureBoxClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxClose_MouseUp);
            // 
            // pictureBoxCanvas
            // 
            this.pictureBoxCanvas.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCanvas.Name = "pictureBoxCanvas";
            this.pictureBoxCanvas.Size = new System.Drawing.Size(272, 233);
            this.pictureBoxCanvas.TabIndex = 0;
            this.pictureBoxCanvas.TabStop = false;
            this.pictureBoxCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCanvas_MouseDown);
            this.pictureBoxCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCanvas_MouseMove);
            this.pictureBoxCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCanvas_MouseUp);
            // 
            // timerState1
            // 
            this.timerState1.Interval = 500;
            this.timerState1.Tick += new System.EventHandler(this.timerState1_Tick);
            // 
            // timerState2
            // 
            this.timerState2.Interval = 500;
            this.timerState2.Tick += new System.EventHandler(this.timerState2_Tick);
            // 
            // timerState3
            // 
            this.timerState3.Interval = 500;
            this.timerState3.Tick += new System.EventHandler(this.timerState3_Tick);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pictureBoxClose);
            this.Controls.Add(this.pictureBoxCanvas);
            this.Name = "GameForm";
            this.Text = "GameForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameForm_FormClosed);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCanvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxCanvas;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Timer timerUpdate;
        private System.Windows.Forms.Timer timerStart;
        private System.Windows.Forms.Timer timerSnow;
        private System.Windows.Forms.Timer timerState1;
        private System.Windows.Forms.Timer timerState2;
        private System.Windows.Forms.Timer timerState3;
    }
}