using System;
using Newtonsoft.Json;

namespace IntakeQ.ApiClient.Helpers
{
    public class FromJavascripDateConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((double)reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = value as DateTime?;
            if (date == null) return;
            
            
            var epoch = date.Value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            serializer.Serialize(writer, epoch.ToString("N0"));
        }
    }
}