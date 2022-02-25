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
        public static List<YearlyAmount> GrowAccount(List<Account> accounts, int iteration, int endOfLife, int retYear, Allocations allocations)
        {
            GetRefData();
            double totalAccountValue = 0;
            foreach (var account in accounts)
            {
                totalAccountValue += account.marketValue;
            }

            double cashAmt = 0;
            double equityAmt = 0;
            List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
            for (int index = retYear - DateTime.Now.Year; index <= endOfLife-DateTime.Now.Year; index++)//from current year till EOL year
            {
                cashAmt = allocations.cash * 0.01 * totalAccountValue;//always populate allocations for cash and equity
                equityAmt = allocations.equities * 0.01 * totalAccountValue;//always populate allocations for cash and equity
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
