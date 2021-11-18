using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.UpdateNoSPPH
{
    public class UpdateNoSPPHSql : UpdateNoSPPHDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            if (!bViewAll)
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[UpdateSPPHNo] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.CreatedDateTime DESC", true);
            }
            else
            {
                myLocalDBSetting.LoadDataTable(myBrowseTable, "SELECT A.*, A.CreatedBy + ' - ' + ISNULL(B.USER_NAME, 'USER NAME BELUM SINKRON KE SISTEM') AS FULLNAME FROM [dbo].[UpdateSPPHNo] A left join MASTER_USER B on B.USER_ID=A.CreatedBy ORDER BY A.CreatedDateTime DESC", true);
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["DocKey"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        public override DataTable LoadIncentiveTable(string sAgreementNo)
        {
            myIncentiveTable.Clear();
            myDBSetting.LoadDataTable(myIncentiveTable, @"SELECT * FROM INCENTIVE_TRX WHERE LSAGREE =?", false, sAgreementNo);
            return myIncentiveTable;
        }
        public override DataTable LoadAllIncentiveTable()
        {
            myAllIncentiveTable.Clear();
            myDBSetting.LoadDataTable(myAllIncentiveTable, @"select a.ID, c.C_NAME CNAME, a.LSAGREE, b.NAME CUST_NAME, d.NoSPPH, PAY_TO_TYPE, PAY_TO, INCENTIVE_SRC, a.AMOUNT, a.CRE_DATE, e.DESCS Creator, d.DisburseDate 
                                                                from INCENTIVE_TRX a with(NOLOCK)
                                                                inner join LS_AGREEMENT b with(NOLOCK) on a.LSAGREE = b.LSAGREE 
                                                                inner join SYS_COMPANY c with(NOLOCK) on b.C_CODE = c.C_CODE
                                                                inner join DBNONCORE.SSS.dbo.UpdateSPPHNo d with(NOLOCK) on a.LSAGREE = d.AgreementNo 
                                                                inner join SYS_TBLEMPLOYEE e with(NOLOCK) on d.CreatedBy = e.CODE
                                                                order by a.ID, a.LSAGREE", false);

            //myDBSetting.LoadDataTable(myAllIncentiveTable, @"select a.ID, c.C_NAME CNAME, a.LSAGREE, b.NAME CUST_NAME, d.NoSPPH, PAY_TO_TYPE, PAY_TO, INCENTIVE_SRC, a.AMOUNT, a.CRE_DATE, e.DESCS Creator, d.DisburseDate 
            //                                                    from INCENTIVE_TRX a with(NOLOCK)
            //                                                    inner join LS_AGREEMENT b with(NOLOCK) on a.LSAGREE = b.LSAGREE 
            //                                                    inner join SYS_COMPANY c with(NOLOCK) on b.C_CODE = c.C_CODE
            //                                                    inner join SSS.dbo.UpdateSPPHNo d with(NOLOCK) on a.LSAGREE = d.AgreementNo and d.Status = 'Approve'
            //                                                    inner join SYS_TBLEMPLOYEE e with(NOLOCK) on d.CreatedBy = e.CODE
            //                                                    order by a.ID, a.LSAGREE", false);
            return myAllIncentiveTable;
        }

        protected override DataSet LoadData(long headerid)
        {
            SqlConnection myconn = new SqlConnection(myLocalDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            string sSQLHeader = "SELECT * FROM [dbo].[UpdateSPPHNo] WHERE DocKey=@DocKey";
            using (SqlCommand cmdheader = new SqlCommand(sSQLHeader, myconn))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@DocKey", SqlDbType.BigInt);
                cmdheader.Parameters["@DocKey"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            myHeaderTable.TableName = "Header";
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;
            dataSet.Tables.Add(myHeaderTable);
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
        protected override void SaveData(UpdateNoSPPHEntity Entity, DataSet ds, SaveAction saveaction, string userID, string userName)
        {
            SqlLocalDBSetting dbLocalSetting = this.myLocalDBSetting.StartTransaction();
            SqlConnection con = new SqlConnection(dbLocalSetting.ConnectionString);
            DateTime Mydate = myLocalDBSetting.GetServerTime();
            DataRow dataRow = ds.Tables["Header"].Rows[0];
            try
            {
                dbLocalSetting.StartTransaction();
                if (saveaction == SaveAction.Cancel)
                {
                    dataRow["Cancelled"] = "T";
                    dataRow["CancelledDateTime"] = Mydate;
                    dataRow["Status"] = "CANCELLED";
                    dbLocalSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[UpdateSPPHNo]");
                }
                if (saveaction == SaveAction.UnCancel)
                {
                    dataRow["Cancelled"] = "F";
                    dataRow["CancelledDateTime"] = DBNull.Value;
                    dataRow["Status"] = "UNCANCELLED";
                    dbLocalSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[UpdateSPPHNo]");
                }
                if (saveaction == SaveAction.Approve)
                {
                    dataRow["Status"] = "APPROVE";
                    dataRow["ApproveBy"] = userName;
                    dataRow["ApproveDateTime"] = Mydate;
                    dbLocalSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[UpdateSPPHNo]");
                    UpdateSMILE(Entity, ds, saveaction, userID);
                    //Exec_SP_SMILE(Convert.ToString(Entity.AgreementNo));
                }
                if (saveaction == SaveAction.Reject)
                {
                    dataRow["Status"] = "REJECT";
                    dataRow["ApproveBy"] = userName;
                    dataRow["ApproveDateTime"] = Mydate;
                    dbLocalSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[UpdateSPPHNo]");
                }
                if (saveaction == SaveAction.Save)
                {
                    dbLocalSetting.SimpleSaveDataTable(ds.Tables["Header"], "SELECT * FROM [dbo].[UpdateSPPHNo]");
                    //Exec_SP_SMILE(Convert.ToString(Entity.AgreementNo));
                }
                Entity.strErrorGenTicket = "null";

                if (Entity.strErrorGenTicket == "null")
                {
                    dbLocalSetting.Commit();

                    if (saveaction == SaveAction.Save)
                    {
                        Exec_SP_SMILE(Convert.ToString(Entity.AgreementNo));
                    }
                }
                else
                {
                    dbLocalSetting.Rollback();
                    throw new ArgumentException(Entity.strErrorGenTicket);
                }
            }
            catch (SqlException ex)
            {
                dbLocalSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                dbLocalSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                dbLocalSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                dbLocalSetting.EndTransaction();
            }
        }
        protected override void UpdateSMILE(UpdateNoSPPHEntity Entity, DataSet ds, SaveAction saveaction, string UserID)
        {
            SqlDBSetting mytransdbsetting = myDBSetting.StartTransaction();
            try
            {
                mytransdbsetting.ExecuteNonQuery("UPDATE LS_ASSETVEHICLE SET FAKTURNO=?, MOD_BY=?, MOD_DATE=? WHERE LSAGREE=?", Entity.NoSPPH, UserID, mytransdbsetting.GetServerTime(), Entity.AgreementNo);
                mytransdbsetting.Commit();
            }
            catch (Exception ex)
            {
                mytransdbsetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                mytransdbsetting.EndTransaction();
            }
        }
        protected override void Exec_SP_SMILE(string sAgreeNo)
        {
            SqlDBSetting mytransdbsetting = myDBSetting.StartTransaction();
            try
            {
                mytransdbsetting.StoredProcedure = true;
                SqlParameter param1 = new SqlParameter("@P_LSAGREE ", sAgreeNo);
                mytransdbsetting.ExecuteNonQuery(@"SP_MNCL_GENERATE_INCENTIVE", param1);
                mytransdbsetting.Commit();
            }
            catch (Exception ex)
            {
                mytransdbsetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                mytransdbsetting.EndTransaction();
            }
        }
    }
}