using AnalyticsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyticsAPI.BusinessLayer
{
    class Helper
    {
        public static List<YearlyAmount> CalculateTotalIncomeYearly(List<Cashflow> incomes, int endOfAnalysis)
        {
            List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
            for (int year = DateTime.Now.Year; year <= endOfAnalysis; year++)
            {
                double total = 0;
                foreach(var income in incomes)
                {
                    var amt = income.YearlyAmounts.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                    total += amt;
                }
                yearlyAmounts.Add(new YearlyAmount
                {
                    Amount = total,
                    Year = year
                });
            }
            return yearlyAmounts;
        }
        public static List<YearlyAmount> CalculateTotalExpenseYearly(List<Cashflow> expenses, int endOfAnalysis)
        {
            List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
            for (int year = DateTime.Now.Year; year <= endOfAnalysis; year++)
            {
                double total = 0;
                foreach (var expense in expenses)
                {
                    var amt = expense.YearlyAmounts.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                    total += amt;
                }
                yearlyAmounts.Add(new YearlyAmount
                {
                    Amount = total,
                    Year = year
                });
            }
            return yearlyAmounts;
        }

        public static double ProcessPreRetirement(List<YearlyAmount> incomes, List<YearlyAmount> expenses, double salary,
            int retYear)
        {
            double lumpSum = 0;
            for(int year = DateTime.Now.Year; year <= retYear; year++)
            {
                double inc = incomes.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                double exp = expenses.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                lumpSum += salary + inc - exp;
            }
            return lumpSum;
        }
    }
}
