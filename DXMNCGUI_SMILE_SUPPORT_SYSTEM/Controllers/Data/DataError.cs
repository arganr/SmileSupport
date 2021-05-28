using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data;
using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Localization;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data.SqlClient;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data
{
    public class DataError
    {
        private const int ERR_CONNECTION = 17;
        private const int ERR_NETWORK = 11;
        private const int ERR_TIMEOUT = -2;
        private const int ERR_LOCATING_INSTANCE = -1;
        private const int ERR_CONNECTING_SQL2005 = 53;
        private const int ERR_THE_SPECIFIED_NETWORK_NAME_IS_NO_LONGER_AVAILABLE = 64;
        private const int ERR_CANNOT_ALLOCATE_SPACE = 1105;
        private const int ERR_CONNECTING_SQL2005_2 = 1326;
        private const int ERR_CONNECTING_SQL2005_3 = 10060;
        private const int ERR_CONNECTING_SQL2005_4 = 10061;
        private const int ERR_CONNECTING_SQL2005_5 = 1231;
        private const int ERR_CONNECTING_SQL2005_6 = 11001;
        private const int ERR_CONNECTING_SQL2005_7 = 10051;
        private const int ERR_CONNECTING_SQL2005_8 = 10065;
        private const int ERR_CONNECTING_SQL2005_9 = 1311;
        private const int ERR_CONNECTION_FORCIBLY_CLOSED = 10054;
        private const int ERR_PRIMARYKEY = 2627;
        private const int ERR_FOREIGNKEY = 547;
        private const int ERR_DEADLOCK = 1205;
        private const int ERR_DATABASE_ALREADY_EXISTS = 1801;
        private const int ERR_CANNOT_OVERWRITTEN = 1834;
        private const int ERR_BACKUP_IN_TRANSACTION = 3021;
        private const int ERR_OPENBACKUPDEVICE = 3201;
        private const int ERR_WRITEBACKUPDEVICE = 3202;
        private const int ERR_NOT_SUPPORTED_MICROSOFT_TAPE_FORMAT = 3239;
        private const int ERR_INVALID_MICROSOFT_TAPE_FORMAT = 3242;
        private const int ERR_OTHER_MICROSOFT_TAPE_FORMAT_VERSION = 3243;
        private const int ERR_INSUFFICIENT_FREE_SPACE_TO_CREATEDATABASE = 3257;
        private const int ERR_NOT_EXISTS_OR_NOT_HAVE_PERMISSION = 3701;
        private const int ERR_CANNOT_DROP_BECAUSE_IN_USE = 3702;
        private const int ERR_CANNOT_DETACH_BECAUSE_IN_USE = 3703;
        private const int ERR_CANNOT_OPEN_DATABASE_LOGINFAILED = 4060;
        private const int ERR_DEVICE_ACTIVATION_ERROR = 5105;
        private const int ERR_COMPRESSED_DRIVE_ERROR = 5118;
        private const int ERR_CANNOT_OPEN_DATABASE_FILE_NOT_FOUND_LOGINFAILED = 5120;
        private const int ERR_CREATEDATABASE = 5170;
        private const int ERR_CREATEDATABASE_FAILED = 1802;
        private const int ERR_LOGINFAILED = 18456;
        private const int ERR_DROPDATABASEFAILED = 15181;
        private const int ERR_TRANSPORT_LEVEL_ERROR = 121;
        private const int ERR_LOGIN_PROCESS = 233;
        private const int ERR_5242 = 5242;
        private const int ERR_11004 = 11004;
        private static ArrayList myExceptionHandlerList;

        public static void AddExceptionHandler(ExceptionHandlerDelegate handler)
        {
            if (DataError.myExceptionHandlerList == null)
                DataError.myExceptionHandlerList = new ArrayList();
            DataError.myExceptionHandlerList.Add((object)handler);
        }

        private static string GetConstraintName(string msg)
        {
            int startIndex = msg.IndexOf("FK_");
            if (startIndex < 0)
                return "";
            int index = startIndex + 3;
            int length = msg.Length;
            while (index < length && msg[index] != '"' && msg[index] != '\'')
                ++index;
            return msg.Substring(startIndex, index - startIndex);
        }

        private static string GetUniqueKeyConstraintName(string msg)
        {
            int num1 = msg.IndexOf("UNIQUE KEY constraint '");
            if (num1 == -1)
                return "";
            int num2 = msg.IndexOf("'", num1 + 23);
            return num2 == -1 ? "" : msg.Substring(num1 + 23, num2 - num1 - 23);
        }

        private static Exception HandleBasicSqlException(SqlException ex)
        {
            if (ex.Number == 17)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnecting), (Exception)ex);
            if (ex.Number == 53)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (Exception)ex);
            if (ex.Number == 1326)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (Exception)ex);
            if (ex.Number == 10060)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_10060)), (Exception)ex);
            if (ex.Number == 10061)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_10061)), (Exception)ex);
            if (ex.Number == 1231)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_1231)), (Exception)ex);
            if (ex.Number == 11001)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_11001)), (Exception)ex);
            if (ex.Number == 10051)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_11001)), (Exception)ex);
            if (ex.Number == 10065)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_10065)), (Exception)ex);
            if (ex.Number == 1311)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_1311)), (Exception)ex);
            if (ex.Number == 10054)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnectionForciblyClosed), (Exception)ex);
            if (ex.Number == 11)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorGeneralNetwork), (Exception)ex);
            if (ex.Number == -2)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorTimeoutExpired), (Exception)ex);
            if (ex.Number == -1)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorLocatingServerOrInstance), (Exception)ex);
            if (ex.Number == 64)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorTheSpecifiedNetworkNameIsNoLongerAvailable), (Exception)ex);
            if (ex.Number == 18456)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorLoginFailed), (Exception)ex);
            if (ex.Number == 1205)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorDeadLock), (Exception)ex);
            if (ex.Number == 5170 || ex.Number == 1802 || (ex.Number == 1105 || ex.Number == 3201) || (ex.Number == 3202 || ex.Number == 3021 || (ex.Number == 1834 || ex.Number == 1801)) || (ex.Number == 3239 || ex.Number == 3257 || (ex.Number == 3242 || ex.Number == 3243) || (ex.Number == 15181 || ex.Number == 3702 || (ex.Number == 3701 || ex.Number == 3703))) || (ex.Number == 5105 || ex.Number == 5118 || (ex.Number == 4060 || ex.Number == 5120) || (ex.Number == 121 || ex.Number == 233 || (ex.Number == 5242 || ex.Number == 11004))))
                return (Exception)new CriticalSqlException(ex.Message, (Exception)ex);
            if (ex.Number == 2627)
                return (Exception)new PrimaryKeyException(Localizer.GetString((Enum)DXStringId.ErrorPrimaryKey), DataError.GetUniqueKeyConstraintName(ex.Message), (Exception)ex);
            if (ex.Number == 547)
            {
                string constraintName = DataError.GetConstraintName(ex.Message);
                return constraintName.Length == 0 ? (Exception)new ForeignKeyException(Localizer.GetString((Enum)DXStringId.ErrorForeignKeyMessage, (object)ex.Message), "", (Exception)ex) : (Exception)new ForeignKeyException(Localizer.GetString((Enum)DXStringId.ErrorForeignKeyConstraint, (object)constraintName), constraintName, (Exception)ex);
            }
            return ex.Number < 100 ? (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorCriticalSql, (object)ex.Number, (object)ex.Message), (Exception)ex) : new Exception(Localizer.GetString((Enum)DXStringId.ErrorUnknownSQLError, (object)ex.Number, (object)ex.Message), (Exception)ex);
        }

        public static void HandleSqlException(SqlException ex)
        {
            Exception ex1 = DataError.HandleBasicSqlException(ex);
            if (DataError.myExceptionHandlerList != null)
            {
                for (int index = DataError.myExceptionHandlerList.Count - 1; index >= 0; --index)
                {
                    ExceptionHandlerDelegate exceptionHandler = (ExceptionHandlerDelegate)DataError.myExceptionHandlerList[index];
                    if (exceptionHandler != null)
                    {
                        ExceptionHandlerEventArgs e = new ExceptionHandlerEventArgs(ex1);
                        exceptionHandler(e);
                        if (e.Handled)
                            throw e.Exception;
                    }
                }
            }
            throw ex1;
        }

        private static Exception HandleBasicMySqlException(MySqlException ex)
        {
            if (ex.Number == 17)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnecting), (Exception)ex);
            if (ex.Number == 53)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (Exception)ex);
            if (ex.Number == 1326)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (Exception)ex);
            if (ex.Number == 10060)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_10060)), (Exception)ex);
            if (ex.Number == 10061)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_10061)), (Exception)ex);
            if (ex.Number == 1231)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_1231)), (Exception)ex);
            if (ex.Number == 11001)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_11001)), (Exception)ex);
            if (ex.Number == 10051)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_11001)), (Exception)ex);
            if (ex.Number == 10065)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_10065)), (Exception)ex);
            if (ex.Number == 1311)
                return (Exception)new CriticalSqlException(string.Format("{0}\n\n{1}", (object)Localizer.GetString((Enum)DXStringId.ErrorConnectingSQL2005), (object)Localizer.GetString((Enum)DXStringId.ErrorSQL2005_1311)), (Exception)ex);
            if (ex.Number == 10054)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorConnectionForciblyClosed), (Exception)ex);
            if (ex.Number == 11)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorGeneralNetwork), (Exception)ex);
            if (ex.Number == -2)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorTimeoutExpired), (Exception)ex);
            if (ex.Number == -1)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorLocatingServerOrInstance), (Exception)ex);
            if (ex.Number == 64)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorTheSpecifiedNetworkNameIsNoLongerAvailable), (Exception)ex);
            if (ex.Number == 18456)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorLoginFailed), (Exception)ex);
            if (ex.Number == 1205)
                return (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorDeadLock), (Exception)ex);
            if (ex.Number == 5170 || ex.Number == 1802 || (ex.Number == 1105 || ex.Number == 3201) || (ex.Number == 3202 || ex.Number == 3021 || (ex.Number == 1834 || ex.Number == 1801)) || (ex.Number == 3239 || ex.Number == 3257 || (ex.Number == 3242 || ex.Number == 3243) || (ex.Number == 15181 || ex.Number == 3702 || (ex.Number == 3701 || ex.Number == 3703))) || (ex.Number == 5105 || ex.Number == 5118 || (ex.Number == 4060 || ex.Number == 5120) || (ex.Number == 121 || ex.Number == 233 || (ex.Number == 5242 || ex.Number == 11004))))
                return (Exception)new CriticalSqlException(ex.Message, (Exception)ex);
            if (ex.Number == 2627)
                return (Exception)new PrimaryKeyException(Localizer.GetString((Enum)DXStringId.ErrorPrimaryKey), DataError.GetUniqueKeyConstraintName(ex.Message), (Exception)ex);
            if (ex.Number == 547)
            {
                string constraintName = DataError.GetConstraintName(ex.Message);
                return constraintName.Length == 0 ? (Exception)new ForeignKeyException(Localizer.GetString((Enum)DXStringId.ErrorForeignKeyMessage, (object)ex.Message), "", (Exception)ex) : (Exception)new ForeignKeyException(Localizer.GetString((Enum)DXStringId.ErrorForeignKeyConstraint, (object)constraintName), constraintName, (Exception)ex);
            }
            return ex.Number < 100 ? (Exception)new CriticalSqlException(Localizer.GetString((Enum)DXStringId.ErrorCriticalSql, (object)ex.Number, (object)ex.Message), (Exception)ex) : new Exception(Localizer.GetString((Enum)DXStringId.ErrorUnknownSQLError, (object)ex.Number, (object)ex.Message), (Exception)ex);
        }

        public static void HandleMySqlException(MySqlException ex)
        {
            Exception ex1 = DataError.HandleBasicMySqlException(ex);
            if (DataError.myExceptionHandlerList != null)
            {
                for (int index = DataError.myExceptionHandlerList.Count - 1; index >= 0; --index)
                {
                    ExceptionHandlerDelegate exceptionHandler = (ExceptionHandlerDelegate)DataError.myExceptionHandlerList[index];
                    if (exceptionHandler != null)
                    {
                        ExceptionHandlerEventArgs e = new ExceptionHandlerEventArgs(ex1);
                        exceptionHandler(e);
                        if (e.Handled)
                            throw e.Exception;
                    }
                }
            }
            throw ex1;
        }
    }
}