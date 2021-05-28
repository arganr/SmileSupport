using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.Mitra
{
    public class MitraSql : MitraDB
    {
        object obj = null;
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[Mitra] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.MCode DESC", true);
            }
            else
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[Mitra] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.MCode DESC", true);
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["MKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myBankTable = new DataTable();
            DataTable myUploadDocTable = new DataTable();

            string sSQLHeader = "SELECT * FROM [dbo].[Mitra] WHERE MKey=@MKey";
            string sSQLBank = "SELECT * FROM [dbo].[MitraBankDetail] WHERE MKey=@MKey ORDER BY BankName";
            string sSQLUploadDoc = "SELECT * FROM [dbo].[MitraDocumentDetail] WHERE MKey=@MKey ORDER BY DtlUploadKey";
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@MKey", SqlDbType.BigInt);
                cmdheader.Parameters["@MKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            using (SqlCommand cmdlines = new SqlCommand(sSQLBank, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdlines);
                cmdlines.Parameters.Add("@MKey", SqlDbType.BigInt);
                cmdlines.Parameters["@MKey"].Value = headerid;
                adapter.Fill(myBankTable);
            }
            using (SqlCommand cmduploaddoc = new SqlCommand(sSQLUploadDoc, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmduploaddoc);
                cmduploaddoc.Parameters.Add("@MKey", SqlDbType.BigInt);
                cmduploaddoc.Parameters["@MKey"].Value = headerid;
                adapter.Fill(myUploadDocTable);
            }

            myHeaderTable.TableName = "Header";
            myBankTable.TableName = "BankDetail";
            myUploadDocTable.TableName = "UploadDocDetail";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["MKey"];
            myHeaderTable.PrimaryKey = keyHeader;
            DataColumn[] keyLines = new DataColumn[1];
            keyLines[0] = myBankTable.Columns["MBankKey"];
            myBankTable.PrimaryKey = keyLines;
            DataColumn[] keyUploadDocs = new DataColumn[1];
            keyUploadDocs[0] = myUploadDocTable.Columns["DtlUploadKey"];
            myUploadDocTable.PrimaryKey = keyUploadDocs;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myBankTable);
            dataSet.Tables.Add(myUploadDocTable);
            dataSet.Relations.Add("rlMitraBankDetail", myHeaderTable.Columns["MKey"], myBankTable.Columns["MKey"]);
            dataSet.Relations.Add("rlMitraUploadDocDetail", myHeaderTable.Columns["MKey"], myUploadDocTable.Columns["MKey"]);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                dbSetting.ExecuteNonQuery("DELETE FROM [dbo].[Mitra] WHERE MKey=?", (object)headerid);
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
        protected override void SaveData(MitraEntity Mitra, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlDBSetting dbsetting = this.myDBSetting.StartTransaction();
            SqlLocalDBSetting localdbSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(localdbSetting.ConnectionString);
            DateTime MyLastApproveDate = myLocalDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            string strPassword = Convert.ToDateTime(dataRow["TanggalLahir"]).Date.ToString("ddMMyyyy");
            try
            {
                localdbSetting.StartTransaction();
                if (saveaction == SaveAction.Save)
                {
                    if (dataRow["MCode"].ToString().ToUpper() == "NEW")
                    {
                        DataRow[] myrowDocNo = localdbSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='MTR'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["MCode"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myLocalDBSetting.GetServerTime());
                            localdbSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType='MTR'");
                        }
                        dataRow["Password"] = MD5Hash(strPassword);
                    }

                    obj = dbsetting.ExecuteScalar("select top 1 b.C_NAME from SYS_TBLEMPLOYEE a inner join SYS_COMPANY b on a.C_CODE=b.C_CODE WHERE a.CODE=?", dataRow["CreatedBy"].ToString());
                    if (obj != null && obj != DBNull.Value)
                    {
                        dataRow["Branch"] = obj;
                    }
                    
                    if (Mitra.MKey != null)
                    {
                        ClearBankDetail(Mitra, saveaction);
                    }
                    localdbSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[Mitra]");
                    SaveBankDetail(ds, saveaction);
                }
                Mitra.strErrorGenMitra = "null";

                if (Mitra.strErrorGenMitra == "null")
                {
                    dbsetting.Commit();
                    localdbSetting.Commit();
                }
                else
                {
                    dbsetting.Rollback();
                    localdbSetting.Rollback();
                    throw new ArgumentException(Mitra.strErrorGenMitra);
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
        protected override void SaveBankDetail(DataSet ds, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {
                foreach (DataRow dataRow in ds.Tables["BankDetail"].Rows)
                {

                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO [dbo].[MitraBankDetail] (MBankKey, MKey, Seq, BankName, BankBranch, BankAccNo, BankAccName) VALUES (@MBankKey, @MKey, @Seq, @BankName, @BankBranch, @BankAccNo, @BankAccName)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@MBankKey", SqlDbType.Int);
                    sqlParameter1.Value = dataRow.Field<int>("MBankKey");
                    sqlParameter1.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@MKey", SqlDbType.Int);
                    sqlParameter2.Value = dataRow.Field<int>("MKey");
                    sqlParameter2.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@BankName", SqlDbType.NVarChar, 50);
                    sqlParameter3.Value = dataRow.Field<string>("BankName");
                    sqlParameter3.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@BankBranch", SqlDbType.NVarChar, 50);
                    sqlParameter4.Value = dataRow.Field<string>("BankBranch");
                    sqlParameter4.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@BankAccNo", SqlDbType.NVarChar, 50);
                    sqlParameter5.Value = dataRow.Field<string>("BankAccNo");
                    sqlParameter5.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@BankAccName", SqlDbType.NVarChar, 100);
                    sqlParameter6.Value = dataRow.Field<string>("BankAccName");
                    sqlParameter6.Direction = ParameterDirection.Input;
                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Seq", SqlDbType.Int);
                    sqlParameter7.Value = dataRow.Field<int>("Seq");
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
        protected override void ClearBankDetail(MitraEntity Mitra, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[MitraBankDetail] WHERE MKey=@MKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@MKey", SqlDbType.Int);
                sqlParameter1.Value = Mitra.MKey;
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
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}