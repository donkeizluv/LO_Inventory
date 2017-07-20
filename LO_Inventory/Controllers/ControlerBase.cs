using LO_Inventory.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Linq.Expressions;

namespace LO_Inventory.Controllers
{
    //T: controller Type
    //V: view entity
    public abstract class ControllerBase<T, V> : IController
    {
        public DataGridView Grid { get; private set; }

        public IViewer Viewer { get; private set; }
        public List<string> HiddenColumns { get; set; } = new List<string>();

        protected abstract IQueryable<V> MainQuery(InventoryDbEntities context);

        protected abstract IQueryable<V> FilteredQuery(InventoryDbEntities context, string like);

        protected abstract void Innit();

        protected abstract void InnitReportViewers();

        protected abstract Func<List<string[]>, List<T>> ParseFunc { get; set; }

        protected abstract Func<List<T>, int> InsertAction { get; set; }

        public string Username { get; set; }

        public int UserId { get; set; }
        private int _totalRows;
        public int TotalRows
        {
            get
            {
                return _totalRows;
            }
            set
            {
                _totalRows = value;
                Viewer.TotalRows = value;
            }
        }
        private int _currentPage;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                Viewer.CurrentPage = value;
            }
        }
        private int _totalPages;
        public int TotalPages
        {
            get
            {
                return _totalPages;
            }
            set
            {
                _totalPages = value;
                Viewer.TotalPages = value;
            }
        }
        public int ItemPerPage { get; set; } = 75;


        public virtual DataTable GetMainGridDataTable(int page)
        {
            CurrentPage = page;
            using (var context = new InventoryDbEntities())
            {
                var paged = PagedResult(MainQuery(context), page);
                return HelperMethods.LINQToDataTable(paged.ToList());
            }
        }

        public virtual DataTable GetFilterdDataTable(string s)
        {
            using (var context = new InventoryDbEntities())
            {
                return HelperMethods.LINQToDataTable(FilteredQuery(context, s).ToList());
            }
        }

        //let viewer handle exception thrown by this method, probly best practice :/
        public virtual int Insert(List<string[]> content)
        {
            var entities = ParseFunc(content);
            return InsertAction(entities);
        }

        public ControllerBase(IViewer view)
        {
            Innit();
            InnitReportViewers();
            Viewer = view;
            Grid = Viewer.Grid;
        }

        public void ShowDatatable(DataTable table)
        {
            Grid.DataSource = table;
        }

        public virtual string[] MainGridToArray(string delimitor)
        {
            throw new NotImplementedException();
        }

        public void MainGridToClip()
        {
            HelperMethods.DataGridViewToClipboard(Grid);
        }

        public void HideColumns()
        {
            try
            {
                foreach (var colName in HiddenColumns)
                {
                    Grid.Columns[colName].Visible = false;
                }
            }
            catch (NullReferenceException) //ignore not exist column
            {
            }
        }

        protected IQueryable<V> PagedResult(IQueryable<V> query, int pageNum)
        {
            if (pageNum < 1) throw new ArgumentException();
            int excludedRows = (pageNum - 1) * ItemPerPage;
            return query.Skip(excludedRows).Take(ItemPerPage);
        }

        public void RefreshPageNumber()
        {
            using (var context = new InventoryDbEntities())
            {
                TotalRows = MainQuery(context).Count();
                TotalPages = (TotalRows + ItemPerPage - 1) / ItemPerPage;
                if (TotalPages < 1)
                    TotalPages = 1;
            }
        }
    }
}