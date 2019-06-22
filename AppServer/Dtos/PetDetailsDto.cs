namespace AppServer.Dtos
{
    using AppServer.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Pet Details Dto includes Gender and Collection of Pets
    /// </summary>
    public class PetDetailsDto
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender gender { get; set; }

        [JsonProperty(PropertyName = "pets")]
        public List<pets> petsCollection { get; set; }
    }
}
