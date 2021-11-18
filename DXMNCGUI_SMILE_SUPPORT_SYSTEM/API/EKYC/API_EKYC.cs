using DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.EKYC.Models;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.API.EKYC
{
    public class API_EKYC : BasePage
    {
        string secretkey = ConfigurationManager.AppSettings["ekyc_token"].ToString();
        Uri EKYC_Uri = new Uri(ConfigurationManager.AppSettings["ekyc_uri"].ToString());

        public async Task<String> BasicVerification(DataTable dtEKYC, string auditUser)
        {
            string strResult = "";
            
            if (dtEKYC.Rows.Count > 0)
            {
                foreach (DataRow item in dtEKYC.Rows)
                {
                    ModelEKYC mdlAcc = new ModelEKYC();
                    ResultEKYC messageResult = new ResultEKYC();

                    mdlAcc.name = item["NAME"].ToString();
                    mdlAcc.nik = item["KTP"].ToString();
                    mdlAcc.trx_id = Guid.NewGuid().ToString();
                    mdlAcc.birthdate = item["INBORNDT"].ToString();
                    mdlAcc.birthplace = item["INBORNPLC"].ToString();
                    mdlAcc.address = item["ADDRESS"].ToString();
                    mdlAcc.identity_photo = "";

                    //check client is company
                    bool isCompany = false;
                    string clientid = item["CLIENT"].ToString();
                    string pengurusid = item["SID_PENGURUSID"].ToString();
                    
                    string jsonString = JsonConvert.SerializeObject(mdlAcc);

                    try
                    {
                        var response = new HttpResponseMessage();
                        var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                        HttpClientHandler clientHandler = new HttpClientHandler();
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Add("token", secretkey);
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                        response = await client.PostAsync(EKYC_Uri, httpContent);
                        var stringResponse = await response.Content.ReadAsStringAsync();

                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };

                        messageResult = JsonConvert.DeserializeObject<ResultEKYC>(stringResponse);
                        updateEKYC(clientid, messageResult, pengurusid, auditUser);
                        
                        InsertLogData(EKYC_Uri.ToString(), stringResponse, 0, jsonString, stringResponse, "sysadmin");
                    }
                    catch (Exception ex)
                    {
                        //strResult = "ERROR: " + ex.Message;
                        strResult = "ERROR: " + ex.Message;
                    }
                }
            }

            return strResult;
        }

        public void updateEKYC(string clientID, ResultEKYC dtEKYC, string pengurusid, string auditUser)
        {
            string cName, cINBORNDT, cINBORNPLC, cADDRESS;
            cName = getConvertBool(dtEKYC.data.name);
            cINBORNDT = getConvertBool(dtEKYC.data.birthdate);
            cINBORNPLC = getConvertBool(dtEKYC.data.birthplace);
            cADDRESS = dtEKYC.data.address;

            //string connString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;
            //string ssql;
            //ssql = "exec spMNCL_InputEKYC '" + clientID + "'," + pengurusid + ",'" + cName.Replace("'", "") + "','" + cINBORNDT + "','" + cINBORNPLC.Replace("'","") + "','" + cADDRESS.Replace("'", "") + "','" + auditUser + "'";

            //using (SqlConnection conn = new SqlConnection(connString))
            //using (SqlCommand cmd = new SqlCommand(ssql, conn))
            //{
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //}

            SqlConnection myconn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(@"exec spMNCL_InputEKYC @CLIENTID,@PENGURUSID,@NAME,@INBORNDT,@INBORNPLC,@ADDRESS,@USERID");
            sqlCommand.Connection = myconn;
            myconn.Open();

            try
            {
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@CLIENTID", SqlDbType.VarChar);
                sqlParameter1.Value = clientID;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@PENGURUSID", SqlDbType.VarChar);
                sqlParameter2.Value = pengurusid;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@NAME", SqlDbType.VarChar);
                sqlParameter3.Value = cName;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@INBORNDT", SqlDbType.VarChar);
                sqlParameter4.Value = cINBORNDT;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@INBORNPLC", SqlDbType.VarChar);
                sqlParameter5.Value = cINBORNPLC;
                sqlParameter5.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@ADDRESS", SqlDbType.VarChar);
                sqlParameter6.Value = cADDRESS;
                sqlParameter6.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@USERID", SqlDbType.VarChar);
                sqlParameter7.Value = UserID;
                sqlParameter7.Direction = ParameterDirection.Input;

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            myconn.Close();

            
        }

        public string getConvertBool(bool value)
        {
            string strResult = "";
            if(value == true)
            {
                strResult = "Match";
            }else
            {
                strResult = "Not Match";
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