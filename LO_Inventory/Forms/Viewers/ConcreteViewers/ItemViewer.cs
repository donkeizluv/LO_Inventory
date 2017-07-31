using LO_Inventory.Controllers.ConcreteControllers;
using System;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class ItemViewer : Viewer
    {
        public ItemController MyController { get; set; }

        public ItemViewer()
        {
            InitializeComponent();
            MyController = new ItemController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }

        public ItemViewer(ActionLogger logger) : base(logger)
        {
        }

        //orders of item
        private void ButtonReport1_Click(object sender, EventArgs e)
        {
            if (HelperMethods.ExecuteDbRequest(() => MyController.OrdersOfItemReport.RefreshGrid()))
            {
                MyController.OrdersOfItemReport.Show();
            }
        }

        //sales of item
        private void ButtonReport2_Click(object sender, EventArgs e)
        {
            if (HelperMethods.ExecuteDbRequest(() => MyController.SalesOfItemReport.RefreshGrid()))
            {
                MyController.SalesOfItemReport.Show();
            }
        }
    }
}