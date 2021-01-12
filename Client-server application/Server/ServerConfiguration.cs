using Common.Network;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerConfiguration
    {
        #region Properties

        [JsonProperty]
        public TransportType Protocol { get; set; }

        [JsonProperty]
        public int Port { get; set; }

        #endregion //Properties

    }
}
