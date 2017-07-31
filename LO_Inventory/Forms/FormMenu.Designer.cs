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
            this.buttonItemCat = new System.Windows.Forms.Button();
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
            this.groupBox1.Location = new System.Drawing.Point(9, 29);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(388, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Infomation";
            // 
            // labelRole
            // 
            this.labelRole.AutoSize = true;
            this.labelRole.ForeColor = System.Drawing.Color.Red;
            this.labelRole.Location = new System.Drawing.Point(93, 78);
            this.labelRole.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRole.Name = "labelRole";
            this.labelRole.Size = new System.Drawing.Size(24, 13);
            this.labelRole.TabIndex = 4;
            this.labelRole.Text = "test";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.labelUsername.Location = new System.Drawing.Point(93, 41);
            this.labelUsername.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(24, 13);
            this.labelUsername.TabIndex = 3;
            this.labelUsername.Text = "test";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Role:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // groupBoxManagement
            // 
            this.groupBoxManagement.Controls.Add(this.buttonItemCat);
            this.groupBoxManagement.Controls.Add(this.buttonUser);
            this.groupBoxManagement.Controls.Add(this.buttonProvider);
            this.groupBoxManagement.Controls.Add(this.buttonOrder);
            this.groupBoxManagement.Controls.Add(this.buttonPer);
            this.groupBoxManagement.Controls.Add(this.buttonCabinet);
            this.groupBoxManagement.Controls.Add(this.buttonCabinetType);
            this.groupBoxManagement.Controls.Add(this.buttonItem);
            this.groupBoxManagement.Controls.Add(this.buttonTran);
            this.groupBoxManagement.Location = new System.Drawing.Point(9, 165);
            this.groupBoxManagement.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxManagement.Name = "groupBoxManagement";
            this.groupBoxManagement.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBoxManagement.Size = new System.Drawing.Size(388, 124);
            this.groupBoxManagement.TabIndex = 1;
            this.groupBoxManagement.TabStop = false;
            this.groupBoxManagement.Text = "Management";
            // 
            // buttonUser
            // 
            this.buttonUser.Location = new System.Drawing.Point(285, 89);
            this.buttonUser.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonUser.Name = "buttonUser";
            this.buttonUser.Size = new System.Drawing.Size(90, 19);
            this.buttonUser.TabIndex = 7;
            this.buttonUser.Text = "User";
            this.buttonUser.UseVisualStyleBackColor = true;
            this.buttonUser.Click += new System.EventHandler(this.ButtonUser_Click);
            // 
            // buttonProvider
            // 
            this.buttonProvider.Location = new System.Drawing.Point(284, 58);
            this.buttonProvider.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonProvider.Name = "buttonProvider";
            this.buttonProvider.Size = new System.Drawing.Size(90, 19);
            this.buttonProvider.TabIndex = 5;
            this.buttonProvider.Text = "Provider";
            this.buttonProvider.UseVisualStyleBackColor = true;
            this.buttonProvider.Click += new System.EventHandler(this.ButtonProvider_Click);
            // 
            // buttonOrder
            // 
            this.buttonOrder.Location = new System.Drawing.Point(284, 27);
            this.buttonOrder.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(90, 19);
            this.buttonOrder.TabIndex = 2;
            this.buttonOrder.Text = "Order";
            this.buttonOrder.UseVisualStyleBackColor = true;
            this.buttonOrder.Click += new System.EventHandler(this.ButtonOrder_Click);
            // 
            // buttonPer
            // 
            this.buttonPer.Location = new System.Drawing.Point(148, 58);
            this.buttonPer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonPer.Name = "buttonPer";
            this.buttonPer.Size = new System.Drawing.Size(90, 19);
            this.buttonPer.TabIndex = 4;
            this.buttonPer.Text = "Permission";
            this.buttonPer.UseVisualStyleBackColor = true;
            this.buttonPer.Click += new System.EventHandler(this.ButtonPer_Click);
            // 
            // buttonCabinet
            // 
            this.buttonCabinet.Location = new System.Drawing.Point(13, 27);
            this.buttonCabinet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCabinet.Name = "buttonCabinet";
            this.buttonCabinet.Size = new System.Drawing.Size(90, 19);
            this.buttonCabinet.TabIndex = 0;
            this.buttonCabinet.Text = "Cabinet";
            this.buttonCabinet.UseVisualStyleBackColor = true;
            this.buttonCabinet.Click += new System.EventHandler(this.ButtonCabinet_Click);
            // 
            // buttonCabinetType
            // 
            this.buttonCabinetType.Location = new System.Drawing.Point(13, 58);
            this.buttonCabinetType.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonCabinetType.Name = "buttonCabinetType";
            this.buttonCabinetType.Size = new System.Drawing.Size(90, 19);
            this.buttonCabinetType.TabIndex = 3;
            this.buttonCabinetType.Text = "Cabinet Type";
            this.buttonCabinetType.UseVisualStyleBackColor = true;
            this.buttonCabinetType.Click += new System.EventHandler(this.ButtonCabinetType_Click);
            // 
            // buttonItem
            // 
            this.buttonItem.Location = new System.Drawing.Point(13, 89);
            this.buttonItem.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonItem.Name = "buttonItem";
            this.buttonItem.Size = new System.Drawing.Size(90, 19);
            this.buttonItem.TabIndex = 6;
            this.buttonItem.Text = "Item";
            this.buttonItem.UseVisualStyleBackColor = true;
            this.buttonItem.Click += new System.EventHandler(this.ButtonItem_Click);
            // 
            // buttonTran
            // 
            this.buttonTran.Location = new System.Drawing.Point(148, 27);
            this.buttonTran.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonTran.Name = "buttonTran";
            this.buttonTran.Size = new System.Drawing.Size(90, 19);
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
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(406, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItemExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItemExit
            // 
            this.exitToolStripMenuItemExit.Name = "exitToolStripMenuItemExit";
            this.exitToolStripMenuItemExit.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItemExit.Text = "Exit";
            this.exitToolStripMenuItemExit.Click += new System.EventHandler(this.ExitToolStripMenuItemExit_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.showLogToolStripMenuItem.Text = "Show Log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.ShowLogToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVer.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.labelVer.Location = new System.Drawing.Point(329, 292);
            this.labelVer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(55, 13);
            this.labelVer.TabIndex = 3;
            this.labelVer.Text = "Ver x.xx";
            // 
            // buttonItemCat
            // 
            this.buttonItemCat.Location = new System.Drawing.Point(148, 89);
            this.buttonItemCat.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemCat.Name = "buttonItemCat";
            this.buttonItemCat.Size = new System.Drawing.Size(90, 19);
            this.buttonItemCat.TabIndex = 8;
            this.buttonItemCat.Text = "Item Cat";
            this.buttonItemCat.UseVisualStyleBackColor = true;
            this.buttonItemCat.Click += new System.EventHandler(this.ButtonItemCat_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 313);
            this.Controls.Add(this.labelVer);
            this.Controls.Add(this.groupBoxManagement);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.Button buttonItemCat;
    }
}