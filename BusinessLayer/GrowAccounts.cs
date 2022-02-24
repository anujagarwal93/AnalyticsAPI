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
        public static void GrowAccount(List<Account> accounts, int iteration)
        {
            GetRefData();
            foreach (var account in accounts)
            {
                for (int year = 1; year < 50; year++)//from current year till EOL year
                {
                    //int cashAmt = account.Allocations[0].AssetClassPercent * account.y
                    YearlyAmount yearlyAmount = new YearlyAmount
                    {
                        Amount = 0,
                        Year = year

                    };
                }
            }
        }
        public static void GetRefData()
        {
            cashGrowthData = ReadFileRefData.ReadCashRefData();
            equityGrowthData = ReadFileRefData.ReadEquityRefData();
        }
    }
}
