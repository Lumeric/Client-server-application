using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class ErrorReceivedEventArgs
    {
        #region Properties

        public ErrorType ErrorType { get; }

        public string Message { get; }

        public DateTime Date { get; }

        #endregion Properties

        #region Constructors

        public ErrorReceivedEventArgs(ErrorType errorType, string message, DateTime date)
        {
            ErrorType = errorType;
            Message = message;
            Date = date;
        }

        #endregion Constructors
    }
}
