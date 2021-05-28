using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public class Applicationlib
    {
        static string ConnectionString = WebConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString.ToString();
        static string LocalConnectionString = WebConfigurationManager.ConnectionStrings["SqlLocalConnectionString"].ConnectionString.ToString();
        public static SqlDBSetting sqlDBSetting = new SqlDBSetting(DBServerType.SQL2000, ConnectionString, 10500);
        static string SqlQuery = string.Empty;
        public static string ServerUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["serverURL"].ToString();
            }
        }
        public static string ServerSource
        {
            get
            {
                return WebConfigurationManager.AppSettings["serverSource"].ToString();
            }
        }
        public static string ApplicationPort
        {
            get
            {
                return WebConfigurationManager.AppSettings["applicationPort"].ToString();
            }
        }
        public static string ImagePath
        {
            get
            {
                return WebConfigurationManager.AppSettings["imagePath"].ToString();
            }
        }
        public static string DocumentAutoNumber(string documentType, SqlDBSetting dbsetting)
        {
            string newAutoNumber = string.Empty;
            string oneMonthOneSet = string.Empty;
            long formatID = -1;

            oneMonthOneSet = sqlDBSetting.ExecuteScalar("select OneMonthOneSet from DocNoFormat where DocType=?", documentType).ToString();
            formatID = Convert.ToInt64(sqlDBSetting.ExecuteScalar("select DocKey from DocNoFormat where DocType=?", documentType));
            string formatAutoNumber = string.Empty;
            string[] arrFormatAutoNumber = { };
            string formatNumber = string.Empty;
            int formatNumberPos = -1;
            string formatDate = string.Empty;
            string[] arrFormatDate = { };
            int formatDatePos = -1;
            int nextNumber = 0;

            try
            {
                formatAutoNumber = sqlDBSetting.ExecuteScalar("select Format from DocNoFormat where DocKey=?", formatID).ToString();

                for (int i = 0; i < formatAutoNumber.Length; i++)
                {
                    if (formatAutoNumber[i].ToString() == "0")
                    {
                        formatNumber += "0";
                    }
                }

                arrFormatAutoNumber = formatAutoNumber.Split('/');
                for (int i = 0; i < arrFormatAutoNumber.Length; i++)
                {
                    if (formatNumberPos == -1)
                    {
                        if (arrFormatAutoNumber[i].Contains("<"))
                        {
                            formatNumberPos = i;
                        }
                    }

                    if (formatDatePos == -1)
                    {
                        if (arrFormatAutoNumber[i].Contains("@"))
                        {
                            formatDatePos = i;
                        }
                    }
                }

                if (oneMonthOneSet == "F")
                {
                    nextNumber = Convert.ToInt32(sqlDBSetting.ExecuteScalar("select NextNo from DocNoFormat where DocKey=?", formatID));
                    sqlDBSetting.ExecuteNonQuery("update DocNoFormat set NextNo=?+1 where DocKey=?", (object)nextNumber, (object)formatID);
                }
                else
                {
                    int year = -1;
                    int month = -1;

                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;

                    nextNumber = Convert.ToInt32(sqlDBSetting.ExecuteScalar("select M" + month.ToString() + "NextNumber from SCH_DocNoFormatYearlyNumber where Year=" + year.ToString() + " and FormatID=" + formatID));
                    sqlDBSetting.ExecuteNonQuery("update SCH_DocNoFormatYearlyNumber set M" + month.ToString() + "NextNumber=? where Year=? and FormatID=?", (object)nextNumber, (object)year.ToString(), (object)formatID);
                }

                formatNumber = nextNumber.ToString(formatNumber);

                formatDate = formatAutoNumber.Substring(formatAutoNumber.LastIndexOf("@") - 1);
                formatDate = formatDate.Replace("{", "").Replace("}", "").Replace("@", "");
                arrFormatDate = formatDate.Split('/');

                for (int i = 0; i < arrFormatAutoNumber.Length; i++)
                {
                    if (formatNumberPos == i)
                    {
                        arrFormatAutoNumber[i] = formatNumber;
                    }

                    if (formatDatePos == i)
                    {
                        formatDate = string.Empty;
                        for (int j = 0; j < arrFormatDate.Length; j++)
                        {
                            formatDate += DateTime.Now.ToString(arrFormatDate[j].ToString()) + "/";
                        }

                        formatDate = formatDate.Substring(0, formatDate.LastIndexOf("/"));
                        arrFormatAutoNumber[i] = formatDate;
                    }
                }

                for (int i = 0; i < arrFormatAutoNumber.Length; i++)
                {
                    if (!arrFormatAutoNumber[i].Contains("}"))
                    {
                        newAutoNumber += arrFormatAutoNumber[i].ToString() + "/";
                    }
                }

                newAutoNumber = newAutoNumber.Substring(0, newAutoNumber.LastIndexOf("/"));
            }
            catch (Exception ex)
            {
                newAutoNumber = string.Empty;
                newAutoNumber = ex.Message.ToString() + Environment.NewLine + ex.StackTrace.ToString();
            }

            return newAutoNumber;
        }
        //public static long GetWorklistDocKey()
        //{
        //    DBRegistry regWorklist;
        //    regWorklist = DBRegistry.Create(sqlDBSetting);
        //    return regWorklist.IncOne((IRegistryID)new WorkListDocKey());
        //}

        //public static long GetCutiTransAppKey()
        //{
        //    DBRegistry regcutiTransApp;
        //    regcutiTransApp = DBRegistry.Create(sqlDBSetting);
        //    return regcutiTransApp.IncOne((IRegistryID)new CutiTransAppKey());
        //}
        public static long ApprovalLevelCount(string userID)
        {
            string jobTitle = string.Empty;
            long approvalCount = -1;

            jobTitle = sqlDBSetting.ExecuteScalar("select upper(JobTitle) from SCH_Karyawan where [ID Karyawan]=?", userID).ToString();
            approvalCount = Convert.ToInt64(sqlDBSetting.ExecuteScalar("select ApprovalCount from SCH_Level where upper(LevelCode)=?", jobTitle));

            return approvalCount;
        }
        public static void SendEmail(string subjectMail, string bodyMessage, string emailDestination, string fileName)
        {
            // REMARK BY IMAN, EMAILNYA MASIH NGACO
            // int smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["smtpPort"]);
            // string smtpMail = WebConfigurationManager.AppSettings["smtpMail"].ToString();
            // string mailSource = WebConfigurationManager.AppSettings["mailSource"].ToString();
            // string mailPassword = WebConfigurationManager.AppSettings["mailPassword"].ToString();


            //// mailPassword = "Zaq123456";
            // SmtpClient smtpClient = new SmtpClient(smtpMail, smtpPort);
            // smtpClient.UseDefaultCredentials = false;
            // System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(mailSource, mailPassword);
            // smtpClient.Credentials = networkCredential;
            // for (int i = 0; i < emailDestination.Split(';').Length; i++)
            // {
            //     if (emailDestination.Split(';')[i].ToString() != "")
            //     {
            //         MailMessage mailMessage = new MailMessage();
            //         mailMessage.From = new MailAddress(mailSource, "Soechi Human Resources Information System");
            //         mailMessage.To.Add(new MailAddress(emailDestination.Split(';')[i].ToString()));
            //         mailMessage.Subject = subjectMail;
            //         mailMessage.Body = bodyMessage;
            //         mailMessage.IsBodyHtml = true;
            //         //  mailMessage.Attachments.Add(new Attachment(fileName));
            //         smtpClient.Send(mailMessage);
            //         mailMessage.Dispose();
            //         mailMessage = null;
            //     }
            // }
            // smtpClient.Dispose();
            // smtpClient = null;
            // networkCredential = null;
        }
        public static bool ApprovalCurrentProcess(string userID, long docKey, out long approvalLevel)
        {
            bool validF = false;
            DataTable dtApproval = new DataTable();
            approvalLevel = -1;

            try
            {
                sqlDBSetting.LoadDataTable(dtApproval, "select UserID,AppLevel from SCH_CutiTransApp where ApprovalFlag='F' and UserID=? and DocKey=?", false, userID, docKey);
                if (dtApproval.Rows.Count > 0)
                {
                    if (dtApproval.Rows[0][0].ToString() == userID.ToUpper())
                    {
                        approvalLevel = Convert.ToInt64(dtApproval.Rows[0][1]);
                        validF = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                validF = false;
            }

            return validF;
        }
        public static bool ApprovalLastProcess(long docKey)
        {
            bool lastF = false;

            try
            {
                SqlQuery = "select DocKey from SCH_CutiTransApp where ApprovalFlag='F' and DocKey=?";
                if (sqlDBSetting.ExecuteScalar(SqlQuery, docKey) != null)
                {
                    lastF = false;
                }
                else
                {
                    lastF = true;
                }
            }
            catch (Exception ex)
            {
                lastF = false;
            }

            return lastF;
        }
        public static long ApprovalTotalProcess(string userID)
        {
            long totalProcess = -1;

            try
            {
                SqlQuery = "select isnull(T2.ApprovalCount,-1) from SCH_Karyawan T1 inner join SCH_Level T2 on upper(T1.JobTitle)=upper(T2.LevelCode) where T1.[ID Karyawan]=?";
                totalProcess = Convert.ToInt64(sqlDBSetting.ExecuteScalar(SqlQuery, userID));
            }
            catch (Exception ex)
            {
                totalProcess = -1;
            }

            return totalProcess;
        }
        public static string GetLookUpName(string lookupCode)
        {
            string lookupName = string.Empty;

            try
            {
                string[] arrLookupCode = { };
                arrLookupCode = lookupCode.Split(';');
                lookupName = sqlDBSetting.ExecuteScalar("select LOOKUP_NAME from SCH_Lookups where upper(LOOKUP_CODE)=upper(?)", arrLookupCode[0]).ToString();
            }
            catch (Exception ex)
            {
                lookupName = string.Empty;
            }

            return lookupName;
        }
    }
}