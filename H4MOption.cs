using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace H4M
{
    public class H4MOption
    {
        [JsonProperty("reverse_mode")]
        public bool ReverseMode { get; set; }

        public H4MOption(bool reverseMode)
        {
            ReverseMode = reverseMode;
        }
    }
}
