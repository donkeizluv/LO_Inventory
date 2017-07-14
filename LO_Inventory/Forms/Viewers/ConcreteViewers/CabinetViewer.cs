using LO_Inventory.Controllers.ConcreteControllers;
using System;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class CabinetViewer : Viewer
    {
        public CabinetViewer()
        {
            InitializeComponent();
            MyController = new CabinetController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }

        public CabinetViewer(ActionLogger logger) : base(logger)
        {
        }

        public CabinetController MyController { get; set; }

        //on hand report
        private void ButtonReport1_Click(object sender, EventArgs e)
        {
            if (HelperMethods.ExecuteDbRequest(() => MyController.OnHandReport.RefreshGrid()))
            {
                MyController.OnHandReport.Show();
            }
        }
    }
}