using System;

namespace LO_Inventory.Log
{
    public class NewLogEventArgs : EventArgs
    {
        public NewLogEventArgs(string log)
        {
            Log = log;
        }

        public string Log { get; private set; }
    }
}