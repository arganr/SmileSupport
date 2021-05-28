using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.UpdateSLIK
{
    public class UpdateSLIKSql : UpdateSLIKDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[UpdateSLIK] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocDate DESC", true);
            }
            else
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[UpdateSLIK] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.DocDate DESC", true);
            }
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

            string sSQLHeader = "SELECT * FROM [dbo].[UpdateSLIK] WHERE DocKey=@DocKey";
            string sSQLBank = "SELECT * FROM [dbo].[UpdateSLIKDetail] WHERE DocKey=@DocKey ORDER BY Seq";
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            using (SqlCommand cmdlines = new SqlCommand(sSQLBank, myconn))
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
            dataSet.Relations.Add("rlUpdateSLIKDetail", myHeaderTable.Columns["DocKey"], myDetailTable.Columns["DocKey"]);
            return dataSet;
        }
        public override void Delete(long headerid)
        {
            SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction();
            try
            {
                dbSetting.ExecuteNonQuery("DELETE FROM [dbo].[UpdateSLIK] WHERE DocKey=?", (object)headerid);
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
        protected override void SaveData(UpdateSLIKEntity UpdateSLIK, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlLocalDBSetting dblocalSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(dblocalSetting.ConnectionString);
            DateTime MyLastApproveDate = myLocalDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                dblocalSetting.StartTransaction();
                if (saveaction == SaveAction.Save)
                {
                    if (dataRow["DocNo"].ToString().ToUpper() == "NEW")
                    {
                        DataRow[] myrowDocNo = dblocalSetting.GetDataTable("select * from DocNoFormat", false, "").Select("DocType='SLK'", "", DataViewRowState.CurrentRows);
                        if (myrowDocNo != null)
                        {
                            dataRow["DocNo"] = Document.FormatDocumentNo(myrowDocNo[0]["Format"].ToString(), System.Convert.ToInt32(myrowDocNo[0]["NextNo"]), myLocalDBSetting.GetServerTime());
                            dblocalSetting.ExecuteNonQuery("Update DocNoFormat set NextNo=NextNo+1 Where DocType='SLK'");
                        }
                    }
                    if (UpdateSLIK.DocKey != null)
                    {
                        ClearDetail(UpdateSLIK, saveaction);
                    }
                    dblocalSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[UpdateSLIK]");
                    SaveDetail(ds, saveaction);
                    SaveToHistory(UpdateSLIK, userID);
                }
                UpdateSLIK.strErrorGenUpdateSLIK = "null";

                if (UpdateSLIK.strErrorGenUpdateSLIK == "null")
                {
                    dblocalSetting.Commit();
                }
                else
                {
                    dblocalSetting.Rollback();
                    throw new ArgumentException(UpdateSLIK.strErrorGenUpdateSLIK);
                }
            }
            catch (SqlException ex)
            {
                dblocalSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                dblocalSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                dblocalSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                dblocalSetting.EndTransaction();
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

                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[UpdateSLIKDetail] 
                                                                (DtlKey, DocKey, Seq, JenisPembiayaan, PerusahaanPembiayaan, AtasNama, Plafon, BakiDebet, Bunga, TglAkadAwal, TglAwalSisaTenor, TglJatuhTempo, Angsuran, Kolektibilitas, HistoryKolek, AktualOverDue) 
                                                                    VALUES 
                                                                        (@DtlKey, @DocKey, @Seq, @JenisPembiayaan, @PerusahaanPembiayaan, @AtasNama, @Plafon, @BakiDebet, @Bunga, @TglAkadAwal, @TglAwalSisaTenor, @TglJatuhTempo, @Angsuran, @Kolektibilitas, @HistoryKolek, @AktualOverDue)");
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

                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@JenisPembiayaan", SqlDbType.NVarChar, 100);
                    sqlParameter4.Value = dataRow.Field<string>("JenisPembiayaan");
                    sqlParameter4.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@PerusahaanPembiayaan", SqlDbType.NVarChar, 100);
                    sqlParameter5.Value = dataRow.Field<string>("PerusahaanPembiayaan");
                    sqlParameter5.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@AtasNama", SqlDbType.NVarChar, 100);
                    sqlParameter6.Value = dataRow.Field<string>("AtasNama");
                    sqlParameter6.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter7 = sqlCommand.Parameters.Add("@Plafon", SqlDbType.Decimal);
                    sqlParameter7.Value = dataRow.Field<decimal>("Plafon");
                    sqlParameter7.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter8 = sqlCommand.Parameters.Add("@BakiDebet", SqlDbType.Decimal);
                    sqlParameter8.Value = dataRow.Field<decimal>("BakiDebet");
                    sqlParameter8.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter9 = sqlCommand.Parameters.Add("@Bunga", SqlDbType.Decimal);
                    sqlParameter9.Value = dataRow.Field<decimal>("Bunga");
                    sqlParameter9.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter10 = sqlCommand.Parameters.Add("@TglAkadAwal", SqlDbType.DateTime);
                    sqlParameter10.Value = dataRow.Field<DateTime>("TglAkadAwal");
                    sqlParameter10.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter11 = sqlCommand.Parameters.Add("@TglAwalSisaTenor", SqlDbType.DateTime);
                    sqlParameter11.Value = dataRow.Field<DateTime>("TglAwalSisaTenor");
                    sqlParameter11.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter12 = sqlCommand.Parameters.Add("@TglJatuhTempo", SqlDbType.DateTime);
                    sqlParameter12.Value = dataRow.Field<DateTime>("TglJatuhTempo");
                    sqlParameter12.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter13 = sqlCommand.Parameters.Add("@Angsuran", SqlDbType.Decimal);
                    sqlParameter13.Value = dataRow.Field<decimal>("Angsuran");
                    sqlParameter13.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter14 = sqlCommand.Parameters.Add("@Kolektibilitas", SqlDbType.NVarChar, 50);
                    sqlParameter14.Value = dataRow.Field<string>("Kolektibilitas");
                    sqlParameter14.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter15 = sqlCommand.Parameters.Add("@HistoryKolek", SqlDbType.NVarChar, 50);
                    sqlParameter15.Value = dataRow.Field<string>("HistoryKolek");
                    sqlParameter15.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter16 = sqlCommand.Parameters.Add("@AktualOverDue", SqlDbType.NVarChar, 15);
                    sqlParameter16.Value = dataRow.Field<string>("AktualOverDue");
                    sqlParameter16.Direction = ParameterDirection.Input;

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
        protected override void ClearDetail(UpdateSLIKEntity UpdateSLIK, SaveAction saveaction)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            SqlCommand sqlCommand = new SqlCommand("DELETE [dbo].[UpdateSLIKDetail] WHERE DocKey=@DocKey");
            sqlCommand.Connection = myconn;
            try
            {
                myconn.Open();
                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@DocKey", SqlDbType.Int);
                sqlParameter1.Value = UpdateSLIK.DocKey;
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

        protected void SaveToHistory(UpdateSLIKEntity UpdateSLIK, string userID)
        {
            SqlDBSetting dbsetting = this.myDBSetting.StartTransaction();
            SqlConnection myconn = new SqlConnection(dbsetting.ConnectionString);
            myconn.Open();
            SqlTransaction trans = myconn.BeginTransaction();
            try
            {

                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO [dbo].[MNCL_APP_TIME_STAMP] 
                                                                (APPLICNO, STEP, TIME_STAMP, DURATION, CRE_DATE, CRE_BY, NOTES) 
                                                                    VALUES 
                                                                        (@APPLICNO, @STEP, @TIME_STAMP, dbo.GET_DURATION_TIME_STAMP(@APPLICNO), @CRE_DATE, @CRE_BY, @NOTES)");
                    sqlCommand.Connection = myconn;
                    sqlCommand.Transaction = trans;

                    SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@APPLICNO", SqlDbType.NVarChar);
                    sqlParameter1.Value = UpdateSLIK.RefNo;
                    sqlParameter1.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@STEP", SqlDbType.NVarChar);
                    sqlParameter2.Value = "SLIK-CHECKING";
                    sqlParameter2.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@TIME_STAMP", SqlDbType.DateTime);
                    sqlParameter3.Value = dbsetting.GetServerTime();
                    sqlParameter3.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter4 = sqlCommand.Parameters.Add("@CRE_DATE", SqlDbType.DateTime);
                    sqlParameter4.Value = dbsetting.GetServerTime();
                    sqlParameter4.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter5 = sqlCommand.Parameters.Add("@CRE_BY", SqlDbType.NVarChar);
                    sqlParameter5.Value = userID;
                    sqlParameter5.Direction = ParameterDirection.Input;

                    SqlParameter sqlParameter6 = sqlCommand.Parameters.Add("@NOTES", SqlDbType.NVarChar);
                    sqlParameter6.Value = UpdateSLIK.Remark;
                    sqlParameter6.Direction = ParameterDirection.Input;

                    sqlCommand.ExecuteNonQuery();
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