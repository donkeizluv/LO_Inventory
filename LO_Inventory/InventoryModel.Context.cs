﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LO_Inventory
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class InventoryDbEntities : DbContext
    {
        //public InventoryDbEntities()
        //    : base("name=InventoryDbEntities")
        //{
        //}
        public InventoryDbEntities() : base(Properties.Resources.LocalDBConnectionString)
        { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cabinet> Cabinets { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<TransactionPermission> TransactionPermissions { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<CabinetType> CabinetTypes { get; set; }
        public virtual DbSet<CabinetList> CabinetLists { get; set; }
        public virtual DbSet<ItemList> ItemLists { get; set; }
        public virtual DbSet<TransactionList> TransactionLists { get; set; }
        public virtual DbSet<CabinetTypeList> CabinetTypeLists { get; set; }
        public virtual DbSet<OrderList> OrderLists { get; set; }
        public virtual DbSet<ProviderList> ProviderLists { get; set; }
        public virtual DbSet<UserList> UserLists { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<TransPermissionList> TransPermissionList { get; set; }
        public virtual DbSet<ItemCat> ItemCats { get; set; }
        public virtual DbSet<CatList> CatLists { get; set; }
    
        [DbFunction("InventoryDbEntities", "GetCurrentItemsCabinetHas")]
        public virtual IQueryable<GetCurrentItemsCabinetHas_Result> GetCurrentItemsCabinetHas(Nullable<int> cabinetId)
        {
            var cabinetIdParameter = cabinetId.HasValue ?
                new ObjectParameter("cabinetId", cabinetId) :
                new ObjectParameter("cabinetId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetCurrentItemsCabinetHas_Result>("[InventoryDbEntities].[GetCurrentItemsCabinetHas](@cabinetId)", cabinetIdParameter);
        }
    
        [DbFunction("InventoryDbEntities", "GetCurrentItemsInUse")]
        public virtual IQueryable<GetCurrentItemsInUse_Result> GetCurrentItemsInUse()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetCurrentItemsInUse_Result>("[InventoryDbEntities].[GetCurrentItemsInUse]()");
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        [DbFunction("InventoryDbEntities", "GetLoginLevel")]
        public virtual IQueryable<Nullable<int>> GetLoginLevel(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<Nullable<int>>("[InventoryDbEntities].[GetLoginLevel](@userName)", userNameParameter);
        }
    
        [DbFunction("InventoryDbEntities", "GetItemTransactionsOfCabinet")]
        public virtual IQueryable<GetItemTransactionsOfCabinet_Result> GetItemTransactionsOfCabinet(Nullable<int> cabinetId, Nullable<int> itemId)
        {
            var cabinetIdParameter = cabinetId.HasValue ?
                new ObjectParameter("cabinetId", cabinetId) :
                new ObjectParameter("cabinetId", typeof(int));
    
            var itemIdParameter = itemId.HasValue ?
                new ObjectParameter("itemId", itemId) :
                new ObjectParameter("itemId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetItemTransactionsOfCabinet_Result>("[InventoryDbEntities].[GetItemTransactionsOfCabinet](@cabinetId, @itemId)", cabinetIdParameter, itemIdParameter);
        }
    
        [DbFunction("InventoryDbEntities", "GetOrdersOfItem")]
        public virtual IQueryable<GetOrdersOfItem_Result> GetOrdersOfItem(Nullable<int> itemId)
        {
            var itemIdParameter = itemId.HasValue ?
                new ObjectParameter("itemId", itemId) :
                new ObjectParameter("itemId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetOrdersOfItem_Result>("[InventoryDbEntities].[GetOrdersOfItem](@itemId)", itemIdParameter);
        }
    
        [DbFunction("InventoryDbEntities", "GetSalesOfItem")]
        public virtual IQueryable<GetSalesOfItem_Result> GetSalesOfItem(Nullable<int> itemId)
        {
            var itemIdParameter = itemId.HasValue ?
                new ObjectParameter("itemId", itemId) :
                new ObjectParameter("itemId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetSalesOfItem_Result>("[InventoryDbEntities].[GetSalesOfItem](@itemId)", itemIdParameter);
        }
    
        [DbFunction("InventoryDbEntities", "GetOrdersOfUser")]
        public virtual IQueryable<GetOrdersOfUser_Result> GetOrdersOfUser(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetOrdersOfUser_Result>("[InventoryDbEntities].[GetOrdersOfUser](@userName)", userNameParameter);
        }
    }
}
