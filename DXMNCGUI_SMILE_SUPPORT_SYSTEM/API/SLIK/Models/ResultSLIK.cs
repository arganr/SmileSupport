using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK.Models
{
    public partial class ResultSLIK
    {
        [JsonProperty("ResultFlag")]
        public bool ResultFlag { get; set; }

        [JsonProperty("ResultIdeb")]
        public List<ResultIdeb> ResultIdeb { get; set; }

        [JsonProperty("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }

    public partial class ResultIdeb
    {
        [JsonProperty("nik_npwp")]
        public string NikNpwp { get; set; }

        [JsonProperty("Ideb")]
        public Ideb Ideb { get; set; }
    }

    public partial class Ideb
    {
        [JsonProperty("IdebIndividu")]
        public IdebIndividu IdebIndividu { get; set; }
    }

    public partial class IdebIndividu
    {
        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("individual")]
        public Individual Individual { get; set; }
    }

    public partial class Header
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

    public partial class Individual
    {
        [JsonProperty("nomorLaporan")]
        public string NomorLaporan { get; set; }

        [JsonProperty("posisiDataTerakhir")]
        public string PosisiDataTerakhir { get; set; }

        [JsonProperty("tanggalPermintaan")]
        public string TanggalPermintaan { get; set; }

        [JsonProperty("parameterPencarian")]
        public ParameterPencarian ParameterPencarian { get; set; }

        [JsonProperty("dataPokokDebitur")]
        public List<DataPokokDebitur> DataPokokDebitur { get; set; }

        [JsonProperty("ringkasanFasilitas")]
        public RingkasanFasilitas RingkasanFasilitas { get; set; }

        [JsonProperty("fasilitas")]
        public Fasilitas Fasilitas { get; set; }
    }

    public partial class DataPokokDebitur
    {
        [JsonProperty("namaDebitur")]
        public string NamaDebitur { get; set; }

        [JsonProperty("identitas")]
        public string Identitas { get; set; }

        [JsonProperty("noIdentitas")]
        public string NoIdentitas { get; set; }

        [JsonProperty("alamat")]
        public string Alamat { get; set; }

        [JsonProperty("jenisKelamin")]
        public string JenisKelamin { get; set; }

        [JsonProperty("jenisKelaminKet")]
        public string JenisKelaminKet { get; set; }

        [JsonProperty("npwp")]
        public string Npwp { get; set; }

        [JsonProperty("tempatLahir")]
        public string TempatLahir { get; set; }

        [JsonProperty("tanggalLahir")]
        public string TanggalLahir { get; set; }

        [JsonProperty("pelapor")]
        public string Pelapor { get; set; }

        [JsonProperty("pelaporKet")]
        public string PelaporKet { get; set; }

        [JsonProperty("tanggalDibentuk")]
        public string TanggalDibentuk { get; set; }

        [JsonProperty("tanggalUpdate")]
        public string TanggalUpdate { get; set; }

        [JsonProperty("kelurahan")]
        public string Kelurahan { get; set; }

        [JsonProperty("kecamatan")]
        public string Kecamatan { get; set; }

        [JsonProperty("kabKota")]
        public string KabKota { get; set; }

        [JsonProperty("kabKotaKet")]
        public string KabKotaKet { get; set; }

        [JsonProperty("kodePos")]
        public string KodePos { get; set; }

        [JsonProperty("negara")]
        public string Negara { get; set; }

        [JsonProperty("negaraKet")]
        public string NegaraKet { get; set; }

        [JsonProperty("pekerjaan")]
        public string Pekerjaan { get; set; }

        [JsonProperty("pekerjaanKet")]
        public string PekerjaanKet { get; set; }

        [JsonProperty("tempatBekerja")]
        public string TempatBekerja { get; set; }

        [JsonProperty("bidangUsaha")]
        public string BidangUsaha { get; set; }

        [JsonProperty("bidangUsahaKet")]
        public string BidangUsahaKet { get; set; }

        [JsonProperty("kodeGelarDebitur")]
        public string KodeGelarDebitur { get; set; }

        [JsonProperty("statusGelarDebitur")]
        public string StatusGelarDebitur { get; set; }
    }

    public partial class Fasilitas
    {
        [JsonProperty("kreditPembiayan")]
        //public List<KreditPembiayan> KreditPembiayan { get; set; }
        public List<dynamic> KreditPembiayan { get; set; }
        
    }

    //public partial class KreditPembiayan
    //{
    //    [JsonProperty("ljk")]
    //    public string Ljk { get; set; }

    //    [JsonProperty("ljkKet")]
    //    public string LjkKet { get; set; }

    //    [JsonProperty("cabang")]
    //    public string Cabang { get; set; }

    //    [JsonProperty("cabangKet")]
    //    public string CabangKet { get; set; }

    //    [JsonProperty("bakiDebet")]
    //    public string BakiDebet { get; set; }

    //    [JsonProperty("tanggalDibentuk")]
    //    public string TanggalDibentuk { get; set; }

    //    [JsonProperty("tanggalUpdate")]
    //    public string TanggalUpdate { get; set; }

    //    [JsonProperty("bulan")]
    //    public string Bulan { get; set; }

    //    [JsonProperty("tahun")]
    //    public string Tahun { get; set; }

    //    [JsonProperty("sifatKreditPembiayaan")]
    //    public string SifatKreditPembiayaan { get; set; }

    //    [JsonProperty("sifatKreditPembiayaanKet")]
    //    public string SifatKreditPembiayaanKet { get; set; }

    //    [JsonProperty("jenisKreditPembiayaan")]
    //    public string JenisKreditPembiayaan { get; set; }

    //    [JsonProperty("jenisKreditPembiayaanKet")]
    //    public string JenisKreditPembiayaanKet { get; set; }

    //    [JsonProperty("akadKreditPembiayaan")]
    //    public string AkadKreditPembiayaan { get; set; }

    //    [JsonProperty("akadKreditPembiayaanKet")]
    //    public string AkadKreditPembiayaanKet { get; set; }

    //    [JsonProperty("noRekening")]
    //    public string NoRekening { get; set; }

    //    [JsonProperty("frekPerpjganKreditPembiayaan")]
    //    public string FrekPerpjganKreditPembiayaan { get; set; }

    //    [JsonProperty("noAkadAwal")]
    //    public string NoAkadAwal { get; set; }

    //    [JsonProperty("tanggalAkadAwal")]
    //    public string TanggalAkadAwal { get; set; }

    //    [JsonProperty("noAkadAkhir")]
    //    public string NoAkadAkhir { get; set; }

    //    [JsonProperty("tanggalAkadAkhir")]
    //    public string TanggalAkadAkhir { get; set; }

    //    [JsonProperty("tanggalAwalKredit")]
    //    public string TanggalAwalKredit { get; set; }

    //    [JsonProperty("tanggalMulai")]
    //    public string TanggalMulai { get; set; }

    //    [JsonProperty("tanggalJatuhTempo")]
    //    public string TanggalJatuhTempo { get; set; }

    //    [JsonProperty("kategoriDebiturKode")]
    //    public string KategoriDebiturKode { get; set; }

    //    [JsonProperty("kategoriDebiturKet")]
    //    public string KategoriDebiturKet { get; set; }

    //    [JsonProperty("jenisPenggunaan")]
    //    public string JenisPenggunaan { get; set; }

    //    [JsonProperty("jenisPenggunaanKet")]
    //    public string JenisPenggunaanKet { get; set; }

    //    [JsonProperty("sektorEkonomi")]
    //    public string SektorEkonomi { get; set; }

    //    [JsonProperty("sektorEkonomiKet")]
    //    public string SektorEkonomiKet { get; set; }

    //    [JsonProperty("kreditProgramPemerintah")]
    //    public string KreditProgramPemerintah { get; set; }

    //    [JsonProperty("kreditProgramPemerintahKet")]
    //    public string KreditProgramPemerintahKet { get; set; }

    //    [JsonProperty("lokasiProyek")]
    //    public string LokasiProyek { get; set; }

    //    [JsonProperty("lokasiProyekKet")]
    //    public string LokasiProyekKet { get; set; }

    //    [JsonProperty("valutaKode")]
    //    public string ValutaKode { get; set; }

    //    [JsonProperty("sukuBungaImbalan")]
    //    public string SukuBungaImbalan { get; set; }

    //    [JsonProperty("jenisSukuBungaImbalan")]
    //    public string JenisSukuBungaImbalan { get; set; }

    //    [JsonProperty("jenisSukuBungaImbalanKet")]
    //    public string JenisSukuBungaImbalanKet { get; set; }

    //    [JsonProperty("kualitas")]
    //    public string Kualitas { get; set; }

    //    [JsonProperty("kualitasKet")]
    //    public string KualitasKet { get; set; }

    //    [JsonProperty("jumlahHariTunggakan")]
    //    public string JumlahHariTunggakan { get; set; }

    //    [JsonProperty("nilaiProyek")]
    //    public string NilaiProyek { get; set; }

    //    [JsonProperty("plafonAwal")]
    //    public string PlafonAwal { get; set; }

    //    [JsonProperty("plafon")]
    //    public string Plafon { get; set; }

    //    [JsonProperty("realisasiBulanBerjalan")]
    //    public string RealisasiBulanBerjalan { get; set; }

    //    [JsonProperty("nilaiDalamMataUangAsal")]
    //    public string NilaiDalamMataUangAsal { get; set; }

    //    [JsonProperty("kodeSebabMacet")]
    //    public string KodeSebabMacet { get; set; }

    //    [JsonProperty("sebabMacetKet")]
    //    public string SebabMacetKet { get; set; }

    //    [JsonProperty("tanggalMacet")]
    //    public string TanggalMacet { get; set; }

    //    [JsonProperty("tunggakanPokok")]
    //    public string TunggakanPokok { get; set; }

    //    [JsonProperty("tunggakanBunga")]
    //    public string TunggakanBunga { get; set; }

    //    [JsonProperty("frekuensiTunggakan")]
    //    public string FrekuensiTunggakan { get; set; }

    //    [JsonProperty("denda")]
    //    public string Denda { get; set; }

    //    [JsonProperty("frekuensiRestrukturisasi")]
    //    public string FrekuensiRestrukturisasi { get; set; }

    //    [JsonProperty("tanggalRestrukturisasiAkhir")]
    //    public string TanggalRestrukturisasiAkhir { get; set; }

    //    [JsonProperty("kodeCaraRestrukturisasi")]
    //    public string KodeCaraRestrukturisasi { get; set; }

    //    [JsonProperty("restrukturisasiKet")]
    //    public string RestrukturisasiKet { get; set; }

    //    [JsonProperty("kondisi")]
    //    public string Kondisi { get; set; }

    //    [JsonProperty("kondisiKet")]
    //    public string KondisiKet { get; set; }

    //    [JsonProperty("tanggalKondisi")]
    //    public string TanggalKondisi { get; set; }

    //    [JsonProperty("keterangan")]
    //    public string Keterangan { get; set; }

    //    [JsonProperty("tahunBulan01Ht")]
    //    public string TahunBulan01Ht { get; set; }

    //    [JsonProperty("tahunBulan01")]
    //    public string TahunBulan01 { get; set; }

    //    [JsonProperty("tahunBulan01Kol")]
    //    public string TahunBulan01Kol { get; set; }

    //    [JsonProperty("tahunBulan02Ht")]
    //    public string TahunBulan02Ht { get; set; }

    //    [JsonProperty("tahunBulan02")]
    //    public string TahunBulan02 { get; set; }

    //    [JsonProperty("tahunBulan02Kol")]
    //    public string TahunBulan02Kol { get; set; }

    //    [JsonProperty("tahunBulan03Ht")]
    //    public string TahunBulan03Ht { get; set; }

    //    [JsonProperty("tahunBulan03")]
    //    public string TahunBulan03 { get; set; }

    //    [JsonProperty("tahunBulan03Kol")]
    //    public string TahunBulan03Kol { get; set; }

    //    [JsonProperty("tahunBulan04Ht")]
    //    public string TahunBulan04Ht { get; set; }

    //    [JsonProperty("tahunBulan04")]
    //    public string TahunBulan04 { get; set; }

    //    [JsonProperty("tahunBulan04Kol")]
    //    public string TahunBulan04Kol { get; set; }

    //    [JsonProperty("tahunBulan05Ht")]
    //    public string TahunBulan05Ht { get; set; }

    //    [JsonProperty("tahunBulan05")]
    //    public string TahunBulan05 { get; set; }

    //    [JsonProperty("tahunBulan05Kol")]
    //    public string TahunBulan05Kol { get; set; }

    //    [JsonProperty("tahunBulan06Ht")]
    //    public string TahunBulan06Ht { get; set; }

    //    [JsonProperty("tahunBulan06")]
    //    public string TahunBulan06 { get; set; }

    //    [JsonProperty("tahunBulan06Kol")]
    //    public string TahunBulan06Kol { get; set; }

    //    [JsonProperty("tahunBulan07Ht")]
    //    public string TahunBulan07Ht { get; set; }

    //    [JsonProperty("tahunBulan07")]
    //    public string TahunBulan07 { get; set; }

    //    [JsonProperty("tahunBulan07Kol")]
    //    public string TahunBulan07Kol { get; set; }

    //    [JsonProperty("tahunBulan08Ht")]
    //    public string TahunBulan08Ht { get; set; }

    //    [JsonProperty("tahunBulan08")]
    //    public string TahunBulan08 { get; set; }

    //    [JsonProperty("tahunBulan08Kol")]
    //    public string TahunBulan08Kol { get; set; }

    //    [JsonProperty("tahunBulan09Ht")]
    //    public string TahunBulan09Ht { get; set; }

    //    [JsonProperty("tahunBulan09")]
    //    public string TahunBulan09 { get; set; }

    //    [JsonProperty("tahunBulan09Kol")]
    //    public string TahunBulan09Kol { get; set; }

    //    [JsonProperty("tahunBulan10Ht")]
    //    public string TahunBulan10Ht { get; set; }

    //    [JsonProperty("tahunBulan10")]
    //    public string TahunBulan10 { get; set; }

    //    [JsonProperty("tahunBulan10Kol")]
    //    public string TahunBulan10Kol { get; set; }

    //    [JsonProperty("tahunBulan11Ht")]
    //    public string TahunBulan11Ht { get; set; }

    //    [JsonProperty("tahunBulan11")]
    //    public string TahunBulan11 { get; set; }

    //    [JsonProperty("tahunBulan11Kol")]
    //    public string TahunBulan11Kol { get; set; }

    //    [JsonProperty("tahunBulan12Ht")]
    //    public string TahunBulan12Ht { get; set; }

    //    [JsonProperty("tahunBulan12")]
    //    public string TahunBulan12 { get; set; }

    //    [JsonProperty("tahunBulan12Kol")]
    //    public string TahunBulan12Kol { get; set; }

    //    [JsonProperty("agunan")]
    //    public List<dynamic> Agunan { get; set; }

    //    [JsonProperty("penjamin")]
    //    public List<dynamic> Penjamin { get; set; }
    //}

    public partial class ParameterPencarian
    {
        [JsonProperty("namaDebitur")]
        public string NamaDebitur { get; set; }

        [JsonProperty("jenisKelamin")]
        public string JenisKelamin { get; set; }

        [JsonProperty("jenisKelaminKet")]
        public string JenisKelaminKet { get; set; }

        [JsonProperty("noIdentitas")]
        public string NoIdentitas { get; set; }

        [JsonProperty("npwp")]
        public string Npwp { get; set; }

        [JsonProperty("tempatLahir")]
        public string TempatLahir { get; set; }

        [JsonProperty("tanggalLahir")]
        public string TanggalLahir { get; set; }
    }

    public partial class RingkasanFasilitas
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

    public partial class ResultSLIK
    {
        public static ResultSLIK FromJson(string json) => JsonConvert.DeserializeObject<ResultSLIK>(json, ResultSLIKConverter.Settings);
    }

    public static class ResultSLIKSerialize
    {
        public static string ToJson(this ResultSLIK self) => JsonConvert.SerializeObject(self, ResultSLIKConverter.Settings);
    }

    internal static class ResultSLIKConverter
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
