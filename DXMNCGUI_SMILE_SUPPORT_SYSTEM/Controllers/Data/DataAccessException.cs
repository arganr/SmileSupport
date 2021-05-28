using DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data
{
    [Serializable]
    public class DataAccessException : AppException
    {
        public DataAccessException()
        {
        }

        public DataAccessException(string message)
          : base(message)
        {
        }

        public DataAccessException(string message, Exception innerException)
          : base(message, innerException)
        {
        }

        protected DataAccessException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}