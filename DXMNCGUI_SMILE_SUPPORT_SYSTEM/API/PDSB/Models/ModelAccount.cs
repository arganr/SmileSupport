using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB.Models
{
    public class ModelAccount
    {
        public string request_ref_number { get; set; }
        public string kode_sumber_dana { get; set; }
        public string kode_cabang { get; set; }
        public string nama_nasabah { get; set; }
        public string nama_ibu_kandung { get; set; }
        public string tempat_lahir { get; set; }
        public string tanggal_lahir { get; set; }
        public string jenis_kelamin { get; set; }
        public string status_perkawinan { get; set; }
        public string nomor_identitas { get; set; }
        public string tanggal_terbit_identitas { get; set; }
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
        public string telepon_hp_nomor { get; set; }
        public string pekerjaan { get; set; }
        public string sumber_dana { get; set; }
    }

}