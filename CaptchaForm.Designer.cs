namespace DemoExamApp
{
    partial class CaptchaForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox zone1;
        private System.Windows.Forms.PictureBox zone2;
        private System.Windows.Forms.PictureBox zone3;
        private System.Windows.Forms.PictureBox zone4;
        private System.Windows.Forms.PictureBox spawn1;
        private System.Windows.Forms.PictureBox spawn2;
        private System.Windows.Forms.PictureBox spawn3;
        private System.Windows.Forms.PictureBox spawn4;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Label lblAttempts;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.zone1 = new System.Windows.Forms.PictureBox();
            this.zone2 = new System.Windows.Forms.PictureBox();
            this.zone3 = new System.Windows.Forms.PictureBox();
            this.zone4 = new System.Windows.Forms.PictureBox();
            this.spawn1 = new System.Windows.Forms.PictureBox();
            this.spawn2 = new System.Windows.Forms.PictureBox();
            this.spawn3 = new System.Windows.Forms.PictureBox();
            this.spawn4 = new System.Windows.Forms.PictureBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.lblAttempts = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.zone1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zone2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zone3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zone4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn4)).BeginInit();
            this.SuspendLayout();
            // 
            // zone1
            // 
            this.zone1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zone1.Location = new System.Drawing.Point(30, 30);
            this.zone1.Name = "zone1";
            this.zone1.Size = new System.Drawing.Size(120, 90);
            this.zone1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zone1.TabIndex = 0;
            this.zone1.TabStop = false;
            // 
            // zone2
            // 
            this.zone2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zone2.Location = new System.Drawing.Point(160, 30);
            this.zone2.Name = "zone2";
            this.zone2.Size = new System.Drawing.Size(120, 90);
            this.zone2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zone2.TabIndex = 1;
            this.zone2.TabStop = false;
            // 
            // zone3
            // 
            this.zone3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zone3.Location = new System.Drawing.Point(30, 130);
            this.zone3.Name = "zone3";
            this.zone3.Size = new System.Drawing.Size(120, 90);
            this.zone3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zone3.TabIndex = 2;
            this.zone3.TabStop = false;
            // 
            // zone4
            // 
            this.zone4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zone4.Location = new System.Drawing.Point(160, 130);
            this.zone4.Name = "zone4";
            this.zone4.Size = new System.Drawing.Size(120, 90);
            this.zone4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zone4.TabIndex = 3;
            this.zone4.TabStop = false;
            // 
            // spawn1
            // 
            this.spawn1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spawn1.Location = new System.Drawing.Point(380, 30);
            this.spawn1.Name = "spawn1";
            this.spawn1.Size = new System.Drawing.Size(120, 90);
            this.spawn1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.spawn1.TabIndex = 4;
            this.spawn1.TabStop = false;
            // 
            // spawn2
            // 
            this.spawn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spawn2.Location = new System.Drawing.Point(510, 30);
            this.spawn2.Name = "spawn2";
            this.spawn2.Size = new System.Drawing.Size(120, 90);
            this.spawn2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.spawn2.TabIndex = 5;
            this.spawn2.TabStop = false;
            // 
            // spawn3
            // 
            this.spawn3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spawn3.Location = new System.Drawing.Point(380, 130);
            this.spawn3.Name = "spawn3";
            this.spawn3.Size = new System.Drawing.Size(120, 90);
            this.spawn3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.spawn3.TabIndex = 6;
            this.spawn3.TabStop = false;
            // 
            // spawn4
            // 
            this.spawn4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spawn4.Location = new System.Drawing.Point(510, 130);
            this.spawn4.Name = "spawn4";
            this.spawn4.Size = new System.Drawing.Size(120, 90);
            this.spawn4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.spawn4.TabIndex = 7;
            this.spawn4.TabStop = false;
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(30, 240);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(250, 35);
            this.btnCheck.TabIndex = 8;
            this.btnCheck.Text = "Проверить";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lblAttempts
            // 
            this.lblAttempts.AutoSize = true;
            this.lblAttempts.Location = new System.Drawing.Point(30, 220);
            this.lblAttempts.Name = "lblAttempts";
            this.lblAttempts.Size = new System.Drawing.Size(145, 17);
            this.lblAttempts.TabIndex = 9;
            this.lblAttempts.Text = "Осталось попыток: 3";
            // 
            // CaptchaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 320);
            this.Controls.Add(this.lblAttempts);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.spawn4);
            this.Controls.Add(this.spawn3);
            this.Controls.Add(this.spawn2);
            this.Controls.Add(this.spawn1);
            this.Controls.Add(this.zone4);
            this.Controls.Add(this.zone3);
            this.Controls.Add(this.zone2);
            this.Controls.Add(this.zone1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CaptchaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Капча";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CaptchaForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.zone1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zone2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zone3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zone4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spawn4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
