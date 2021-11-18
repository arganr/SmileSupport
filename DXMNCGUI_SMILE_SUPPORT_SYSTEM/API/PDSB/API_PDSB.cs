using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB.Models;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.PDSB
{
    public class API_PDSB : BasePage
    {
        string secretkey = ConfigurationManager.AppSettings["pdsb_secretkey"].ToString();
        Uri LoginUri = new Uri(ConfigurationManager.AppSettings["pdsb_login_uri"].ToString());
        Uri AccUri = new Uri(ConfigurationManager.AppSettings["pdsb_acc_uri"].ToString());

        public async Task<string> GetToken()
        {
            string strResult = "";
            ResultLogin messageResult = new ResultLogin();

            try
            {
                var response = new HttpResponseMessage();

                HttpClientHandler clientHandler = new HttpClientHandler();
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", secretkey);

                response = await client.PostAsync(LoginUri, null);
                var stringResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    messageResult = JsonConvert.DeserializeObject<ResultLogin>(stringResponse);
                    strResult = messageResult.token;

                    InsertLogData(LoginUri.ToString(), stringResponse, 0, "", strResult, "sysadmin");
                }
                catch (Exception ex2)
                {
                    strResult = "ERROR: " + ex2.Message;
                }
            }
            catch (Exception ex)
            {
                strResult = "ERROR: " + ex.Message;
            }

            return strResult;
        }
        
        public async Task<string> Account_Registeration(string token, DataTable dtAcc, string branch)
        {
            string strResult = "";
            ModelAccount mdlAcc = new ModelAccount();
            ResultAccount messageResult = new ResultAccount();

            if (dtAcc.Rows.Count > 0)
            {
                foreach (DataRow row in dtAcc.Rows)
                {
                    //mdlCIF.kode_cabang = row["kode_cabang"].ToString();
                    mdlAcc.kode_cabang = branch;
                    mdlAcc.request_ref_number = row["request_ref_number"].ToString();
                    //mdlAcc.request_ref_number = "";
                    mdlAcc.nama_nasabah = row["nama_nasabah"].ToString();
                    mdlAcc.nama_ibu_kandung = row["nama_ibu_kandung"].ToString();
                    mdlAcc.tempat_lahir = row["tempat_lahir"].ToString();
                    mdlAcc.tanggal_lahir = row["tanggal_lahir"].ToString();
                    mdlAcc.jenis_kelamin = row["jenis_kelamin"].ToString();
                    mdlAcc.status_perkawinan = row["status_perkawinan"].ToString();
                    mdlAcc.nomor_identitas = row["nomor_identitas"].ToString();
                    mdlAcc.tanggal_terbit_identitas = row["tanggal_terbit_identitas"].ToString();
                    mdlAcc.pendidikan = row["pendidikan"].ToString();
                    mdlAcc.alamat_rumah_jalan = row["alamat_rumah_jalan"].ToString();
                    mdlAcc.alamat_rumah_rt = row["alamat_rumah_rt"].ToString();
                    mdlAcc.alamat_rumah_rw = row["alamat_rumah_rw"].ToString();
                    mdlAcc.alamat_rumah_kelurahan = row["alamat_rumah_kelurahan"].ToString();
                    mdlAcc.alamat_rumah_kecamatan = row["alamat_rumah_kecamatan"].ToString();
                    mdlAcc.alamat_rumah_kota_kabupaten = row["alamat_rumah_kota_kabupaten"].ToString();
                    mdlAcc.alamat_rumah_kode_pos = row["alamat_rumah_kode_pos"].ToString();
                    mdlAcc.telepon_hp_nomor = row["telepon_hp_nomor"].ToString();
                    mdlAcc.pekerjaan = row["pekerjaan"].ToString();
                    mdlAcc.sumber_dana = row["sumber_dana"].ToString();
                }

                string jsonString = JsonConvert.SerializeObject(mdlAcc);

                try
                {
                    var response = new HttpResponseMessage();
                    var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");


                    HttpClientHandler clientHandler = new HttpClientHandler();
                    var client = new HttpClient();
                    client.Timeout = TimeSpan.FromMinutes(30);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                    response = await client.PostAsync(AccUri, httpContent);


                    var stringResponse = await response.Content.ReadAsStringAsync();

                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };

                    try
                    {
                        messageResult = JsonConvert.DeserializeObject<ResultAccount>(stringResponse);
                        if(messageResult.responseCode == "500")
                        {
                            strResult = "ERROR: " + messageResult.messages;
                            InsertLogData(AccUri.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");
                        }
                        else
                        {
                            strResult = messageResult.account_number;
                            InsertLogData(AccUri.ToString(), stringResponse, 0, jsonString, strResult, "sysadmin");
                        }
                    }
                    catch (Exception ex2)
                    {
                        //var ErrorResult = JsonConvert.DeserializeObject<ResultArray>(stringResponse);
                        strResult = "ERROR: " + ex2.Message;
                        //strResult = "ERROR: " + ErrorResult.messages;
                        //strResult = "ERROR";
                        InsertLogData(AccUri.ToString(), stringResponse, 1, jsonString, "", "sysadmin");
                    }
                }
                catch (Exception ex)
                {
                    //strResult = "ERROR: " + ex.Message;
                    strResult = "ERROR: " + ex.Message;
                }
            }

            return strResult;
        }

        public void InsertLogData(string api_url, string result, int is_error, string postval, string retval, string user)
        {
            result = result.Replace("'", "");
            postval = postval.Replace("'", "");

            //string api_url = request.Url.Scheme + @":/" + request.ApplicationPath + @"/api/InfoAgreement";
            string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            string ssql = "INSERT INTO [dbo].[API_Log] SELECT '" + api_url + "', '" + result + "', '" + retval + "'," + is_error + ",'" +
                UserID + "',GETDATE(),'" + postval + "'";
            using (SqlConnection conn = new SqlConnection(connString))
            using (SqlCommand cmd = new SqlCommand(ssql, conn))
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}