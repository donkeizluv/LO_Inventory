using System.Collections.Generic;

namespace LO_Inventory.Parser
{
    public interface IRaw<T>
    {
        List<T> ToEntities();

        List<string[]> CSV { get; }
        int ColumnCount { get; }
    }
}