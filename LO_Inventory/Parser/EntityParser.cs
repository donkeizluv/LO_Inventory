using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;

namespace LO_Inventory.Parser
{
    public class EntityParsingException : Exception
    {
        public int ErrorIndex { get; private set; } = -1;
        public string ValueName { get; private set; } = string.Empty;
        public string RawValue { get; private set; } = string.Empty;
        public EntityParsingException(string message) : base(message)
        {
        }

        public EntityParsingException(string message, int? row) : base(message)
        {
            ErrorIndex = row ?? -1;
        }

        public EntityParsingException(string message, string valueName, int? row) : base(message)
        {
            ValueName = valueName;
            ErrorIndex = row ?? -1;
        }

        public EntityParsingException(string message, string valueName, string rawData, int? row) : base(message)
        {
            ValueName = valueName;
            ErrorIndex = row ?? -1;
            RawValue = rawData;
        }
    }

    public class EntityParser
    {
        public int UserId { get; private set; }

        public EntityParser(int userId)
        {
            UserId = userId;
        }

        public List<Cabinet> ParseToCabinets(List<string[]> content)
        {
            var raw = new RawCabinets(content);
            return raw.ToEntities();
        }

        public List<CabinetType> ParseToCabinetTypes(List<string[]> content)
        {
            var raw = new RawCabinetType(content);
            return raw.ToEntities();
        }

        public List<Item> ParseToItems(List<string[]> content)
        {
            var raw = new RawItems(content);
            return raw.ToEntities();
        }

        public List<Transaction> ParseToTransactions(List<string[]> content)
        {
            var raw = new RawTransactions(content, UserId);
            return raw.ToEntities();
        }

        public List<Order> ParseToOrders(List<string[]> content)
        {
            var raw = new RawOrders(content, UserId);
            return raw.ToEntities();
        }

        public List<Provider> ParseToProviders(List<string[]> content)
        {
            var raw = new RawProvider(content);
            return raw.ToEntities();
        }

        public List<TransactionPermission> ParseToPermission(List<string[]> content)
        {
            var raw = new RawPermission(content);
            return raw.ToEntities();
        }

        private static bool CheckArray(List<string[]> content, out string message)
        {
            message = string.Empty;
            if (content.Count < 1) message = "Insert content is empty!";
            return message == string.Empty;
        }

        public static bool ParseInt(string s, out int result, out string message)
        {
            result = -1;
            message = string.Empty;
            try
            {
                result = int.Parse(s.Trim());
                return true;
            }
            catch (ArgumentException ex)
            {
                message = "Empty value.";
                return false;
            }
            catch (FormatException ex)
            {
                message = $"Cant convert {s} to number.";
                return false;
            }
            catch (OverflowException ex)
            {
                message = $"{s} is too big.";
                return false;
            }
        }

        public static bool ParseDatetime(string s, out DateTime dateTime, out string message)
        {
            message = string.Empty;
            if (DateTime.TryParseExact(FixDateText(s),
                @"dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateTime))
            {
                return true;
            }
            message = $"Cant convert {s} to datetime.";
            return false;
        }

        public static string FixDateText(string date)
        {
            string AddZero(string s)
            {
                if (s.Length < 2)
                {
                    return string.Format("{0}{1}", "0", s);
                }
                return s;
            }
            var split = date.Replace(@"//", @"/").Split('/');
            if (split.Count() != 3) return date; //invalid
            split[0] = AddZero(split[0]);
            split[1] = AddZero(split[1]);
            return string.Join("/", split);
        }

        public static IEnumerable<ValidationResult> TryDecodeDbUpdateException(DbUpdateException ex)
        {
            if (!(ex.InnerException is System.Data.Entity.Core.UpdateException) ||
                !(ex.InnerException.InnerException is System.Data.SqlClient.SqlException))
                return null;
            var sqlException =
                (System.Data.SqlClient.SqlException)ex.InnerException.InnerException;
            var result = new List<ValidationResult>();
            for (int i = 0; i < sqlException.Errors.Count; i++)
            {
                if (DbExceptionTranslater.ErrorCodeSkipList.Contains(sqlException.Errors[i].Number)) continue;
                string message = DbExceptionTranslater.Translate(sqlException.Errors[i]);
                result.Add(new ValidationResult(message));
            }
            return result.Any() ? result : null;
        }

        public static IEnumerable<ActionLog> ValidationResultToActionLog(string actionName, IEnumerable<ValidationResult> validations, bool error)
        {
            if (validations == null) return null;
            var actionLogs = new List<ActionLog>();
            foreach (var v in validations)
            {
                actionLogs.Add(new ActionLog(actionName, v.ErrorMessage, error));
            }
            return actionLogs;
        }
    }
}