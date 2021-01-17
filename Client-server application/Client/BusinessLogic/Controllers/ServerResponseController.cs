using Common.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.BusinessLogic
{
    public class ServerResponseController : IServerResponseController
    {
        #region Events

        public event EventHandler<UserConnectedEventArgs> ClientConnected;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion //Events

        #region Fields



        #endregion //Fields

        #region Properties



        #endregion //Properties

        #region Constructors



        #endregion //Constructors

        #region Methods



        #endregion //Methods
    }
}
