using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK.Models
{
    public partial class ResultReqSLIK
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("request_reff_id")]
        public long RequestReffId { get; set; }
    }

    public partial class Message
    {
        [JsonProperty("name")]
        public List<string> Name { get; set; }

        [JsonProperty("dob")]
        public List<string> DOB { get; set; }

        [JsonProperty("ktp")]
        public List<string> KTP { get; set; }

        [JsonProperty("email")]
        public List<string> Email { get; set; }
    }

    public partial class ResultReqSLIK
    {
        public static ResultReqSLIK FromJson(string json) => JsonConvert.DeserializeObject<ResultReqSLIK>(json, ResultReqSLIKConverter.Settings);
    }

    public static class ResultReqSLIKSerialize
    {
        public static string ToJson(this ResultReqSLIK self) => JsonConvert.SerializeObject(self, ResultReqSLIKConverter.Settings);
    }

    internal static class ResultReqSLIKConverter
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
}