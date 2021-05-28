using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB.Models
{
    public class ModelCIF
    {
        public string kode_cabang { get; set; }
        public string request_ref_number { get; set; }
        public string request_date { get; set; }
        public string nama_nasabah { get; set; }
        public string nama_singkat { get; set; }
        public string nama_ibu_kandung { get; set; }
        public string gelar_depan { get; set; }
        public string gelar_belakang { get; set; }
        public string tempat_lahir { get; set; }
        public string tanggal_lahir { get; set; }
        public string tanggal_buka { get; set; }
        public string jenis_kelamin { get; set; }
        public string agama { get; set; }
        public string status_perkawinan { get; set; }
        public string jenis_identitas { get; set; }
        public string nomor_identitas { get; set; }
        public string golongan_nasabah { get; set; }
        public string tanggal_terbit_identitas { get; set; }
        public string tanggal_berakhir_identitas { get; set; }
        public string nomor_npwp_individu { get; set; }
        public string pendidikan { get; set; }
        public string alamat_email { get; set; }
        public string alamat_rumah_jalan { get; set; }
        public string alamat_rumah_rt { get; set; }
        public string alamat_rumah_rw { get; set; }
        public string alamat_rumah_kelurahan { get; set; }
        public string alamat_rumah_kecamatan { get; set; }
        public string alamat_rumah_kota_kabupaten { get; set; }
        public string alamat_rumah_provinsi { get; set; }
        public string alamat_rumah_kode_pos { get; set; }
        public string alamat_surat_jalan { get; set; }
        public string alamat_surat_rt { get; set; }
        public string alamat_surat_rw { get; set; }
        public string alamat_surat_kelurahan { get; set; }
        public string alamat_surat_kecamatan { get; set; }
        public string alamat_surat_kode_pos { get; set; }
        public string alamat_surat_kota_kabupaten { get; set; }
        public string alamat_surat_provinsi { get; set; }
        public string telepon_rumah_kode_area { get; set; }
        public string telepon_rumah_nomor { get; set; }
        public string telepon_hp_nomor { get; set; }
        public string fax_kode_area { get; set; }
        public string fax_nomor { get; set; }
        public string status_keterkaitan { get; set; }
        public string pekerjaan { get; set; }
        public string jabatan_pekerjaan { get; set; }
        public string badan_hukum_pekerjaan { get; set; }
        public string bidang_usaha_pekerjaan { get; set; }
        public string nama_perusahaan_kerja { get; set; }
        public string alamat_kerja_jalan { get; set; }
        public string alamat_kerja_rt { get; set; }
        public string alamat_kerja_rw { get; set; }
        public string alamat_kerja_kelurahan { get; set; }
        public string alamat_kerja_kecamatan { get; set; }
        public string alamat_kerja_kode_pos { get; set; }
        public string alamat_kerja_kota_kabupaten { get; set; }
        public string alamat_kerja_provinsi { get; set; }
        public string telepon_kerja_kode_area { get; set; }
        public string telepon_kerja_nomor { get; set; }
        public string fax_kerja_kode_area { get; set; }
        public string fax_kerja_nomor { get; set; }
        public string limit_nom_setor_tunai { get; set; }
        public string limit_nom_setor_nontunai { get; set; }
        public string limit_nom_tarik_tunai { get; set; }
        public string limit_nom_tarik_nontunai { get; set; }
        public string limit_frek_setor_tunai { get; set; }
        public string limit_frek_setor_nontunai { get; set; }
        public string limit_frek_tarik_tunai { get; set; }
        public string limit_frek_tarik_nontunai { get; set; }
        public string status_tempat_tinggal { get; set; }
        public string tujuan_buka_rekening { get; set; }
        public string tujuan_penggunaan_dana { get; set; }
        public string sumber_dana { get; set; }
        public string sumber_dana_lainnya { get; set; }
        public string jenis_penduduk { get; set; }
        public string penghasilan_utama_per_tahun { get; set; }
        public string penghasilan_tambahan { get; set; }
        public string pengeluaran_utama_per_tahun { get; set; }
        public string kode_ao { get; set; }
        public string nama_keluarga_dihubungi { get; set; }
        public string telepon_keluarga_dihubungi { get; set; }
        public string Keterangan { get; set; }
    }
}