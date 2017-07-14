using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class CabinetTypeController : ControllerBase<CabinetType, CabinetTypeList>
    {
        public CabinetTypeController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<CabinetType>>((c) =>
              (new EntityParser(UserId).ParseToCabinetTypes(c)));
            InsertAction = new Func<List<CabinetType>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.CabinetTypes.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override Func<List<string[]>, List<CabinetType>> ParseFunc { get; set; }
        protected override Func<List<CabinetType>, int> InsertAction { get; set; }

        protected override IQueryable<CabinetTypeList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.CabinetTypeLists
                         where row.Description.ToLower().Contains(like) ||
                         row.Name.ToLower().Contains(like)
                         orderby row.CabinetTypeId
                         select row);
            return query;
        }

        protected override void Innit()
        {
            HiddenColumns.Add("CabinetTypeId");
        }

        protected override void InnitReportViewers()
        {
        }

        protected override IQueryable<CabinetTypeList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.CabinetTypeLists
                         select row
             ).OrderBy(row => row.CabinetTypeId);
            return query;
        }
    }
}