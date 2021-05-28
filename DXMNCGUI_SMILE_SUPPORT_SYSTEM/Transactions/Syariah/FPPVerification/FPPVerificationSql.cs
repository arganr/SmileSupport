using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Syariah.FPPVerification
{
    public class FPPVerificationSql : FPPVerificationDB
    {
        public override DataTable LoadBrowseTable(bool bViewAll, string userID)
        {
            myBrowseTable.Clear();
            try
            {
                myMySqlDBSetting.LoadDataTable(myBrowseTable, "SELECT * FROM cust_prospect ORDER BY cust_prospect_id desc", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myBrowseTable.Columns["cust_prospect_id"];
            myBrowseTable.PrimaryKey = keyHeader;
            return myBrowseTable;
        }
        protected override DataSet LoadData(long headerid)
        {
            MySqlConnection myconn = new MySqlConnection(myMySqlDBSetting.ConnectionString);
            DataSet dataSet = new DataSet();
            DataTable myHeaderTable = new DataTable();
            DataTable myDetailTable = new DataTable();
            string sSQLHeader = "SELECT * FROM cust_prospect WHERE cust_prospect_id=@cust_prospect_id";
            string sSQLLines = "SELECT * FROM cust_prospect_doc WHERE cust_prospect_id=@cust_prospect_id";

            using (MySqlCommand cmdheader = new MySqlCommand(sSQLHeader, myconn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdheader);
                cmdheader.Parameters.Add("@cust_prospect_id", MySqlDbType.Int32);
                cmdheader.Parameters["@cust_prospect_id"].Value = headerid;
                adapter.Fill(myHeaderTable);
            }
            using (MySqlCommand cmdlines = new MySqlCommand(sSQLLines, myconn))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmdlines);
                cmdlines.Parameters.Add("@cust_prospect_id", MySqlDbType.Int32);
                cmdlines.Parameters["@cust_prospect_id"].Value = headerid;
                adapter.Fill(myDetailTable);
            }

            myHeaderTable.TableName = "Header";
            myDetailTable.TableName = "Lines";

            DataColumn[] keyHeader = new DataColumn[1];
            keyHeader[0] = myHeaderTable.Columns["DocKey"];
            myHeaderTable.PrimaryKey = keyHeader;
            DataColumn[] keyLines = new DataColumn[1];
            keyLines[0] = myDetailTable.Columns["DtlKey"];
            myDetailTable.PrimaryKey = keyLines;

            dataSet.Tables.Add(myHeaderTable);
            dataSet.Tables.Add(myDetailTable);
            dataSet.Relations.Add("rlCustProspectDetail", myHeaderTable.Columns["cust_prospect_id"], myDetailTable.Columns["cust_prospect_id"]);
            return dataSet;
        }
    }
}