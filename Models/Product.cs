using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace buildxact_supplies.Model
{
    public class Product
    {
        public string ID { get; set; }
        [JsonProperty("description")]
        public string name { get; set; }
        [JsonProperty("priceInCents")]
        public decimal price { get; set; }
        public bool isAUD { get; set; }
    }
}
