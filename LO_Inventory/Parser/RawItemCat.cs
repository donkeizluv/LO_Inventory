using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Parser
{
    public class RawItemCat : IRaw<ItemCat>
    {
        private const int NameLength = 50;

        public List<string[]> CSV { get; private set; }

        public int ColumnCount => 1;

        public RawItemCat(List<string[]> csv)
        {
            CSV = csv;
        }

        public List<ItemCat> ToEntities()
        {
            var entities = new List<ItemCat>();
            for (int i = 0; i < CSV.Count; i++)
            {
                if (CSV[i].Count() != ColumnCount) throw new EntityParsingException("Invalid column count", i);
                string name = CSV[i][0];
                if (name.Length > NameLength) throw new EntityParsingException("Invalid length", "ItemName", name, i);

                var cat = new ItemCat()
                {
                    CatName = name
                };
                entities.Add(cat);
            }
            return entities;
        }
    }
}