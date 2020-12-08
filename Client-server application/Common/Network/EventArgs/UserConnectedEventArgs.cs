using System;

namespace Common.Network
{
    public class UserConnectedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public Guid ClientId { get; }

        #endregion //Properties

        #region Constructors

        public UserConnectedEventArgs(string clientName, Guid clientId)
        {
            ClientName = clientName;
            ClientId = clientId;
        }

        #endregion //Constructors
    }
}