using LO_Inventory.Controllers.ConcreteControllers;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class PermissionViewer : LO_Inventory.Forms.Viewer
    {
        public PermissionController MyController { get; set; }

        public PermissionViewer()
        {
            InitializeComponent();
            MyController = new PermissionController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}