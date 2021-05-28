using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections.CollectionLetter
{
    public class CollectionLetterSQL : CollectionLetterDB
    {
        string sQuery = "";
        public override DataTable LoadDataMain()
        {
            myMainTable.Clear();
            sQuery = "";
            sQuery = @"SELECT * FROM LS_COLLECTION_LETTER_SEND
                            ORDER BY SEND_DATE DESC";
            myDBSetting.LoadDataTable(myMainTable, sQuery, true);
            return myMainTable;
        }
        public override DataTable LoadDataCollLetter()
        {
            myCollLetterTable.Clear();
            sQuery = "";
            sQuery = @"SELECT a.ID, LETTER_NO, LETTER_DATE, a.LSAGREE, b.NAME, a.MOD_BY
                        FROM LS_COLLECTION_LETTER a WITH(NOLOCK)
                        INNER JOIN LS_AGREEMENT b WITH(NOLOCK) on a.LSAGREE = b.LSAGREE
                        WHERE	a.MOD_BY <> 'eod' AND YEAR(letter_date) >= 2019 
								and not exists (select LETTER_NO, LSAGREE from LS_COLLECTION_LETTER_SEND where LETTER_NO=a.LETTER_NO and LSAGREE=a.LSAGREE )
		                        --AND (a.LETTER_NO NOT IN ( select LETTER_NO from LS_COLLECTION_LETTER_SEND )
								--or a.LSAGREE NOT IN ( select LSAGREE from LS_COLLECTION_LETTER_SEND ))
                        ORDER BY letter_date DESC";
            myDBSetting.LoadDataTable(myCollLetterTable, sQuery, true);
            return myCollLetterTable;
        }
        public override void Submit(DataTable myTable)
        {
            DataRow myResultRow = myTable.Rows[0];
            SqlConnection connection = new SqlConnection(this.myDBSetting.ConnectionString);
            try
            {
                connection.Open();
                sQuery = @"INSERT INTO LS_COLLECTION_LETTER_SEND 
                            ([LETTER_NO]
                            ,[LSAGREE]
                            ,[SEND_DATE]
                            ,[NO_RESI]
                            ,[KURIR]
                            ,[FEE_AMT]
                            ,[TUJUAN_KIRIM]
                            ,[REMARK]
                            ,[CRE_BY]
                            ,[CRE_DATE])
                        VALUES (@LETTER_NO,@LSAGREE,@SEND_DATE,@NO_RESI,@KURIR,@FEE_AMT,@TUJUAN_KIRIM,@REMARK,@CRE_BY,@CRE_DATE)";
                using (SqlCommand cmd = new SqlCommand(sQuery, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlParameter sParam1 = new SqlParameter("@LETTER_NO", myResultRow["LETTER_NO"]);
                    sParam1.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam1);
                    SqlParameter sParam2 = new SqlParameter("@LSAGREE", myResultRow["LSAGREE"]);
                    sParam2.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam2);
                    SqlParameter sParam3 = new SqlParameter("@SEND_DATE", myResultRow["SEND_DATE"]);
                    sParam3.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam3);
                    SqlParameter sParam4 = new SqlParameter("@NO_RESI", myResultRow["NO_RESI"]);
                    sParam4.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam4);
                    SqlParameter sParam5 = new SqlParameter("@KURIR", myResultRow["KURIR"]);
                    sParam5.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam5);
                    SqlParameter sParam6 = new SqlParameter("@FEE_AMT", myResultRow["FEE_AMT"]);
                    sParam6.SqlDbType = SqlDbType.Decimal;
                    cmd.Parameters.Add(sParam6);
                    SqlParameter sParam7 = new SqlParameter("@TUJUAN_KIRIM", myResultRow["TUJUAN_KIRIM"]);
                    sParam7.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam7);
                    SqlParameter sParam8 = new SqlParameter("@REMARK", myResultRow["REMARK"]);
                    sParam8.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam8);
                    SqlParameter sParam9 = new SqlParameter("@CRE_BY", myResultRow["CRE_BY"]);
                    sParam9.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(sParam9);
                    SqlParameter sParam10 = new SqlParameter("@CRE_DATE", myResultRow["CRE_DATE"]);
                    sParam10.SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.Add(sParam10);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (SqlException ex)
            {
                DataError.HandleSqlException(ex);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}