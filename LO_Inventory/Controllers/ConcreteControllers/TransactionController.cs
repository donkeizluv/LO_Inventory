using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class TransactionController : ControllerBase<Transaction, TransactionList>
    {
        public TransactionController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<Transaction>>((c) => (new EntityParser(UserId).ParseToTransactions(c)));
            InsertAction = new Func<List<Transaction>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.Transactions.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override Func<List<string[]>, List<Transaction>> ParseFunc { get; set; }
        protected override Func<List<Transaction>, int> InsertAction { get; set; }

        protected override IQueryable<TransactionList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.TransactionLists
                         where
                         row.From.ToLower().Contains(like) ||
                         row.To.ToLower().Contains(like) ||
                         row.TransactionDate.ToString().ToLower().Contains(like) ||
                         row.Username.ToLower().Contains(like) ||
                         row.ItemCode.ToLower().Contains(like) ||
                         row.Note.ToLower().Contains(like)
                         orderby row.TransactionsId
                         select row);
            return query;
        }

        protected override void Innit()
        {
            HiddenColumns.Add("TransactionsId");
            HiddenColumns.Add("UserId");
            HiddenColumns.Add("ItemId");
        }

        protected override void InnitReportViewers()
        {
        }

        protected override IQueryable<TransactionList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.TransactionLists
                         select row).OrderBy(row => row.TransactionsId);
            return query;
        }
    }
}