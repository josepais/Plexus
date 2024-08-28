using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiBonitaUIPath.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GisTestController : ControllerBase
    {
        static HttpClient _httpClient;

        public GisTestController()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://gisapim.iberostar.com/", UriKind.Absolute)
            };
        }

        // GET: api/<GisTestController>
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync([FromHeader(Name = "client_id")] string client_id, [FromHeader(Name = "client_secret")] string client_secret, [FromHeader(Name = "IBH-Apim-Subscription-Key")] string subscriptionKey)
        {
            if (string.IsNullOrEmpty(client_id))
            {
                throw new System.ArgumentException($"'{nameof(client_id)}' cannot be null or empty.", nameof(client_id));
            }

            if (string.IsNullOrEmpty(client_secret))
            {
                throw new System.ArgumentException($"'{nameof(client_secret)}' cannot be null or empty.", nameof(client_secret));
            }

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                throw new System.ArgumentException($"'{nameof(subscriptionKey)}' cannot be null or empty.", nameof(subscriptionKey));
            }

            using HttpRequestMessage requestMessage = new(HttpMethod.Get, new Uri("login", UriKind.Relative));
            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Headers.Add("client_id", client_id);
            requestMessage.Headers.Add("client_secret", client_secret);
            requestMessage.Headers.Add("IBH-Apim-Subscription-Key", subscriptionKey);

            using HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();

            return Ok(await responseMessage.Content.ReadAsStringAsync());
        }

        // POST api/<GisTestController>
        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] string body, [FromHeader(Name = "IBH-Apim-Subscription-Key")] string subscriptionKey, [FromHeader(Name = "Token")] string authorization)
        {
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentException($"'{nameof(body)}' cannot be null or empty.", nameof(body));
            }

            if (string.IsNullOrEmpty(subscriptionKey))
            {
                throw new ArgumentException($"'{nameof(subscriptionKey)}' cannot be null or empty.", nameof(subscriptionKey));
            }

            if (string.IsNullOrEmpty(authorization))
            {
                throw new ArgumentException($"'{nameof(authorization)}' cannot be null or empty.", nameof(authorization));
            }

            using HttpRequestMessage requestMessage = new(HttpMethod.Post, new Uri("waa/intraStayGuestInfo", UriKind.Relative));
            requestMessage.Headers.Add("IBH-Apim-Subscription-Key", subscriptionKey);
            requestMessage.Headers.Add("Authorization", authorization);
            requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/soap+xml");

            using HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();

            return Ok(await responseMessage.Content.ReadAsStringAsync());
        }
    }
}
