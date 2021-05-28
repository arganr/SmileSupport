using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Localization
{
    public sealed class Localizer
    {
        private static Hashtable myTable = new Hashtable();

        private Localizer()
        {
        }


        public static string GetString(Enum id, params object[] args)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            Type type = id.GetType();
            LocalizableStringAttribute customAttribute1 = Attribute.GetCustomAttribute((MemberInfo)type, typeof(LocalizableStringAttribute)) as LocalizableStringAttribute;
            StringIdAttribute customAttribute2 = Attribute.GetCustomAttribute((MemberInfo)type, typeof(StringIdAttribute)) as StringIdAttribute;
            if (customAttribute1 == null && customAttribute2 == null)
                throw new ArgumentException("Neither LocalizableStringAttribute nor StringIdAttribute is defined.", nameof(id));
            string str = (string)null;
            if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName != "en")
                str = Localizer.GetStringFromResource(type, customAttribute1 != null ? customAttribute1.BaseName : customAttribute2.BaseName, id.ToString());
            if (str == null)
            {
                //str = !(Attribute.GetCustomAttribute(type.GetMember(id.ToString())[0], typeof(DefaultStringAttribute )) is DefaultStringAttribute customAttribute3) ? string.Empty : customAttribute3.Text;
            }
            StringBuilder stringBuilder = new StringBuilder();
            int index = 0;
            while (index < str.Length)
            {
                if (str[index] == '\\' && index < str.Length - 1 && str[index + 1] == '\\')
                {
                    stringBuilder.Append('\\');
                    index += 2;
                }
                else if (str[index] == '\\' && index < str.Length - 1 && str[index + 1] == 'n')
                {
                    stringBuilder.AppendLine();
                    index += 2;
                }
                else if (str[index] == '\\' && index < str.Length - 1 && str[index + 1] == 't')
                {
                    stringBuilder.Append('\t');
                    index += 2;
                }
                else
                    stringBuilder.Append(str[index++]);
            }
            string format = stringBuilder.ToString();
            return args != null ? string.Format(format, args) : format;
        }

        private static string GetStringFromResource(Type type, string baseName, string id)
        {
            lock (Localizer.myTable.SyncRoot)
            {
                if (!Localizer.myTable.ContainsKey((object)type))
                {
                    if (baseName.Length == 0)
                        baseName = type.Assembly.GetName().Name + ".LocalizationRes";
                    ResourceManager resourceManager = new ResourceManager(baseName, type.Assembly);
                    Localizer.myTable.Add((object)type, (object)resourceManager);
                }
            }
            ResourceManager resourceManager1 = (ResourceManager)Localizer.myTable[(object)type];
            if (resourceManager1 == null)
                return (string)null;
            try
            {
                return resourceManager1.GetString(type.FullName + "." + id);
            }
            catch (MissingManifestResourceException ex)
            {
                return (string)null;
            }
        }

        public static string[] GetEnumStringsWithExclude(Type type, params Enum[] idToExclude)
        {
            if (!type.IsEnum)
                throw new ArgumentException("type is not enum.", nameof(type));
            List<string> stringList = new List<string>();
            LocalizableStringAttribute customAttribute1 = Attribute.GetCustomAttribute((MemberInfo)type, typeof(LocalizableStringAttribute)) as LocalizableStringAttribute;
            StringIdAttribute customAttribute2 = Attribute.GetCustomAttribute((MemberInfo)type, typeof(StringIdAttribute)) as StringIdAttribute;
            if (customAttribute1 == null && customAttribute2 == null)
                throw new ArgumentException("Neither LocalizableStringAttribute nor StringIdAttribute is defined.", nameof(type));
            foreach (FieldInfo field in type.GetFields())
            {
                if (idToExclude != null && idToExclude.Length > 0)
                {
                    bool flag = false;
                    for (int index = 0; index < idToExclude.Length; ++index)
                    {
                        if (field.Name.Equals(idToExclude[index].ToString()))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        continue;
                }
                DefaultStringAttribute[] customAttributes = field.GetCustomAttributes(typeof(DefaultStringAttribute), false) as DefaultStringAttribute[];
                if (customAttributes.Length > 0)
                {
                    string str = (string)null;
                    if (Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName != "en")
                        str = Localizer.GetStringFromResource(type, customAttribute1 != null ? customAttribute1.BaseName : customAttribute2.BaseName, field.Name);
                    if (str != null)
                        stringList.Add(str);
                    else
                        stringList.Add(customAttributes[0].Text);
                }
            }
            return stringList.ToArray();
        }

        public static string[] GetEnumStrings(Type type)
        {
            return Localizer.GetEnumStringsWithExclude(type);
        }
    }
}