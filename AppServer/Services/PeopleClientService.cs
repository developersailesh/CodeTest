namespace AppServer.HttpClientFactory
{

    using AppServer.Helpers;
    using AppServer.Interfaces;
    using AppServer.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Service to consume People Data
    /// </summary>
    public class PeopleClientService : IPeopleClientService
    {
        public HttpClient _httpClient { get; }
        private readonly IConfiguration _configuration;

        public PeopleClientService(HttpClient httpClient,IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_configuration["AppSettings:ApiBaseUrl"]);
            _httpClient.Timeout = new TimeSpan(0, 0, 30);
        }

        /// <summary>
        /// HttpClient that consumes People.json and also supports Cancellation of request
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<People>> GetPeopleData(CancellationToken cancellationToken)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "people.json");

            try
            {
                var response = await _httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead);
                if(!response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return null;
                    }
                    else if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return null;
                    }
                    response.EnsureSuccessStatusCode();
                }


                //Reading Data with out Stream
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<People>>(content);

            }
        }

    }
    
}
