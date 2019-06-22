namespace AppServer.Models
{

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Pet Details
    /// </summary>
    public class pets
    {
        public string name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Type type { get; set; }
    }
}
