namespace AppServer.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;

    /// <summary>
    /// People details
    /// </summary>
    public class People
    {

        public People()
        {
            petsCollection = new List<pets>();
        }

        public string name { get; set; }
        public int age { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [JsonProperty(PropertyName = "pets")]
        public List<pets> petsCollection { get; set; }
    }
}
