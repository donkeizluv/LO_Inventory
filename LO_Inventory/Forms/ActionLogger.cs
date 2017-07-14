using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace LO_Inventory.Forms
{
    public partial class ActionLogger : System.Windows.Forms.Form
    {
        public Color BackgroundErrorColor { get; set; } = Color.Salmon;

        public ActionLogger()
        {
            InitializeComponent();
        }

        private void ActionLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        public void ClearLog()
        {
            dataGridViewActionLog.Rows.Clear();
        }

        private void AddToLog(string[] rowContent, bool error)
        {
            if (rowContent.Length > 3) throw new ArgumentException("Invalid row content length");
            var row = (DataGridViewRow)dataGridViewActionLog.RowTemplate.Clone();

            row.CreateCells(dataGridViewActionLog, rowContent);
            if (error)
                row.DefaultCellStyle.BackColor = BackgroundErrorColor;
            dataGridViewActionLog.Rows.Add(row);
            Show();
        }

        public void AddToLog(string action, string mess, bool error = false)
        {
            AddToLog(new string[] { DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"), action, mess }, error);
        }

        public void AddToLog(ActionLog log)
        {
            AddToLog(log.Action, log.Message, log.Error);
        }

        public void AddToLog(List<ActionLog> logs)
        {
            foreach (var log in logs)
            {
                AddToLog(log);
            }
        }
    }
}