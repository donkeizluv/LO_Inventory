using LO_Inventory.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace LO_Inventory.Controllers
{
    public interface IController
    {
        string Username { get; set; }
        int UserId { get; set; }

        List<string> HiddenColumns { get; set; }

        DataTable GetMainGridDataTable(int page);
        DataTable GetFilterdDataTable(string s);
        string[] MainGridToArray(string delimitor);

        void ShowDatatable(DataTable table);
        void RefreshPageNumber();
        //IReportViewer MyReportViewer { get; }

        IViewer Viewer { get; }
        DataGridView Grid { get; }

        //void ShowReportViewer(DataTable table, string reportName);

        void MainGridToClip();

        void HideColumns();

        int Insert(List<string[]> content);
    }
}