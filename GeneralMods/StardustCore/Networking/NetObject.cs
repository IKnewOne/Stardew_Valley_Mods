using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Netcode;
using Newtonsoft.Json;

namespace Omegasis.StardustCore.Networking
{
    /// <summary>
    /// Class used to make other classes be able to be serialized over the net.
    /// </summary>
    public class NetObject : INetObject<NetFields>
    {
        [XmlIgnore]
        [JsonIgnore]
        public NetFields NetFields { get; } = new NetFields("Omegasis.StardustCore.Networking.NetObject");

        protected virtual void initializeNetFields()
        {

        }
    }
}
