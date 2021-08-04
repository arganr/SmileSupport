using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK.Models
{
    

    public partial class ResultCompanySLIK
    {
        [JsonProperty("ResultFlag")]
        public bool ResultFlag { get; set; }

        [JsonProperty("ResultIdeb")]
        public List<ResultIdeb_Company> ResultIdeb { get; set; }

        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }

    public partial class ResultIdeb_Company
    {
        [JsonProperty("nik_npwp")]
        public string NikNpwp { get; set; }

        [JsonProperty("Ideb")]
        public Ideb Ideb { get; set; }
    }

    public partial class Ideb
    {
        [JsonProperty("IdebPerusahaan")]
        public IdebPerusahaan IdebPerusahaan { get; set; }
    }

    public partial class IdebPerusahaan
    {
        [JsonProperty("header")]
        public Header_Company Header { get; set; }

        [JsonProperty("perusahaan")]
        public Perusahaan Perusahaan { get; set; }
    }

    public partial class Header_Company
    {
        [JsonProperty("kodeReferensiPengguna")]
        public string KodeReferensiPengguna { get; set; }

        [JsonProperty("tanggalHasil")]
        public string TanggalHasil { get; set; }

        [JsonProperty("idPermintaan")]
        public string IdPermintaan { get; set; }

        [JsonProperty("idPenggunaPermintaan")]
        public string IdPenggunaPermintaan { get; set; }

        [JsonProperty("dibuatOleh")]
        public string DibuatOleh { get; set; }

        [JsonProperty("kodeLJKPermintaan")]
        public string KodeLjkPermintaan { get; set; }

        [JsonProperty("kodeCabangPermintaan")]
        public string KodeCabangPermintaan { get; set; }

        [JsonProperty("kodeTujuanPermintaan")]
        public string KodeTujuanPermintaan { get; set; }

        [JsonProperty("tanggalPermintaan")]
        public string TanggalPermintaan { get; set; }

        [JsonProperty("totalBagian")]
        public string TotalBagian { get; set; }

        [JsonProperty("nomorBagian")]
        public string NomorBagian { get; set; }
    }

    public partial class Perusahaan
    {
        [JsonProperty("nomorLaporan")]
        public string NomorLaporan { get; set; }

        [JsonProperty("posisiDataTerakhir")]
        public string PosisiDataTerakhir { get; set; }

        [JsonProperty("tanggalPermintaan")]
        public string TanggalPermintaan { get; set; }

        [JsonProperty("parameterPencarian")]
        public ParameterPencarian_Company ParameterPencarian { get; set; }

        [JsonProperty("dataPokokDebitur")]
        public List<dynamic> DataPokokDebitur { get; set; }

        [JsonProperty("ringkasanFasilitas")]
        public RingkasanFasilitas_Company RingkasanFasilitas { get; set; }

        [JsonProperty("fasilitas")]
        public Fasilitas_Company Fasilitas { get; set; }
    }
    

    public partial class Fasilitas_Company
    {
        [JsonProperty("kreditPembiayan")]
        public List<dynamic> KreditPembiayan { get; set; }
    }
    
    public partial class ParameterPencarian_Company
    {
        [JsonProperty("namaBadanUsaha")]
        public string NamaBadanUsaha { get; set; }

        [JsonProperty("npwp")]
        public string Npwp { get; set; }

        [JsonProperty("tempatPendirian")]
        public string TempatPendirian { get; set; }

        [JsonProperty("tanggalAktaPendirian")]
        public string TanggalAktaPendirian { get; set; }

        [JsonProperty("nomorAktaPendirian")]
        public string NomorAktaPendirian { get; set; }
    }

    public partial class RingkasanFasilitas_Company
    {
        [JsonProperty("plafonEfektifKreditPembiayaan")]
        public string PlafonEfektifKreditPembiayaan { get; set; }

        [JsonProperty("plafonEfektifLc")]
        public string PlafonEfektifLc { get; set; }

        [JsonProperty("plafonEfektifGyd")]
        public string PlafonEfektifGyd { get; set; }

        [JsonProperty("plafonEfektifSec")]
        public string PlafonEfektifSec { get; set; }

        [JsonProperty("plafonEfektifLain")]
        public string PlafonEfektifLain { get; set; }

        [JsonProperty("plafonEfektifTotal")]
        public string PlafonEfektifTotal { get; set; }

        [JsonProperty("bakiDebetKreditPembiayaan")]
        public string BakiDebetKreditPembiayaan { get; set; }

        [JsonProperty("bakiDebetLc")]
        public string BakiDebetLc { get; set; }

        [JsonProperty("bakiDebetGyd")]
        public string BakiDebetGyd { get; set; }

        [JsonProperty("bakiDebetSec")]
        public string BakiDebetSec { get; set; }

        [JsonProperty("bakiDebetLain")]
        public string BakiDebetLain { get; set; }

        [JsonProperty("bakiDebetTotal")]
        public string BakiDebetTotal { get; set; }

        [JsonProperty("krediturBankUmum")]
        public string KrediturBankUmum { get; set; }

        [JsonProperty("krediturLp")]
        public string KrediturLp { get; set; }

        [JsonProperty("krediturLainnya")]
        public string KrediturLainnya { get; set; }

        [JsonProperty("kualitasTerburuk")]
        public string KualitasTerburuk { get; set; }

        [JsonProperty("kualitasBulanDataTerburuk")]
        public string KualitasBulanDataTerburuk { get; set; }
    }

    public partial class ResultCompanySLIK
    {
        public static ResultCompanySLIK FromJson(string json) => JsonConvert.DeserializeObject<ResultCompanySLIK>(json, ResultCompanySLIKConverter.Settings);
    }

    public static class ResultCompanySLIKSerialize
    {
        public static string ToJson(this ResultCompanySLIK self) => JsonConvert.SerializeObject(self, ResultCompanySLIKConverter.Settings);
    }

    internal static class ResultCompanySLIKConverter
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
