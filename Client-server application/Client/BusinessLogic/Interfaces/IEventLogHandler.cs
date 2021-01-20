using Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public interface IEventLogHandler
    {
        #region Methods

        void SendFiltrationRequest(DateTime firstDate, DateTime secondDate, EventType eventType);

        #endregion // Methods
    }
}
