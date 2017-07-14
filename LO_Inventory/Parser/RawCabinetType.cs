using System.Collections.Generic;

namespace LO_Inventory.Parser
{
    public class RawCabinetType : IRaw<CabinetType>
    {
        public List<string[]> CSV { get; set; }

        public int ColumnCount => 2;
        private const int NameLen = 50;
        private const int DescLen = 120;

        public RawCabinetType(List<string[]> content)
        {
            CSV = content;
        }

        public List<CabinetType> ToEntities()
        {
            var types = new List<CabinetType>();
            for (int i = 0; i < CSV.Count; i++)
            {
                string typeName = CSV[i][0];
                string desciption = CSV[i][1];

                if (typeName.Length > NameLen) throw new EntityParsingException("Invalid length", nameof(typeName), typeName, i);
                if (desciption.Length > DescLen) throw new EntityParsingException("Invalid length", nameof(desciption), desciption, i);

                var type = new CabinetType()
                {
                    Name = typeName,
                    Description = desciption
                };
                types.Add(type);
            }
            return types;
        }
    }
}