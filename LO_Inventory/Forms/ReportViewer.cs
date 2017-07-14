using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace LO_Inventory.Forms
{
    public interface IReportViewer : IDisposable
    {
        IReportViewer SubReport { get; set; }
        List<string> HiddenColumns { get; set; }
        string Name { get; set; }
        string SubReportName { get; set; }
        DataGridView Grid { get; }

        void FetchGrid(DataTable table);

        void Show();

        bool Focus();

        void ShowSubReport();

        void RefreshGrid();

        Func<DataTable> GetDatatableFunc { get; set; }
    }

    public partial class ReportViewer : Form, IReportViewer
    {
        public ReportViewer(string name) : this()
        {
            Text = name;
        }

        public string SubReportName { get; set; }

        public DataGridView Grid
        {
            get
            {
                return dataGridViewReport;
            }
        }

        private IReportViewer _subReport;

        public IReportViewer SubReport
        {
            get
            {
                return _subReport;
            }
            set
            {
                buttonDetail.Enabled = true;
                _subReport = value;
            }
        }

        public Func<DataTable> GetDatatableFunc { get; set; }
        public List<string> HiddenColumns { get; set; } = new List<string>();

        private ReportViewer()
        {
            InitializeComponent();
        }

        public void DisplayGrid(DataTable table)
        {
            Grid.DataSource = table;
        }

        public void FetchGrid(DataTable table)
        {
            Grid.DataSource = table;
            HideColumns();
        }

        private void HideColumns()
        {
            try
            {
                foreach (var c in HiddenColumns)
                {
                    Grid.Columns[c].Visible = false;
                }
            }
            catch (NullReferenceException)
            {
            }
        }

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

        public void Dispose()
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        {
            base.Hide();
            base.Dispose();
        }

        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            HelperMethods.DataGridViewToClipboard(dataGridViewReport);
        }

        public void ShowSubReport()
        {
            if (SubReport == null)
            {
                throw new InvalidProgramException("Sub report is not set.");
            }
            SubReport.RefreshGrid();
            SubReport.Show();
            SubReport.Focus();
        }

        /// <summary>
        /// set DataTable from GetDatatableFunc to main grid data source.
        /// </summary>
        public void RefreshGrid()
        {
            if (GetDatatableFunc == null) throw new InvalidProgramException("No get datatable func.");
            FetchGrid(GetDatatableFunc.Invoke());
        }

        private void ReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        private void ButtonDetail_Click(object sender, EventArgs e)
        {
            if (SubReport == null) throw new InvalidProgramException("Sub report is not set.");
            SubReport.Show();
            SubReport.RefreshGrid();
        }
    }
}