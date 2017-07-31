using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Parser
{
    public class RawCabinets : IRaw<Cabinet>
    {
        private const int NameLength = 50;
        private const int typeNameLength = 50;
        private const int AddressLength = 150;
        private const int PhoneLength = 30;

        public RawCabinets(List<string[]> csv)
        {
            CSV = csv;
        }

        public List<string[]> CSV { get; set; }

        public int ColumnCount => 4;

        public List<Cabinet> ToEntities()
        {
            var cabinets = new List<Cabinet>();
            for (int i = 0; i < CSV.Count; i++)
            {
                if (CSV[i].Count() != ColumnCount) throw new EntityParsingException("Invalid column count", i);
                string name = CSV[i][0];
                string typeName = CSV[i][1];
                string address = CSV[i][2];
                string phone = CSV[i][3];
                //check length
                if (name.Length > NameLength) throw new EntityParsingException("Invalid length", nameof(name), name, i);
                if (typeName.Length > typeNameLength) throw new EntityParsingException("Invalid length", nameof(typeName), typeName, i);
                if (address.Length > AddressLength) throw new EntityParsingException("Invalid length", nameof(address), address, i);
                if (phone.Length > PhoneLength) throw new EntityParsingException("Invalid length", nameof(phone), phone, i);
                //get id
                int? typeId;
                using (var context = new InventoryDbEntities())
                {
                    if (!IdTranslater.GetCabinetTypeId(typeName, out typeId, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "CabinetType", typeName, i);
                    }
                }
                var cabinet = new Cabinet()
                {
                    CabinetName = name,
                    CabinetTypeId = typeId ?? -1,
                    Address = address,
                    Phone = phone
                };
                cabinets.Add(cabinet);
            }
            return cabinets;
        }
    }
}