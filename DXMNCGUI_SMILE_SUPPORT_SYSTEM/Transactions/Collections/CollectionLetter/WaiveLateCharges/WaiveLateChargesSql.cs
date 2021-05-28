using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.WaiveLateCharges
{
    public class WaiveLateChargesSql : WaiveLateChargesDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM [dbo].[LateChargesWaive] ORDER BY DocDate DESC", true);

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);

            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myDetailTable = new DataTable();
            DataTable myImageTable = new DataTable();

            string sSQLHeader = "SELECT * FROM dbo.LateChargesWaive WHERE DocKey=@DocKey";
            string sSQLheaderDetail = "SELECT * FROM dbo.LateChargesWaiveDetail WHERE DocKey=@DocKey";

            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }

            using (SqlCommand cmddetail = new SqlCommand(sSQLheaderDetail, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmddetail);
                cmddetail.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmddetail.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myDetailTable);
            }

            myHeaderTable.TableName = "Header";
            myDetailTable.TableName = "Detail";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;

            DataColumn[] keyDetail = new DataColumn[1];
            keyDetail[0] = myDetailTable.Columns["DtlKey"];
            myDetailTable.PrimaryKey = keyDetail;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myDetailTable);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                localdbSetting.ExecuteNonQuery("DELETE FROM dbo.LateChargesWaive WHERE DocKey=?", (object)headerid);
                localdbSetting.Commit();

            }
            catch (SqlException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                localdbSetting.EndTransaction();
            }
        }
        protected override void SaveData(WaiveLateChargesEntity WaiveLateCharges, DataSet ds, string strDocName, SaveAction saveaction)
        {
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(localdbSetting.ConnectionString);
            DateTime Mydate = myDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                localdbSetting.StartTransaction();
                if (saveaction == SaveAction.Cancel)
                {
                    dataRow["Cancelled"] = "T";
                }
                if (saveaction == SaveAction.UnCancel)
                {
                    dataRow["Cancelled"] = "F";
                }
                if (saveaction == SaveAction.Save && dataRow["DocNo"].ToString().ToUpper().Contains("NEW"))
                {
                    dataRow["DocDate"] = Mydate;
                    DataRow[] myrowDocNo = localdbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='WLC'", "", DataViewRowState.CurrentRows);
                    if (myrowDocNo != null)
                    {
                        dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
                        localdbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType=?", strDocName);
                    }
                }
                if (saveaction == SaveAction.Save)
                {
                    localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM dbo.LateChargesWaive");
                }
                else if (saveaction == SaveAction.Approve)
                {
                    localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM dbo.LateChargesWaive");

                    SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
                    myconn.Open();
                    try
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"sp_MNCL_WaiveOSLC");
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Connection = myconn;

                        string ipAdd = "";
                        if (HttpContext.Current != null)
                        {
                            var Request = HttpContext.Current.Request;
                            ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                            if (string.IsNullOrEmpty(ipAdd))
                            {
                                ipAdd = Request.ServerVariables["REMOTE_ADDR"];
                            }
                        }

                        SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@LSAGREE", SqlDbType.NVarChar);
                        sqlParameter1.Value = dataRow["RefNo"];

                        SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@WAIVE_AMT", SqlDbType.Float);
                        sqlParameter2.Value = dataRow["WaiveAmount"];

                        SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@USR_CRE", SqlDbType.NVarChar);
                        sqlParameter3.Value = dataRow["SubmitBy"];

                        SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CRE_IP_ADDR", SqlDbType.NVarChar);
                        sqlParameter4.Value = ipAdd;

                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentException(ex.Message);
                    }
                    finally
                    {
                        myconn.Close();
                    }
                }
                if (saveaction == SaveAction.Reject)
                {
                    localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM dbo.LateChargesWaive");
                }

                WaiveLateCharges.strErrorGenTicket = "null";

                if (WaiveLateCharges.strErrorGenTicket == "null")
                {
                    localdbSetting.Commit();
                }
                else
                {
                    localdbSetting.Rollback();
                    throw new ArgumentException(WaiveLateCharges.strErrorGenTicket);
                }
            }
            catch (SqlException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                localdbSetting.EndTransaction();
            }
        }
    }
}