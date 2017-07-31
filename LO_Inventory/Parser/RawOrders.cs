using System;
using System.Collections.Generic;
using System.Linq;

namespace LO_Inventory.Parser
{
    public class RawOrders : IRaw<Order>
    {
        public List<string[]> CSV { get; private set; }

        public int ColumnCount => 7;

        private const int ItemCodeLen = 50;
        private const int ProviderLen = 50;
        private const int CabinetNameLen = 50;
        private const int NoteLen = 150;
        public int UserId { get; private set; }

        public RawOrders(List<string[]> csv, int userId)
        {
            CSV = csv;
            UserId = userId;
        }

        public List<Order> ToEntities()
        {
            var orders = new List<Order>();
            for (int i = 0; i < CSV.Count; i++)
            {
                if (CSV[i].Count() != ColumnCount) throw new EntityParsingException("Invalid column count", i);
                string itemCode = CSV[i][0];
                string provider = CSV[i][3];
                string toCabinet = CSV[i][2];
                string quanlityStr = CSV[i][1];
                string priceStr = CSV[i][4];
                string orderDateStr = CSV[i][5];
                string note = CSV[i][6];
                //string userName = CSV[i][3];
                //check length
                if (itemCode.Length > ItemCodeLen) throw new EntityParsingException("Invalid length", nameof(itemCode), itemCode, i);
                if (provider.Length > ProviderLen) throw new EntityParsingException("Invalid length", nameof(provider), provider, i);
                if (toCabinet.Length > CabinetNameLen) throw new EntityParsingException("Invalid length", nameof(toCabinet), toCabinet, i);
                if (note.Length > NoteLen) throw new EntityParsingException("Invalid length", nameof(note), note, i);

                string parseMessage = string.Empty;
                int price, quanlity;
                DateTime orderDate;
                if (!EntityParser.ParseInt(quanlityStr, out quanlity, out parseMessage))
                    throw new EntityParsingException(parseMessage, nameof(quanlity), quanlityStr, i);
                if (!EntityParser.ParseInt(priceStr, out price, out parseMessage))
                    throw new EntityParsingException(parseMessage, nameof(price), priceStr, i);
                if (!EntityParser.ParseDatetime(orderDateStr, out orderDate, out parseMessage))
                    throw new EntityParsingException(parseMessage, nameof(orderDate), orderDateStr, i);

                //check valid
                if (price < 0) throw new EntityParsingException("Price must > 0");
                if (quanlity < 1) throw new EntityParsingException("Quanlity must > 0");

                //get ids
                int toCabinetId, providerId, itemId;
                int? id;
                using (var context = new InventoryDbEntities())
                {
                    //from cabinet id
                    if (!IdTranslater.GetCabinetId(toCabinet, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "ToCabinet", toCabinet, i);
                    }
                    toCabinetId = id ?? -1;
                    //to cabinet id
                    if (!IdTranslater.GetProviderId(provider, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "Provider", provider, i);
                    }
                    providerId = id ?? -1;
                    //item code
                    if (!IdTranslater.GetItemId(itemCode, out id, context))
                    {
                        throw new EntityParsingException("Name doesnt exist", "ItemCode", itemCode, i);
                    }
                    itemId = id ?? -1;
                }
                var order = new Order()
                {
                    ItemId = itemId,
                    Quanlity = quanlity,
                    Price = price,
                    CabinetId = toCabinetId,
                    ProviderId = providerId,
                    OrderDate = DateTime.Now,
                    InputDate = orderDate,
                    Note = note,
                    UserId = this.UserId
                };
                orders.Add(order);
            }
            return orders;
        }
    }
}