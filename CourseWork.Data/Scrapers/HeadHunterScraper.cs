using CourseWork.Lib.Entities;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Data.Scrapers
{
    class HeadHunterScraper
    {
        private static RestClient MakeClient()
        {
            var client = new RestClient("https://api.hh.ru/");
            client.UseNewtonsoftJson();
            return client;
        }

        private readonly RestClient client;

        public HeadHunterScraper() => client = MakeClient();

        public async Task<IEnumerable<Area>> GetAreasAsync()
        {
            var areas = new List<Area>();

            var iRequest = new RestRequest("industries");
            var industries = await client.GetAsync<JArray>(iRequest);

            foreach (JObject jArea in industries)
            {
                var areaID = Guid.NewGuid().ToString();
                var area = new Area
                {
                    ID = areaID,
                    Name = (string)jArea["name"],
                    Type = AreaType.Industry
                };
                foreach (JObject spec in jArea["industries"])
                {
                    area.Specializations.Add(new Specialization
                    {
                        AreaID = areaID,
                        Name = (string)spec["name"]
                    });
                }
                areas.Add(area);
            }

            var pRequest = new RestRequest("specializations");
            var professionals = await client.GetAsync<JArray>(pRequest);
            foreach (JObject jArea in professionals)
            {
                var areaID = Guid.NewGuid().ToString();
                var area = new Area
                {
                    ID = areaID,
                    Name = (string)jArea["name"],
                    Type = AreaType.Professional
                };
                foreach (JObject spec in jArea["specializations"])
                {
                    area.Specializations.Add(new Specialization
                    {
                        AreaID = areaID,
                        Name = (string)spec["name"],
                        Laboring = (bool)spec["laboring"]
                    });
                }
                areas.Add(area);
            }

            return areas;
        }
    }
}
