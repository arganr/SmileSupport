using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections
{
    public class CollectionRemarkSQL : CollectionRemarkDB
    {
        string sQuery = "";
        public override DataTable LoadDataKontrak()
        {
            myNoKontrakTable.Clear();

            sQuery = "select b.C_NAME[CABANG], a.LSAGREE[NO KONTRAK], a.NAME[DEBITUR], \n";
            sQuery += "case a.MODULE when 1 then 'Leasing' when 2 then 'Consumer Finance' when 3 then 'IMBT' when 4 then 'Murabahah' when 5 then 'Factoring' when 6 then 'OPL' when 7 then 'Hawalah' end[JENIS PEMBIAYAAN], \n";
            sQuery += "c.DESCRIPTION[PRODUCT FACILITY], LSPERIOD[TENOR], a.AMTLEASE[NTF], a.OUTSTANDING_AR[OUTSTANDING AR], a.OVERDUE, d.valuedate[LAST PAYMENT], a.OS_PERIOD[SISA TENOR], \n";
            sQuery += "case a.OS_PERIOD when 0 then NULL else a.LAST_DUEDATE end[NEXT DUEDATE] from LS_AGREEMENT a \n";
            sQuery += "inner join SYS_COMPANY b on b.C_CODE = a.C_CODE \n";
            sQuery += "left join PRODUCT_FACILITY c on c.MODULE = a.MODULE and c.CODE = a.PRODUCT_FACILITY_CODE \n";
            sQuery += "left join (select LSAGREE, max(VALUEDATE)[valuedate] from LS_LEDGERRENTAL where PAYMENT < 0 group by LSAGREE) d on d.LSAGREE = a.LSAGREE \n";
            sQuery += "where a.CONTRACT_STATUS = 'GOLIVE' \n";

            myDBSetting.LoadDataTable(myNoKontrakTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myNoKontrakTable.Columns["NoKontrak"];
            myNoKontrakTable.PrimaryKey = keyHeader;
            return myNoKontrakTable;
        }
        public override void Submit(string suserid, string slsagree, int iactionid, string sremark, string spromisetopay)
        {
            SqlConnection connection = new SqlConnection(this.myDBSetting.ConnectionString);
            try
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("[SP_MNCL_INSERT_COLLECTION]", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter spID = new SqlParameter("@userid", suserid);
                    spID.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(spID);
                    SqlParameter splsagree = new SqlParameter("@lsagree", slsagree);
                    splsagree.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(splsagree);
                    SqlParameter spactionid = new SqlParameter("@actionid", iactionid);
                    spactionid.SqlDbType = SqlDbType.Int;
                    cmd.Parameters.Add(spactionid);
                    SqlParameter spremark = new SqlParameter("@remark", sremark);
                    spremark.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(spremark);
                    if (spromisetopay.Length == 0)
                    {
                        SqlParameter sppromisepaydate = new SqlParameter("@promisepaydate", DBNull.Value);
                        sppromisepaydate.SqlDbType = SqlDbType.DateTime;
                        cmd.Parameters.Add(sppromisepaydate);
                    }
                    else
                    {
                        SqlParameter sppromisepaydate = new SqlParameter("@promisepaydate", spromisetopay);
                        sppromisepaydate.SqlDbType = SqlDbType.DateTime;
                        cmd.Parameters.Add(sppromisepaydate);
                    }
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