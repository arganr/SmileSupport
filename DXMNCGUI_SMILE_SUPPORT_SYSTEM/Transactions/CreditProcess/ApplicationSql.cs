using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess
{
    public class ApplicationSql : ApplicationDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                //myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM [dbo].[Application] ORDER BY DocDate DESC", true);
                //myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + B.USER_NAME AS FULLNAME FROM [dbo].[Application] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocDate DESC", true);
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[Application] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocDate DESC", true);
            }
            else
            {
                //myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM [dbo].[Application] ORDER BY DocDate DESC", true);
                //myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + B.USER_NAME AS FULLNAME FROM [dbo].[Application] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocDate DESC", true);
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[Application] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocDate DESC", true);
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadBrowseTableHistory(string sDocNo)
        {
            myBrowseTableHistory.Clear();
            myLocalDBSetting.LoadDataTable(myBrowseTableHistory, "SELECT B.Status, A.DocNo, B.TransDate, B.TransBy, B.DiffTime, B.FromStatus FROM [dbo].[Application] A INNER JOIN [dbo].[ApplicationHistory] B ON A.DocKey = B.DocKey  WHERE A.DocNo=? ORDER BY B.TransDate Desc", true, sDocNo);
            DataColumn[] KeyDetail = new DataColumn[1];
            KeyDetail[0] = myBrowseTableHistory.Columns["DtlKey"];
            myBrowseTableHistory.PrimaryKey = KeyDetail;
            return myBrowseTableHistory;
        }
        public override DataTable LoadBrowseTableComment(string sDocNo)
        {
            myBrowseTableComment.Clear();
            myLocalDBSetting.LoadDataTable(myBrowseTableComment, "SELECT * FROM [dbo].[ApplicationCommentHistory]  WHERE DocNo=? ORDER BY CommentDate Desc", true, sDocNo);
            DataColumn[] KeyDetail = new DataColumn[1];
            KeyDetail[0] = myBrowseTableComment.Columns["DtlKey"];
            myBrowseTableComment.PrimaryKey = KeyDetail;
            return myBrowseTableComment;
        }
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myDetailTable = new DataTable();
            DataTable myCommentTable = new DataTable();
            string sSQLHeader = "SELECT * FROM [dbo].[Application] WHERE DocKey=@DocKey";
            string sSQLLines = "SELECT * FROM [dbo].[ApplicationDetail] WHERE DocKey=@DocKey ORDER BY Seq";
            //string sSQLComment = "SELECT * FROM [dbo].[ApplicationCommentHistory] WHERE DocKey=@DocKey ORDER BY CommentDate";
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            using (SqlCommand cmdlines = new SqlCommand(sSQLLines, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdlines);
                cmdlines.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdlines.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myDetailTable);
            }
            //using (SqlCommand cmdlines = new SqlCommand(sSQLComment, myconn))
            //{
            //    SqlDataAdapter adapter = new SqlDataAdapter(cmdlines);
            //    cmdlines.Parameters.Add("@DocKey", SqlDbType.BigInt);
            //    cmdlines.Parameters["@DocKey"].Value = headerid;
            //    adapter.Fill(myCommentTable);
            //}
            myHeaderTable.TableName = "Header";
            myDetailTable.TableName = "Lines";
            //myCommentTable.TableName = "Comment";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;
            DataColumn[] keyLines = new DataColumn[1];
            keyLines[0] = myDetailTable.Columns["DtlKey"];
            myDetailTable.PrimaryKey = keyLines;
            //DataColumn[] KeyComment = new DataColumn[1];
            //keyLines[0] = myDetailTable.Columns["DtlKey"];
            //myCommentTable.PrimaryKey = KeyComment;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myDetailTable);
            //dataSet.Tables.Add(myCommentTable);
            dataSet.Relations.Add("rlApplicationDetail", myHeaderTable.Columns["DocKey"], myDetailTable.Columns["DocKey"]);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                dbSetting.ExecuteNonQuery("DELETE FROM [dbo].[Application] WHERE DocKey=?", (object)headerid);
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
        protected override void SaveData(ApplicationEntity Application, DataSet ds, string strDocName, SaveAction saveaction, string strUpline, string userID, string userName)
        {
            string myLastState = "";
            SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(dbSetting.ConnectionString);
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            DateTime MyLastApproveDate = myLocalDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                dbSetting.StartTransaction();
                if (saveaction == SaveAction.Cancel)
                {
                    dataRow["Cancelled"] = "T";
                    dataRow["CancelledDateTime"] = Mydate;

                    myLastState = dataRow["Status"].ToString();
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["Status"] = "CANCELLED";
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    Application.Status = "CANCELLED";
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.UnCancel)
                {
                    dataRow["Cancelled"] = "F";
                    dataRow["CancelledDateTime"] = DBNull.Value;
                }
                if (saveaction == SaveAction.Reject)
                {
                    myLastState = dataRow["Status"].ToString();
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["Status"] = "REJECTED";
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    Application.Status = "REJECTED";
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.Save)
                {
                    if (dataRow["Submit"].ToString().ToUpper() == "T")
                    {
                        if (dataRow["Status"].ToString() == "PROSPECT")
                        {
                            object obj = null;
                            obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                            if (obj != null && obj != DBNull.Value)
                            {
                                MyLastApproveDate = Convert.ToDateTime(obj);
                            }

                            myLastState = "PROSPECT";
                            dataRow["Status"] = "BRH MGR";
                            dataRow["SubmitBy"] = userID;
                            dataRow["SubmitDateTime"] = Mydate;
                        }
                    }
                    if (dataRow["Submit"].ToString().ToUpper() == "F")
                    {
                        dataRow["Status"] = "PROSPECT";
                        dataRow["SubmitBy"] = "";
                        dataRow["SubmitDateTime"] = DBNull.Value;
                    }
                    if (dataRow["DocNo"].ToString().ToUpper() == "NEW")
                    {
                        dataRow["DocDate"] = Mydate;
                        DataRow[] myrowDocNo = dbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='APP'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myLocalDBSetting.GetServerTime());
                            dbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType=?", strDocName);
                        }
                    }
                    if (Application.DocKey != null)
                    {
                        ClearDetail(Application, saveaction);
                    }
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    SaveDetail(ds, saveaction);
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.Approve)
                {
                    myLastState = dataRow["Status"].ToString();
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["Status"] = GetNextStatus(Application.Status.ToString());
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.Return)
                {
                    myLastState = dataRow["Status"].ToString();
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["Status"] = GetPreviousStatus(Application.Status.ToString());
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.OnHold)
                {
                    myLastState = dataRow["Status"].ToString();
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["OnHold"] = "T";
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.Release)
                {
                    myLastState = "RELEASE";
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["OnHold"] = "F";
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                if (saveaction == SaveAction.Cam)
                {
                    myLastState = dataRow["Status"].ToString();
                    object obj = null;
                    obj = LocalDBSetting.ExecuteScalar("SELECT TOP 1 TransDate FROM [dbo].[ApplicationHistory] WHERE DocKey=? ORDER BY TransDate DESC", Application.DocKey);
                    if (obj != null && obj != DBNull.Value)
                    {
                        MyLastApproveDate = Convert.ToDateTime(obj);
                    }
                    dataRow["Status"] = "AM";
                    dbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Application]");
                    dbSetting.SimpleSaveDataTable(ds.Tables["Lines"], "SELECT * FROM [dbo].[ApplicationDetail]");
                    Application.Status = "AM";
                    SaveApplicationHistory(Application, ds, saveaction, userID, userName, MyLastApproveDate, myLastState);
                }
                Application.strErrorGenTicket = "null";

                if (Application.strErrorGenTicket == "null")
                {
                    dbSetting.Commit();
                }
                else
                {
                    dbSetting.Rollback();
                    throw new ArgumentException(Application.strErrorGenTicket);
                }
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
        protected override void SaveDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Lines"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationDetail] (DtlKey, DocKey, Seq, Condition, ItemDescription, Year, UnitPrice, Qty, SubTotal, AssetTypeDetail) VALUES (@DtlKey, @DocKey, @Seq, @Condition, @ItemDescription ,@Year, @UnitPrice, @Qty, @SubTotal, @AssetTypeDetail)");
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
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@Condition", SqlDbType.NVarChar, 50);
                    sqlParameter4.Value = dataRow.Field<string>("Condition");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@ItemDescription", SqlDbType.NVarChar);
                    sqlParameter5.Value = dataRow.Field<string>("ItemDescription");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@Year", SqlDbType.Float);
                    sqlParameter6.Value = dataRow.Field<Decimal>("Year");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@UnitPrice", SqlDbType.Float);
                    sqlParameter7.Value = dataRow.Field<Decimal>("UnitPrice");
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@Qty", SqlDbType.Float);
                    sqlParameter8.Value = dataRow.Field<Decimal>("Qty");
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@SubTotal", SqlDbType.Float);
                    sqlParameter9.Value = dataRow.Field<Decimal>("SubTotal");
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@AssetTypeDetail", SqlDbType.NVarChar);
                    sqlParameter10.Value = dataRow.Field<string>("AssetTypeDetail");
                    sqlParameter10.Direction = ParameterDirection.Input;

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
        protected override void SaveApplicationHistory(ApplicationEntity Application, DataSet ds, SaveAction saveaction, string userID, string userName, DateTime myLastApprove, string myLastState)
        {
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            int imyDiffTime;

            imyDiffTime = Convert.ToInt32((Mydate - myLastApprove).TotalMinutes);

            if (saveaction == SaveAction.OnHold)
                Application.Status = "ON HOLD";

            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationHistory] (DocKey, Status, TransByID, TransBy, TransDate, DiffTime, FromStatus) VALUES (@DocKey, @Status, @TransByID, @TransBy, @TransDate ,@DiffTime, @FromStatus)");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = Application.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
                sqlParameter2.Value = Application.Status;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TransByID", SqlDbType.NVarChar, 20);
                sqlParameter3.Value = userID;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@TransBy", SqlDbType.NVarChar, 20);
                sqlParameter4.Value = userName;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@TransDate", SqlDbType.DateTime);
                sqlParameter5.Value = Mydate;
                sqlParameter5.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@DiffTime", SqlDbType.Int);
                sqlParameter6.Value = imyDiffTime;
                sqlParameter6.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@FromStatus", SqlDbType.NVarChar, 50);
                sqlParameter7.Value = myLastState;
                sqlParameter7.Direction = ParameterDirection.Input;

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
        protected override void SaveComment(ApplicationEntity Application, SaveAction saveaction, string userFullName, string userComment, DateTime distDate)
        {
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationCommentHistory] (SourceDocKey, DocNo, CommentBy, CommentNote, CommentDate, DistDate) VALUES (@SourceDocKey, @DocNo, @CommentBy, @CommentNote, @CommentDate, @distDate)");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@SourceDocKey", SqlDbType.Int);
                sqlParameter1.Value = Application.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.NVarChar, 20);
                sqlParameter2.Value = Application.DocNo;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@CommentBy", SqlDbType.NVarChar, 20);
                sqlParameter3.Value = userFullName;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CommentNote", SqlDbType.NVarChar);
                sqlParameter4.Value = userComment;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CommentDate", SqlDbType.DateTime);
                sqlParameter5.Value = Mydate;
                sqlParameter5.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@distDate", SqlDbType.DateTime);
                
                if(distDate.Date != null)
                {
                    sqlParameter6.Value = distDate;
                }
                else{
                    sqlParameter6.Value = DBNull.Value;
                }
                sqlParameter6.Direction = ParameterDirection.Input;
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
        protected override void SaveAssign(ApplicationEntity Application, SaveAction saveaction, string userFullName, string userAssign)
        {
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ApplicationCommentHistory] (SourceDocKey, DocNo, CommentBy, CommentNote, CommentDate) VALUES (@SourceDocKey, @DocNo, @CommentBy, @CommentNote, @CommentDate)");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@SourceDocKey", SqlDbType.Int);
                sqlParameter1.Value = Application.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@DocNo", SqlDbType.NVarChar, 20);
                sqlParameter2.Value = Application.DocNo;
                sqlParameter2.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@CommentBy", SqlDbType.NVarChar, 20);
                sqlParameter3.Value = userFullName;
                sqlParameter3.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CommentNote", SqlDbType.NVarChar);
                sqlParameter4.Value = "Assign credit process to : " + userAssign;
                sqlParameter4.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CommentDate", SqlDbType.DateTime);
                sqlParameter5.Value = Mydate;
                sqlParameter5.Direction = ParameterDirection.Input;
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
        protected override void DeleteWorkingList(ApplicationEntity Application, string myID)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE WorkList WHERE Source=@Source AND NeedApproveByID=@NeedApproveByID");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@Source", SqlDbType.NVarChar, 100);
                sqlParameter1.Value = Application.DocKey;
                sqlParameter1.Direction = ParameterDirection.Input;
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@NeedApproveByID", SqlDbType.NVarChar, 20);
                sqlParameter2.Value = myID;
                sqlParameter2.Direction = ParameterDirection.Input;
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
        protected override void UpdateWorkingList()
        {
            try
            {
                LocalDBSetting.ExecuteNonQuery("UPDATE dbo.WorkList SET WorkList.Source = (SELECT DocKey FROM dbo.ChangeDataList WHERE WorkList.TicketNo=ChangeDataList.TicketNo)");
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
            }
        }
        protected override string GetNextStatus(string myLastStatus)
        {
            string myNextStatus = "";
            try
            {
                object obj = null;
                obj = myLocalDBSetting.ExecuteScalar("SELECT A.StateDescription FROM [dbo].[ApplicationWorkflowScheme] A WHERE A.Seq = (SELECT Seq + 1 FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=?)", myLastStatus);
                if (obj != null && obj != DBNull.Value)
                {
                    myNextStatus = obj.ToString();
                }
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

            }
            return myNextStatus;
        }
        protected override string GetPreviousStatus(string myLastStatus)
        {
            string myNextStatus = "";
            try
            {
                object obj = null;
                obj = myLocalDBSetting.ExecuteScalar("SELECT A.StateDescription FROM [dbo].[ApplicationWorkflowScheme] A WHERE A.Seq = (SELECT Seq - 1 FROM [dbo].[ApplicationWorkflowScheme] WHERE StateDescription=?)", myLastStatus);
                if (obj != null && obj != DBNull.Value)
                {
                    myNextStatus = obj.ToString();
                }
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

            }
            return myNextStatus;
        }
        protected override void ClearDetail(ApplicationEntity Application, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[ApplicationDetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = Application.DocKey;
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
    }
}