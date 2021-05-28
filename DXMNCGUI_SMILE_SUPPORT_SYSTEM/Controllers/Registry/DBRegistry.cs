using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry
{
    public class DBRegistry
    {
        protected SqlDBSetting myDBSetting;
        protected SqlLocalDBSetting myLocalDBSetting;
        protected bool myUseCache;
        protected DataTable myCacheDBRegistryTable;

        public bool UseCache
        {
            get
            {
                return this.myUseCache;
            }
            set
            {
                this.myUseCache = value;
                if (!this.myUseCache)
                {
                    if (this.myCacheDBRegistryTable != null)
                        this.myCacheDBRegistryTable.Dispose();
                    this.myCacheDBRegistryTable = (DataTable)null;
                }
            }
        }

        internal DBRegistry()
        {
        }

        public static DBRegistry Create(SqlLocalDBSetting dbSetting)
        {
            if (dbSetting.ServerType == DBServerType.SQL2000)
            {
                DBRegistry dbRegistry = (DBRegistry)new DBRegistrySQL();
                dbRegistry.myLocalDBSetting = dbSetting;
                return dbRegistry;
            }
            else
                throw new ArgumentException("Server type: " + (object)dbSetting.ServerType + " not supported.");
        }
        public static DBRegistry Create(SqlDBSetting dbSetting)
        {
            if (dbSetting.ServerType == DBServerType.SQL2000)
            {
                DBRegistry dbRegistry = (DBRegistry)new DBRegistrySQL();
                dbRegistry.myDBSetting = dbSetting;
                return dbRegistry;
            }
            else
                throw new ArgumentException("Server type: " + (object)dbSetting.ServerType + " not supported.");
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use static Create method.")]
        public static DBRegistry CreateDBRegistry(SqlLocalDBSetting dbSetting)
        {
            return DBRegistry.Create(dbSetting);
        }

        public virtual long IncOne(int regID, long startValue)
        {
            return -1L;
        }

        public long IncOne(IRegistryID reg)
        {
            return this.IncOne(reg.ID, System.Convert.ToInt64(reg.DefaultValue));
        }

        public virtual object GetValue(int regID, object startValue)
        {
            return (object)null;
        }

        public object GetValue(IRegistryID reg)
        {
            return this.GetValue(reg.ID, reg.DefaultValue);
        }

        public sbyte GetSByte(IRegistryID reg)
        {
            return Convert.ToSByte(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public short GetInt16(IRegistryID reg)
        {
            return Convert.ToInt16(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public int GetInt32(IRegistryID reg)
        {
            return Convert.ToInt32(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public long GetInt64(IRegistryID reg)
        {
            return Convert.ToInt64(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public ulong GetUInt64(IRegistryID reg)
        {
            return Convert.ToUInt64(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public Decimal GetDecimal(IRegistryID reg)
        {
            return Convert.ToDecimal(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public bool GetBoolean(IRegistryID reg)
        {
            return Convert.ToBoolean(this.GetValue(reg.ID, reg.DefaultValue));
        }

        public string GetString(IRegistryID reg)
        {
            return this.GetValue(reg.ID, reg.DefaultValue).ToString();
        }

        public virtual void SetValue(int regID, object regValue)
        {
        }

        public void SetValue(IRegistryID reg)
        {
            this.SetValue(reg.ID, reg.NewValue);
        }
    }
}