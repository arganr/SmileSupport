using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Localization;
using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security.AccessControl;
using System.Text;


namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Application
{
    public class Application
    {
        private static string myStartupPath;

        internal Application()
        {
        }

        [Obsolete("You should use Application.OEM.GetCurrentOEM().")]
        public static OEM AppOEM
        {
            get
            {
                return OEM.GetCurrentOEM();
            }
        }

        public static string GetFullProductVersion()
        {
            return System.Windows.Forms.Application.ProductVersion;
        }

        public static string GetCompleteProductVersion(string verSuffix)
        {
            string str = Application.GetMajorMinorProductVersion() + verSuffix;
            string[] strArray = Application.GetFullProductVersion().Split('.');
            return strArray.Length >= 4 ? Localizer.GetString((Enum)DXStringId.CompleteVersion, (object)str, (object)strArray[2], (object)strArray[3]) : (strArray.Length >= 3 ? Localizer.GetString((Enum)DXStringId.CompleteVersion, (object)str, (object)strArray[2], (object)"") : Localizer.GetString((Enum)DXStringId.CompleteVersion, (object)str, (object)"", (object)""));
        }

        public static string GetCompleteProductVersion()
        {
            return Application.GetCompleteProductVersion("");
        }

        public static string GetProductVersion()
        {
            string[] strArray = Application.GetFullProductVersion().Split('.');
            StringBuilder stringBuilder = new StringBuilder(10);
            for (int index = 0; index < 3; ++index)
            {
                if (index < strArray.Length)
                {
                    if (index > 0)
                        stringBuilder.Append('.');
                    stringBuilder.Append(strArray[index]);
                }
            }
            return stringBuilder.ToString();
        }

        public static string GetMajorMinorProductVersion()
        {
            string[] strArray = Application.GetFullProductVersion().Split('.');
            StringBuilder stringBuilder = new StringBuilder(10);
            for (int index = 0; index < 2; ++index)
            {
                if (index < strArray.Length)
                {
                    if (index > 0)
                        stringBuilder.Append('.');
                    stringBuilder.Append(strArray[index]);
                }
            }
            return stringBuilder.ToString();
        }

        public static void SetStartupPath(string path)
        {
            Application.myStartupPath = path;
        }

        private static void OpenFolderFullControlAccessRight(string directory, string identity)
        {
            if (!Directory.Exists(directory))
                return;
            FileSystemAccessRule rule = new FileSystemAccessRule(identity, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow);
            DirectorySecurity accessControl = Directory.GetAccessControl(directory, AccessControlSections.Access);
            accessControl.AddAccessRule(rule);
            accessControl.SetAccessRule(rule);
            Directory.SetAccessControl(directory, accessControl);
        }

        public static string CommonApplicationDataPath
        {
            get
            {
                string str = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + (object)Path.DirectorySeparatorChar + OEM.GetCurrentOEM().ApplicationDataPath;
                if (!Directory.Exists(str))
                    Directory.CreateDirectory(str);
                try
                {
                    Application.OpenFolderFullControlAccessRight(str, "Authenticated Users");
                    Application.OpenFolderFullControlAccessRight(str, "Users");
                }
                catch
                {
                }
                return str;
            }
        }

        public static string ApplicationDataPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + (object)Path.DirectorySeparatorChar + OEM.GetCurrentOEM().ApplicationDataPath;
            }
        }

        public static string UserSettingDataPath
        {
            get
            {
                string path = Application.ApplicationDataPath + (object)Path.DirectorySeparatorChar + "User Settings";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        public static void SaveSettings(object obj, string fileName)
        {
            try
            {
                using (Stream serializationStream = (Stream)File.Open(fileName, FileMode.Create))
                    new BinaryFormatter().Serialize(serializationStream, obj);
            }
            catch
            {
            }
        }

        public static object LoadSettings(string fileName)
        {
            if (!File.Exists(fileName))
                return (object)null;
            try
            {
                using (Stream serializationStream = (Stream)File.Open(fileName, FileMode.Open))
                {
                    try
                    {
                        return new BinaryFormatter().Deserialize(serializationStream);
                    }
                    catch
                    {
                        serializationStream.Position = 0L;
                        return new SoapFormatter().Deserialize(serializationStream);
                    }
                }
            }
            catch
            {
                return (object)null;
            }
        }

        public static void SaveUserSettings(object obj, string fileName)
        {
            string fileName1 = Application.UserSettingDataPath + (object)Path.DirectorySeparatorChar + fileName;
            Application.SaveSettings(obj, fileName1);
        }

        public static object LoadUserSettings(string fileName)
        {
            return Application.LoadSettings(Application.UserSettingDataPath + (object)Path.DirectorySeparatorChar + fileName);
        }

        public static bool IsGroupSimilarTaskBarButtonsOn()
        {
            OperatingSystem osVersion = Environment.OSVersion;
            if (osVersion.Platform != PlatformID.Win32NT || osVersion.Version.Major < 5 || osVersion.Version.Minor < 1)
                return false;
            RegistryKey currentUser = null;
            try
            {
                object obj = currentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced").GetValue("TaskbarGlomming");
                return obj == null || Convert.ToUInt32(obj) != 0U;
            }
            catch
            {
                return false;
            }
        }
    }
}