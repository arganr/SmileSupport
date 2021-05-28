using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Insurance
{
    public class PolisAsuransiFASql : PolisAsuransiFADB
    {
        object obj = null;
        private string sQuery = "";
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myDetailTable = new DataTable();

            string sSQLHeader = "SELECT * FROM [dbo].[PolisAsuransiFA] WHERE DocKey=@DocKey";
            string sSQLDetail = "SELECT * FROM [dbo].[PolisAsuransiFADetail] WHERE DocKey=@DocKey ORDER BY Seq";
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            using (SqlCommand cmdlines = new SqlCommand(sSQLDetail, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdlines);
                cmdlines.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdlines.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myDetailTable);
            }

            myHeaderTable.TableName = "Header";
            myDetailTable.TableName = "Detail";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;
            DataColumn[] keyLines = new DataColumn[1];
            keyLines[0] = myDetailTable.Columns["DtlKey"];
            myDetailTable.PrimaryKey = keyLines;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myDetailTable);
            dataSet.Relations.Add("rlDetail", myHeaderTable.Columns["DocKey"], myDetailTable.Columns["DocKey"]);
            return dataSet;
        }
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[PolisAsuransiFA] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocKey DESC", true);
            }
            else
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[PolisAsuransiFA] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocKey DESC", true);
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadMaskapai()
        {
            myMaskapaiTable.Clear();

            sQuery = "SELECT * FROM [dbo].[InsuranceMaskapai] WHERE IsActive='1'";

            myLocalDBSetting.LoadDataTable(myMaskapaiTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myMaskapaiTable.Columns["Code"];
            myMaskapaiTable.PrimaryKey = keyHeader;
            return myMaskapaiTable;
        }
        public override DataTable LoadCoverage()
        {
            myCoverageTable.Clear();

            sQuery = "SELECT * FROM [dbo].[InsuranceCoverage] WHERE IsActive='1'";

            myLocalDBSetting.LoadDataTable(myCoverageTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myCoverageTable.Columns["Code"];
            myCoverageTable.PrimaryKey = keyHeader;
            return myCoverageTable;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                dbSetting.ExecuteNonQuery("DELETE FROM [dbo].[PolisAsuransiFA] WHERE DocKey=?", (object)headerid);
                dbSetting.Commit();

            }
            catch (SqlException ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                dbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                dbSetting.EndTransaction();
            }
        }
        protected override void SaveData(PolisAsuransiFAEntity PolisAsuransiFA, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlDBSetting dbsetting = this.myDBSetting.StartTransaction();
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(localdbSetting.ConnectionString);
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                localdbSetting.StartTransaction();
                if (saveaction == SaveAction.Save)
                {
                    if (PolisAsuransiFA.DocKey != null)
                    {
                        ClearDetail(PolisAsuransiFA, saveaction);
                    }
                    localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[PolisAsuransiFA]");
                    SaveDetail(ds, saveaction);
                    PostToSmile(ds, saveaction, userID, userName);
                }
                PolisAsuransiFA.strErrorGenPolisAsuransiFA = "null";

                if (PolisAsuransiFA.strErrorGenPolisAsuransiFA == "null")
                {
                    dbsetting.Commit();
                    localdbSetting.Commit();
                }
                else
                {
                    dbsetting.Rollback();
                    localdbSetting.Rollback();
                    throw new ArgumentException(PolisAsuransiFA.strErrorGenPolisAsuransiFA);
                }
            }
            catch (SqlException ex)
            {
                dbsetting.Rollback();
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                dbsetting.Rollback();
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                dbsetting.Rollback();
                localdbSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                dbsetting.EndTransaction();
                localdbSetting.EndTransaction();
            }
        }
        protected override void SaveDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Detail"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[PolisAsuransiFADetail] (DtlKey, DocKey, Seq, Maskapai, NoPolis, StartDate, EndDate, Coverage) VALUES (@DtlKey, @DocKey, @Seq, @Maskapai, @NoPolis, @StartDate, @EndDate, @Coverage)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DtlKey", SqlDbType.Int);
                    sqlParameter1.Value = dataRow.Field<int>("DtlKey");
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                    sqlParameter2.Value = dataRow.Field<int>("DocKey");
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
                    sqlParameter3.Value = dataRow.Field<int>("Seq");
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Maskapai", SqlDbType.NVarChar, 150);
                    sqlParameter4.Value = dataRow.Field<string>("Maskapai");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@NoPolis", SqlDbType.NVarChar, 100);
                    sqlParameter5.Value = dataRow.Field<string>("NoPolis");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    sqlParameter6.Value = dataRow.Field<DateTime>("StartDate");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
                    sqlParameter7.Value = dataRow.Field<DateTime>("EndDate");
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Coverage", SqlDbType.NVarChar, 100);
                    sqlParameter8.Value = dataRow.Field<string>("Coverage");
                    sqlParameter8.Direction = ParameterDirection.Input;
                    sqlCommand.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
        }
        protected override void ClearDetail(PolisAsuransiFAEntity PolisAsuransiFA, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[PolisAsuransiFADetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = PolisAsuransiFA.DocKey;
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
                myconn.Dispose();
            }
        }
        protected override void PostToSmile(DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            DataRow[] detaildataRow = ds.Tables["Detail"].Select("", "Seq DESC");
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"SP_SMILESUPPORT_INSURANCE_INSERT");
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

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@user_id", SqlDbType.VarChar, 10);
                sqlParameter1.Value = userID;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@ip_address", SqlDbType.VarChar, 15);
                sqlParameter2.Value = ipAdd;

                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@ast_code", SqlDbType.VarChar, 20);
                sqlParameter3.Value = dataRow["DocNo"];

                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@maskapai", SqlDbType.VarChar, 100);
                sqlParameter4.Value = detaildataRow[0]["Maskapai"];

                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@no_polis", SqlDbType.VarChar, 30);
                sqlParameter5.Value = detaildataRow[0]["NoPolis"];

                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@start_date", SqlDbType.Date);
                sqlParameter6.Value = Convert.ToDateTime(detaildataRow[0]["StartDate"]);

                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@end_date", SqlDbType.Date);
                sqlParameter7.Value = Convert.ToDateTime(detaildataRow[0]["EndDate"]);

                SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@coverage", SqlDbType.VarChar, 100);
                sqlParameter8.Value = detaildataRow[0]["Coverage"];

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
    }
}