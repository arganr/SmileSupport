using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Application
{
    public class OEM
    {
        protected string myCompanyName;
        protected string myProductName;
        protected string myInternalProductName;
        private static OEM myCurrentOEM;

        public static OEM GetCurrentOEM()
        {
            if (OEM.myCurrentOEM == null)
                throw new InvalidOperationException("CurrentOEM not yet set.");
            return OEM.myCurrentOEM;
        }

        public static void SetCurrentOEM(OEM oem)
        {
            if (OEM.myCurrentOEM != null)
                throw new InvalidOperationException("CurrentOEM has been set, it cannot be set for second time.");
            OEM.myCurrentOEM = oem;
        }

        public OEM(string companyName, string productName, string internalProductName)
        {
            this.myCompanyName = companyName;
            this.myProductName = productName;
            this.myInternalProductName = internalProductName;
        }

        public string CompanyName
        {
            get
            {
                return this.myCompanyName;
            }
        }

        public string ProductName
        {
            get
            {
                return this.myProductName;
            }
        }

        public string InternalProductName
        {
            get
            {
                return this.myInternalProductName;
            }
        }

        public string ApplicationDataPath
        {
            get
            {
                return this.CompanyName + (object)Path.DirectorySeparatorChar + this.InternalProductName;
            }
        }
    }
}