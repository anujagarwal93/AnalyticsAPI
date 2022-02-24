using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    class GrowAccounts
    {
        private static int[][] cashGrowthData;
        private static int[][] equityGrowthData;
        public static List<YearlyAmount> GrowAccount(List<Account> accounts, int iteration, int endOfLife, int retYear)
        {
            GetRefData();
            double totalAccountValue = 0;
            foreach (var account in accounts)
            {
                totalAccountValue += account.Amount;
            }

            double cashAmt = 0;
            double equityAmt = 0;
            List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
            for (int index = retYear - DateTime.Now.Year; index <= endOfLife-DateTime.Now.Year; index++)//from current year till EOL year
            {
                cashAmt = accounts[0].Allocations[0].AssetClassPercent * 0.01 * totalAccountValue;//always populate allocations for cash and equity
                equityAmt = accounts[0].Allocations[1].AssetClassPercent * 0.01 * totalAccountValue;//always populate allocations for cash and equity
                cashAmt += cashAmt * cashGrowthData[index][iteration] * 0.01;
                equityAmt += equityAmt * equityGrowthData[index][iteration] * 0.01;
                YearlyAmount yearlyAmount = new YearlyAmount
                {
                    Amount = cashAmt + equityAmt,
                    Year = index + DateTime.Now.Year
                };
                yearlyAmounts.Add(yearlyAmount);
            }
            return yearlyAmounts;
            
        }
        public static void GetRefData()
        {
            cashGrowthData = ReadFileRefData.ReadCashRefData();
            equityGrowthData = ReadFileRefData.ReadEquityRefData();
        }
    }
}
