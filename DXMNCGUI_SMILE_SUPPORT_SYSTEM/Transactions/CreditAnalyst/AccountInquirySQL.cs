using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditAnalyst
{
    public class AccountInquirySQL : AccountInquiryDB
    {
        string sQuery = "";
        public override DataTable LoadMainTable()
        {
            myMainTable.Clear();
            sQuery = "SELECT DISTINCT a.NAME, a.ADDRESS1 + ' ' + isnull(a.ADDRESS2,'') ADDRESS, a.INBORNDT, a.CLIENT from sys_client a inner join LS_APPLICATION b on b.LESSEE=a.CLIENT and b.APPSTATUS='APPROVE' order by a.CLIENT";
            myDBSetting.LoadDataTable(myMainTable, sQuery, true);
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myMainTable.Columns["CLIENT"];
            myMainTable.PrimaryKey = keyHeader;
            return myMainTable;
        }
    }
}