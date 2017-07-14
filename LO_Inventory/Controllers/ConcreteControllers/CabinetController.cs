using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class CabinetController : ControllerBase<Cabinet, CabinetList>
    {
        public readonly string OnHandReportName = "On Hand Item";
        public readonly string ItemTransactionReportName = "Transaction History";
        public readonly string InsertCabinetActionName = "Insert Cabinet";
        public IReportViewer OnHandReport { get; set; }
        public IReportViewer ItemTransactionSubReport { get; set; }
        protected override Func<List<string[]>, List<Cabinet>> ParseFunc { get; set; }
        protected override Func<List<Cabinet>, int> InsertAction { get; set; }

        public CabinetController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<Cabinet>>((c) => (new EntityParser(UserId).ParseToCabinets(c)));
            InsertAction = new Func<List<Cabinet>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.Cabinets.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override void Innit()
        {
            HiddenColumns.Add("CabinetId");
        }

        protected override void InnitReportViewers()
        {
            //innit on hand report
            OnHandReport = new ReportViewer(OnHandReportName);
            OnHandReport.GetDatatableFunc = GetItemCabinetHas;
            OnHandReport.HiddenColumns.Add("ItemId");

            //innit sub report
            ItemTransactionSubReport = new ReportViewer(ItemTransactionReportName);
            ItemTransactionSubReport.GetDatatableFunc = GetItemTransactionOfCabinet;
            ItemTransactionSubReport.HiddenColumns.Add("ItemId");
            OnHandReport.SubReport = ItemTransactionSubReport;
        }

        public DataTable GetItemCabinetHas()
        {
            using (var context = new InventoryDbEntities())
            {
                return HelperMethods.LINQToDataTable(context.GetCurrentItemsCabinetHas((int)Grid.SelectedRows[0].Cells["CabinetId"].Value));
            }
        }

        public DataTable GetItemTransactionOfCabinet()
        {
            int cabinetId = (int)Grid.SelectedRows[0].Cells["CabinetId"].Value;
            int itemId = (int)OnHandReport.Grid.SelectedRows[0].Cells["ItemId"].Value;
            using (var context = new InventoryDbEntities())
            {
                return HelperMethods.LINQToDataTable(context.GetItemTransactionsOfCabinet(cabinetId, itemId).OrderBy(r => r.ActionDate));
            }
        }

        public override DataTable GetMainGridDataTable(int page)
        {
            CurrentPage = page;
            using (var context = new InventoryDbEntities())
            {
                var paged = PagedResult(MainQuery(context), page);
                return HelperMethods.LINQToDataTable(paged.ToList());
            }
        }

        protected override IQueryable<CabinetList> MainQuery(InventoryDbEntities context)
        {
            var query = (from item in context.CabinetLists
                         select item
             ).OrderBy(row => row.CabinetId);
            return query;
        }

        protected override IQueryable<CabinetList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.CabinetLists
                         where row.CabinetName.ToLower().Contains(like) ||
                         row.Phone.ToLower().Contains(like) ||
                         row.Type.ToLower().Contains(like) ||
                         row.Address.ToLower().Contains(like)
                         orderby row.CabinetId
                         select row);
            return query;
        }
    }
}