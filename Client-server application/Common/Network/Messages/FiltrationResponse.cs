using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class FiltrationResponse
    {
        #region Properties

        public List<Message> FilteredLogs{ get; set; }

        #endregion // Properties

        #region Constructors

        public FiltrationResponse(List<Message> filteredLogs)
        {
            FilteredLogs = filteredLogs;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(FiltrationResponse),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
