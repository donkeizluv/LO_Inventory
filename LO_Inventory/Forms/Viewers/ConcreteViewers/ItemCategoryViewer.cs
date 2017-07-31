using LO_Inventory.Controllers.ConcreteControllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class ItemCategoryViewer : Viewer
    {
        public ItemCategoryController MyController { get; set; }

        public ItemCategoryViewer()
        {
            InitializeComponent();
            MyController = new ItemCategoryController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}
