using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Parser
{
    public class RawItems : IRaw<Item>
    {
        public List<string[]> CSV { get; private set; }

        public int ColumnCount => 2;

        private const int CodeLength = 50;
        private const int NameLength = 50;

        public RawItems(List<string[]> csv)
        {
            CSV = csv;
        }

        public List<Item> ToEntities()
        {
            var entities = new List<Item>();
            for (int i = 0; i < CSV.Count; i++)
            {
                if (CSV[i].Count() != ColumnCount) throw new EntityParsingException("Invalid column count", i);
                string code = CSV[i][0];
                string name = CSV[i][1];
                if (code.Length > CodeLength) throw new EntityParsingException("Invalid length", "ItemCode", code, i);
                if (name.Length > NameLength) throw new EntityParsingException("Invalid length", "ItemName", name, i);

                var item = new Item()
                {
                    ItemCode = code,
                    ItemName = name,
                };
                entities.Add(item);
            }
            return entities;
        }
    }
}