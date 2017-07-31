using LO_Inventory.Controllers.ConcreteControllers;

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