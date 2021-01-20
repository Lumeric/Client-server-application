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

        public string Message { get; }

        public DateTime Date { get; }

        #endregion Properties

        #region Constructors

        public ErrorReceivedEventArgs(string message, DateTime date)
        {
            Message = message;
            Date = date;
        }

        #endregion Constructors
    }
}
