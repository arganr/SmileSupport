using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Registry
{
    public class BaseRegistryID : IRegistryID
    {
        protected object myNewValue;
        protected int myID;
        protected object myDefaultValue;

        public int ID
        {
            get
            {
                return this.myID;
            }
        }

        public object DefaultValue
        {
            get
            {
                return this.myDefaultValue;
            }
        }

        public object NewValue
        {
            get
            {
                return this.myNewValue;
            }
            set
            {
                this.myNewValue = value;
            }
        }

        public BaseRegistryID()
        {
            this.Init();
            this.myNewValue = (object)null;
        }

        public BaseRegistryID(object newValue)
        {
            this.Init();
            this.myNewValue = newValue;
        }

        protected virtual void Init()
        {
        }
    }
}