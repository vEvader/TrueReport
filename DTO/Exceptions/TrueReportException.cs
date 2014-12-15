using System;

namespace Common.Exceptions
{
    public class TrueReportException : Exception
    {
        public TrueReportException(string message)
            : base(message)
        {
        }
    }
}
