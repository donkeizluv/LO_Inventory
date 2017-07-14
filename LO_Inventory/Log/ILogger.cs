using System;

namespace LO_Inventory.Log
{
    public interface ILogger
    {
        Type ClassType { get; }

        void Log(string log);

        event OnNewLogHandler OnNewLog;
    }
}