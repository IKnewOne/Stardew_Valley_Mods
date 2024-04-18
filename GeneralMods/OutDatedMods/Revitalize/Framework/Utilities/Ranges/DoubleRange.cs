using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netcode;
using Newtonsoft.Json;
using Omegasis.StardustCore.Networking;
using StardewValley;

namespace Omegasis.Revitalize.Framework.Utilities.Ranges
{
    public class DoubleRange:NetObject
    {
        [JsonIgnore]
        public readonly NetDouble min = new NetDouble();
        [JsonIgnore]
        public readonly NetDouble max = new NetDouble();

        [JsonProperty("min")]
        public double Min { get => this.min.Value; set => this.min.Value = value; }
        [JsonProperty("max")]
        public double Max { get => this.max.Value; set => this.max.Value = value; }

        public DoubleRange() { }

        public DoubleRange(double min, double max)
        {
            this.Min = min;
            this.Max= max;
        }

        protected override void initializeNetFields()
        {
            base.initializeNetFields();
            this.NetFields.AddFields(this.min, this.max);
        }

        /// <summary>
        /// Checks to see if the value is inside the range.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool containsInclusive(double value)
        {
            if (value >= this.Min && value <= this.Max) return true;
            else return false;
        }

        /// <summary>
        /// Checks to see if the value is inside the range.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool containsExclusive(double value)
        {
            if (value >= this.Min && value < this.Max) return true;
            else return false;
        }
    }
}
