namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    partial class ItemViewer
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
            this.groupBoxReport.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReport1
            // 
            this.buttonReport1.Text = "Orders";
            this.buttonReport1.Click += new System.EventHandler(this.ButtonReport1_Click);
            // 
            // buttonReport2
            // 
            this.buttonReport2.Text = "Sales";
            this.buttonReport2.Click += new System.EventHandler(this.buttonReport2_Click);
            // 
            // ItemViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(881, 681);
            this.Name = "ItemViewer";
            this.groupBoxReport.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
