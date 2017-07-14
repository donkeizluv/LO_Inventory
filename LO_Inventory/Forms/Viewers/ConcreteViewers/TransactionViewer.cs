using LO_Inventory.Controllers.ConcreteControllers;

namespace LO_Inventory.Forms.Viewers.ConcreteViewers
{
    public partial class TransactionViewer : Viewer
    {
        public TransactionController MyController { get; set; }

        public TransactionViewer()
        {
            InitializeComponent();
            MyController = new TransactionController(this);
            Controller = MyController;
            CleanEmptyReportButtons();
        }
    }
}