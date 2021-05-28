using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.AssetCollateral
{
    public class AssetCollateralSQL : AssetCollateralDB
    {
        string sQuery = "";
        string sQuery2 = "";
        public override DataTable LoadDataNoPol()
        {
            myAssetCollateralTable.Clear();
            sQuery = @"SELECT a.AST_CODE, LOCATION, b.PLAT_NO, b.BPKB_NO, b.FISCAL_STATUS
                        FROM ASSET_COLLATERAL_LOCATION a
                        INNER JOIN FA_ASSETREGISTER b ON a.AST_CODE = b.AST_CODE";
            myDBSetting.LoadDataTable(myAssetCollateralTable, sQuery, true);
            return myAssetCollateralTable;
        }
        public override void Save(DataTable myInsertTable, DataTable myUpdateTable)
        {
            DataRow myInsertResultRow = myInsertTable.Rows[0];
            DataRow myUpdateResultRow = myUpdateTable.Rows[0];
            SqlConnection connection = new SqlConnection(this.myDBSetting.ConnectionString);
            try
            {
                myDBSetting.StartTransaction();
                connection.Open();
                sQuery = @"INSERT INTO ASSET_COLLATERAL_LOCATION_HIST 
                            ([AST_CODE]
                            ,[FROM_LOC]
                            ,[TO_LOC]
                            ,[CRE_DATE]
                            ,[CRE_BY]
                            ,[DATE]
                            ,[PROMISE_DATE])
                        VALUES (@AST_CODE,@FROM_LOC,@TO_LOC,@CRE_DATE,@CRE_BY,@DATE,@PROMISE_DATE)";
                using (SqlCommand cmd = new SqlCommand(sQuery, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlParameter sParam1 = new SqlParameter("@AST_CODE", myInsertResultRow["AST_CODE"]);
                    sParam1.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam1);
                    SqlParameter sParam2 = new SqlParameter("@FROM_LOC", myInsertResultRow["FROM_LOC"]);
                    sParam2.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam2);
                    SqlParameter sParam3 = new SqlParameter("@TO_LOC", myInsertResultRow["TO_LOC"]);
                    sParam3.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam3);
                    SqlParameter sParam4 = new SqlParameter("@CRE_DATE", myInsertResultRow["CRE_DATE"]);
                    sParam4.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam4);
                    SqlParameter sParam5 = new SqlParameter("@CRE_BY", myInsertResultRow["CRE_BY"]);
                    sParam5.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam5);
                    SqlParameter sParam6= new SqlParameter("@DATE", myInsertResultRow["DATE"]);
                    sParam6.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam6);
                    SqlParameter sParam7 = new SqlParameter("@PROMISE_DATE", myInsertResultRow["PROMISE_DATE"]);
                    sParam7.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam7);
                    cmd.ExecuteNonQuery();
                }

                sQuery2 = @"UPDATE ASSET_COLLATERAL_LOCATION
                            SET LOCATION=@LOCATION, DETAIL_LOCATION=@DETAIL_LOCATION, DATE=@DATE, 
                                REMARKS=@REMARKS, PROMISE_DATE=@PROMISE_DATE, MOD_DATE=@MOD_DATE, 
                                MOD_BY=@MOD_BY WHERE AST_CODE=@AST_CODE";
                using (SqlCommand cmd = new SqlCommand(sQuery2, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlParameter sParam1 = new SqlParameter("@AST_CODE", myUpdateResultRow["AST_CODE"]);
                    sParam1.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam1);
                    SqlParameter sParam2 = new SqlParameter("@LOCATION", myUpdateResultRow["LOCATION"]);
                    sParam2.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam2);
                    SqlParameter sParam3 = new SqlParameter("@DETAIL_LOCATION", myUpdateResultRow["DETAIL_LOCATION"]);
                    sParam3.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam3);
                    SqlParameter sParam4 = new SqlParameter("@DATE", myUpdateResultRow["DATE"]);
                    sParam4.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam4);
                    SqlParameter sParam5 = new SqlParameter("@REMARKS", myUpdateResultRow["REMARKS"]);
                    sParam5.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam5);
                    SqlParameter sParam6 = new SqlParameter("@PROMISE_DATE", myUpdateResultRow["PROMISE_DATE"]);
                    sParam6.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam6);
                    SqlParameter sParam7 = new SqlParameter("@MOD_DATE", myUpdateResultRow["MOD_DATE"]);
                    sParam7.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam7);
                    SqlParameter sParam8 = new SqlParameter("@MOD_BY", myUpdateResultRow["MOD_BY"]);
                    sParam5.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam8);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                myDBSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (HttpUnhandledException ex)
            {
                myDBSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                myDBSetting.Rollback();
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myDBSetting.Commit();
                myDBSetting.EndTransaction();
            }
        }
    }
}