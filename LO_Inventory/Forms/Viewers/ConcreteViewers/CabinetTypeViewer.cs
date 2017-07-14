using LO_Inventory.Controllers.ConcreteControllers;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class CabinetTypeViewer : Viewer
    {
        public CabinetTypeController MyController { get; set; }

        public CabinetTypeViewer()
        {
            InitializeComponent();
            MyController = new CabinetTypeController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}