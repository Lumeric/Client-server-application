using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public class WsServer
    {
        #region Fields

        private IPEndPoint iPEndPoint;

        #endregion Fields

        #region Events
        #endregion Events

        #region Constructors

        public WsServer(IPEndPoint iPEndPoint)
        {
            this.iPEndPoint = iPEndPoint;
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void Send(string message)
        {

        }

        internal void HandleMessage()
        {

        }

        internal void AddConnection(WsConnection connection)
        {

        }

        internal void RemoveConnection(Guid connectionId)
        {

        }

        #endregion Methods


    }
}
