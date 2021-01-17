using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class FilteredLogsReceivedEventArgs
    {
        #region Properties

        public List<Message> FilteredLogs { get; }

        #endregion //Properties

        #region Constructors

        public FilteredLogsReceivedEventArgs(List<Message> filteredLogs)
        {
            FilteredLogs = filteredLogs;
        }

        #endregion //Constructors
    }
}
