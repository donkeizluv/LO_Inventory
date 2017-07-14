using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class ProviderController : ControllerBase<Provider, ProviderList>
    {
        public ProviderController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<Provider>>((c) => (new EntityParser(UserId).ParseToProviders(c)));
            InsertAction = new Func<List<Provider>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.Providers.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override Func<List<string[]>, List<Provider>> ParseFunc { get; set; }
        protected override Func<List<Provider>, int> InsertAction { get; set; }
        protected override IQueryable<ProviderList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.ProviderLists
                         where
                         row.Name.ToLower().Contains(like) ||
                         row.Phone.ToLower().Contains(like) ||
                         row.Address.ToLower().Contains(like)
                         orderby row.ProviderId
                         select row);
            return query;
        }

        protected override void Innit()
        {
            HiddenColumns.Add("ProviderId");
        }

        protected override void InnitReportViewers()
        {

        }

        protected override IQueryable<ProviderList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.ProviderLists
                         select row
           ).OrderBy(row => row.ProviderId);
            return query;
        }
    }
}