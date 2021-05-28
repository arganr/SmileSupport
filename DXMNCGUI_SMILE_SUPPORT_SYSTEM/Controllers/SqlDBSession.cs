using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public delegate void AfterLoginEvent();
    public class SqlDBSessionFB : SqlDBSession
    {
    }
    public class SqlDBSession
    {
        protected bool myAllowRootLogin = true;
        private const string myPrivateKeyFilename = "login.key";
        protected string myConnectionString;
        protected string myLocalConnectionString;
        protected int mySessionKey;
        protected string myLoginUserID;
        protected string myLoginNoUrut;
        protected AfterLoginEvent myAfterLoginEvent;
        protected bool myUseEncryptedPassword;

        public bool AllowRootLogin
        {
            get
            {
                return this.myAllowRootLogin;
            }
            set
            {
                this.myAllowRootLogin = value;
            }
        }

        public bool UseEncryptedPassword
        {
            get
            {
                return this.myUseEncryptedPassword;
            }
            set
            {
                this.myUseEncryptedPassword = value;
            }
        }

        public AfterLoginEvent AfterLogin
        {
            get
            {
                return this.myAfterLoginEvent;
            }
            set
            {
                this.myAfterLoginEvent = value;
            }
        }

        public string LoginUserID
        {
            get
            {
                return this.myLoginUserID;
            }
        }
        public string LoginNoUrut
        {
            get
            {
                return this.myLoginNoUrut;
            }
            set
            {
                this.myLoginNoUrut = value;
            }
        }
        public int SessionKey
        {
            get
            {
                return this.mySessionKey;
            }
        }

        public bool IsLogin
        {
            get
            {
                return this.mySessionKey > -1;
            }
        }

        public SqlDBSession()
        {
            this.mySessionKey = -1;
            this.myLoginUserID = "";
            this.myLoginNoUrut = "";
        }

        public static SqlDBSession Create(SqlDBSetting dbSetting, SqlLocalDBSetting localdbSetting)
        {
            SqlDBSession dbSession = dbSetting.ServerType != DBServerType.SQL2000 ? (SqlDBSession)new SqlDBSessionFB() : (SqlDBSession)new SqlDBSessionSQL();
            dbSession.myConnectionString = dbSetting.ConnectionString;
            dbSession.myLocalConnectionString = localdbSetting.ConnectionString;
            return dbSession;
        }

        [Obsolete("Use static Create method.")]
        public static SqlDBSession CreatesqlDBSession(SqlDBSetting dbSetting, SqlLocalDBSetting localdbSetting)
        {
            return SqlDBSession.Create(dbSetting, localdbSetting);
        }

        protected virtual bool GetSession(string aComputerName, string aUserName, int privateKey)
        {
            return false;
        }

        protected virtual bool CloseSession()
        {
            return false;
        }

        protected virtual void CreateSessionRecord(string aComputerName, string aUserName, int privateKey, string aUserID)
        {
        }

        public virtual bool Authenticate(string aUserID, string aPassword)
        {
            return false;
        }

        private int GetPrivateKey()
        {
            string applicationDataPath = HttpContext.Current.Request.PhysicalApplicationPath;
            if (!Directory.Exists(applicationDataPath))
                Directory.CreateDirectory(applicationDataPath);
            string path = applicationDataPath + (object)'\\' + "login.key";
            int num;
            if (File.Exists(path))
            {
                FileStream fileStream = File.OpenRead(path);
                num = new BinaryReader((Stream)fileStream).ReadInt32();
                fileStream.Close();
            }
            else
            {
                Thread.Sleep(1);
                num = new Random().Next();
                FileStream fileStream = File.OpenWrite(path);
                new BinaryWriter((Stream)fileStream).Write(num);
                fileStream.Close();
            }
            return num;
        }
        public bool Login(string userID, string password)
        {
            if (this.Authenticate(userID, password))
            {
                int privateKey = this.GetPrivateKey();
                if (string.Compare(userID, "__ROOT", true) == 0)
                    userID = "ADMIN";
                this.CreateSessionRecord(Environment.MachineName, Environment.UserName, privateKey, userID);
                if (this.myAfterLoginEvent != null)
                    this.myAfterLoginEvent();
                return true;
            }
            else
                return false;
        }
        public bool Login()
        {
            int privateKey = this.GetPrivateKey();
            if (!this.GetSession(Environment.MachineName, Environment.UserName, privateKey))
            {
                try
                {
                    while (true)
                    {

                    }

                    if (this.myAfterLoginEvent != null)
                    {
                        this.myAfterLoginEvent();
                        goto label_10;
                    }
                    else
                        goto label_10;
                    label_8:
                    return false;
                }
                finally
                {

                }
            label_10:
                return true;
            }
            else
                return true;
        }
        public bool Logout()
        {
            return this.CloseSession();
        }
    }
}