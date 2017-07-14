using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class OrderController : ControllerBase<Order,OrderList>
    {
        public OrderController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<Order>>((c) => (new EntityParser(UserId).ParseToOrders(c)));
            InsertAction = new Func<List<Order>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.Orders.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override Func<List<string[]>, List<Order>> ParseFunc { get; set; }
        protected override Func<List<Order>, int> InsertAction { get; set; }

        protected override IQueryable<OrderList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.OrderLists
                         where row.CabinetName.ToLower().Contains(like) ||
                         row.InputDate.ToString().ToLower().Contains(like) ||
                         row.ItemCode.ToLower().Contains(like) ||
                         row.Note.ToLower().Contains(like) ||
                         row.Provider.ToLower().Contains(like) ||
                         row.Username.ToLower().Contains(like)
                         orderby row.OrderId
                         select row);
            return query;
        }

        protected override void Innit()
        {
            HiddenColumns.Add("ItemId");
            HiddenColumns.Add("OrderId");
            HiddenColumns.Add("CabinetId");
            HiddenColumns.Add("ProviderId");
            HiddenColumns.Add("UserId");
        }

        protected override void InnitReportViewers()
        {
        }

        protected override IQueryable<OrderList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.OrderLists
                         select row
            ).OrderBy(row => row.OrderId);
            return query;
        }
    }
}