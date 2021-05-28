using System;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Localization
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class DefaultStringAttribute : Attribute
    {
        private string myText;

        public string Text
        {
            get
            {
                return this.myText;
            }
        }

        public DefaultStringAttribute(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            this.myText = text;
        }
    }
}