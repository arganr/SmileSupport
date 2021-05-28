using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry
{
    public class DBRegistrySQL : DBRegistry
    {
        public override long IncOne(int regID, long startValue)
        {
            using (SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction())
            {
                DataTable dataTable = dbSetting.GetDataTable("SELECT * FROM [dbo].[Registry] WITH (UPDLOCK, ROWLOCK) WHERE RegID=" + (object)regID, false, new object[0]);
                long num1;
                if (dataTable.Rows.Count > 0)
                {
                    long num2 = System.Convert.ToInt64(dataTable.Rows[0]["RegValue"]);
                    dataTable.Rows[0]["RegValue"] = (object)(num1 = num2 + 1L);
                }
                else
                {
                    DataRow row = dataTable.NewRow();
                    row["RegID"] = (object)regID;
                    row["RegType"] = (object)3;
                    row["RegValue"] = (object)1;
                    row.EndEdit();
                    num1 = 1L;
                    dataTable.Rows.Add(row);
                }
                dbSetting.SimpleSaveDataTable(dataTable, "SELECT * FROM [dbo].[Registry]");
                dbSetting.Commit();
                return num1;
            }
        }

        public override object GetValue(int regID, object startValue)
        {
            if (!this.myUseCache)
            {
                return this.myLocalDBSetting.ExecuteScalar("SELECT RegValue FROM [dbo].[Registry] WHERE RegID=" + (object)regID, new object[0]) ?? startValue;
            }
            else
            {
                if (this.myCacheDBRegistryTable == null)
                    this.myCacheDBRegistryTable = this.myLocalDBSetting.GetDataTable("SELECT RegID, RegValue FROM [dbo].[Registry] ORDER BY RegID", true, new object[0]);
                DataRow dataRow = this.myCacheDBRegistryTable.Rows.Find((object)regID);
                if (dataRow == null)
                    return startValue;
                else
                    return dataRow["RegValue"];
            }
        }

        public override void SetValue(int regID, object regValue)
        {
            using (SqlLocalDBSetting dbSetting = this.myLocalDBSetting.StartTransaction())
            {
                DataTable dataTable = dbSetting.GetDataTable("SELECT * FROM [dbo].[Registry] WHERE RegID=" + (object)regID, false, new object[0]);
                if (dataTable.Rows.Count > 0)
                {
                    dataTable.Rows[0]["RegValue"] = regValue;
                }
                else
                {
                    DataRow row = dataTable.NewRow();
                    row["RegID"] = (object)regID;
                    row["RegType"] = (object)3;
                    row["RegValue"] = regValue;
                    dataTable.Rows.Add(row);
                }
                dbSetting.SimpleSaveDataTable(dataTable, "SELECT * FROM [dbo].[Registry]");
                dbSetting.Commit();
            }
        }
    }
}