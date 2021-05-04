using CourseWork.Lib.Converters;
using CourseWork.Lib.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CourseWork.Data.Scrapers
{
    class UdemyScraper
    {
        private static RestClient MakeClient()
        {
            string id = "BsWmx0Qrxq4xFurhoVcMY9tD9BJDXALOWRAEhf1h";
            string secret = "qQqnGPcd4h3dgIWL6KV4e1P7wwHg6hBqiirSpPZjPRfYwU8eum3UoCeHBpf1yobQTCk7RgRKJk3t1bRNUWbCzAVpoK3MIIF8a2q4yPRjsKB6aVK3ntj10Olgc3jpDM6A";
            var client = new RestClient("https://www.udemy.com/api-2.0/");
            client.UseNewtonsoftJson();
            client.ConfigureWebRequest(configurator =>
            {
                configurator.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{id}:{secret}"))}");
            });
            return client;
        }

        private readonly RestClient client;

        public UdemyScraper() => client = MakeClient();

        public async IAsyncEnumerable<IEnumerable<Course>> GetCoursesAsync()
        {
            var nextUrl = "courses?language=ru&fields[course]=title,price_detail,num_lectures,avg_rating,url,description";
            while (nextUrl != null)
            {
                var request = new RestRequest(nextUrl);
                var data = await client.GetAsync<JObject>(request);

                var items = (JArray)data["results"];
                nextUrl = (string)data["next"];

                yield return items.Select(item => JsonConvert.DeserializeObject<Course>(item.ToString(), new CourseConverter()));
            }
        }
    }
}
