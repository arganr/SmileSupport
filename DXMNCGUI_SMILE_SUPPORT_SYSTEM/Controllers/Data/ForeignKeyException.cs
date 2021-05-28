using System;
using System.Runtime.Serialization;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data
{
    [Serializable]
    public class ForeignKeyException : DataAccessException
    {
        private string myConstraintName;

        public string ConstraintName
        {
            get
            {
                return this.myConstraintName;
            }
        }

        public ForeignKeyException(string message)
          : base(message)
        {
        }

        public ForeignKeyException(string message, string constraintName)
          : base(message)
        {
            this.myConstraintName = constraintName;
        }

        public ForeignKeyException(string message, string constraintName, Exception innerException)
          : base(message, innerException)
        {
            this.myConstraintName = constraintName;
        }

        protected ForeignKeyException(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
        }
    }
}