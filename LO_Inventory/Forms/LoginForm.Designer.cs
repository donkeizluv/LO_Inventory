namespace LO_Inventory.Forms
{
    partial class LoginForm
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
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelVer = new System.Windows.Forms.Label();
            this.labelDataSourse = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.groupBoxInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(103, 80);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(80, 28);
            this.buttonLogin.TabIndex = 0;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(103, 18);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(166, 22);
            this.textBoxUsername.TabIndex = 2;
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.Location = new System.Drawing.Point(103, 50);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.PasswordChar = '*';
            this.textBoxPwd.Size = new System.Drawing.Size(166, 22);
            this.textBoxPwd.TabIndex = 4;
            this.textBoxPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPwd_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pwd:";
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVer.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelVer.Location = new System.Drawing.Point(16, 25);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(72, 17);
            this.labelVer.TabIndex = 5;
            this.labelVer.Text = "Ver x.xx";
            // 
            // labelDataSourse
            // 
            this.labelDataSourse.AutoSize = true;
            this.labelDataSourse.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDataSourse.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelDataSourse.Location = new System.Drawing.Point(16, 53);
            this.labelDataSourse.Name = "labelDataSourse";
            this.labelDataSourse.Size = new System.Drawing.Size(64, 17);
            this.labelDataSourse.TabIndex = 6;
            this.labelDataSourse.Text = "Server:";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.labelDataSourse);
            this.groupBoxInfo.Controls.Add(this.labelVer);
            this.groupBoxInfo.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.groupBoxInfo.Location = new System.Drawing.Point(23, 111);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(246, 80);
            this.groupBoxInfo.TabIndex = 7;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Info";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 212);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.textBoxPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelVer;
        private System.Windows.Forms.Label labelDataSourse;
        private System.Windows.Forms.GroupBox groupBoxInfo;
    }
}