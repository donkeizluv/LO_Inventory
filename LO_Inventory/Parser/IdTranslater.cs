using System.Linq;

namespace LO_Inventory.Parser
{
    public static class IdTranslater
    {
        //public InventoryDbEntities Context { get; private set; }
        //public IdTranslater(InventoryDbEntities context)
        //{
        //    Context = context;
        //}
        //private static int ExecuteWrapper(Func<int> func)
        //{
        //    try
        //    {
        //        return func.Invoke();
        //    }
        //    catch (EntityException ex) when (ex.InnerException != null && ex.InnerException.Message.Contains("The underlying provider failed on Open."))
        //    {
        //        throw new EntityParsingException("test");
        //    }
        //    catch (EntityException ex) when (ex.Message.Contains("The underlying provider failed on Open."))
        //    {
        //        throw new EntityParsingException("test");
        //    }
        //}
        public static bool GetUserId(string name, out int? id, InventoryDbEntities context)
        {
            id = null;
            var user = context.Users.FirstOrDefault(e => string.Compare(name, e.Username, true) == 0);
            if (user == null) return false;
            id = user.UserId;
            return true;
        }

        public static bool GetItemId(string name, out int? id, InventoryDbEntities context)
        {
            id = null;
            var item = context.Items.FirstOrDefault(e => string.Compare(name, e.ItemCode, true) == 0);
            if (item == null) return false;
            id = item.ItemId;
            return true;
        }

        public static bool GetCabinetId(string name, out int? id, InventoryDbEntities context)
        {
            id = null;
            var cabinet = context.Cabinets.FirstOrDefault(e => string.Compare(name, e.CabinetName, true) == 0);
            if (cabinet == null) return false;
            id = cabinet.CabinetId;
            return true;
        }

        public static bool GetCabinetTypeId(string name, out int? id, InventoryDbEntities context)
        {
            id = null;
            var type = context.CabinetTypes.FirstOrDefault(e => string.Compare(name, e.Name, true) == 0);
            if (type == null) return false;
            id = type.CabinetTypeId;
            return true;
        }

        public static bool GetProviderId(string name, out int? id, InventoryDbEntities context)
        {
            id = null;
            var provider = context.Providers.FirstOrDefault(e => string.Compare(name, e.Name, true) == 0);
            if (provider == null) throw new EntityParsingException("Username is not exist!");
            id = provider.ProviderId;
            return true;
        }
    }
}