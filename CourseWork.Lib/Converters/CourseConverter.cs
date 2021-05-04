using CourseWork.Lib.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Lib.Converters
{
    public class CourseConverter : JsonConverter<Course>
    {
        private static readonly Random random = new();

        public override Course ReadJson(JsonReader reader, Type objectType, Course existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (!hasExistingValue)
            {
                JObject mObj = JObject.Load(reader);
                var course = new Course
                {
                    ID = (int)mObj["id"],
                    Title = (string)mObj["title"],
                    Rating = (float)mObj["avg_rating"],
                    Hours = (int)mObj["num_lectures"] * random.Next(1, 4),
                    Url = "https://udemy.com" + (string)mObj["url"],
                    Description = (string)mObj["description"],
                    PriceDetail = mObj["price_detail"].Type == JTokenType.Null ? null : new PriceDetail
                    {
                        Amount = (int)mObj["price_detail"]["amount"],
                        Currency = (string)mObj["price_detail"]["currency"],
                        CurrencySymbol = (string)mObj["price_detail"]["currency_symbol"],
                        PriceString = (string)mObj["price_detail"]["price_string"],
                    }
                };
                return course;
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, Course value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
