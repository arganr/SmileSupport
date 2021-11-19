using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Finance.SPD
{
    public class PerjalananDinasSql: PerjalananDinasDB
    {
        object obj;
        bool bIsNew = false;

        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM [dbo].[trxPerjalananDinas] ORDER BY DocDate DESC", true);

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
            DataTable myBudgetTable = new DataTable();
            DataTable myApprovalTable = new DataTable();

            string sSQLHeader = "SELECT * FROM dbo.trxPerjalananDinas WHERE DocKey=@DocKey";
            string sSQLDetail = "SELECT * FROM dbo.trxPerjalananDinasDetail WHERE DocKey=@DocKey";
            string sSQLDetailBudget = "SELECT * FROM dbo.trxPerjalananDinasDetailBudget WHERE DocKey=@DocKey";
            string sSQLApproval = "SELECT * FROM dbo.trxPerjalananDinasApprovalList WHERE DocKey=@DocKey";
            
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.Int);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }

            using (SqlCommand cmddetail = new SqlCommand(sSQLDetail, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmddetail);
                cmddetail.Parameters.Add("@DocKey", SqlDbType.Int);
                cmddetail.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myDetailTable);
            }

            using (SqlCommand cmddetailbudget = new SqlCommand(sSQLDetailBudget, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmddetailbudget);
                cmddetailbudget.Parameters.Add("@DocKey", SqlDbType.Int);
                cmddetailbudget.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myBudgetTable);
            }

            using (SqlCommand cmdapproval = new SqlCommand(sSQLApproval, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdapproval);
                cmdapproval.Parameters.Add("@DocKey", SqlDbType.Int);
                cmdapproval.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myApprovalTable);
            }

            myHeaderTable.TableName = "Header";
            myDetailTable.TableName = "Detail";
            myBudgetTable.TableName = "Budget";
            myApprovalTable.TableName = "Approval";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;

            DataColumn[] keyDetail = new DataColumn[1];
            keyDetail[0] = myDetailTable.Columns["DtlKey"];
            myDetailTable.PrimaryKey = keyDetail;

            DataColumn[] keyDetailBudget = new DataColumn[1];
            keyDetailBudget[0] = myBudgetTable.Columns["DtlKey"];
            myBudgetTable.PrimaryKey = keyDetailBudget;

            DataColumn[] keyApproval = new DataColumn[1];
            keyApproval[0] = myApprovalTable.Columns["DtlKey"];
            myApprovalTable.PrimaryKey = keyApproval;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myDetailTable);
            dataSet.Tables.Add(myBudgetTable);
            dataSet.Tables.Add(myApprovalTable);

            return dataSet;
        }

        protected override void SaveData(PerjalananDinasEntity SPDEntity, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(localdbSetting.ConnectionString);
            DateTime Mydate = myDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                localdbSetting.StartTransaction();
               
                if (saveaction == SaveAction.Save && dataRow["DocNo"].ToString().ToUpper().Contains("NEW"))
                {
                    bIsNew = true;
                    dataRow["DocDate"] = Mydate;
                    dataRow["Status"] = "NEED APPROVAL";

                    DataRow[] myrowDocNo = localdbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='SPD'", "", DataViewRowState.CurrentRows);
                    if (myrowDocNo != null)
                    {
                        try
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myDBSetting.GetServerTime());
                        }
                        catch (Exception ex)
                        {
                            localdbSetting.Rollback();
                            throw new ArgumentException("Memo tidak bisa di save cabang / CRM Code belum diregistrasi pada master Document Number. Silahkan hubungi sistem Administrator.", ex);
                        }
                        localdbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType='SPD'");
                    }
                }
                if (saveaction == SaveAction.Save)
                {
                    ClearSPDDetail(SPDEntity, saveaction);
                    SaveDataDetail(ds, saveaction);

                    ClearSPDBudget(SPDEntity, saveaction);
                    SaveDataDetailBudget(ds, saveaction);

                    if (bIsNew)
                    {
                        SaveDataApproval(ds, saveaction);
                    }
                }
                localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM dbo.trxPerjalananDinas");

                if (saveaction == SaveAction.Approve || saveaction == SaveAction.Reject)
                {
                    
                }

                SPDEntity.strErrorGenTicket = "null";

                if (SPDEntity.strErrorGenTicket == "null")
                {
                    localdbSetting.Commit();
                }
                else
                {
                    localdbSetting.Rollback();
                    throw new ArgumentException(SPDEntity.strErrorGenTicket);
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

        protected override void SaveDataDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Detail"].Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[trxPerjalananDinasDetail] (DtlKey, DocKey, TypeSPD, StartDate, EndDate, JumlahHari, Kendaraan, Remarks) VALUES (@DtlKey, @DocKey, @TypeSPD, @StartDate, @EndDate, @JumlahHari, @Kendaraan, @Remarks)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DtlKey", SqlDbType.Int);
                    sqlParameter1.Value = dataRow.Field<int>("DtlKey");
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                    sqlParameter2.Value = dataRow.Field<int>("DocKey");
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TypeSPD", SqlDbType.VarChar);
                    sqlParameter3.Value = dataRow.Field<string>("TypeSPD");
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@StartDate", SqlDbType.DateTime);
                    sqlParameter4.Value = dataRow.Field<DateTime>("StartDate");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@EndDate", SqlDbType.DateTime);
                    sqlParameter5.Value = dataRow.Field<DateTime>("EndDate");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@JumlahHari", SqlDbType.Int);
                    sqlParameter6.Value = dataRow.Field<int>("JumlahHari");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Kendaraan", SqlDbType.VarChar, 50);
                    sqlParameter7.Value = dataRow.Field<string>("Kendaraan");
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Remarks", SqlDbType.VarChar, 50);
                    sqlParameter8.Value = dataRow.Field<string>("Remarks");
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

        protected override void SaveDataDetailBudget(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Detail"].Rows)
                {
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[trxPerjalananDinasDetailBudget] (DtlKey, DocKey, Seq, TypeSPD, TypeBudget, BudgetDesc, BudgetAmount) VALUES (@DtlKey, @DocKey, @Seq, @TypeSPD, @TypeBudget, @BudgetDesc, @BudgetAmount)");
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
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@TypeSPD", SqlDbType.VarChar, 50);
                    sqlParameter4.Value = dataRow.Field<string>("TypeSPD");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@TypeBudget", SqlDbType.VarChar, 50);
                    sqlParameter5.Value = dataRow.Field<string>("TypeBudget");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@BudgetDesc", SqlDbType.VarChar, 50);
                    sqlParameter6.Value = dataRow.Field<string>("BudgetDesc");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@BudgetAmount", SqlDbType.Decimal);
                    sqlParameter7.Value = dataRow.Field<decimal>("BudgetAmount");
                    sqlParameter7.Direction = ParameterDirection.Input;

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

        private void ClearSPDDetail(PerjalananDinasEntity SPDEntity, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[trxPerjalananDinasDetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = SPDEntity.DocKey;
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

        private void ClearSPDBudget(PerjalananDinasEntity SPDEntity, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[trxPerjalananDinasDetailBudget] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = SPDEntity.DocKey;
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

        protected override void SaveDataApproval(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Approval"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[trxPerjalananDinasApprovalList] (DtlKey, DocKey, Seq, NIK, Nama, Jabatan, IsDecision, DecisionState, DecisionDate, DecisionNote, Email) VALUES (@DtlKey, @DocKey, @Seq, @NIK, @Nama, @Jabatan, @IsDecision, @DecisionState, @DecisionDate, @DecisionNote, @Email)");
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
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@NIK", SqlDbType.NVarChar, 20);
                    sqlParameter4.Value = dataRow.Field<string>("NIK");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@Nama", SqlDbType.NVarChar, 100);
                    sqlParameter5.Value = dataRow.Field<string>("Nama");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Jabatan", SqlDbType.NVarChar, 100);
                    sqlParameter6.Value = dataRow.Field<string>("Jabatan");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@IsDecision", SqlDbType.NVarChar, 1);
                    sqlParameter7.Value = "F";
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@DecisionState", SqlDbType.NVarChar, 1);
                    sqlParameter8.Value = "";
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@DecisionDate", SqlDbType.DateTime);
                    sqlParameter9.Value = DBNull.Value;
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@DecisionNote", SqlDbType.NVarChar, 250);
                    sqlParameter10.Value = "";
                    sqlParameter10.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 100);
                    obj = myDBSetting.ExecuteScalar("SELECT Email FROM SYS_TBLEMPLOYEE WHERE CODE=?", dataRow.Field<string>("NIK"));
                    if (obj != null || obj != DBNull.Value)
                    { sqlParameter11.Value = obj; }
                    else
                    { sqlParameter11.Value = "branchsupport.mncleasing@mncgroup.com"; }

                    if (dataRow.Field<string>("NIK") == "1704010")
                    { sqlParameter11.Value += "; khristiana.b@mncgroup.com"; }
                    if (dataRow.Field<string>("NIK") == "1907003")
                    { sqlParameter11.Value += "; khristiana.b@mncgroup.com"; }
                    if (dataRow.Field<string>("NIK") == "U0106546")
                    { sqlParameter11.Value += "; khristiana.b@mncgroup.com"; }
                    if (dataRow.Field<string>("NIK") == "1906024")
                    { sqlParameter11.Value += "; khristiana.b@mncgroup.com"; }

                    sqlParameter11.Direction = ParameterDirection.Input;
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
    }
}