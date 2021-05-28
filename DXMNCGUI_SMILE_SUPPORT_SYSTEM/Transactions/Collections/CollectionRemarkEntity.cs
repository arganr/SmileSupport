using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Collections
{
    public class CollectionRemarkEntity
    {
        private CollectionRemarkDB myCollectionRemarkcommand;
        internal DataSet myDataSet;
        private DataRow myRow;
        private DataTable myHeaderTable;
        private DXSSAction myAction;
        private DXSSType myDocType;
        public string strErrorGenTicket;
        private string myAppNote;

        internal DataRow Row
        {
            get { return myRow; }
        }
        public CollectionRemarkDB CollectionRemarkcommand
        {
            get
            {
                return this.myCollectionRemarkcommand;
            }
        }
        public DataTable DataTableHeader
        {
            get
            {
                return this.myHeaderTable;
            }
        }
        public DataSet CollectionRemarkDataSet
        {
            get
            {
                return this.myDataSet;
            }
        }
        public DXSSAction Action
        {
            get
            {
                return this.myAction;
            }
        }
        public DXSSType DocType
        {
            get
            {
                return this.myDocType;
            }
        }
    }
}