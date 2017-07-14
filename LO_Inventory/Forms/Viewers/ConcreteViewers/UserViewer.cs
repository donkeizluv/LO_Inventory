using LO_Inventory.Controllers.ConcreteControllers;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class UserViewer : Viewer
    {
        public UserController MyController { get; set; }

        public UserViewer()
        {
            InitializeComponent();
            MyController = new UserController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}