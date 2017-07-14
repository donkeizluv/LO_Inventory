using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace LO_Inventory
{
    public static class DbExceptionTranslater
    {
        //3621: statement rollback
        public static List<int> ErrorCodeSkipList = new List<int>() { 3621 };

        private static readonly Regex ParenthesesContentRegex = new Regex(@"\((.*?)\)");
        private static readonly Regex QuoteContentRegex = new Regex(@"\'(.*?)\'");

        public static string Translate(SqlError ex)
        {
            string message = string.Empty;
            switch (ex.Number)
            {
                case 515:
                    message = $"Value cannot be NULL: {GetQuoteContent(ex.Message)}";
                    break;
                case 2627:
                    message = $"Value: {GetParentheseContent(ex.Message)} is already exist.";
                    break;
                case 50000: //trigger raised exception
                    message = ex.Message;
                    break;
                default:
                    message = $"Unknown error code:{ex.Number}";
                    break;
            }
            return message;
        }

        private static string GetParentheseContent(string message)
        {
            var match = ParenthesesContentRegex.Match(message);
            if (!match.Success) return "''";
            return match.Value.Replace("(", "'").Replace(")", "'");
        }
        private static string GetQuoteContent(string message)
        {
            var match = QuoteContentRegex.Match(message);
            if (!match.Success) return "''";
            return match.Value.Replace("(", "'").Replace(")", "'");
        }
    }
}