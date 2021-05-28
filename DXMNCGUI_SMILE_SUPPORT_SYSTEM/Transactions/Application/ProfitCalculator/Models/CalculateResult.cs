using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXMNCGUI_SMILE_SUPPORT_SYSTEM.Transactions.Application.ProfitCalculator.Models
{
    public class CalculateResult
    {
        public CalculateParam calculatorParam;
        public double flatRateFullTerm;
        public double flatRateAnnual;
        public double flatRateFullTermBank;
        public double flatRateAnnualBank;
        public double hutangPembiayaan;
        public double interestIncome;
        public double installment;
        public double installmentBank;
        public double provisionIncome;
        public double incomeInsurance;
        public double incomeAdm;
        public double totalIncome;
        public double bebanBungaBank;
        public double bebanProvisiBank;
        public double bebanAdmBank;
        public double bebanGeneralAdmExpenseCabang;
        public double totalBeban;
        public double profitLossBeforeTax;
        public double bebanCadangan;

    }
}