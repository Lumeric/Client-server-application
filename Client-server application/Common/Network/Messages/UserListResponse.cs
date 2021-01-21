using System.Collections.Generic;

namespace Common.Network.Messages
{
    public class UserListResponse
    {
        #region Properties

        public List<string> UserList { get; set; }

        #endregion Properties

        #region Constructors

        public UserListResponse(List<string> userList)
        {
            UserList = userList;
        }

        #endregion // Constructors

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(UserListResponse),
                Payload = this
            };

            return container;
        }

        #endregion // Methods
    }
}
