using LO_Inventory.Forms;
using LO_Inventory.Parser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class PermissionController : ControllerBase<TransactionPermission, TransPermissionList>
    {
        public PermissionController(IViewer view) : base(view)
        {
            ParseFunc = new Func<List<string[]>, List<TransactionPermission>>((c) =>
                (new EntityParser(UserId).ParseToPermission(c)));
            InsertAction = new Func<List<TransactionPermission>, int>((e) =>
            {
                using (var context = new InventoryDbEntities())
                {
                    context.TransactionPermissions.AddRange(e);
                    return context.SaveChanges();
                }
            });
        }

        protected override Func<List<string[]>, List<TransactionPermission>> ParseFunc { get; set; }
        protected override Func<List<TransactionPermission>, int> InsertAction { get; set; }

        protected override IQueryable<TransPermissionList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.TransPermissionList
                         where
                         row.Cabinet_Type.ToLower().Contains(like) ||
                         row.Note.ToLower().Contains(like) ||
                         row.PermissionType.ToLower().Contains(like) ||
                         row.Username.ToLower().Contains(like)
                         orderby row.PermissionId
                         select row);
            return query;
        }

        protected override void Innit()
        {
            HiddenColumns.Add("UserId");
            HiddenColumns.Add("CabinetTypeId");
            HiddenColumns.Add("PermissionId");
        }

        protected override void InnitReportViewers()
        {
        }

        protected override IQueryable<TransPermissionList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.TransPermissionList
                         select row
            ).OrderBy(row => row.PermissionId);
            return query;
        }
    }
}