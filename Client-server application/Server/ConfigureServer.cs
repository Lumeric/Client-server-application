using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ConfigureServer
    {
        #region Methods

        public ServerConfiguration ReadConfig(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string configurationFile = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<ServerConfiguration>(configurationFile);
            }
        }

        #endregion //Methods
    }
}
