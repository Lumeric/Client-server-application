using Common.Network;
using System;
using System.Xml.Serialization;

namespace Server
{
    [Serializable]
    [XmlRoot(ElementName = "configuration")]
    public class ServerSettings
    {
        #region Properties

        [XmlElement("transport")]
        public TransportTypes Transport { get; set; }

        [XmlElement("ip")]
        public string Ip { get; set; }

        [XmlElement("port")]
        public int Port { get; set; }

        [XmlElement("dbName")]
        public string DbName { get; set; }

        [XmlElement("connectionString")]
        public string ConnectionString { get; set; }

        [XmlElement("providerName")]
        public string ProviderName { get; set; }

        #endregion //Properties

    }
}
