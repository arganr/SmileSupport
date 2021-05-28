using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.SimulationRescheduling
{
    public class SimulationReschedulingSQL : SimulationReschedulingDB
    {
        string sClientName = "";
        double dDenda = 0, dEffRateRounding = 0, dAccruedIntAmt = 0, dInsDueAmt = 0;

        DataTable dtCloned = new DataTable();
        DataTable dtCloned2 = new DataTable();

        public override DataTable LoadReturnTable(string AgreeNo, DateTime EffDate, double EffRate, double Tenor, double[] NumbOfStepDown, double[] StepDownAmt)
        {
            DateTime NextInstDt;
            double OSPrincipal = 0, LastTenorPaid = 0, Denda = 0, EffRateRounding = 0, AccruedIntAmt = 0, InsDueAmt=0;
            string ClientName = "";

            myReturnTable.Clear();
            myFinalReturnTable.Clear();

            SqlConnection myconn = new SqlConnection(myDBSetting.ConnectionString);
            myconn.Open();
            try
            {
                SqlCommand sqlCommand = new SqlCommand(@"sp_MNCL_GetInstSchdl_Restruk");
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Connection = myconn;

                SqlParameter sqlParameter1 = sqlCommand.Parameters.Add("@LsAgree", SqlDbType.NVarChar);
                sqlParameter1.Value = AgreeNo;

                SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@EffectiveDate", SqlDbType.DateTime);
                sqlParameter2.Value = EffDate;

                sqlCommand.Parameters.Add("@OSPrincipal", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add("@NextInstDt", SqlDbType.Date).Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add("@LastTenorPaid", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add("@AccruedIntAmt", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add("@ClientName", SqlDbType.NVarChar, 2000).Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add("@InsDueAmt", SqlDbType.Decimal).Direction = ParameterDirection.Output;

                sqlCommand.ExecuteNonQuery();

                OSPrincipal = sqlCommand.Parameters["@OSPrincipal"].Value == DBNull.Value ? 0 : Convert.ToDouble(sqlCommand.Parameters["@OSPrincipal"].Value);
                LastTenorPaid = sqlCommand.Parameters["@LastTenorPaid"].Value == DBNull.Value ? 0 : Convert.ToDouble(sqlCommand.Parameters["@LastTenorPaid"].Value);
                AccruedIntAmt = sqlCommand.Parameters["@AccruedIntAmt"].Value == DBNull.Value ? 0 : Convert.ToDouble(sqlCommand.Parameters["@AccruedIntAmt"].Value);
                NextInstDt = sqlCommand.Parameters["@NextInstDt"].Value == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(sqlCommand.Parameters["@NextInstDt"].Value);
                ClientName = sqlCommand.Parameters["@ClientName"].Value == DBNull.Value ? "" : Convert.ToString(sqlCommand.Parameters["@ClientName"].Value);
                InsDueAmt = sqlCommand.Parameters["@InsDueAmt"].Value == DBNull.Value ? 0 : Convert.ToDouble(sqlCommand.Parameters["@InsDueAmt"].Value);

                using (SqlDataReader dr = sqlCommand.ExecuteReader())
                {
                    myReturnTable.Load(dr);       
                }

                if (myReturnTable.Rows.Count > 0)
                {
                    double iMaxTenor = 0;
                    iMaxTenor = Tenor - LastTenorPaid + 1;

                    myFinalReturnTable = Reschedule.CalculateReschedule.Calculate(
                    OSPrincipal,
                    EffRate,
                    iMaxTenor,
                    1000,
                    EffDate,
                    NextInstDt,
                    NumbOfStepDown,
                    StepDownAmt,
                    LastTenorPaid,
                    out Denda,
                    out EffRateRounding);

                    dtCloned.Clear();
                    dtCloned = myReturnTable.Clone();
                    dtCloned.Columns.Remove("paid_amt");
                    dtCloned.Columns[0].DataType = typeof(Int32);
                    dtCloned.Columns[1].DataType = typeof(DateTime);
                    dtCloned.Columns[2].DataType = typeof(double);
                    dtCloned.Columns[3].DataType = typeof(double);
                    dtCloned.Columns[4].DataType = typeof(double);
                    dtCloned.Columns[5].DataType = typeof(double);
                    dtCloned.Columns[6].DataType = typeof(double);
                    foreach (DataRow row in myReturnTable.Rows)
                    {
                        dtCloned.ImportRow(row);
                    }

                    dtCloned2.Clear();
                    dtCloned2 = myFinalReturnTable.Clone();
                    dtCloned2.Columns[0].DataType = typeof(Int32);
                    dtCloned2.Columns[1].DataType = typeof(DateTime);
                    dtCloned2.Columns[2].DataType = typeof(double);
                    dtCloned2.Columns[3].DataType = typeof(double);
                    dtCloned2.Columns[4].DataType = typeof(double);
                    dtCloned2.Columns[5].DataType = typeof(double);
                    dtCloned2.Columns[6].DataType = typeof(double);
                    foreach (DataRow row in myFinalReturnTable.Rows)
                    {
                        dtCloned2.ImportRow(row);
                    }

                    dtCloned.PrimaryKey = new DataColumn[] {dtCloned.Columns[0]};
                    dtCloned2.PrimaryKey = new DataColumn[] {dtCloned2.Columns[0]};

                    dtCloned.Merge(dtCloned2, false);

                    dDenda = Denda;
                    dEffRateRounding = EffRateRounding;
                    dAccruedIntAmt = AccruedIntAmt;
                    sClientName = ClientName;
                    dInsDueAmt = InsDueAmt;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            finally
            {
                myconn.Close();
            }
            return dtCloned;
        }
        public override double GetDenda()
        {
            return dDenda;
        }
        public override double GetEffRateRounding()
        {
            return dEffRateRounding;
        }
        public override double GetAccruedIntAmt()
        {
            return dAccruedIntAmt;
        }
        public override string GetClientName()
        {
            return sClientName;
        }
        public override double GetInsDueAmt()
        {
            return dInsDueAmt;
        }
    }
}