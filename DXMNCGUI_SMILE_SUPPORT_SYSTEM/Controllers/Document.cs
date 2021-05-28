using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers
{
    public class Document
    {
        protected SqlDBSetting myDBSetting;
        internal Document()
        {
        }
        public virtual string GetDocNoFormatName(string docType, string accNo)
        {
            return "";
        }
        public static Document CreateDocument(SqlDBSetting dbSetting)
        {

            Document document = (Document)new Document.DocumentSQL();
            document.myDBSetting = dbSetting;
            return document;

        }

        public string GetDocumentNo(string docType, string docNoFormatName, DateTime documentDate)
        {
            if (docNoFormatName.Length > 0)
                return this.GetDocumentNoByName(docNoFormatName, documentDate);
            else
                return this.GetDocumentNo(docType, documentDate);
        }

        public virtual string GetDocumentNo(string docType, DateTime documentDate)
        {
            return "";
        }

        public virtual string GetDocumentNo(string paymentMethod, bool isPayment, DateTime documentDate)
        {
            return "";
        }

        public string IncreaseNextNumber(string docType, string docNoFormatName, DateTime documentDate)
        {
            if (docNoFormatName != null && docNoFormatName.Length > 0)
                return this.IncreaseNextNumberByName(docNoFormatName, documentDate);
            else
                return this.IncreaseNextNumber(docType, documentDate);
        }

        public virtual string IncreaseNextNumber(string docType, DateTime documentDate)
        {
            return "";
        }

        public virtual string IncreaseNextNumber(string paymentMethod, bool isPayment, DateTime documentDate)
        {
            return "";
        }

        protected virtual string GetDocumentNoByName(string docNoFormatName, DateTime documentDate)
        {
            return "";
        }

        protected virtual string IncreaseNextNumberByName(string docNoFormatName, DateTime documentDate)
        {
            return "";
        }

        public virtual bool IncreaseNextNumberByCount(string docNoFormatName, DateTime documentDate, int count, ref string fromDocNo, ref string toDocNo)
        {
            return false;
        }
        public static string FormatDocumentNo(string formatString, int nextNumber, DateTime documentDate)
        {
            int length = formatString.Length;
            string str1 = "";
            string str2 = "";
            string[] strArray1 = new string[5];
            string[] strArray2 = new string[5];
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;
            while (index1 < length)
            {
                if ((int)formatString[index1] != 60 && (int)formatString[index1] != 123)
                    ++index1;
                else if ((int)formatString[index1] == 60)
                {
                    int index4;
                    for (index4 = index1 + 1; index4 < length && (int)formatString[index4] != 62; ++index4)
                        str2 = str2 + System.Convert.ToString((object)formatString[index4]);
                    strArray1[index2] = str2;
                    str2 = "";
                    ++index2;
                    index1 = index4 + 1;
                }
                else
                {
                    int index4;
                    for (index4 = index1 + 1; index4 < length && (int)formatString[index4] != 125; ++index4)
                    {
                        if ((int)formatString[index4] != 64)
                            str2 = str2 + System.Convert.ToString((object)formatString[index4]);
                    }
                    strArray2[index3] = str2;
                    str2 = "";
                    ++index3;
                    index1 = index4 + 1;
                }
            }
            int index5 = 0;
            int index6 = 0;
            int index7 = 0;
            while (index7 < length)
            {
                if ((int)formatString[index7] != 60 && (int)formatString[index7] != 123)
                {
                    str1 = str1 + System.Convert.ToString((object)formatString[index7]);
                    ++index7;
                }
                else if ((int)formatString[index7] == 60)
                {
                    str1 = str1 + nextNumber.ToString(strArray1[index5]);
                    while (index7 < length && (int)formatString[index7] != 62)
                        ++index7;
                    if (index7 < length && (int)formatString[index7] == 62)
                    {
                        ++index7;
                        ++index5;
                    }
                }
                else
                {
                    if (index7 + 1 < length)
                    {
                        DateTime dateTime = (int)formatString[index7 + 1] != 64 ? DateTime.Today.Date : documentDate;
                        if (strArray2[index6].Length > 0)
                        {
                            try
                            {
                                str1 = str1 + dateTime.ToString(strArray2[index6], (IFormatProvider)CultureInfo.InvariantCulture);
                            }
                            catch
                            {
                            }
                        }
                    }
                    while (index7 < length && (int)formatString[index7] != 125)
                        ++index7;
                    if (index7 < length && (int)formatString[index7] == 125)
                    {
                        ++index7;
                        ++index6;
                    }
                }
            }
            return str1;
        }
        public class DocumentSQL : Document
        {

        }
    }
}