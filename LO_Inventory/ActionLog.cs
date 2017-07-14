namespace LO_Inventory
{
    public class ActionLog
    {
        public string Action { get; private set; }
        public string Message { get; private set; }
        public bool Error { get; private set; }
        public ActionLog()
        {

        }
        public ActionLog(string action, string message)
        {
            Action = action;
            Message = message;
            Error = false;
        }

        public ActionLog(string action, string message, bool error = true)
        {
            Action = action;
            Message = message;
            Error = error;
        }
    }
}