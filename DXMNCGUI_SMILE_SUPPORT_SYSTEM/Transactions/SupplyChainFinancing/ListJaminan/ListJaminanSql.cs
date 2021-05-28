using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.SupplyChainFinancing.ListJaminan
{
    public class ListJaminanSql : ListJaminanDB
    {
        object obj = null;
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                myDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[ListJaminan] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocNo DESC", true);
            }
            else
            {
                myDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[ListJaminan] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocNo DESC", true);
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myDetailTable = new DataTable();
            DataTable myUploadDocTable = new DataTable();

            string sSQLHeader = "SELECT * FROM [dbo].[ListJaminan] WHERE DocKey=@DocKey";
            string sSQLDetail = "SELECT * FROM [dbo].[ListJaminanDetail] WHERE DocKey=@DocKey ORDER BY Seq";

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
            dataSet.Tables.Add(myUploadDocTable);
            dataSet.Relations.Add("rlDetail", myHeaderTable.Columns["DocKey"], myDetailTable.Columns["DocKey"]);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                dbSetting.ExecuteNonQuery("DELETE FROM [dbo].[ListJaminan] WHERE DocKey=?", (object)headerid);
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

        protected override void SaveData(ListJaminanEntity ListJaminan, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlDBSetting dbsetting = this.myDBSetting.StartTransaction();
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                if (saveaction == SaveAction.Save)
                {
                    if (dataRow["DocNo"].ToString().ToUpper() == "NEW")
                    {
                        DataRow[] myrowDocNo = localdbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='LJ'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), Convert.ToInt32(myrowDocNo[0]["NextNo"]), myLocalDBSetting.GetServerTime());
                            localdbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType='LJ'");
                        }
                    }

                    if (ListJaminan.IsPost.ToString() == "T")
                    {
                        PostToSmile(ListJaminan);
                    }

                    if (ListJaminan.DocKey != null)
                    {
                        ClearDetail(ListJaminan, saveaction);
                    }
                    dbsetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[ListJaminan]");
                    SaveDetail(ds, saveaction);
                }
                ListJaminan.strErrorGenListJaminan = "null";

                if (ListJaminan.strErrorGenListJaminan == "null")
                {
                    dbsetting.Commit();
                    localdbSetting.Commit();
                }
                else
                {
                    dbsetting.Rollback();
                    localdbSetting.Rollback();
                    throw new ArgumentException(ListJaminan.strErrorGenListJaminan);
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
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["Detail"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[ListJaminanDetail] (DtlKey, DocKey, Seq, ItemCode, ItemDesc, ItemCategory, ItemBrand, UOM, Qty, DBP, RBP, DBPSubTotal) VALUES (@DtlKey, @DocKey, @Seq, @ItemCode, @ItemDesc, @ItemCategory, @ItemBrand, @UOM, @Qty, @DBP, @RBP, @DBPSubTotal)");
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
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@ItemCode", SqlDbType.NVarChar, 20);
                    sqlParameter4.Value = DBNull.Value;
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@ItemDesc", SqlDbType.NVarChar, 250);
                    sqlParameter5.Value = dataRow.Field<string>("ItemDesc");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@ItemCategory", SqlDbType.NVarChar, 50);
                    sqlParameter6.Value = dataRow.Field<string>("ItemCategory");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@ItemBrand", SqlDbType.NVarChar, 50);
                    sqlParameter7.Value = dataRow.Field<string>("ItemBrand");
                    sqlParameter7.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@UOM", SqlDbType.NVarChar, 10);
                    sqlParameter8.Value = dataRow.Field<string>("UOM");
                    sqlParameter8.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@Qty", SqlDbType.Decimal);
                    sqlParameter9.Value = dataRow.Field<decimal>("Qty");
                    sqlParameter9.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@DBP", SqlDbType.Decimal);
                    sqlParameter10.Value = dataRow.Field<decimal>("DBP");
                    sqlParameter10.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@RBP", SqlDbType.Decimal);
                    sqlParameter11.Value = dataRow.Field<decimal>("RBP");
                    sqlParameter11.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@DBPSubTotal", SqlDbType.Decimal);
                    sqlParameter12.Value = dataRow.Field<decimal>("DBPSubTotal");
                    sqlParameter12.Direction = ParameterDirection.Input;
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
        protected override void ClearDetail(ListJaminanEntity ListJaminan, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[ListJaminanDetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = ListJaminan.DocKey;
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
        protected void PostToSmile(ListJaminanEntity ListJaminan)
        {
            //SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            SqlConnection myconn = new SqlConnection("Data Source=192.168.1.10\\MGUISVR;Initial Catalog=IFINANCING_GOLIVE; Persist Security Info=True;User ID=mncl;Password=Mncleasing123");
            SqlCommand sqlCommand = new SqlCommand("spMNCL_UpdateDocJaminanFMUInv");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@doc_no", SqlDbType.VarChar, 50);
                sqlParameter1.Value = Convert.ToString(ListJaminan.DocNo);
                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@applino", SqlDbType.VarChar, 50);
                sqlParameter2.Value = Convert.ToString(ListJaminan.RefNo);
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