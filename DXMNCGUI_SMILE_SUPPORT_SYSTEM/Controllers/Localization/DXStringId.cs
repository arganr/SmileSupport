using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Localization
{
    [LocalizableString]
    public enum DXStringId
    {
        [DefaultString("&OK")]
        Button_OK,
        [DefaultString("&Cancel")]
        Button_Cancel,
        [DefaultString("Edit")]
        Button_Edit,
        [DefaultString("View")]
        Button_View,
        [DefaultString("Delete")]
        Button_Delete,
        [DefaultString("Preview")]
        Button_Preview,
        [DefaultString("Print")]
        Button_Print,
        [DefaultString("Refresh")]
        Button_Refresh,
        [DefaultString("Error while connecting to SQL Server, please check the network, firewall and make sure SQL Server is running and is network-enabled. Then try again.")]
        ErrorConnecting,
        [DefaultString("Error while connecting to SQL Server 2005, please check the network, firewall and make sure SQL Server 2005 is running and is network-enabled. Then try again.")]
        ErrorConnectingSQL2005,
        [DefaultString("(Note: Inner Error Message is 'A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond.')")]
        ErrorSQL2005_10060,
        [DefaultString("(Note: Inner Error Message is 'No connection could be made because the target machine actively refused it.')")]
        ErrorSQL2005_10061,
        [DefaultString("(Note: Inner Error Message is 'A socket operation was attempted to an unreachable host.')")]
        ErrorSQL2005_10065,
        [DefaultString("(Note: Inner Error Message is 'An error has occurred while establishing a connection to the server.')")]
        ErrorSQL2005_1311,
        [DefaultString("(Note: Inner Error Message is 'A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections.')")]
        ErrorSQL2005_1231,
        [DefaultString("(Note: Inner Error Message is 'No such host is known.')")]
        ErrorSQL2005_11001,
        [DefaultString("(Note: Inner Error Message is 'A socket operation was attempted to an unreachable network.')")]
        ErrorSQL2005_10051,
        [DefaultString("An existing connection was forcibly closed by the remote host.")]
        ErrorConnectionForciblyClosed,
        [DefaultString("General network error. Check your network documentation and try again.")]
        ErrorGeneralNetwork,
        [DefaultString("Timeout expired error. Your server is either too busy or your command take too long to complete, you may try again later.")]
        ErrorTimeoutExpired,
        [DefaultString("Error Locating Server/Instance Specified.")]
        ErrorLocatingServerOrInstance,
        [DefaultString("The specified network name is no longer available.")]
        ErrorTheSpecifiedNetworkNameIsNoLongerAvailable,
        [DefaultString("Login failed. Incorrect password.")]
        ErrorLoginFailed,
        [DefaultString("Critial Sql Exception (Number={0}, Message={1})")]
        ErrorCriticalSql,
        [DefaultString("Primary Key Error")]
        ErrorPrimaryKey,
        [DefaultString("Foreign Key Error (Message={0})")]
        ErrorForeignKeyMessage,
        [DefaultString("Foreign Key Error (Constraint Name={0})")]
        ErrorForeignKeyConstraint,
        [DefaultString("There was a database transaction deadlock occurred, please try again.")]
        ErrorDeadLock,
        [DefaultString("Unknown Sql Exception (Number={0}, Message={1})")]
        ErrorUnknownSQLError,
        [DefaultString("Access Denied.")]
        AccessDenied,
        [DefaultString("(Note: If you want to access this function, please contact your AutoCount System Administrator.)")]
        ContactSystemAdministrator,
        [DefaultString("Invalid user ID or password.")]
        InvalidUserIdOrPassword,
        [DefaultString("The new user does not have the access right to access this function.")]
        NewUserAccessDenied,
        [DefaultString("No SQL Server is selected.")]
        Data_NoSQLServer,
        [DefaultString("No SQL Database Name is selected.")]
        Data_NoSQLDatabaseName,
        [DefaultString("Please change your Log On Account as Local System account for SQL Server 2005 at Windows Services.")]
        BackupRestore_ChangeSQLServer2005LogOnAccountToLocalSystem,
        [DefaultString("The Server Backup Shared Folder and the Client Access Shared Folder Path does not point to same folder.\nPlease re-enter your Database Server Info and try again.")]
        BackupRestore_UnmatchServerBackupDirectory,
        [DefaultString("Unable to determine file size.")]
        BackupRestore_UnableToDetermineFileSize,
        [DefaultString("Unknown error when checking the file/directory exist or not.")]
        BackupRestore_UnknownErrorInCheckingFileExistence,
        [DefaultString("Unable to create directory.")]
        BackupRestore_UnableToCreateDirectory,
        [DefaultString("Unable to access to network shared folder.")]
        BackupRestore_UnauthorizedAccessNetworkFolder,
        [DefaultString("Path too long.")]
        BackupRestore_PathTooLong,
        [DefaultString("The path specified is invalid. Please check the Database Server Info and try again.")]
        BackupRestore_BackupDirectoryNotFound,
        [DefaultString("I/O Error.")]
        BackupRestore_IOError,
        [DefaultString("Please make sure the Database Server Info is correct or you have to change the LogOn user account of DX Scheduled Backup service.")]
        BackupRestore_FileNotFound,
        [DefaultString("Error while copying file. {0}\n1) Please make sure the Client Access Shared Folder Path and Server Backup Shared Folder point to same folder.\n2) Or make sure you can access to Client Access Shared Folder Path.")]
        BackupRestore_CopyFileError,
        [DefaultString("Please make sure you have sufficient permission to read file from {0} and write to {1}. If you're using Vista, please don't backup to Root folder of Drive C.")]
        BackupRestore_NoReadWritePermission,
        [DefaultString("Please make sure the Database Server Info is correct or you have not logged on to shared folder in Windows Explorer or you have to change the Log On user account of AutoCount Scheduled Backup service.")]
        BackupRestore_UnableToAccessSharedFolder,
        [DefaultString("I/O error when reading file from {0} and write to {1}.")]
        BackupRestore_IOErrorWhenReadWrite,
        [DefaultString("You don't have sufficient permission to delete {0}.")]
        BackupRestore_NoPermissionToDeleteFile,
        [DefaultString("The path specified is invalid.")]
        BackupRestore_InvalidPath,
        [DefaultString("An I/O error occurred while deleting {0}.")]
        BackupRestore_IOErrorWhenDelete,
        [DefaultString("Invalid DDL file.")]
        InvalidDDLFile,
        [DefaultString("Invalid DDL Statement {0}, error message is {1}.")]
        InvalidDDLStatement,
        [DefaultString("Expected digit character at position {0}.")]
        Parser_ExpectedDigitCharacter,
        [DefaultString("Expected character '+' or '/' at position {0}.")]
        Parser_ExpectedPlusOrSlashCharacter,
        [DefaultString("Invalid discount character '{0}' at position {1}.")]
        Parser_InvalidDiscountCharacter,
        [DefaultString("Invalid markup character '{0}' at position {1}.")]
        Parser_InvalidMarkupCharacter,
        [DefaultString("Auto Width")]
        XtraUtils_AutoWidth,
        [DefaultString("Export to Excel")]
        XtraUtils_ExportToExcel,
        [DefaultString("Export to Html")]
        XtraUtils_ExportToHtml,
        [DefaultString("Export to Text")]
        XtraUtils_ExportToText,
        [DefaultString("Export to Xml")]
        XtraUtils_ExportToXml,
        [DefaultString("Print Grid")]
        XtraUtils_PrintGrid,
        [DefaultString("Html files")]
        XtraUtils_HtmlFiles,
        [DefaultString("Microsoft Excel files")]
        XtraUtils_ExcelFiles,
        [DefaultString("Text files")]
        XtraUtils_TextFiles,
        [DefaultString("Xml files")]
        XtraUtils_XmlFiles,
        [DefaultString("Do you really want to delete this {0} '{1}'?")]
        ConfirmDelete,
        [DefaultString("Data was modified. Do you want to cancel?")]
        ConfirmCancel,
        [DefaultString("Data was modified. Do you want to save changes?")]
        ConfirmSaveChanges,
        [DefaultString("Window")]
        ThreadForm_Window,
        [DefaultString("Send To Back")]
        ThreadForm_SendToBack,
        [DefaultString("Window List")]
        ThreadForm_WindowList,
        [DefaultString("Yes")]
        YesNo_Yes,
        [DefaultString("No")]
        YesNo_No,
        [DefaultString("Version {0} (Build: {1}, Revision: {2})")]
        CompleteVersion,
    }
}