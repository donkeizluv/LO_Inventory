using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Parser
{
    internal class RawProvider : IRaw<Provider>
    {
        public RawProvider(List<string[]> csv)
        {
            CSV = csv;
        }

        public List<string[]> CSV { get; private set; }

        public int ColumnCount => 3;

        private const int PhoneLength = 30;
        private const int NameLength = 50;
        private const int AddressLength = 150;

        public List<Provider> ToEntities()
        {
            var entities = new List<Provider>();
            for (int i = 0; i < CSV.Count; i++)
            {
                if (CSV[i].Count() != ColumnCount) throw new EntityParsingException("Invalid column count", i);
                string name = CSV[i][0];
                string address = CSV[i][1];
                string phone = CSV[i][2];
                if (name.Length > NameLength) throw new EntityParsingException("Invalid length", nameof(name), name, i);
                if (address.Length > AddressLength) throw new EntityParsingException("Invalid length", nameof(address), address, i);
                if (phone.Length > NameLength) throw new EntityParsingException("Invalid length", nameof(phone), phone, i);

                var item = new Provider()
                {
                    Name = name,
                    Address = address,
                    Phone = phone
                };
                entities.Add(item);
            }
            return entities;
        }
    }
}