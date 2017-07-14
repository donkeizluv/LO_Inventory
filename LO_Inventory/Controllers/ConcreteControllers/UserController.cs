using LO_Inventory.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LO_Inventory.Controllers.ConcreteControllers
{
    public class UserController : ControllerBase<User, UserList>
    {
        public UserController(IViewer view) : base(view)
        {
        }

        protected override Func<List<string[]>, List<User>> ParseFunc { get; set; }
        protected override Func<List<User>, int> InsertAction { get; set; }

        protected override IQueryable<UserList> FilteredQuery(InventoryDbEntities context, string like)
        {
            var query = (from row in context.UserLists
                         where
                         row.Username.ToLower().Contains(like) ||
                         row.RoleName.ToLower().Contains(like) ||
                         row.Note.ToString().ToLower().Contains(like)
                         orderby row.UserId
                         select row);
            return query;
        }

        protected override void Innit()
        {
            HiddenColumns.Add("UserId");
        }

        protected override void InnitReportViewers()
        {
        }

        protected override IQueryable<UserList> MainQuery(InventoryDbEntities context)
        {
            var query = (from row in context.UserLists
                         select row).OrderBy(row => row.UserId);
            return query;
        }
    }
}