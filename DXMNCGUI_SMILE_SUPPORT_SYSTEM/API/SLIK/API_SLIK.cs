using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK.Models;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.VisualBasic;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.SLIK
{
    public class API_SLIK : BasePage
    {
        Uri ReqSLIK_Uri = new Uri(ConfigurationManager.AppSettings["slik_req_uri"].ToString());
        Uri GetSLIK_Uri = new Uri(ConfigurationManager.AppSettings["slik_get_uri"].ToString());
        string SLIK_Tenant = ConfigurationManager.AppSettings["slik_tenant"].ToString();
        string SLIK_Email = ConfigurationManager.AppSettings["slik_email"].ToString();

        public async Task<string> RequestSLIK(DataTable dtDeb)
        {
            string strResult = "";

            if (dtDeb.Rows.Count > 0)
            {
                foreach (DataRow item in dtDeb.Rows)
                {
                    ModelReqSLIK mdlReq = new ModelReqSLIK();
                    ResultReqSLIK messageResult = new ResultReqSLIK();

                    string clientid = item["CLIENT"].ToString();
                    string pengurusid = item["SID_PENGURUSID"].ToString();

                    mdlReq.ReferenceId = Guid.NewGuid().ToString();
                    mdlReq.Name = item["NAME"].ToString();
                    mdlReq.Dob = item["INBORNDT"].ToString();
                    mdlReq.Ktp = item["KTP"].ToString();
                    mdlReq.Npwp = item["NPWP"].ToString();
                    mdlReq.Gender = item["INGENDER"].ToString();

                    string strPOB = item["INBORNPLC"].ToString();
                    if(strPOB.Length > 30)
                    {
                        mdlReq.Pob = strPOB.Substring(0, 30);
                    }
                    else
                    {
                        mdlReq.Pob = strPOB;
                    }
                    
                    //mdlReq.Mmn = item["IBUKANDUNG"].ToString();
                    mdlReq.Mmn = "";
                    mdlReq.Address = item["ADDRESS"].ToString();
                    mdlReq.Custtype = item["CUSTTYPE"].ToString();
                    mdlReq.Tenant = SLIK_Tenant;
                    mdlReq.Email = SLIK_Email;

                    //FOR UPLOAD REQUEST
                    if(mdlReq.Ktp == "")
                    {
                        mdlReq.Ktp = "1";
                    }

                    if(mdlReq.Dob == "")
                    {
                        mdlReq.Dob = "2000-01-01";
                    }

                    if(mdlReq.Custtype == "Perusahaan")
                    {
                        mdlReq.Custtype = "2";
                    }

                    string jsonString = JsonConvert.SerializeObject(mdlReq);
                    string stringResponse = String.Empty;

                    try
                    {
                        var response = new HttpResponseMessage();
                        var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                        HttpClientHandler clientHandler = new HttpClientHandler();
                        var client = new HttpClient();
                        //client.DefaultRequestHeaders.Add("token", secretkey);
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                        response = await client.PostAsync(ReqSLIK_Uri, httpContent);
                        stringResponse = await response.Content.ReadAsStringAsync();

                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        messageResult = JsonConvert.DeserializeObject<ResultReqSLIK>(stringResponse);

                        if(messageResult.Status == "OK")
                        {
                            var retReffID = messageResult.Data.RequestReffId.ToString();
                            InsertRequestSLIK(mdlReq, retReffID, clientid, pengurusid);

                            InsertLogData(ReqSLIK_Uri.ToString(), stringResponse, 0, jsonString, strResult, "sysadmin");
                        }
                        else
                        {
                            strResult = "ERROR: \n";
                            if (messageResult.Code == "INVALID_PARAMETERS")
                            {
                                if(messageResult.Message.Name != null)
                                {
                                    strResult += "- name: " + messageResult.Message.Name.FirstOrDefault() + "\n";
                                }
                                if (messageResult.Message.DOB != null)
                                {
                                    strResult += "- dob: " + messageResult.Message.DOB.FirstOrDefault() + "\n";
                                }
                                if (messageResult.Message.KTP != null)
                                {
                                    strResult += "- ktp: " + messageResult.Message.KTP.FirstOrDefault() + "\n";
                                }
                                if (messageResult.Message.Email != null)
                                {
                                    strResult += "- email: " + messageResult.Message.Email.FirstOrDefault() + "\n";
                                }
                                if (messageResult.Message.NamaIbu != null)
                                {
                                    strResult += "- namaibu: " + messageResult.Message.NamaIbu.FirstOrDefault() + "\n";
                                }
                            }
                            else if(messageResult.Code == "SYSTEM_FAILURE")
                            {
                                strResult += "- Error connecting to API";
                            }
                            else
                            {
                                strResult += "- Error connecting to API";
                            }

                            InsertLogData(ReqSLIK_Uri.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");
                        }
                    }
                    catch (Exception ex)
                    {
                        strResult = "ERROR: " + ex.Message;
                        InsertLogData(ReqSLIK_Uri.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");

                    }
                }
            }

            return strResult;
        }

        public async Task<string> RequestSLIKUpload(DataTable dtDeb)
        {
            string strResult = "";

            if(dtDeb != null)
            {
                foreach (DataRow item in dtDeb.Rows)
                {
                    ModelReqSLIK mdlReq = new ModelReqSLIK();
                    ResultReqSLIK messageResult = new ResultReqSLIK();

                    mdlReq.ReferenceId = Guid.NewGuid().ToString();
                    mdlReq.Name = item["Name"].ToString();
                    mdlReq.Dob = "";
                    mdlReq.Ktp = "";
                    mdlReq.Npwp = item["NPWP"].ToString();
                    mdlReq.Gender = "";
                    mdlReq.Pob = "";
                    mdlReq.Mmn = "";
                    mdlReq.Address = "";

                    //mdlReq.Custtype = item["Type"].ToString();
                    mdlReq.Custtype = "2"; // default for company
                    if (mdlReq.Custtype == "2")
                    {
                        mdlReq.Dob = "2000-01-01";
                        mdlReq.Ktp = "1";
                    }
                    
                    mdlReq.Tenant = SLIK_Tenant;
                    mdlReq.Email = SLIK_Email;

                    string jsonString = JsonConvert.SerializeObject(mdlReq);
                    string stringResponse = String.Empty;
                    try
                    {
                        if(mdlReq.Name != "" && mdlReq.Name != "-" && mdlReq.Npwp != "-" && mdlReq.Npwp != "")
                        {
                            var response = new HttpResponseMessage();
                            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                            HttpClientHandler clientHandler = new HttpClientHandler();
                            var client = new HttpClient();
                            //client.DefaultRequestHeaders.Add("token", secretkey);
                            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                            response = await client.PostAsync(ReqSLIK_Uri, httpContent);
                            stringResponse = await response.Content.ReadAsStringAsync();

                            var settings = new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore
                            };

                            messageResult = JsonConvert.DeserializeObject<ResultReqSLIK>(stringResponse);

                            if (messageResult.Status == "OK")
                            {
                                var retReffID = messageResult.Data.RequestReffId.ToString();
                                InsertRequestSLIK(mdlReq, retReffID, mdlReq.Name, "0");
                                InsertLogData(ReqSLIK_Uri.ToString(), stringResponse, 0, jsonString, strResult, "sysadmin");
                            }
                            else
                            {
                                strResult = "ERROR: " + messageResult.Code;
                                InsertLogData(ReqSLIK_Uri.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        strResult = "ERROR: " + ex.Message;
                        InsertLogData(ReqSLIK_Uri.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");
                    }
                }
            }

            return strResult;
        }

        public async Task<string> GetSLIK(string reff_id)
        {
            string strResult = "";
            
            ModelReference mdlRef = new ModelReference();
            ResultSLIK messageResult = new ResultSLIK();

            mdlRef.request_reff_id = reff_id;
            string jsonString = JsonConvert.SerializeObject(mdlRef);
            string stringResponse = String.Empty;
            try
            {
                var response = new HttpResponseMessage();
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                var client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                response = await client.PostAsync(GetSLIK_Uri, httpContent);
                stringResponse = await response.Content.ReadAsStringAsync();

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                messageResult = JsonConvert.DeserializeObject<ResultSLIK>(stringResponse);

                if(messageResult.ResultFlag == true)
                {
                    if (messageResult.ErrorMsg == null)
                    {
                        if (messageResult.ResultIdeb.Count > 0)
                        {
                            var dtDeb = messageResult.ResultIdeb.FirstOrDefault();
                            var dtSLIK = dtDeb.Ideb.IdebIndividu.Individual.Fasilitas.KreditPembiayan;

                            if (dtSLIK.Count > 0)
                            {
                                CalculateSLIK(dtSLIK, reff_id);
                            }
                        }
                    }
                    else
                    {
                        strResult = "Data SLIK Tidak Ditemukan";
                    }
                }
                else
                {
                    strResult = messageResult.ErrorMsg;
                }

                InsertLogData(GetSLIK_Uri.ToString(), stringResponse, 0, jsonString, strResult, "sysadmin");
            }
            catch (Exception ex)
            {
                strResult = "ERROR: " + ex.Message;

                InsertLogData(GetSLIK_Uri.ToString(), stringResponse, 1, jsonString, strResult, "sysadmin");
            }

            return strResult;
        }

        public async Task<string> GetCompanySLIK(string reff_id)
        {
            string strResult = "";

            ModelReference mdlRef = new ModelReference();
            ResultCompanySLIK messageResult = new ResultCompanySLIK();

            mdlRef.request_reff_id = reff_id;
            string jsonString = JsonConvert.SerializeObject(mdlRef);

            try
            {
                var response = new HttpResponseMessage();
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                HttpClientHandler clientHandler = new HttpClientHandler();
                var client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                response = await client.PostAsync(GetSLIK_Uri, httpContent);
                var stringResponse = await response.Content.ReadAsStringAsync();

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };

                messageResult = JsonConvert.DeserializeObject<ResultCompanySLIK>(stringResponse);

                if (messageResult.ResultFlag == true)
                {
                    if(messageResult.ErrorMsg == null)
                    {
                        if (messageResult.ResultIdeb.Count > 0)
                        {
                            var dtDeb = messageResult.ResultIdeb.FirstOrDefault();
                            var dtSLIK = dtDeb.Ideb.IdebPerusahaan.Perusahaan.Fasilitas.KreditPembiayan;

                            if (dtSLIK.Count > 0)
                            {
                                CalculateSLIK(dtSLIK, reff_id);
                            }
                        }
                    }
                    else
                    {
                        strResult = "Data SLIK Tidak Ditemukan";
                    }
                }
                else
                {
                    strResult = messageResult.ErrorMsg;
                }
            }
            catch (Exception ex)
            {
                strResult = "ERROR: " + ex.Message;
            }

            return strResult;
        }

        public void CalculateSLIK(List<dynamic> dtSLIK, string reff_id)
        {
            foreach (var v in (dtSLIK as IEnumerable<object>))
            {
                ModelCreditFinancing mdlCredit = new ModelCreditFinancing();
                ModelFlagSLIK mdlFlag = new ModelFlagSLIK();

                var d = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(v.ToString());
                mdlCredit.REFID = reff_id;
                mdlCredit.LJK = d["ljkKet"];
                mdlCredit.JENIS = d["jenisKreditPembiayaanKet"];
                mdlCredit.JENIS_PENGGUNAAN = d["jenisPenggunaanKet"];
                mdlCredit.KONDISI = d["kondisiKet"];
                mdlCredit.FREKUENSI_TUNGGAKAN = Convert.ToInt32(d["frekuensiTunggakan"]);
                mdlCredit.TUNGGAKAN_POKOK = Convert.ToDouble(d["tunggakanPokok"]);
                mdlCredit.TUNGGAKAN_BUNGA = Convert.ToDouble(d["tunggakanBunga"]);
                mdlCredit.DENDA = Convert.ToDouble(d["denda"]);

                //GET LIST AGUNAN
                int indexAgunan = 0;
                string strAgunan = "";
                foreach (dynamic item in d["agunan"])
                {
                    //if(strAgunan.Contains(item["jenisAgunanKet"]) == false)
                    //{
                    //    if (indexAgunan > 0) strAgunan += ", ";
                    //    strAgunan += item["jenisAgunanKet"];
                    //}
                    if (indexAgunan > 0) strAgunan += ", ";
                    strAgunan += item["jenisAgunanKet"];

                    indexAgunan++;
                }
                mdlCredit.AGUNAN_LIST = strAgunan;

                //CALCULATE
                if (d["plafonAwal"] != "")
                {
                    mdlCredit.PLAFON = Convert.ToDouble(d["plafon"]);
                }

                if (d["bakiDebet"] != "")
                {
                    mdlCredit.BAKIDEBET = Convert.ToDouble(d["bakiDebet"]);
                }

                if (d["sukuBungaImbalan"] != "")
                {
                    string strVal = d["sukuBungaImbalan"];
                    mdlCredit.BUNGA = Convert.ToDouble(strVal) / 100;
                }

                if (d["tanggalAkadAwal"] != "")
                {
                    mdlCredit.AKADAWAL = ConvertDateFromString(d["tanggalAkadAwal"]);
                }

                if (d["tanggalJatuhTempo"] != "")
                {
                    mdlCredit.JATUHTEMPO = ConvertDateFromString(d["tanggalJatuhTempo"]);
                }

                if (d["bulan"] != "" && d["tahun"] != "")
                {
                    int intYear = Convert.ToInt32(d["tahun"]);
                    int intMonth = Convert.ToInt32(d["bulan"]);
                    mdlCredit.TGLAWAL_SISATENOR = new DateTime(intYear, intMonth, 1);
                    mdlCredit.TGLAWAL_SISATENOR = mdlCredit.TGLAWAL_SISATENOR.AddMonths(1);
                }

                //TERM
                //mdlCredit.JANGKA = Math.Round(Math.Round((mdlCredit.JATUHTEMPO - mdlCredit.AKADAWAL).TotalDays, 0) / 365);
                mdlCredit.JANGKA = ((mdlCredit.JATUHTEMPO.Year - mdlCredit.AKADAWAL.Year) * 12) + mdlCredit.JATUHTEMPO.Month - mdlCredit.AKADAWAL.Month;

                //SISA TENOR
                //mdlCredit.SISATENOR = Math.Round(Math.Round((mdlCredit.JATUHTEMPO - mdlCredit.TGLAWAL_SISATENOR).TotalDays, 0) / 365);
                DateTime tglTerakirByr = new DateTime(Convert.ToInt32(d["tahun"]), Convert.ToInt32(d["bulan"]), 1);
                int monthdiff = ((mdlCredit.JATUHTEMPO.Year - tglTerakirByr.Year) * 12) + mdlCredit.JATUHTEMPO.Month - tglTerakirByr.Month;
                //double monthdiff = mdlCredit.JATUHTEMPO.Subtract(tglTerakirByr).Days / (365.25 / 12);
                int hariTunggakan = Convert.ToInt32(d["jumlahHariTunggakan"]) / 30;
                mdlCredit.SISATENOR = monthdiff + hariTunggakan;

                //Rumus Angsuran
                if (mdlCredit.JENIS_PENGGUNAAN == "Modal Kerja")
                {
                    mdlCredit.ANGSURAN = mdlCredit.BAKIDEBET * mdlCredit.BUNGA / 12;
                }
                else if(mdlCredit.JENIS.Contains("Kartu Kredit") == true)
                {
                    mdlCredit.ANGSURAN = mdlCredit.BAKIDEBET * 0.1;
                }
                else
                {
                    //var valTermPerMonth = Math.Round(mdlCredit.JANGKA) * 12;
                    //var valPMT = Financial.Pmt(mdlCredit.BUNGA / valTermPerMonth, valTermPerMonth, mdlCredit.PLAFON, 0, 0);
                    //mdlCredit.ANGSURAN = ExcelCeiling(-valPMT, 1000);

                    if(mdlCredit.SISATENOR > 0)
                    {
                        var valPMT = Financial.Pmt(mdlCredit.BUNGA / 12, mdlCredit.SISATENOR, mdlCredit.BAKIDEBET, 0, 0);
                        mdlCredit.ANGSURAN = ExcelCeiling(-valPMT, 1000);
                    }
                    else
                    {
                        mdlCredit.ANGSURAN = 0;
                    }
                    
                }
                
                //Check Minus
                if (mdlCredit.SISATENOR < 0)
                {
                    mdlCredit.SISATENOR = 0;
                }
                if (mdlCredit.ANGSURAN < 0)
                {
                    mdlCredit.ANGSURAN = 0;
                }

                //Get Max Kol History
                int maxKol = 0;
                int maxKolDays = 0;
                for (int i = 1; i <= 24; i++)
                {
                    string runningNum = String.Format("{0:00}", i);
                    string colName = "tahunBulan" + runningNum + "Kol";
                    string colDaysName = "tahunBulan" + runningNum + "Ht";

                    try
                    {
                        var colValue = d[colName];
                        var colDaysValue = d[colDaysName];
                        if (colValue != "")
                        {
                            int currKol = Convert.ToInt32(colValue);
                            int currKolDays = Convert.ToInt32(colDaysValue);
                            if (currKol > maxKol)
                            {
                                maxKol = currKol;
                                maxKolDays = currKolDays;
                            }
                            if(currKol == maxKol)
                            {
                                if(currKolDays > maxKolDays)
                                {
                                    maxKolDays = currKolDays;
                                }
                            }
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
                mdlCredit.KOLEKTIBILITAS = d["kualitas"] + " - " + d["jumlahHariTunggakan"] + " Hari Tunggakan";
                mdlCredit.HISTORY_KOLEKTIBILITAS = "";
                if (maxKol > 0)
                {
                    mdlCredit.HISTORY_KOLEKTIBILITAS = maxKol.ToString() + " - " + maxKolDays.ToString() + " Hari Tunggakan";
                }

                //Fill the Flag
                string LJKCode = d["ljk"];
                if(CheckBankSLIK(LJKCode) == true)
                {
                    mdlFlag.flagBank = 1;
                }
                if (mdlCredit.PLAFON == mdlCredit.BAKIDEBET)
                {
                    mdlFlag.flagPlafon = 1;
                }
                if (mdlCredit.JANGKA < 12)
                {
                    mdlFlag.flagTenor = 1;
                }
                if(mdlCredit.BUNGA < 0.05)
                {
                    mdlFlag.flagRate = 1;
                }
                if(mdlCredit.BAKIDEBET == 0 && DateTime.Now < mdlCredit.JATUHTEMPO && mdlCredit.KONDISI == "Fasilitas Aktif")
                {
                    mdlFlag.flagBakiDebt = 1;
                }

                InsertFinancingCredit(mdlCredit, mdlFlag);
            }
        }

        public void InsertRequestSLIK(ModelReqSLIK mdlReq, string reffno, string clientno, string pengurusid)
        {
            if(mdlReq != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                SqlConnection myconn = new SqlConnection(connString);
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[trxRequestSLIK] VALUES(@TRXID,@REFID,@CLIENT,@SID_PENGURUSID,@REQSTATUS,@NAME,@DOB,@KTP,@NPWP,@GENDER,@POB,@MMN,@ADDRESS,@CUSTTYPE,@CRE_BY,GETDATE())");
                sqlCommand.Connection = myconn;
                myconn.Open();
                try
                {
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@TRXID", SqlDbType.VarChar);
                    sqlParameter1.Value = mdlReq.ReferenceId.ToUpper();
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@REFID", SqlDbType.VarChar);
                    sqlParameter2.Value = reffno;
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@CLIENT", SqlDbType.VarChar);
                    sqlParameter3.Value = clientno;
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@SID_PENGURUSID", SqlDbType.VarChar);
                    sqlParameter4.Value = pengurusid;
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@REQSTATUS", SqlDbType.VarChar);
                    sqlParameter5.Value = "Data on Process";
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@NAME", SqlDbType.VarChar);
                    sqlParameter6.Value = mdlReq.Name;
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@DOB", SqlDbType.VarChar);
                    sqlParameter7.Value = mdlReq.Dob;
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@KTP", SqlDbType.VarChar);
                    sqlParameter8.Value = mdlReq.Ktp;
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@NPWP", SqlDbType.VarChar);
                    sqlParameter9.Value = mdlReq.Npwp;
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@GENDER", SqlDbType.VarChar);
                    sqlParameter10.Value = mdlReq.Gender;
                    sqlParameter10.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@POB", SqlDbType.VarChar);
                    sqlParameter11.Value = mdlReq.Pob;
                    sqlParameter11.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@MMN", SqlDbType.VarChar);
                    sqlParameter12.Value = mdlReq.Mmn;
                    sqlParameter12.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@ADDRESS", SqlDbType.VarChar);
                    sqlParameter13.Value = mdlReq.Address;
                    sqlParameter13.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@CUSTTYPE", SqlDbType.VarChar);
                    sqlParameter14.Value = mdlReq.Custtype;
                    sqlParameter14.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.VarChar);
                    sqlParameter15.Value = this.UserID;
                    sqlParameter15.Direction = ParameterDirection.Input;
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                myconn.Close();
            }
        }

        public void InsertFinancingCredit(ModelCreditFinancing mdlCredit, ModelFlagSLIK mdlFlag)
        {
            if(mdlCredit != null)
            {
                string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
                SqlConnection myconn = new SqlConnection(connString);
                SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[trxFinancingCreditSLIK] ([REFID],[LJK],[JENIS],[PLAFON],[BAKIDEBET],[BUNGA],
                [AKADAWAL],[TGLAWAL_SISATENOR],[JATUHTEMPO],[JANGKA],[SISATENOR],[ANGSURAN],[KOLEKTIBILITAS],[HISTORY_KOLEKTIBILITAS],[CRE_BY],[CRE_DATE],[FREKUENSI_TUNGGAKAN],[TUNGGAKAN_POKOK],[TUNGGAKAN_BUNGA],[DENDA],[AGUNAN_LIST],[JENIS_PENGGUNAAN],[KONDISI],[flagPlafon],[flagTenor],[flagBank],[flagRate],[flagBakiDebt]) VALUES 
                (@REFID,@LJK,@JENIS,@PLAFON,@BAKIDEBET,@BUNGA,@AKADAWAL,@TGLAWAL_SISATENOR,@JATUHTEMPO,@JANGKA,@SISATENOR,@ANGSURAN,@KOLEKTIBILITAS,@HISTORY_KOLEKTIBILITAS,@CRE_BY,GETDATE(),@FREKUENSI_TUNGGAKAN,@TUNGGAKAN_POKOK,@TUNGGAKAN_BUNGA,@DENDA,@AGUNAN_LIST,@JENIS_PENGGUNAAN,@KONDISI,@flagPlafon,@flagTenor,@flagBank,@flagRate,@flagBakiDebt)");
                sqlCommand.Connection = myconn;
                myconn.Open();
                try
                {
                    
                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@REFID", SqlDbType.VarChar);
                    sqlParameter1.Value = mdlCredit.REFID;
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@LJK", SqlDbType.VarChar);
                    sqlParameter2.Value = mdlCredit.LJK;
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@JENIS", SqlDbType.VarChar);
                    sqlParameter3.Value = mdlCredit.JENIS;
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@PLAFON", SqlDbType.Decimal);
                    sqlParameter4.Value = Convert.ToDecimal(mdlCredit.PLAFON);
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@BAKIDEBET", SqlDbType.Decimal);
                    sqlParameter5.Value = Convert.ToDecimal(mdlCredit.BAKIDEBET);
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@BUNGA", SqlDbType.Float);
                    sqlParameter6.Value = mdlCredit.BUNGA * 100;
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@AKADAWAL", SqlDbType.DateTime);
                    if(mdlCredit.AKADAWAL != DateTime.MinValue)
                    {
                        sqlParameter7.Value = mdlCredit.AKADAWAL.ToString("yyyy-MM-dd");
                    }else
                    {
                        sqlParameter7.Value = DBNull.Value;
                    }
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@TGLAWAL_SISATENOR", SqlDbType.DateTime);
                    if (mdlCredit.TGLAWAL_SISATENOR != DateTime.MinValue)
                    {
                        sqlParameter8.Value = mdlCredit.TGLAWAL_SISATENOR.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        sqlParameter8.Value = DBNull.Value;
                    }
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@JATUHTEMPO", SqlDbType.DateTime);
                    if (mdlCredit.JATUHTEMPO != DateTime.MinValue)
                    {
                        sqlParameter9.Value = mdlCredit.JATUHTEMPO.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        sqlParameter9.Value = DBNull.Value;
                    }
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@JANGKA", SqlDbType.Float);
                    sqlParameter10.Value = mdlCredit.JANGKA;
                    sqlParameter10.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@SISATENOR", SqlDbType.Float);
                    sqlParameter11.Value = mdlCredit.SISATENOR;
                    sqlParameter11.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@ANGSURAN", SqlDbType.Decimal);
                    sqlParameter12.Value = Convert.ToDecimal(mdlCredit.ANGSURAN);
                    sqlParameter12.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@KOLEKTIBILITAS", SqlDbType.VarChar);
                    sqlParameter13.Value = mdlCredit.KOLEKTIBILITAS;
                    sqlParameter13.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@HISTORY_KOLEKTIBILITAS", SqlDbType.VarChar);
                    sqlParameter14.Value = mdlCredit.HISTORY_KOLEKTIBILITAS;
                    sqlParameter14.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.VarChar);
                    sqlParameter15.Value = this.UserID;
                    sqlParameter15.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@FREKUENSI_TUNGGAKAN", SqlDbType.Int);
                    sqlParameter16.Value = mdlCredit.FREKUENSI_TUNGGAKAN;
                    sqlParameter16.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter17 = sqlCommand.Parameters.Add("@TUNGGAKAN_POKOK", SqlDbType.Decimal);
                    sqlParameter17.Value = mdlCredit.TUNGGAKAN_POKOK;
                    sqlParameter17.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter18 = sqlCommand.Parameters.Add("@TUNGGAKAN_BUNGA", SqlDbType.Decimal);
                    sqlParameter18.Value = mdlCredit.TUNGGAKAN_BUNGA;
                    sqlParameter18.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter19 = sqlCommand.Parameters.Add("@DENDA", SqlDbType.Decimal);
                    sqlParameter19.Value = mdlCredit.DENDA;
                    sqlParameter19.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter20 = sqlCommand.Parameters.Add("@AGUNAN_LIST", SqlDbType.VarChar);
                    sqlParameter20.Value = mdlCredit.AGUNAN_LIST;
                    sqlParameter20.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter21 = sqlCommand.Parameters.Add("@JENIS_PENGGUNAAN", SqlDbType.VarChar);
                    sqlParameter21.Value = mdlCredit.JENIS_PENGGUNAAN;
                    sqlParameter21.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter22 = sqlCommand.Parameters.Add("@KONDISI", SqlDbType.VarChar);
                    sqlParameter22.Value = mdlCredit.KONDISI;
                    sqlParameter22.Direction = ParameterDirection.Input;

                    //FLAG
                    SqlParameter sqlParameter23 = sqlCommand.Parameters.Add("@flagPlafon", SqlDbType.Int);
                    sqlParameter23.Value = mdlFlag.flagPlafon;
                    sqlParameter23.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter24 = sqlCommand.Parameters.Add("@flagTenor", SqlDbType.Int);
                    sqlParameter24.Value = mdlFlag.flagTenor;
                    sqlParameter24.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter25 = sqlCommand.Parameters.Add("@flagBank", SqlDbType.Int);
                    sqlParameter25.Value = mdlFlag.flagBank;
                    sqlParameter25.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter26 = sqlCommand.Parameters.Add("@flagRate", SqlDbType.Int);
                    sqlParameter26.Value = mdlFlag.flagRate;
                    sqlParameter26.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter27 = sqlCommand.Parameters.Add("@flagBakiDebt", SqlDbType.Int);
                    sqlParameter27.Value = mdlFlag.flagRate;
                    sqlParameter27.Direction = ParameterDirection.Input;
                    
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
                myconn.Close();
            }
            
        }

        public void InsertLogData(string api_url, string result, int is_error, string postval, string retval, string user)
        {
            result = result.Replace("'", "");
            postval = postval.Replace("'", "");
            
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

        public DateTime ConvertDateFromString(string value)
        {
            string[] format = { "yyyyMMdd" };
            DateTime date;

            if (DateTime.TryParseExact(value,
                                       format,
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None,
                                       out date))
            {
                //valid
                date = DateTime.ParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture);
            }

            return date;
        }

        public double ExcelRoundUp(double number, int digits)
        {
            return Math.Ceiling(number * Math.Pow(10, digits)) / Math.Pow(10, digits);
        }

        public double ExcelCeiling(double value, double significance)
        {
            if ((value % significance) != 0)
            {
                return ((int)(value / significance) * significance) + significance;
            }

            return Convert.ToDouble(value);
        }

        public bool CheckBankSLIK(string code)
        {
            bool retBool = false;

            int countBank = 0;
            string ssql = "select count(1) [Bank] from mstBankCodeSLIK where IS_ACTIVE = 1 and CODE = '" + code + "'";
            DataTable resDT = new DataTable();
            SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(ssql);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = myconn;

                SqlDataReader reader = sqlCommand.ExecuteReader();
                resDT.Load(reader);
                foreach (DataRow row in resDT.Rows)
                {
                    countBank = Convert.ToInt32(row["Bank"]);
                    if(countBank > 0)
                    {
                        retBool = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }

            return retBool;
        }
    }
}