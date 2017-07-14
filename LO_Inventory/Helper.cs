using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace LO_Inventory
{
    public static class HelperMethods
    {
        //entity list to table
        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            var dtReturn = new DataTable();
            // column names
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (var pi in oProps)
                    {
                        var colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                var dr = dtReturn.NewRow();
                foreach (var pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

        public static void DataGridViewToClipboard(DataGridView grid)
        {
            var newline = Environment.NewLine;
            var tab = "\t";
            var clipboard_string = new StringBuilder();
            bool header = false;
            foreach (DataGridViewRow row in grid.Rows)
            {
                //insert header
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (header) break;
                    if (i == (row.Cells.Count - 1))
                        clipboard_string.Append(row.Cells[i].OwningColumn.HeaderText + newline);
                    else
                        clipboard_string.Append(row.Cells[i].OwningColumn.HeaderText + tab);
                }
                header = true;
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (i == (row.Cells.Count - 1))
                        clipboard_string.Append(row.Cells[i].Value + newline);
                    else
                        clipboard_string.Append(row.Cells[i].Value + tab);
                }
            }
            var content = clipboard_string.ToString();
            if (string.IsNullOrEmpty(content)) return;
            Clipboard.SetText(clipboard_string.ToString());
        }

        public static bool ShowCSVFileBrowser(IWin32Window owner, out string path)
        {
            var browser = new OpenFileDialog()
            {
                Filter = "CSV File|*.csv"
            };
            browser.ShowDialog(owner);
            path = browser.FileName;
            if (!string.IsNullOrEmpty(path))
                return true;
            return false;
        }

        public static List<string[]> ReadCSV(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream = new StreamReader(fs, true);
            //var streamReader = new StreamReader(fs, true);
            var detectedDelimiter = DetectDelimiter(new StreamReader(fs, true), 1, new List<char>() { '\t', ';' });
            if (detectedDelimiter == '\0') throw new ArgumentException("Cant detect CSV delimiter.");

            //reset position
            fs.Seek(0, SeekOrigin.Begin);

            var csv = new CsvReader(stream,
                new CsvHelper.Configuration.CsvConfiguration() { Delimiter = detectedDelimiter.ToString() });
            var content = new List<string[]>();
            while (csv.Read())
            {
                content.Add(csv.CurrentRecord);
            }
            //foreach (string[] line in content)
            //{
            //    Debug.Print(string.Join("", line));
            //}
            return content;
        }

        public static char DetectDelimiter(TextReader reader, int rowCount, IList<char> allowSeparators)
        {
            IList<int> separatorsCount = new int[allowSeparators.Count];

            int character;

            int row = 0;

            bool quoted = false;
            bool firstChar = true;

            while (row < rowCount)
            {
                character = reader.Read();

                switch (character)
                {
                    case '"':
                        if (quoted)
                        {
                            if (reader.Peek() != '"') // Value is quoted and
                                                      // current character is " and next character is not ".
                                quoted = false;
                            else
                                reader.Read(); // Value is quoted and current and
                                               // next characters are "" - read (skip) peeked qoute.
                        }
                        else
                        {
                            if (firstChar)  // Set value as quoted only if this quote is the
                                            // first char in the value.
                                quoted = true;
                        }
                        break;

                    case '\n':
                        if (!quoted)
                        {
                            ++row;
                            firstChar = true;
                            continue;
                        }
                        break;

                    case -1:
                        row = rowCount;
                        break;

                    default:
                        if (!quoted)
                        {
                            int index = allowSeparators.IndexOf((char)character);
                            if (index != -1)
                            {
                                ++separatorsCount[index];
                                firstChar = true;
                                continue;
                            }
                        }
                        break;
                }

                if (firstChar)
                    firstChar = false;
            }

            int maxCount = separatorsCount.Max();

            return maxCount == 0 ? '\0' : allowSeparators[separatorsCount.IndexOf(maxCount)];
        }

        public static T ExecuteDbRequest<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (EntityException ex) when (ex.InnerException != null && ex.InnerException.Message.Contains("The underlying provider failed on Open."))
            {
                MessageBox.Show("Fail to connect to the database.", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }
            catch (EntityException ex) when (ex.Message.Contains("The underlying provider failed on Open."))
            {
                MessageBox.Show("Fail to connect to the database.", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }
        }

        public static bool ExecuteDbRequest(Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (EntityException ex) when (ex.Message.Contains("The underlying provider failed on Open."))
            {
                MessageBox.Show("Fail to connect to the database.", "Login Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}