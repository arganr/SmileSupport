using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Globalization;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public delegate void BeforeOpenMySqlConnectionEventHandler(MySqlDBSetting dbSetting);
    [Serializable]

    public class MySqlDBSetting : IDisposable
    {
        private int myCommandTimeout = 30;
        private DBServerType myServerType;
        private string myServerName;
        private string myDBName;
        private string myConnectionString;
        [NonSerialized]
        private bool myStoredProcedure;
        [NonSerialized]
        private MySqlConnection myConnection;
        [NonSerialized]
        private MySqlTransaction myTransaction;
        [NonSerialized]
        private int myLevel;
        [NonSerialized]
        private int myLevel2;
        private BeforeOpenMySqlConnectionEventHandler BeforeOpenMySqlConnectionEvent;

        public DBServerType ServerType
        {
            get
            {
                return this.myServerType;
            }
        }
        public string ServerName
        {
            get
            {
                if (this.myServerName.Length == 0)
                {
                    string str1 = this.myConnectionString;
                    char[] chArray = new char[1]
          {
            ';'
          };
                    foreach (string str2 in str1.Split(chArray))
                    {
                        if (str2.StartsWith("server=", StringComparison.InvariantCultureIgnoreCase))
                            return str2.Substring(7).Trim();
                    }
                    return "";
                }
                else
                    return this.myServerName;
            }
        }
        public string DBName
        {
            get
            {
                return this.myDBName;
            }
        }
        public string ConnectionString
        {
            get
            {
                return this.myConnectionString;
            }
        }
        public int CommandTimeOut
        {
            get
            {
                return this.myCommandTimeout;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                else
                    this.myCommandTimeout = value;
            }
        }
        public bool StoredProcedure
        {
            get
            {
                return this.myStoredProcedure;
            }
            set
            {
                this.myStoredProcedure = value;
            }
        }
        public string PrivateDataPath
        {
            get
            {
                MySqlConnection sqlConnection = new MySqlConnection(this.myConnectionString);
                string str = sqlConnection.Database + (sqlConnection.DataSource.Length > 0 ? "@" + sqlConnection.DataSource : "");
                StringBuilder stringBuilder = new StringBuilder();
                for (int index = 0; index < str.Length; ++index)
                {
                    bool flag = false;
                    foreach (char ch in Path.GetInvalidFileNameChars())
                    {
                        if ((int)str[index] == (int)ch)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                        stringBuilder.Append(str[index]);
                }
                string path = Application.Application.ApplicationDataPath + (object)Path.DirectorySeparatorChar + ((object)stringBuilder).ToString();
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public string PrivateCachingDataPath
        {
            get
            {
                string path = this.PrivateDataPath + (object)Path.DirectorySeparatorChar + "Cache";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        public string PrivateCriteriaDataPath
        {
            get
            {
                string path = this.PrivateDataPath + (object)Path.DirectorySeparatorChar + "Criteria";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }
        [Obsolete("You should not access to Connection object to associate it to a SqlCommand. Instead, you better use CreateCommand to create your own SqlCommand.")]
        public MySqlConnection Connection
        {
            get
            {
                return this.myConnection;
            }
        }
        [Obsolete("You should not access to Transaction object to associate it to a SqlCommand. Instead, you better use CreateCommand to create your own SqlCommand.")]
        public MySqlTransaction Transaction
        {
            get
            {
                return this.myTransaction;
            }
        }
        [Obsolete("You should use StartTransaction instead.")]
        public MySqlTransaction BeginTransaction()
        {
            if (this.myLevel == 0)
            {
                throw new InvalidOperationException("Please call CopyForTransaction first.");
            }
            else
            {
                if (this.myLevel == 1)
                {
                    if (this.myConnection == null)
                        this.OpenConnection();
                    else
                        ++this.myLevel2;
                }
                else if (this.myTransaction == null || this.myTransaction.Connection == null)
                    throw new InvalidOperationException("The transaction is end, cannot start new transaction.");
                return this.myTransaction;
            }
        }
        [Obsolete("You should change to StartTransaction to make the coding simple yet elegant.")]
        public MySqlDBSetting BeginTransaction(bool alwaysUseTransaction, out MySqlConnection conn, out MySqlTransaction tran)
        {
            MySqlDBSetting dbSetting = this.StartTransaction();
            tran = dbSetting.myTransaction;
            conn = dbSetting.myConnection;
            return dbSetting;
        }
        public event BeforeOpenMySqlConnectionEventHandler myBeforeOpenLocalConnectionEvent
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                this.BeforeOpenMySqlConnectionEvent = this.BeforeOpenMySqlConnectionEvent + value;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                this.BeforeOpenMySqlConnectionEvent = this.BeforeOpenMySqlConnectionEvent - value;
            }
        }
        public MySqlDBSetting(DBServerType serverType, string connectionString, int commandTimeout)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }
            else
            {
                this.myServerType = serverType;
                this.myServerName = string.Empty;
                this.myDBName = string.Empty;
                this.myConnectionString = connectionString;
                this.CommandTimeOut = commandTimeout;
            }
        }
        public MySqlDBSetting(DBServerType serverType, string serverName, string userId, string password, string dbName)
            : this(serverType, serverName, userId, password, dbName, true)
        {
        }
        public MySqlDBSetting(DBServerType serverType, string serverName, string userId, string password, string dbName, bool usePool)
        {
            if (serverName == null)
                throw new ArgumentNullException("serverName");
            else if (userId == null)
                throw new ArgumentNullException("userId");
            else if (password == null)
                throw new ArgumentNullException("password");
            else if (dbName == null)
            {
                throw new ArgumentNullException("dbName");
            }
            else
            {
                this.myServerType = serverType;
                this.myServerName = serverName;
                this.myDBName = dbName;
                this.myConnectionString = string.Format((IFormatProvider)CultureInfo.InvariantCulture, "packet size=4096;user id={0};password={1};data source={2};initial catalog={3}" + (usePool ? "" : ";Pooling=False"), (object)userId, (object)password, (object)serverName, (object)dbName);
            }
        }
        public MySqlDBSetting(DBServerType serverType, string serverName, string dbName)
            : this(serverType, serverName, dbName, true)
        {
        }
        public MySqlDBSetting(DBServerType serverType, string serverName, string dbName, bool usePool)
        {
            if (serverName == null)
                throw new ArgumentNullException("serverName");
            else if (dbName == null)
            {
                throw new ArgumentNullException("dbName");
            }
            else
            {
                this.myServerType = serverType;
                this.myServerName = serverName;
                this.myDBName = dbName;
                this.myConnectionString = string.Format((IFormatProvider)CultureInfo.InvariantCulture, "workstation id=\"{0}\";packet size=4096;integrated security=SSPI;data source={1};initial catalog={2}" + (usePool ? "" : ";Pooling=False"), (object)Environment.MachineName, (object)serverName, (object)dbName);
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }
        private void Dispose(bool disposing)
        {
            if (disposing)
                this.EndTransaction();
        }
        private void OpenConnection()
        {
            try
            {
                if (this.BeforeOpenMySqlConnectionEvent != null)
                    this.BeforeOpenMySqlConnectionEvent(this);
                this.myConnection = new MySqlConnection(this.ConnectionString);
                this.myConnection.Open();
                this.myTransaction = this.myConnection.BeginTransaction();
            }
            catch (Exception ex1)
            {
                try
                {
                    this.myConnection.Close();
                }
                catch
                {
                }
                this.myConnection = (MySqlConnection)null;
                this.myTransaction = (MySqlTransaction)null;
                MySqlException ex2 = ex1 as MySqlException;
                if (ex2 != null)
                    DataError.HandleMySqlException(ex2);
                else
                    throw;
            }
        }
        public void ThrowServerTypeNotSupportedException()
        {
            throw new ArgumentException("Server type: " + (object)this.ServerType + " not supported.");
        }
        public MySqlDBSetting Copy()
        {
            return new MySqlDBSetting(this.ServerType, this.ConnectionString, this.CommandTimeOut)
            {
                myServerName = this.ServerName,
                myDBName = this.DBName,
                BeforeOpenMySqlConnectionEvent = this.BeforeOpenMySqlConnectionEvent
            };
        }
        public MySqlCommand CreateCommand()
        {
            return this.CreateCommand(string.Empty, (object[])null);
        }
        public MySqlCommand CreateCommand(string cmdText, params object[] parameters)
        {
            if (this.myConnection == null || this.myConnection.State != ConnectionState.Open)
                throw new InvalidOperationException("Please call StartTransaction to open the database connection.");
            else if (this.myTransaction != null && this.myTransaction.Connection == null)
            {
                throw new InvalidOperationException("The transaction has completed, it is no longer usable.");
            }
            else
            {
                if (cmdText == null)
                    cmdText = string.Empty;
                if (parameters != null)
                {
                    bool flag = false;
                    for (int index = 0; index < parameters.Length; ++index)
                    {
                        if (!(parameters[index] is MySqlParameter))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        StringBuilder stringBuilder = new StringBuilder();
                        string[] strArray = cmdText.Split(new char[1]
            {
              '?'
            });
                        for (int index = 0; index < strArray.Length; ++index)
                        {
                            stringBuilder.Append(strArray[index]);
                            if (index != strArray.Length - 1)
                                stringBuilder.Append("@p" + (object)(index + 1));
                        }
                        cmdText = ((object)stringBuilder).ToString();
                    }
                }
                MySqlCommand sqlCommand = new MySqlCommand(cmdText, this.myConnection, this.myTransaction);
                sqlCommand.CommandTimeout = this.CommandTimeOut;
                if (this.StoredProcedure)
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    int num = 1;
                    for (int index = 0; index < parameters.Length; ++index)
                    {
                        if (parameters[index] is MySqlParameter)
                        {
                            MySqlParameter sqlParameter1 = (MySqlParameter)parameters[index];
                            MySqlParameter sqlParameter2 = new MySqlParameter(sqlParameter1.ParameterName, sqlParameter1.MySqlDbType, sqlParameter1.Size, sqlParameter1.Direction, sqlParameter1.IsNullable, sqlParameter1.Precision, sqlParameter1.Scale, sqlParameter1.SourceColumn, sqlParameter1.SourceVersion, sqlParameter1.Value);
                            sqlCommand.Parameters.Add(sqlParameter2);
                        }
                        else
                        {
                            MySqlParameter sqlParameter = new MySqlParameter("@p" + (object)num++, parameters[index]);
                            sqlCommand.Parameters.Add(sqlParameter);
                        }
                    }
                }
                return sqlCommand;
            }
        }
        public MySqlDBSetting StartTransaction()
        {
            return this.StartTransaction(this.CommandTimeOut);
        }
        public MySqlDBSetting StartTransaction(int commandTimeout)
        {
            MySqlDBSetting dbSetting = this.Copy();
            dbSetting.CommandTimeOut = commandTimeout;
            dbSetting.myLevel = this.myLevel + 1;
            if (this.myLevel == 0)
                dbSetting.OpenConnection();
            else if (this.myTransaction == null || this.myTransaction.Connection == null)
            {
                throw new InvalidOperationException("The transaction is end, cannot start new transaction.");
            }
            else
            {
                dbSetting.myConnection = this.myConnection;
                dbSetting.myTransaction = this.myTransaction;
            }
            return dbSetting;
        }
        public void Commit()
        {
            if (this.myLevel == 1 && this.myTransaction != null && this.myTransaction.Connection != null)
            {
                if (this.myLevel2 <= 0)
                {
                    try
                    {
                        this.myTransaction.Commit();
                    }
                    catch (MySqlException ex)
                    {
                        DataError.HandleMySqlException(ex);
                    }
                }
            }
        }
        public void Rollback()
        {
            if (this.myLevel == 1 && this.myTransaction != null && this.myTransaction.Connection != null)
            {
                if (this.myLevel2 <= 0)
                {
                    try
                    {
                        ((DbTransaction)this.myTransaction).Rollback();
                    }
                    catch
                    {
                    }
                }
            }
        }
        public void EndTransaction()
        {
            if (this.myLevel == 1)
            {
                if (this.myLevel2 > 0)
                {
                    --this.myLevel2;
                }
                else
                {
                    if (this.myTransaction != null && this.myTransaction.Connection != null)
                        this.Rollback();
                    if (this.myConnection != null)
                    {
                        try
                        {
                            this.myConnection.Close();
                        }
                        catch
                        {
                        }
                    }
                    this.myConnection = (MySqlConnection)null;
                    this.myTransaction = (MySqlTransaction)null;
                }
            }
        }
        public object ExecuteScalar(string cmdText, params object[] parameters)
        {
            if (cmdText == null)
            {
                throw new ArgumentNullException("cmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    object obj = dbSetting.CreateCommand(cmdText, parameters).ExecuteScalar();
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                    return obj;
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
                return (object)null;
            }
        }
        public int ExecuteNonQuery(string cmdText, params object[] parameters)
        {
            if (cmdText == null)
            {
                throw new ArgumentNullException("cmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    int num = dbSetting.CreateCommand(cmdText, parameters).ExecuteNonQuery();
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                    return num;
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
                return 0;
            }
        }
        public DataRow GetFirstDataRow(string cmdText, params object[] parameters)
        {
            if (cmdText == null)
            {
                throw new ArgumentNullException("cmdText");
            }
            else
            {
                DataTable dataTable = this.GetDataTable(cmdText, false, parameters);
                if (dataTable.Rows.Count <= 0)
                    return (DataRow)null;
                else
                    return dataTable.Rows[0];
            }
        }
        public DataTable GetDataTable(string cmdText, bool loadSchema, params object[] parameters)
        {
            if (cmdText == null)
            {
                throw new ArgumentNullException("cmdText");
            }
            else
            {
                DataTable table = new DataTable();
                this.LoadDataTable(table, cmdText, loadSchema, parameters);
                return table;
            }
        }
        public DataTable GetSchema(string selectCmdText)
        {
            if (selectCmdText == null)
            {
                throw new ArgumentNullException("selectCmdText");
            }
            else
            {
                DataTable table = new DataTable();
                this.LoadSchema(table, selectCmdText);
                return table;
            }
        }
        public int LoadDataTable(DataTable table, string cmdText, bool loadSchema, params object[] parameters)
        {
            if (table == null)
                throw new ArgumentNullException("table");
            else if (cmdText == null)
            {
                throw new ArgumentNullException("cmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(dbSetting.CreateCommand(cmdText, parameters));
                    if (loadSchema)
                        sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    int num = sqlDataAdapter.Fill(table);
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                    return num;
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
                return 0;
            }
        }
        public int LoadDataSet(DataSet ds, string tableName, string cmdText, bool loadSchema, params object[] parameters)
        {
            if (ds == null)
                throw new ArgumentNullException("ds");
            else if (tableName == null)
                throw new ArgumentNullException("tableName");
            else if (tableName.Length == 0)
                throw new ArgumentException("Table name cannot be empty.");
            else if (cmdText == null)
            {
                throw new ArgumentNullException("cmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter(dbSetting.CreateCommand(cmdText, parameters));
                    if (loadSchema)
                        sqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    int num = sqlDataAdapter.Fill(ds, tableName);
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                    return num;
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
                return 0;
            }
        }
        public void LoadSchema(DataTable table, string selectCmdText)
        {
            if (table == null)
                throw new ArgumentNullException("table");
            else if (selectCmdText == null)
            {
                throw new ArgumentNullException("selectCmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    new MySqlDataAdapter(dbSetting.CreateCommand(selectCmdText, new object[0])).FillSchema(table, SchemaType.Source);
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
            }
        }
        public void LoadSchema(DataSet ds, string tableName, string selectCmdText)
        {
            if (ds == null)
                throw new ArgumentNullException("ds");
            else if (tableName == null)
                throw new ArgumentNullException("tableName");
            else if (tableName.Length == 0)
                throw new ArgumentException("Table name cannot be empty.");
            else if (selectCmdText == null)
            {
                throw new ArgumentNullException("selectCmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    new MySqlDataAdapter(dbSetting.CreateCommand(selectCmdText, new object[0])).FillSchema(ds, SchemaType.Source, tableName);
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
            }
        }
        public int SimpleSaveDataTable(DataTable table, string selectCmdText)
        {
            if (table == null)
                throw new ArgumentNullException("table");
            else if (selectCmdText == null)
            {
                throw new ArgumentNullException("selectCmdText");
            }
            else
            {
                MySqlDBSetting dbSetting = this.myLevel == 0 ? this.StartTransaction() : this;
                try
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(dbSetting.CreateCommand(selectCmdText, new object[0]));
                    MySqlCommandBuilder sqlCommandBuilder = new MySqlCommandBuilder(adapter);
                    int num = adapter.Update(table);
                    if (this.myLevel == 0)
                        dbSetting.Commit();
                    return num;
                }
                catch (MySqlException ex)
                {
                    DataError.HandleMySqlException(ex);
                }
                finally
                {
                    if (this.myLevel == 0)
                        dbSetting.EndTransaction();
                }
                return 0;
            }
        }
        public int SimpleSaveDataSet(DataSet ds, string tableName, string selectCmdText)
        {
            if (ds == null)
                throw new ArgumentNullException("ds");
            else if (tableName == null)
                throw new ArgumentNullException("tableName");
            else if (tableName.Length == 0)
                throw new ArgumentException("Table name cannot be empty.");
            else if (selectCmdText == null)
                throw new ArgumentNullException("selectCmdText");
            else
                return this.SimpleSaveDataTable(ds.Tables[tableName], selectCmdText);
        }
        public DateTime GetServerTime()
        {
            object obj = this.ExecuteScalar("SELECT NOW()", new object[0]);
            if (obj is DateTime)
                return (DateTime)obj;
            else
                return DateTime.Now;
        }
        [Obsolete("You should use StartTransaction instead. CopyForTransaction method will be removed in the future.")]
        public MySqlDBSetting CopyForTransaction()
        {
            MySqlDBSetting dbSetting = this.Copy();
            dbSetting.myLevel = this.myLevel + 1;
            dbSetting.myConnection = this.myConnection;
            dbSetting.myTransaction = this.myTransaction;
            return dbSetting;
        }
    }
}