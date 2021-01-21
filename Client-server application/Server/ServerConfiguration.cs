using Common.Network;
using System.Configuration;
using System.IO;
using System.Net;
using System.Xml.Serialization;

namespace Server
{
    public class ServerConfiguration
    {
        #region Properties

        public TransportTypes Transport { get; set; }

        public int Port { get; set; }

        public IPAddress Ip { get; set; }

        public ConnectionStringSettings ConnectionSettings { get; set; }

        #endregion //Properties

        #region Methods

        public ServerConfiguration(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ServerSettings));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                ServerSettings configuration = (ServerSettings)serializer.Deserialize(fileStream);

                Transport = configuration.Transport;
                Ip = IPAddress.Parse(configuration.Ip);
                Port = configuration.Port;
                ConnectionSettings = new ConnectionStringSettings(configuration.DbName, configuration.ConnectionString, configuration.ProviderName);
            }
        }

        #endregion //Methods
    }
}
