using System.Collections.Generic;

namespace LO_Inventory.Parser
{
    public class RawPermission : IRaw<TransactionPermission>
    {
        public RawPermission(List<string[]> content)
        {
            CSV = content;
        }

        public List<string[]> CSV { get; private set; }

        public int ColumnCount => 4;
        private const int NoteLength = 150;

        public List<TransactionPermission> ToEntities()
        {
            var entities = new List<TransactionPermission>();
            for (int i = 0; i < CSV.Count; i++)
            {
                string pType = CSV[i][0];
                string userName = CSV[i][1];
                string cabinetType = CSV[i][2];
                string note = CSV[i][3];
                if (note.Length > NoteLength) throw new EntityParsingException("Invalid length", nameof(note), note, i);

                int cId, userId;

                int? id;
                using (var context = new InventoryDbEntities())
                {
                    //to cabinet id
                    if (!IdTranslater.GetCabinetTypeId(cabinetType, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "CabinetType", cabinetType, i);
                    }
                    cId = id ?? -1;
                    //username
                    if (!IdTranslater.GetUserId(userName, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "Username", userName, i);
                    }
                    userId = id ?? -1;
                }

                var per = new TransactionPermission()
                {
                    PermissionTypeName = pType,
                    CabinetTypeId = cId,
                    UserId = userId,
                    Note = note
                };
                entities.Add(per);
            }
            return entities;
        }
    }
}