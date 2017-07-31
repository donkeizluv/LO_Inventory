using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class ItemCategoryController : ControllerBase<ItemCat, CatList>
    {
        public ItemCategoryController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<ItemCat>>((c) => (new EntityParser(UserId).ParseToItemCats(c)));
            InsertAction = new Func<List<ItemCat>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.ItemCats.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override Func<List<string[]>, List<ItemCat>> ParseFunc { get; set; }
        protected override Func<List<ItemCat>, int> InsertAction { get; set; }

        protected override void Innit()
        {
            HiddenColumns.Add("CatId");
        }

        protected override void InnitReportViewers()
        {
        }

        protected override IQueryable<CatList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from itemCat in context.CatLists
                         where itemCat.Category_Name.ToLower().Contains(like)
                         orderby itemCat.CatId
                         select itemCat);
            return query;
        }

        protected override IQueryable<CatList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.CatLists
                         select row
                         ).OrderBy(row => row.CatId);
            return query;
        }
    }
}