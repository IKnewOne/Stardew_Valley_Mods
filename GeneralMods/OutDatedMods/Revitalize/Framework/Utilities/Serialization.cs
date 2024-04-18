using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Omegasis.Revitalize.Framework.Constants.PathConstants;
using Omegasis.StardustCore.Compatibility.SpaceCore;
using StardewValley;
using StardewValley.Objects;

namespace Omegasis.Revitalize.Framework.Utilities
{
    /// <summary>
    /// Handles serialization of all objects in existence.
    /// </summary>
    public static class Serializer
    {

        /// <summary>
        /// Automatically serialize all mod classes that have the XMLAttribute tag on them.
        /// </summary>
        public static void SerializeTypesForXMLUsingSpaceCore()
        {
            SpaceCoreAPIUtil.RegisterTypesForMod(RevitalizeModCore.Instance);
        }


    }
}
