using System;
using System.Runtime.Serialization;


namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Application
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException()
        {
        }

        public AppException(string message)
          : base(message)
        {
        }

        public AppException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        protected AppException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}