using LO_Inventory.Controllers;
using System;
using System.Windows.Forms;

namespace LO_Inventory.Forms
{
    public interface IViewer : IDisposable
    {
        DataGridView Grid { get; }
        ActionLogger ActionLogger { get; set; }

        void Show();

        bool Focus();

        bool RefeshMainGrid(int page = 1);

        void EnableControls(bool enable = false);

        IController Controller { get; set; }
        bool IsFiltered { get; }
        string Text { get; set; }
        int TotalRows { get; set; }
        int CurrentPage { get; set; }    
        int TotalPages { get; set; }
        void Next();
        void Last();
        void Prev();
        void First();
        void GotoPage(int page);
        void Navigation(bool enable);

    }
}