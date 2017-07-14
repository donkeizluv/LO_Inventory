namespace LO_Inventory.Forms
{
    partial class FormMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelRole = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxManagement = new System.Windows.Forms.GroupBox();
            this.buttonUser = new System.Windows.Forms.Button();
            this.buttonProvider = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonPer = new System.Windows.Forms.Button();
            this.buttonCabinet = new System.Windows.Forms.Button();
            this.buttonCabinetType = new System.Windows.Forms.Button();
            this.buttonItem = new System.Windows.Forms.Button();
            this.buttonTran = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelVer = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxManagement.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelRole);
            this.groupBox1.Controls.Add(this.labelUsername);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 161);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Infomation";
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.ForeColor = System.Drawing.Color.Red;
            this.labelRole.Location = new System.Drawing.Point(124, 96);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(31, 17);
            this.labelRole.TabIndex = 4;
            this.labelRole.Text = "test";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.labelUsername.Location = new System.Drawing.Point(124, 50);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(31, 17);
            this.labelUsername.TabIndex = 3;
            this.labelUsername.Text = "test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Role:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // groupBoxManagement
            // 
            this.groupBoxManagement.Controls.Add(this.buttonUser);
            this.groupBoxManagement.Controls.Add(this.buttonProvider);
            this.groupBoxManagement.Controls.Add(this.buttonOrder);
            this.groupBoxManagement.Controls.Add(this.buttonPer);
            this.groupBoxManagement.Controls.Add(this.buttonCabinet);
            this.groupBoxManagement.Controls.Add(this.buttonCabinetType);
            this.groupBoxManagement.Controls.Add(this.buttonItem);
            this.groupBoxManagement.Controls.Add(this.buttonTran);
            this.groupBoxManagement.Location = new System.Drawing.Point(12, 203);
            this.groupBoxManagement.Name = "groupBoxManagement";
            this.groupBoxManagement.Size = new System.Drawing.Size(517, 153);
            this.groupBoxManagement.TabIndex = 1;
            this.groupBoxManagement.TabStop = false;
            this.groupBoxManagement.Text = "Management";
            // 
            // buttonUser
            // 
            this.buttonUser.Location = new System.Drawing.Point(198, 109);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(120, 23);
            this.buttonUser.TabIndex = 7;
            this.buttonUser.Text = "User";
            this.buttonUser.UseVisualStyleBackColor = true;
            this.buttonUser.Click += new System.EventHandler(this.ButtonUser_Click);
            // 
            // buttonProvider
            // 
            this.buttonProvider.Location = new System.Drawing.Point(379, 71);
            this.buttonProvider.Name = "buttonProvider";
            this.buttonProvider.Size = new System.Drawing.Size(120, 23);
            this.buttonProvider.TabIndex = 5;
            this.buttonProvider.Text = "Provider";
            this.buttonProvider.UseVisualStyleBackColor = true;
            this.buttonProvider.Click += new System.EventHandler(this.ButtonProvider_Click);
            // 
            // buttonOrder
            // 
            this.buttonOrder.Location = new System.Drawing.Point(379, 33);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(120, 23);
            this.buttonOrder.TabIndex = 2;
            this.buttonOrder.Text = "Order";
            this.buttonOrder.UseVisualStyleBackColor = true;
            this.buttonOrder.Click += new System.EventHandler(this.ButtonOrder_Click);
            // 
            // buttonPer
            // 
            this.buttonPer.Location = new System.Drawing.Point(198, 71);
            this.buttonPer.Name = "buttonPer";
            this.buttonPer.Size = new System.Drawing.Size(120, 23);
            this.buttonPer.TabIndex = 4;
            this.buttonPer.Text = "Permission";
            this.buttonPer.UseVisualStyleBackColor = true;
            this.buttonPer.Click += new System.EventHandler(this.ButtonPer_Click);
            // 
            // buttonCabinet
            // 
            this.buttonCabinet.Location = new System.Drawing.Point(17, 33);
            this.buttonCabinet.Name = "buttonCabinet";
            this.buttonCabinet.Size = new System.Drawing.Size(120, 23);
            this.buttonCabinet.TabIndex = 0;
            this.buttonCabinet.Text = "Cabinet";
            this.buttonCabinet.UseVisualStyleBackColor = true;
            this.buttonCabinet.Click += new System.EventHandler(this.ButtonCabinet_Click);
            // 
            // buttonCabinetType
            // 
            this.buttonCabinetType.Location = new System.Drawing.Point(17, 71);
            this.buttonCabinetType.Name = "buttonCabinetType";
            this.buttonCabinetType.Size = new System.Drawing.Size(120, 23);
            this.buttonCabinetType.TabIndex = 3;
            this.buttonCabinetType.Text = "Cabinet Type";
            this.buttonCabinetType.UseVisualStyleBackColor = true;
            this.buttonCabinetType.Click += new System.EventHandler(this.ButtonCabinetType_Click);
            // 
            // buttonItem
            // 
            this.buttonItem.Location = new System.Drawing.Point(17, 109);
            this.buttonItem.Name = "buttonItem";
            this.buttonItem.Size = new System.Drawing.Size(120, 23);
            this.buttonItem.TabIndex = 6;
            this.buttonItem.Text = "Item";
            this.buttonItem.UseVisualStyleBackColor = true;
            this.buttonItem.Click += new System.EventHandler(this.ButtonItem_Click);
            // 
            // buttonTran
            // 
            this.buttonTran.Location = new System.Drawing.Point(198, 33);
            this.buttonTran.Name = "buttonTran";
            this.buttonTran.Size = new System.Drawing.Size(120, 23);
            this.buttonTran.TabIndex = 1;
            this.buttonTran.Text = "Transaction";
            this.buttonTran.UseVisualStyleBackColor = true;
            this.buttonTran.Click += new System.EventHandler(this.ButtonTran_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(541, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItemExit
            // 
            this.exitToolStripMenuItemExit.Name = "exitToolStripMenuItemExit";
            this.exitToolStripMenuItemExit.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItemExit.Text = "Exit";
            this.exitToolStripMenuItemExit.Click += new System.EventHandler(this.ExitToolStripMenuItemExit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(149, 26);
            this.showLogToolStripMenuItem.Text = "Show Log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.ShowLogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVer.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelVer.Location = new System.Drawing.Point(439, 359);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(72, 17);
            this.labelVer.TabIndex = 3;
            this.labelVer.Text = "Ver x.xx";
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 385);
            this.Controls.Add(this.labelVer);
            this.Controls.Add(this.groupBoxManagement);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FormMenu";
            this.Text = "LO Inventory - Menu";
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxManagement.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelRole;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxManagement;
        private System.Windows.Forms.Button buttonItem;
        private System.Windows.Forms.Button buttonTran;
        private System.Windows.Forms.Button buttonCabinet;
        private System.Windows.Forms.Button buttonProvider;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonPer;
        private System.Windows.Forms.Button buttonCabinetType;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label labelVer;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.Button buttonUser;
    }
}