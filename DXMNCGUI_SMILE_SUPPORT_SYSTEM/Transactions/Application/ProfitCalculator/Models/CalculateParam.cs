using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.ProfitCalculator.Models.Constanta;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.ProfitCalculator.Models
{
    public class CalculateParam
    {
        public double pembiayaan;
        public double tenor;
        RATE_TYPE ratetype;
        public FIRST_PAYMENT_TYPE firstPaymentType;
        public double effectiveRate;
        public double provisionIncomeRate;
        public double admIncome;
        public double insRate;
        public double generalAdmFee;
        public double insIncomeRate;


        public double effectiveRateBank;
        public double bankAdmFeeRate;
        public double provisionBankRate;
        public double bebanCadanganRate;
        public string officeCode;


        public CalculateParam()
        {
            this.ratetype = RATE_TYPE.EFFECTIVE;
            this.provisionIncomeRate = 1;
            this.admIncome = 1000000;
            this.insRate = 1.25;
            this.insIncomeRate = 20;
            this.provisionBankRate = 0.75;
            this.bankAdmFeeRate = 0.01;

        }
    }
}