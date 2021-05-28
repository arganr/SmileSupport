using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.AssetCollateral
{
    public class AssetCollateralDB
    {
        protected internal SqlDBSetting myDBSetting;
        protected SqlDBSession myDBSession;
        protected DataTable myAssetCollateralTable;
        protected Controllers.Registry.DBRegistry myDBReg;
        internal AssetCollateralDB()
        {
            myAssetCollateralTable = new DataTable();
        }
        public static AssetCollateralDB Create(SqlDBSetting dbSetting, SqlDBSession dbSession)
        {
            AssetCollateralDB aAssetCollateral = (AssetCollateralDB)null;
            aAssetCollateral = new AssetCollateralSQL();
            aAssetCollateral.myDBSetting = dbSetting;
            aAssetCollateral.myDBSession = dbSession;
            return aAssetCollateral;
        }
        public SqlDBSetting DBSetting
        {
            get { return myDBSetting; }
        }
        public SqlDBSession DBSession
        {
            get { return myDBSession; }
        }
        public Controllers.Registry.DBRegistry DBReg
        {
            get
            {
                return this.myDBReg;
            }
        }
        public virtual DataTable LoadDataNoPol()
        {
            return null;
        }
        public virtual void Save(DataTable myInsertTable, DataTable myUpdateTable)
        {

        }
    }
}