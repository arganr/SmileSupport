using System;
using System.Data;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Controllers.Data
{
    public class SeqUtils
    {
        public static int GetLastSeq(DataRow[] rowList)
        {
            return rowList.Length == 0 ? 16 : Convert.ToInt32(rowList[rowList.Length - 1]["Seq"]) + 16;
        }

        public static int GetNewSeqAtThisIndex(int index, DataRow[] rowList)
        {
            if (index < 0 || index >= rowList.Length)
                return (rowList.Length + 1) * 16;
            int int32 = Convert.ToInt32(rowList[index]["Seq"]);
            int num1 = index != 0 ? Convert.ToInt32(rowList[index - 1]["Seq"]) : 0;
            int num2 = (int32 + num1) / 2;
            if (num2 != int32 && num2 != num1)
                return num2;
            rowList[0].Table.BeginLoadData();
            try
            {
                for (int index1 = 0; index1 <= rowList.Length - 1; ++index1)
                    rowList[index1]["Seq"] = index1 >= index ? (object)((index1 + 2) * 16) : (object)((index1 + 1) * 16);
            }
            finally
            {
                rowList[0].Table.EndLoadData();
            }
            return (index + 1) * 16;
        }
    }
}