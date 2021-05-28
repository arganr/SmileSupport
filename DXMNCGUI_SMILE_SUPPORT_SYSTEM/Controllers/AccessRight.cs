using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCE.Data;
using BCE.Localization;
using DevExpress.XtraEditors;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public delegate void AccessRightOverrideDelegate(string orgUserID, string newUserID, string cmdID);
    public delegate void AccessRightListenerDelegate();
    internal class AccessRightListener
    {
        internal AccessRightListenerDelegate myListener;
        internal System.Web.UI.Page myFormOwner;

        public AccessRightListener(AccessRightListenerDelegate listener, System.Web.UI.Page form)
        {
            this.myListener = listener;
            this.myFormOwner = form;
        }
    }
    public class AccesRight
    {
        protected object myLock = new object();
        private AccessRightOverrideDelegate myOverrideEvent;
        protected ArrayList myListenerList;
        protected SqlDBSetting myDBSetting;
        protected SqlLocalDBSetting myLocalDBSetting;
        protected string myUserID;
        protected bool myUseCache;
        protected DataTable myCacheUserTable;
        protected DataTable myCacheUserGroupTable;

        public SqlDBSetting DBSetting
        {
            get
            {
                return this.myDBSetting;
            }
        }

        public SqlLocalDBSetting LocalDBSetting
        {
            get
            {
                return this.myLocalDBSetting;
            }
        }

        public string USER_NAME
        {
            get
            {
                return this.myUserID;
            }
            set
            {
                this.myUserID = value;
            }
        }

        public AccessRightOverrideDelegate OverrideEvent
        {
            get
            {
                return this.myOverrideEvent;
            }
            set
            {
                this.myOverrideEvent = value;
            }
        }

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
                    if (this.myCacheUserTable != null)
                        this.myCacheUserTable.Dispose();
                    this.myCacheUserTable = (DataTable)null;
                    if (this.myCacheUserGroupTable != null)
                        this.myCacheUserGroupTable.Dispose();
                    this.myCacheUserGroupTable = (DataTable)null;
                }
            }
        }

        internal AccesRight()
        {
            this.myListenerList = new ArrayList();
        }

        public static AccesRight Create(SqlLocalDBSetting dbSetting, string userID)
        {
            AccesRight accessRight = (AccesRight)null;
            if (dbSetting.ServerType == DBServerType.SQL2000)
                accessRight = (AccesRight)new AccessRightSQL();
            else
                dbSetting.ThrowServerTypeNotSupportedException();
            accessRight.myLocalDBSetting = dbSetting;
            accessRight.myUserID = userID;
            return accessRight;
        }

        public virtual string[] GetAllUserID(string cmdID)
        {
            return (string[])null;
        }

        public virtual bool IsAccessibleByUserID(string userID, string cmdID)
        {
            return false;
        }

        public virtual bool IsAccessible(string cmdID)
        {
            return false;
        }

        public bool IsAccessible(string cmdID, System.Web.UI.MasterPage parent, string extraMessage)
        {
            bool flag = this.IsAccessible(cmdID);
            if (!flag)
            {
                string newUserID = this.ShowAccessDeniedMessageEx((System.Web.UI.MasterPage)null, extraMessage, cmdID);
                flag = newUserID.Length > 0;
                if (this.myOverrideEvent != null)
                {
                    if (flag)
                    {
                        try
                        {
                            this.myOverrideEvent(this.myUserID, newUserID, cmdID);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return flag;
        }

        public bool IsAccessible(string cmdID, System.Web.UI.MasterPage parent)
        {
            return this.IsAccessible(cmdID, parent, "");
        }

        public bool IsAccessible(string cmdID, bool showAccessDeniedDialog)
        {
            return this.IsAccessible(cmdID, showAccessDeniedDialog, "");
        }

        public bool IsAccessible(string cmdID, string extraMessage)
        {
            return this.IsAccessible(cmdID, true, extraMessage);
        }

        private bool IsAccessible(string cmdID, bool showAccessDeniedDialog, string extraMessage)
        {
            bool flag = this.IsAccessible(cmdID);
            if (!flag && showAccessDeniedDialog)
            {
                string newUserID = this.ShowAccessDeniedMessageEx((System.Web.UI.MasterPage)null, extraMessage, cmdID);
                flag = newUserID.Length > 0;
                if (this.myOverrideEvent != null)
                {
                    if (flag)
                    {
                        try
                        {
                            this.myOverrideEvent(this.myUserID, newUserID, cmdID);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return flag;
        }

        public static void ShowAccessDeniedMessage(System.Web.UI.MasterPage parent, string extraMessage)
        {
            string str = Localizer.GetString((Enum)BCEStringId.AccessDenied, new object[0]);
            if (extraMessage.Length > 0)
                str = str + Environment.NewLine + Environment.NewLine + extraMessage;
            string @string = Localizer.GetString((Enum)BCEStringId.ContactSystemAdministrator, new object[0]);
            //   AppMessage.ShowErrorMessage((Form)parent, str + Environment.NewLine + Environment.NewLine + @string);
        }

        public string ShowAccessDeniedMessageEx(System.Web.UI.MasterPage parent, string extraMessage, string cmdID)
        {
            string str = Localizer.GetString((Enum)BCEStringId.AccessDenied, new object[0]);
            if (extraMessage.Length > 0)
                str = str + Environment.NewLine + Environment.NewLine + extraMessage;
            string @string = Localizer.GetString((Enum)BCEStringId.ContactSystemAdministrator, new object[0]);
            string message = str + Environment.NewLine + Environment.NewLine + @string;
            //  using (FormAccessDenied formAccessDenied = new FormAccessDenied(this, cmdID, message))
            //   {
            //   int num = (int)formAccessDenied.ShowDialog((IWin32Window)parent);
            //      return formAccessDenied.OtherUserID;
            // }
            return "";
        }

        public void AddListener(AccessRightListenerDelegate listener)
        {
            if (listener != null)
            {
                lock (this.myListenerList.SyncRoot)
                {
                    if (this.myListenerList.IndexOf((object)listener) < 0)
                        this.myListenerList.Add((object)listener);
                }
            }
        }

        public void AddListener(AccessRightListenerDelegate listener, System.Web.UI.Page form)
        {
            if (listener != null)
            {
                lock (this.myListenerList.SyncRoot)
                {
                    bool local_0 = false;
                    foreach (object item_0 in this.myListenerList)
                    {
                        if (item_0 is AccessRightListener)
                        {
                            AccessRightListener local_2 = (AccessRightListener)item_0;
                            if (local_2.myFormOwner.Equals((object)form) && local_2.myListener.Equals((object)listener))
                            {
                                local_0 = true;
                                break;
                            }
                        }
                    }
                    if (!local_0)
                    {
                        form.Disposed += new EventHandler(this.form_Disposed);
                        this.myListenerList.Add((object)new AccessRightListener(listener, form));
                    }
                }
            }
        }

        private void form_Disposed(object sender, EventArgs e)
        {
            System.Web.UI.Page form = (System.Web.UI.Page)sender;
            form.Disposed -= new EventHandler(this.form_Disposed);
            foreach (object obj in this.myListenerList)
            {
                if (obj is AccessRightListener)
                {
                    AccessRightListener accessRightListener = (AccessRightListener)obj;
                    if (accessRightListener.myFormOwner.Equals((object)form))
                    {
                        accessRightListener.myFormOwner = (System.Web.UI.Page)null;
                        accessRightListener.myListener = (AccessRightListenerDelegate)null;
                        this.myListenerList.Remove((object)accessRightListener);
                        break;
                    }
                }
            }
        }

        public void RemoveListener(AccessRightListenerDelegate listener)
        {
            if (listener != null)
            {
                lock (this.myListenerList.SyncRoot)
                    this.myListenerList.Remove((object)listener);
            }
        }

        public void NotifyListener()
        {
            lock (this.myListenerList.SyncRoot)
            {
                foreach (object item_0 in this.myListenerList)
                {
                    AccessRightListenerDelegate local_1 = (AccessRightListenerDelegate)null;
                    if (item_0 is AccessRightListener)
                        local_1 = ((AccessRightListener)item_0).myListener;
                    else if (item_0 is AccessRightListenerDelegate)
                        local_1 = (AccessRightListenerDelegate)item_0;
                    if (local_1 != null)
                    {
                        try
                        {
                            //  if (local_1.Target is Control)
                            {
                                //    //Control local_2 = (Control)local_1.Target;
                                // if (local_2.InvokeRequired)
                                //      local_2.Invoke((Delegate)new AccessRightListenerDelegate(local_1.Invoke), new object[0]);
                                //   else
                                local_1();
                            }
                            //else
                            //    local_1();
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }
    }
    public class AccessRightSQL : AccesRight
    {
        public override string[] GetAllUserID(string cmdID)
        {
            SqlConnection connection = new SqlConnection(this.myLocalDBSetting.ConnectionString);
            try
            {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT DISTINCT NIK FROM AccessRight WHERE CmdID=@CmdID ORDER BY USER_NAME", connection);
                selectCommand.Parameters.AddWithValue("@CmdID", (object)cmdID);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    string[] strArray = new string[dataTable.Rows.Count];
                    for (int index = 0; index < dataTable.Rows.Count; ++index)
                        strArray[index] = dataTable.Rows[index][0].ToString();
                    return strArray;
                }
                else
                    return new string[0];
            }
            catch (SqlException ex)
            {
                BCE.Data.DataError.HandleSqlException(ex);
                throw;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public override bool IsAccessibleByUserID(string userID, string cmdID)
        {
            bool flag = false;
            try
            {
                SqlConnection connection = new SqlConnection(this.myLocalDBSetting.ConnectionString);
                try
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("SELECT COUNT(*) FROM AccessRight WHERE CmdID=@CmdID AND NIK=@NIK", connection);
                    sqlCommand.Parameters.AddWithValue("@CmdID", (object)cmdID);
                    sqlCommand.Parameters.AddWithValue("@NIK", (object)userID);
                    if (BCE.Data.Convert.ToInt32(sqlCommand.ExecuteScalar()) == 0)
                    {
                        sqlCommand.CommandText = "SELECT COUNT(*) FROM AccessRight where NIK=@NIK AND CmdID=@CmdID";
                        flag = BCE.Data.Convert.ToInt32(sqlCommand.ExecuteScalar()) > 0;
                    }
                    else
                        flag = true;
                }
                catch (SqlException ex)
                {
                    BCE.Data.DataError.HandleSqlException(ex);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logic.ExceptionUtility.LogException(ex, "HttpCall in AccesRright.cs");
                flag = false;
            }
            return flag;
        }

        public override bool IsAccessible(string cmdID)
        {
            if (!this.myUseCache)
            {
                return this.IsAccessibleByUserID(this.myUserID, cmdID);
            }
            else
            {
                lock (this.myLock)
                {
                    if (this.myCacheUserTable == null || this.myCacheUserGroupTable == null)
                    {
                        SqlConnection local_0 = new SqlConnection(this.myLocalDBSetting.ConnectionString);
                        try
                        {
                            local_0.Open();
                            if (this.myCacheUserTable == null)
                                this.myCacheUserTable = new DataTable();
                            else
                                this.myCacheUserTable.Clear();
                            SqlCommand local_1 = new SqlCommand("SELECT DISTINCT CmdID FROM AccessRight WHERE NIK=@NIK", local_0);
                            local_1.Parameters.AddWithValue("@NIK", (object)this.myUserID);
                            SqlDataAdapter local_2 = new SqlDataAdapter(local_1);
                            local_2.Fill(this.myCacheUserTable);
                            if (this.myCacheUserTable.PrimaryKey.Length == 0)
                                this.myCacheUserTable.PrimaryKey = new DataColumn[1]
                {
                  this.myCacheUserTable.Columns[0]
                };
                            this.myCacheUserGroupTable = new DataTable();
                            local_1.CommandText = " SELECT COUNT(*) FROM ACCES " +
                                                  " FROM sch_users usr   " +
                                                 " left outer join SCH_Karyawan_v mk  " +
                                                  " on mk.[ID Karyawan]=usr.UserID      " +
                                                 " inner join  " +
                                                   " (SELECT distinct UPPER(Department)  Department " +
                                                            " FROM   " +
                                                                " sch_karyawan res) md  " +
                                                  "on md.Department =  UPPER(mk.Department)    " +
                                                 " inner join   " +
                                                      " SCH_accessright mat  " +
                                                       "  on mat.UserID =md.Department  " +
                                                                    " WHERE mat.CmdName=CmdName AND usr.userid=@UserID";
                            local_2.Fill(this.myCacheUserGroupTable);
                            if (myCacheUserGroupTable.Rows.Count == 0)
                            {
                                local_1.CommandText = " SELECT COUNT(*) " +
                                                        " FROM sch_users usr   " +
                                                       " left outer join SCH_Karyawan_v mk  " +
                                                        " on mk.[ID Karyawan]=usr.UserID       " +
                                                       " inner join  " +
                                                         "  (SELECT distinct UPPER([Last Level])  LastLevel " +
                                                                 " FROM   " +
                                                                     " sch_karyawan res) md  " +
                                                       " on md.LastLevel =  UPPER(mk.[Last Level])     " +
                                                       " inner join   " +
                                                            " SCH_accessright mat   " +
                                                              " on mat.UserID =md.LastLevel   " +
                                                                     " WHERE mat.CmdName=CmdName AND usr.userid=@UserID";
                                local_2.Fill(this.myCacheUserGroupTable);
                            }
                            if (this.myCacheUserGroupTable.PrimaryKey.Length == 0)
                                this.myCacheUserGroupTable.PrimaryKey = new DataColumn[1]
                {
                  this.myCacheUserGroupTable.Columns[0]
                };
                        }
                        catch (SqlException exception_0)
                        {
                            BCE.Data.DataError.HandleSqlException(exception_0);
                            throw;
                        }
                        finally
                        {
                            local_0.Close();
                            local_0.Dispose();
                        }
                    }
                    if (this.myCacheUserTable.PrimaryKey.Length > 0 && this.myCacheUserTable.Rows.Find((object)cmdID) != null || this.myCacheUserGroupTable.PrimaryKey.Length > 0 && this.myCacheUserGroupTable.Rows.Find((object)cmdID) != null)
                        return true;
                    else
                        return false;
                }
            }
        }
    }
}