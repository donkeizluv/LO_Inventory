using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class ItemController : ControllerBase<Item, ItemList>
    {
        public readonly string OrdersOfItemReportName = "Order History";
        public readonly string SalesOfItemReportName = "Sale History";
        public readonly string InsertItemsActionName = "Insert Items";

        public IReportViewer OrdersOfItemReport { get; set; }
        public IReportViewer SalesOfItemReport { get; set; }
        protected override Func<List<string[]>, List<Item>> ParseFunc { get; set; }
        protected override Func<List<Item>, int> InsertAction { get; set; }

        public ItemController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<Item>>((c) => (new EntityParser(UserId).ParseToItems(c)));
            InsertAction = new Func<List<Item>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.Items.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override void Innit()
        {
            HiddenColumns.Add("ItemId");
        }

        public DataTable GetOrdersOfItem()
        {
            int itemId = (int)Grid.SelectedRows[0].Cells["ItemId"].Value;
            using (var context = new InventoryDbEntities())
            {
                return HelperMethods.LINQToDataTable(context.GetOrdersOfItem(itemId));
            }
        }

        public DataTable GetSalesOfItem()
        {
            int itemId = (int)Grid.SelectedRows[0].Cells["ItemId"].Value;
            using (var context = new InventoryDbEntities())
            {
                return HelperMethods.LINQToDataTable(context.GetSalesOfItem(itemId));
            }
        }

        protected override void InnitReportViewers()
        {
            OrdersOfItemReport = new ReportViewer(OrdersOfItemReportName);
            OrdersOfItemReport.GetDatatableFunc = GetOrdersOfItem;
            OrdersOfItemReport.HiddenColumns.Add("ItemId");

            SalesOfItemReport = new ReportViewer(SalesOfItemReportName);
            SalesOfItemReport.GetDatatableFunc = GetSalesOfItem;
            SalesOfItemReport.HiddenColumns.Add("ItemId");
        }

        protected override IQueryable<ItemList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.ItemLists
                         select row
                         ).OrderBy(row => row.ItemId);
            return query;
        }

        protected override IQueryable<ItemList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from item in context.ItemLists
                         where item.ItemCode.ToLower().Contains(like) ||
                         item.ItemName.ToLower().Contains(like)
                         orderby item.ItemId
                         select item);
            return query;
        }
    }
}