using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class FiltrationRequest
    {
        #region Properties

        public DateTime FirstDate { get; set; }
        public DateTime SecondDate { get; set; }

        public EventType EventType { get; set; }

        #endregion // Properties

        #region Constructors

        public FiltrationRequest(DateTime firstDate, DateTime secondDate, EventType eventType)
        {
            FirstDate = firstDate;
            SecondDate = secondDate;
            EventType = eventType;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(FiltrationRequest),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
