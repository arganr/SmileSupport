using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.CreditProcess.SimulationRescheduling
{
    public class SimulationReschedulingDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected internal SqlLocalDBSetting myLocalDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myReturnTable;
        protected DataTable myFinalReturnTable;

        internal SimulationReschedulingDB()
        {
            myReturnTable = new DataTable();
            DataColumn[] Key1stTable = new DataColumn[1];
            Key1stTable[0] = myReturnTable.Columns["Tenor"];
            myReturnTable.PrimaryKey = Key1stTable;

            myFinalReturnTable = new DataTable();
            DataColumn[] Key2ndTable = new DataColumn[1];
            Key2ndTable[0] = myFinalReturnTable.Columns["Tenor"];
            myFinalReturnTable.PrimaryKey = Key2ndTable;
        }
        public SqlDBSetting DBSetting
        {
            get { return myDBSetting; }
        }
        public SqlLocalDBSetting localDBSetting
        {
            get { return myLocalDBSetting; }
        }
        public SqlDBSession DBSession
        {
            get { return myDBSession; }
        }
        public DataTable ReturnTable
        {
            get
            {
                return this.myReturnTable;
            }
        }
        public static SimulationReschedulingDB Create(SqlDBSetting dbSetting, SqlLocalDBSetting localdbsetting, SqlDBSession dbSession)
        {
            SimulationReschedulingDB aSimulationReschedulingDB = (SimulationReschedulingDB)null;
            aSimulationReschedulingDB = new SimulationReschedulingSQL();
            aSimulationReschedulingDB.myDBSetting = dbSetting;
            aSimulationReschedulingDB.myLocalDBSetting = localdbsetting;
            aSimulationReschedulingDB.myDBSession = dbSession;
            return aSimulationReschedulingDB;
        }

        public virtual DataTable LoadReturnTable(string AgreeNo, DateTime EffDate, double EffRate, double Tenor, double[] NumbOfStepDown, double[] StepDownAmt)
        {
            return null;
        }
        public virtual double GetDenda()
        {
            return 0;
        }
        public virtual double GetEffRateRounding()
        {
            return 0;
        }
        public virtual double GetAccruedIntAmt()
        {
            return 0;
        }
        public virtual string GetClientName()
        {
            return null;
        }
        public virtual double GetInsDueAmt()
        {
            return 0;
        }
    }
}