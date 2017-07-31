using System;
using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Parser
{
    public class RawTransactions : IRaw<Transaction>
    {
        private const int NoteLength = 150;
        private const int CabinetNameLength = 50;
        private const int ItemCodeLength = 50;
        private const int UserNameLength = 50;
        public int UserId { get; private set; }

        public RawTransactions(List<string[]> csv, int userId)
        {
            CSV = csv;
            UserId = userId;
        }

        public List<string[]> CSV { get; }

        public int ColumnCount => 7;

        public List<Transaction> ToEntities()
        {
            var transactions = new List<Transaction>();
            string currentField = string.Empty;
            for (int i = 0; i < CSV.Count; i++)
            {
                if (CSV[i].Count() != ColumnCount) throw new EntityParsingException("Invalid column count", i);
                string itemCode = CSV[i][0];
                string fromCabinet = CSV[i][2];
                string toCabinet = CSV[i][3];
                string quanlity = CSV[i][1];
                string inputDate = CSV[i][4];
                string price = CSV[i][5];
                string note = CSV[i][6];

                //convert to data type
                string parseMessage = string.Empty;
                DateTime inputDatetime;
                int priceInt, quanlityInt;
                if (!EntityParser.ParseInt(quanlity, out quanlityInt, out parseMessage))
                    throw new EntityParsingException(parseMessage, nameof(quanlity), quanlity, i);
                if (!EntityParser.ParseInt(price, out priceInt, out parseMessage))
                    throw new EntityParsingException(parseMessage, nameof(price), price, i);
                if (!EntityParser.ParseDatetime(inputDate, out inputDatetime, out parseMessage))
                    throw new EntityParsingException(parseMessage, nameof(inputDate), inputDate, i);
                //check valid
                if (priceInt < 0) throw new EntityParsingException("Price must > 0");
                if (quanlityInt < 1) throw new EntityParsingException("Quanlity must > 0");

                //check name length
                if (itemCode.Length > ItemCodeLength) throw new EntityParsingException("Invalid length", nameof(itemCode), itemCode, i);
                if (fromCabinet.Length > CabinetNameLength) throw new EntityParsingException("Invalid length", "ItemName", nameof(fromCabinet), i);
                if (toCabinet.Length > CabinetNameLength) throw new EntityParsingException("Invalid length", "ItemName", nameof(toCabinet), i);

                //get ids
                int fromCabinetId, toCabinetId, itemId;
                int? id;
                using (var context = new InventoryDbEntities())
                {
                    //from cabinet id
                    if (!IdTranslater.GetCabinetId(fromCabinet, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "FromCabinet", fromCabinet, i);
                    }
                    fromCabinetId = id ?? -1;
                    //to cabinet id
                    if (!IdTranslater.GetCabinetId(toCabinet, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "ToCabinet", toCabinet, i);
                    }
                    toCabinetId = id ?? -1;
                    //item code
                    if (!IdTranslater.GetItemId(itemCode, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "ItemCode", itemCode, i);
                    }
                    itemId = id ?? -1;
                }
                var transaction = new Transaction()
                {
                    ItemId = itemId,
                    Quanlity = quanlityInt,
                    ProviderCabinetId = fromCabinetId,
                    ReceiverCabinetId = toCabinetId,
                    InputDate = inputDatetime,
                    TransactionDate = DateTime.Today,
                    Price = priceInt,
                    Note = note,
                    UserId = this.UserId
                };
                transactions.Add(transaction);
            }
            return transactions;
        }
    }
}