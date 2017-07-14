using LO_Inventory.Controllers.ConcreteControllers;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class ProviderViewer : LO_Inventory.Forms.Viewer
    {
        public ProviderController MyController { get; set; }

        public ProviderViewer()
        {
            InitializeComponent();
            MyController = new ProviderController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}