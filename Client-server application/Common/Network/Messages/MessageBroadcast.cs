using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class MessageBroadcast
    {
        #region Properties

        public string Source { get; set; }

        public string Target { get; set; }

        public string Message { get; set; }

        public string Groupname { get; set; }

        public DateTime Date { get; set; }

        #endregion // Properties

        #region Constructors

        public MessageBroadcast(string source, string target, string message, string groupname, DateTime date)
        {
            Source = source;
            Target = target;
            Message = message;
            Groupname = groupname;
            Date = date;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageBroadcast),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
