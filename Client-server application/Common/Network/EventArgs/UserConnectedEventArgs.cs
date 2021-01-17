using System;

namespace Common.Network
{
    public class UserConnectedEventArgs
    {
        #region Properties

        public string Username { get; }

        public Guid UserId { get; }

        #endregion //Properties

        #region Constructors

        public UserConnectedEventArgs(string username, Guid userId)
        {
            Username = username;
            UserId = userId;
        }

        #endregion //Constructors
    }
}