using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK.Models
{
    public partial class ModelReqSLIK
    {
        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dob")]
        public string Dob { get; set; }

        [JsonProperty("ktp")]
        public string Ktp { get; set; }

        [JsonProperty("npwp")]
        public string Npwp { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("pob")]
        public string Pob { get; set; }

        [JsonProperty("mmn")]
        public string Mmn { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("custtype")]
        public string Custtype { get; set; }

        [JsonProperty("tenant")]
        public string Tenant { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public partial class ModelReqSLIK
    {
        public static ModelReqSLIK FromJson(string json) => JsonConvert.DeserializeObject<ModelReqSLIK>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this ModelReqSLIK self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}