using LO_Inventory.Controllers.ConcreteControllers;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class OrderViewer : LO_Inventory.Forms.Viewer
    {
        public OrderController MyController { get; set; }

        public OrderViewer()
        {
            InitializeComponent();
            MyController = new OrderController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}