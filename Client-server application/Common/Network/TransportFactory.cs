using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Network
{
    public static class TransportFactory 
    {
        public static ITransport Create(TransportType type)
        {
            switch (type)
            {
                case TransportType.WebSocket:
                    return new WsClient();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
                }
    }
}
