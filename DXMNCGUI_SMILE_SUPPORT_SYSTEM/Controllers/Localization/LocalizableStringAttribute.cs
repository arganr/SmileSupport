using System;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Localization
{
    [AttributeUsage(AttributeTargets.Enum)]
    public sealed class LocalizableStringAttribute : Attribute
    {
        private string myBaseName;

        public string BaseName
        {
            get
            {
                return this.myBaseName;
            }
        }

        public LocalizableStringAttribute(string baseName)
        {
            if (baseName == null)
                throw new ArgumentNullException(nameof(baseName));
            this.myBaseName = baseName;
        }

        public LocalizableStringAttribute()
        {
            this.myBaseName = "";
        }
    }
}