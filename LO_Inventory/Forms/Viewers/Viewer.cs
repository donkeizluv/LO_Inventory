using LO_Inventory.Controllers;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace LO_Inventory.Forms
{
    public partial class Viewer : Form, IViewer
    {
        public const string EmptyButtonName = "empty";

        public Viewer()
        {
            InitializeComponent();
        }

        public Viewer(ActionLogger actionLog) : this()
        {
            ActionLogger = actionLog;
        }

        public DataGridView Grid => dataGridViewReport;

        public ActionLogger ActionLogger { get; set; }
        public IController Controller { get; set; }
        private int _totalRows;
        public int TotalRows
        {
            get
            {
                return _totalRows;
            }
            set
            {
                _totalRows = value;
                labelRowCount.Text = _totalRows.ToString();
            }
        }
        private int _currentPage;
        public int CurrentPage
        {
            get
            {
                return _currentPage;

            }
            set
            {
                _currentPage = value;
                textBoxCurrentPage.Text = _currentPage.ToString();
            }
        }
        private int _totalPages;
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
            set
            {
                _totalPages = value;
                labelTotalPage.Text = _totalPages.ToString();
            }
        }

        public bool IsFiltered { get; protected set; }

        public virtual void InsertButtonHandler()
        {
            try
            {
                if (!HelperMethods.ShowCSVFileBrowser(this, out string path))
                {
                    return;
                }
                var content = HelperMethods.ReadCSV(path);
                if (MessageBox.Show($"Are you sure to insert: {content.Count} record(s)?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes) return;
                //start insering
                var affedted = Controller.Insert(content);
                ActionLogger.AddToLog($"Insert {Text}", $"Success inserted: {affedted}");
                //show log!
                //foreach (var log in logs)
                //{
                //    ActionLogger.AddToLog(log);
                //}
            }
            //catch all the parsing, db, entities exceptions here!
            catch (ArgumentException ex) //who throws this???
            {
                throw;
            }
            catch (EntityParsingException ex) //parsing exception
            {
                var mess = string.Empty;
                if(ex.ErrorIndex != -1)
                {
                    mess = $"{ex.Message} row: [{ex.ErrorIndex + 1}]";

                }
                if(ex.ValueName != string.Empty)
                {
                    mess += $", value name: [{ex.ValueName}]";
                }
                if(ex.RawValue != string.Empty)
                {
                    mess += $", raw value: [{ex.RawValue}]";
                }
                var actionLog = new ActionLog($"Insert {Text}", mess);
                ActionLogger.AddToLog(actionLog);
            }
            catch (DbUpdateException ex) //db exception
            {
                var dbErrors = EntityParser.TryDecodeDbUpdateException(ex);
                var actions = EntityParser.ValidationResultToActionLog($"Insert {Text}", dbErrors, true);
                var actionLogs = new List<ActionLog>();
                if (actions == null)
                {
                    actionLogs = new List<ActionLog>
                                    {
                                        new ActionLog($"Insert {Text}", "Unknown error.", true)
                                    };
                }
                else
                {
                    actionLogs = actions.ToList();
                }

                ActionLogger.AddToLog(actionLogs);
            }
        }

        public void EnableControls(bool enable = false)
        {
            foreach (Control c in Controls)
            {
                c.Enabled = enable;
            }
        }

        protected void CleanEmptyReportButtons()
        {
            foreach (Control c in groupBoxReport.Controls)
            {
                if (string.Compare(c.Text, EmptyButtonName, true) == 0)
                {
                    c.Visible = false;
                }
            }
        }

        public virtual bool RefeshMainGrid(int page = 1)
        {
            Controller.RefreshPageNumber();
            var table = HelperMethods.ExecuteDbRequest(() => Controller.GetMainGridDataTable(page));
            if (table == default(DataTable))
            {
                EnableControls(false);
                return false;
            }
            Controller.ShowDatatable(table);
            Controller.HideColumns();
            EnableControls(true);
            SetNavigationButton();
            return true;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        private void ButtonGridToClip_Click(object sender, EventArgs e)
        {
            Controller.MainGridToClip();
        }

        private void ButtonToCSV_Click(object sender, EventArgs e)
        {
            MessageBox.Show("chức năng đang được xây dựng");

        }

        private void ButtonInsert_Click(object sender, EventArgs e)
        {
            InsertButtonHandler();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            if (!HelperMethods.ExecuteDbRequest(() => RefeshMainGrid()))
            {
                EnableControls(false);
            }
        }

        private void SetNavigationButton()
        {
            buttonNext.Enabled = !(CurrentPage >= TotalPages);
            buttonLast.Enabled = !(CurrentPage >= TotalPages);
            buttonFirst.Enabled = CurrentPage != 1;
            buttonPrev.Enabled = CurrentPage > 1;
        }

        public void Next()
        {
            CurrentPage++;
            GotoPage(CurrentPage);
        }

        public void Last()
        {
            CurrentPage = TotalPages;
            GotoPage(CurrentPage);
        }

        public void Prev()
        {
            CurrentPage--;
            GotoPage(CurrentPage);
        }

        public void First()
        {
            GotoPage(1);
        }

        public void GotoPage(int page)
        {
            if (page < 1 || page > TotalPages) throw new ArgumentException("Invalid page number.");

            RefeshMainGrid(page);
            //SetNavigationButton();
        }

        private void TextBoxCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 8) return; //backspace
            if(e.KeyChar == 13) //enter
            {
                var page = int.Parse(textBoxCurrentPage.Text.Length > 1 ? 
                    textBoxCurrentPage.Text.TrimStart('0') : textBoxCurrentPage.Text);
                if(page < 1 || page > TotalPages) //invalid page
                {
                    textBoxCurrentPage.Text = CurrentPage.ToString();
                    e.Handled = true;
                    textBoxCurrentPage.SelectAll();
                    return;
                }
                GotoPage(page);
            }
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ButtonFirst_Click(object sender, EventArgs e)
        {
            First();
        }

        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            Prev();
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            Next();
        }

        private void ButtonLast_Click(object sender, EventArgs e)
        {
            Last();
        }

        private void ButtonApplyFilter_Click(object sender, EventArgs e)
        {
            Navigation(false);
            IsFiltered = true;
            var table = HelperMethods.ExecuteDbRequest(() => Controller.GetFilterdDataTable(textBoxFilter.Text));
            if (table == default(DataTable))
            {
                EnableControls(false);
                return;
            }
            Controller.ShowDatatable(table);
            labelRowCount.Text = table.Rows.Count.ToString();
            Controller.HideColumns();
            buttonClearFilter.Enabled = true;
            buttonApplyFilter.Enabled = false;

        }

        private void ButtonClearFilter_Click(object sender, EventArgs e)
        {
            Navigation(true);
            IsFiltered = false;
            textBoxFilter.Text = string.Empty;
            GotoPage(CurrentPage);
            buttonClearFilter.Enabled = false;
            buttonApplyFilter.Enabled = true;
        }

        public void Navigation(bool enable)
        {
            buttonFirst.Enabled = enable;
            buttonNext.Enabled = enable;
            buttonPrev.Enabled = enable;
            buttonLast.Enabled = enable;
            textBoxCurrentPage.Enabled = enable;
        }

        private void TextBoxFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) //enter
            {
                ButtonApplyFilter_Click(this, EventArgs.Empty);
            }
        }
    }
}