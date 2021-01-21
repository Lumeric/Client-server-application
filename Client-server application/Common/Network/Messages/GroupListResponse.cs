using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network.Messages
{
    public class GroupListResponse
    {
        #region // Properties

        public Dictionary<string, List<string>> Groups { get; set; }

        #endregion Properties

        #region Constructors

        public GroupListResponse(Dictionary<string, List<string>> groups)
        {
            Groups = groups;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(GroupListResponse),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
