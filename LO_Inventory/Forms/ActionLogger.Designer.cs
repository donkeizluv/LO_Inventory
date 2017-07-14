namespace LO_Inventory.Forms
{
    partial class ActionLogger
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
            this.dataGridViewActionLog = new System.Windows.Forms.DataGridView();
            this.columnTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ActionTypeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.erorrMessageColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActionLog)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewActionLog
            // 
            this.dataGridViewActionLog.AllowUserToAddRows = false;
            this.dataGridViewActionLog.AllowUserToDeleteRows = false;
            this.dataGridViewActionLog.AllowUserToResizeColumns = false;
            this.dataGridViewActionLog.AllowUserToResizeRows = false;
            this.dataGridViewActionLog.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridViewActionLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewActionLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnTime,
            this.ActionTypeColumn,
            this.erorrMessageColumn});
            this.dataGridViewActionLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewActionLog.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewActionLog.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewActionLog.MultiSelect = false;
            this.dataGridViewActionLog.Name = "dataGridViewActionLog";
            this.dataGridViewActionLog.ReadOnly = true;
            this.dataGridViewActionLog.RowTemplate.Height = 24;
            this.dataGridViewActionLog.ShowEditingIcon = false;
            this.dataGridViewActionLog.Size = new System.Drawing.Size(921, 476);
            this.dataGridViewActionLog.TabIndex = 7;
            // 
            // columnTime
            // 
            this.columnTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.columnTime.HeaderText = "Time";
            this.columnTime.Name = "columnTime";
            this.columnTime.ReadOnly = true;
            this.columnTime.Width = 68;
            // 
            // ActionTypeColumn
            // 
            this.ActionTypeColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ActionTypeColumn.HeaderText = "Action";
            this.ActionTypeColumn.Name = "ActionTypeColumn";
            this.ActionTypeColumn.ReadOnly = true;
            this.ActionTypeColumn.Width = 76;
            // 
            // erorrMessageColumn
            // 
            this.erorrMessageColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.erorrMessageColumn.HeaderText = "Error Message";
            this.erorrMessageColumn.Name = "erorrMessageColumn";
            this.erorrMessageColumn.ReadOnly = true;
            this.erorrMessageColumn.Width = 130;
            // 
            // ActionLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 476);
            this.Controls.Add(this.dataGridViewActionLog);
            this.Name = "ActionLogger";
            this.ShowIcon = false;
            this.Text = "Action Logs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActionLog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActionLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewActionLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ActionTypeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn erorrMessageColumn;
    }
}