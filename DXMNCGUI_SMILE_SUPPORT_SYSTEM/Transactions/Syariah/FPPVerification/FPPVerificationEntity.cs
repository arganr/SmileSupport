using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.FPPVerification
{
    public class FPPVerificationEntity
    {
        private FPPVerificationDB myFPPVerificationcommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DataTable myDetailTable;
        private DXSSAction myAction;
        public string strErrorGenTicket;

        internal DataRow Row
        {
            get { return myRow; }
        }
        public FPPVerificationDB FPPVerificationcommand
        {
            get
            {
                return this.myFPPVerificationcommand;
            }
        }
        public DataTable DataTableHeader
        {
            get
            {
                return this.myHeaderTable;
            }
        }
        public DataTable DataTableDetail
        {
            get
            {
                return this.myDetailTable;
            }
        }
        public DataSet FPPVerificationDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }

        public FPPVerificationEntity(FPPVerificationDB aFPPVerification, DataSet ds, DXSSAction action)
        {
            myFPPVerificationcommand = aFPPVerification;
            myDataSet = ds;
            this.myAction = action;
            this.myHeaderTable = this.myDataSet.Tables["Header"];
            this.myDetailTable = this.myDataSet.Tables["Lines"];
            myRow = myHeaderTable.Rows[0];
        }

        public DXSSAction Action
        {
            get
            {
                return this.myAction;
            }
        }

        public object cust_prospect_id
        {
            get { return myRow["cust_prospect_id"]; }
            set { myRow["cust_prospect_id"] = value; }
        }
        public object fpp_no
        {
            get { return myRow["fpp_no"]; }
            set { myRow["fpp_no"] = value; }
        }
        public object nama_cust
        {
            get { return myRow["nama_cust"]; }
            set { myRow["nama_cust"] = value; }
        }
        public object ktp_cust
        {
            get { return myRow["ktp_cust"]; }
            set { myRow["ktp_cust"] = value; }
        }
        public object gender_cust
        {
            get { return myRow["gender_cust"]; }
            set { myRow["gender_cust"] = value; }
        }
        public object marital_stat_cust
        {
            get { return myRow["marital_stat_cust"]; }
            set { myRow["marital_stat_cust"] = value; }
        }
        public object tempat_lahir_cust
        {
            get { return myRow["tempat_lahir_cust"]; }
            set { myRow["tempat_lahir_cust"] = value; }
        }
        public object tanggal_lahir_cust
        {
            get { return myRow["tanggal_lahir_cust"]; }
            set { myRow["tanggal_lahir_cust"] = value; }
        }
        public object alamat_ktp_cust
        {
            get { return myRow["alamat_ktp_cust"]; }
            set { myRow["alamat_ktp_cust"] = value; }
        }
        public object kota_ktp_cust
        {
            get { return myRow["kota_ktp_cust"]; }
            set { myRow["kota_ktp_cust"] = value; }
        }
        public object tempat_tinggal_ktp_stat_cust
        {
            get { return myRow["tempat_tinggal_ktp_stat_cust"]; }
            set { myRow["tempat_tinggal_ktp_stat_cust"] = value; }
        }
        public object no_telp_cust
        {
            get { return myRow["no_telp_cust"]; }
            set { myRow["no_telp_cust"] = value; }
        }
        public object email_cust
        {
            get { return myRow["email_cust"]; }
            set { myRow["email_cust"] = value; }
        }
        public object nama_spouse
        {
            get { return myRow["nama_spouse"]; }
            set { myRow["nama_spouse"] = value; }
        }
        public object ktp_spouse
        {
            get { return myRow["ktp_spouse"]; }
            set { myRow["ktp_spouse"] = value; }
        }
        public object warga_negara_spouse
        {
            get { return myRow["warga_negara_spouse"]; }
            set { myRow["warga_negara_spouse"] = value; }
        }
        public object tempat_lahir_spouse
        {
            get { return myRow["tempat_lahir_spouse"]; }
            set { myRow["tempat_lahir_spouse"] = value; }
        }
        public object tanggal_lahir_spouse
        {
            get { return myRow["tanggal_lahir_spouse"]; }
            set { myRow["tanggal_lahir_spouse"] = value; }
        }
        public object alamat_ktp_spouse
        {
            get { return myRow["alamat_ktp_spouse"]; }
            set { myRow["alamat_ktp_spouse"] = value; }
        }
        public object kota_ktp_spouse
        {
            get { return myRow["kota_ktp_spouse"]; }
            set { myRow["kota_ktp_spouse"] = value; }
        }
        public object telp_spouse
        {
            get { return myRow["telp_spouse"]; }
            set { myRow["telp_spouse"] = value; }
        }
        public object email_spouse
        {
            get { return myRow["email_spouse"]; }
            set { myRow["email_spouse"] = value; }
        }
        public object alamat_tinggal_cust
        {
            get { return myRow["alamat_tinggal_cust"]; }
            set { myRow["alamat_tinggal_cust"] = value; }
        }
        public object kota_tinggal_cust
        {
            get { return myRow["kota_tinggal_cust"]; }
            set { myRow["kota_tinggal_cust"] = value; }
        }
        public object status_tinggal_cust
        {
            get { return myRow["status_tinggal_cust"]; }
            set { myRow["status_tinggal_cust"] = value; }
        }
        public object lama_tinggal_cust
        {
            get { return myRow["lama_tinggal_cust"]; }
            set { myRow["lama_tinggal_cust"] = value; }
        }
        public object jarak_kantor_cust
        {
            get { return myRow["jarak_kantor_cust"]; }
            set { myRow["jarak_kantor_cust"] = value; }
        }
        public object jenis_pekerjaan_cust
        {
            get { return myRow["jenis_pekerjaan_cust"]; }
            set { myRow["jenis_pekerjaan_cust"] = value; }
        }
        public object group_mnc_cust
        {
            get { return myRow["group_mnc_cust"]; }
            set { myRow["group_mnc_cust"] = value; }
        }
        public object status_karyawan_cust
        {
            get { return myRow["status_karyawan_cust"]; }
            set { myRow["status_karyawan_cust"] = value; }
        }
        public object jenis_profesi
        {
            get { return myRow["jenis_profesi"]; }
            set { myRow["jenis_profesi"] = value; }
        }
        public object bidang_usaha
        {
            get { return myRow["bidang_usaha"]; }
            set { myRow["bidang_usaha"] = value; }
        }
        public object pengalaman_kerja
        {
            get { return myRow["pengalaman_kerja"]; }
            set { myRow["pengalaman_kerja"] = value; }
        }
        public object nama_perusahaan_cust
        {
            get { return myRow["nama_perusahaan_cust"]; }
            set { myRow["nama_perusahaan_cust"] = value; }
        }
        public object penghasilan_cust
        {
            get { return myRow["penghasilan_cust"]; }
            set { myRow["penghasilan_cust"] = value; }
        }
        public object penghasilan_spouse
        {
            get { return myRow["penghasilan_spouse"]; }
            set { myRow["penghasilan_spouse"] = value; }
        }
        public object other_income_cust
        {
            get { return myRow["other_income_cust"]; }
            set { myRow["other_income_cust"] = value; }
        }
        public object jenis_pekerjaan_spouse
        {
            get { return myRow["jenis_pekerjaan_spouse"]; }
            set { myRow["jenis_pekerjaan_spouse"] = value; }
        }
        public object jumlah_tanggungan
        {
            get { return myRow["jumlah_tanggungan"]; }
            set { myRow["jumlah_tanggungan"] = value; }
        }
        public object pekerjaan_status_child1
        {
            get { return myRow["pekerjaan_status_child1"]; }
            set { myRow["pekerjaan_status_child1"] = value; }
        }
        public object pekerjaan_status_child2
        {
            get { return myRow["pekerjaan_status_child2"]; }
            set { myRow["pekerjaan_status_child2"] = value; }
        }
        public object pekerjaan_status_child3
        {
            get { return myRow["pekerjaan_status_child3"]; }
            set { myRow["pekerjaan_status_child3"] = value; }
        }
        public object pekerjaan_status_child4
        {
            get { return myRow["pekerjaan_status_child4"]; }
            set { myRow["pekerjaan_status_child4"] = value; }
        }
        public object status_aplikasi
        {
            get { return myRow["status_aplikasi"]; }
            set { myRow["status_aplikasi"] = value; }
        }
        public object cre_by
        {
            get { return myRow["cre_by"]; }
            set { myRow["cre_by"] = value; }
        }
        public object cre_dt
        {
            get { return myRow["cre_dt"]; }
            set { myRow["cre_dt"] = value; }
        }
        public object upd_by
        {
            get { return myRow["upd_by"]; }
            set { myRow["upd_by"] = value; }
        }
        public object upd_dt
        {
            get { return myRow["upd_dt"]; }
            set { myRow["upd_dt"] = value; }
        }
        public object npwp_cust
        {
            get { return myRow["npwp_cust"]; }
            set { myRow["npwp_cust"] = value; }
        }
        public object hp_cust
        {
            get { return myRow["hp_cust"]; }
            set { myRow["hp_cust"] = value; }
        }
        public object mmn_cust
        {
            get { return myRow["mmn_cust"]; }
            set { myRow["mmn_cust"] = value; }
        }
        public object tenor
        {
            get { return myRow["tenor"]; }
            set { myRow["tenor"] = value; }
        }
        public object education_cust
        {
            get { return myRow["education_cust"]; }
            set { myRow["education_cust"] = value; }
        }
        public object hp_spouse
        {
            get { return myRow["hp_spouse"]; }
            set { myRow["hp_spouse"] = value; }
        }
        public object nama_penjamin
        {
            get { return myRow["nama_penjamin"]; }
            set { myRow["nama_penjamin"] = value; }
        }
        public object ktp_penjamin
        {
            get { return myRow["ktp_penjamin"]; }
            set { myRow["ktp_penjamin"] = value; }
        }
        public object warga_negara_penjamin
        {
            get { return myRow["warga_negara_penjamin"]; }
            set { myRow["warga_negara_penjamin"] = value; }
        }
        public object gender_penjamin
        {
            get { return myRow["gender_penjamin"]; }
            set { myRow["gender_penjamin"] = value; }
        }
        public object marital_stat_penjamin
        {
            get { return myRow["marital_stat_penjamin"]; }
            set { myRow["marital_stat_penjamin"] = value; }
        }
        public object tempat_lahir_penjamin
        {
            get { return myRow["tempat_lahir_penjamin"]; }
            set { myRow["tempat_lahir_penjamin"] = value; }
        }
        public object tgl_lahir_penjamin
        {
            get { return myRow["tgl_lahir_penjamin"]; }
            set { myRow["tgl_lahir_penjamin"] = value; }
        }
        public object curr_addr_penjamin
        {
            get { return myRow["curr_addr_penjamin"]; }
            set { myRow["curr_addr_penjamin"] = value; }
        }
        public object curr_kota_penjamin
        {
            get { return myRow["curr_kota_penjamin"]; }
            set { myRow["curr_kota_penjamin"] = value; }
        }
        public object cur_addr_stat_penjamin
        {
            get { return myRow["cur_addr_stat_penjamin"]; }
            set { myRow["cur_addr_stat_penjamin"] = value; }
        }
        public object length_stay_penjamin
        {
            get { return myRow["length_stay_penjamin"]; }
            set { myRow["length_stay_penjamin"] = value; }
        }
        public object hp_penjamin
        {
            get { return myRow["hp_penjamin"]; }
            set { myRow["hp_penjamin"] = value; }
        }
        public object tlp_penjamin
        {
            get { return myRow["tlp_penjamin"]; }
            set { myRow["tlp_penjamin"] = value; }
        }
        public object email_penjamin
        {
            get { return myRow["email_penjamin"]; }
            set { myRow["email_penjamin"] = value; }
        }
        public object nama_spouse_penjamin
        {
            get { return myRow["nama_spouse_penjamin"]; }
            set { myRow["nama_spouse_penjamin"] = value; }
        }
        public object nama_ec
        {
            get { return myRow["nama_ec"]; }
            set { myRow["nama_ec"] = value; }
        }
        public object relationship_ec
        {
            get { return myRow["relationship_ec"]; }
            set { myRow["relationship_ec"] = value; }
        }
        public object hp_ec
        {
            get { return myRow["hp_ec"]; }
            set { myRow["hp_ec"] = value; }
        }
        public object telp_ec
        {
            get { return myRow["telp_ec"]; }
            set { myRow["telp_ec"] = value; }
        }
        public object email_ec
        {
            get { return myRow["email_ec"]; }
            set { myRow["email_ec"] = value; }
        }
        public object mitra_id
        {
            get { return myRow["mitra_id"]; }
            set { myRow["mitra_id"] = value; }
        }

        public DataTable FPPVerificationtable
        {
            get { return myDataSet.Tables[0]; }
        }
    }
}