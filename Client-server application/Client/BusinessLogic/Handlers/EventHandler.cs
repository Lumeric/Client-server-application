using Common.Network;
using Common.Network.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public class EventHandler : IEventLogHandler
    {
        #region Fields

        private readonly ITransport _transport;

        #endregion // Fields

        #region Constructors

        public EventHandler(ITransport transport)
        {
            _transport = transport;
        }

        #endregion // Constructors

        #region Methods

        public void SendFiltrationRequest(DateTime firstDate, DateTime secondDate, EventType eventType)
        {
            _transport.Send(new FiltrationRequest(firstDate, secondDate, eventType).GetContainer());
        }

        #endregion // Methods
    }
}
