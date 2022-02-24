using Entities;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class GrowCashflows
    {
        private static double _inflationRate = 2;
        public static void GrowIncomes(List<Income> incomes)
        {
            int currentYear = DateTime.Now.Year;
            if (incomes != null && incomes.Count > 0)
            {
                foreach (var income in incomes)
                {
                    List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
                    for (int year = income.StartYear; year <= income.EndYear; year++)
                    {
                        YearlyAmount yearlyAmount = new YearlyAmount
                        {
                            Year = year,
                            Amount = income.Amount * Math.Pow(1 + _inflationRate * 0.01, year - currentYear)
                        };
                        yearlyAmounts.Add(yearlyAmount);
                    }
                    income.YearlyAmounts = yearlyAmounts;
                }
            }
        }

        public static void GrowExpenses(List<Expense> expenses)
        {
            int currentYear = DateTime.Now.Year;
            if (expenses != null && expenses.Count > 0)
            {
                foreach (var expense in expenses)
                {
                    List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
                    for (int year = expense.StartYear; year <= expense.EndYear; year++)
                    {
                        YearlyAmount yearlyAmount = new YearlyAmount
                        {
                            Year = year,
                            Amount = expense.Amount * Math.Pow(1 + _inflationRate * 0.01, year - currentYear)
                        };
                        yearlyAmounts.Add(yearlyAmount);
                    }
                    expense.YearlyAmounts = yearlyAmounts;
                }
            }

        }

        public static void GrowGoalAmount(Goal goal)
        {
            int currentYear = DateTime.Now.Year;
            if (goal != null)
            {
                List<YearlyAmount> yearlyAmounts = new List<YearlyAmount>();
                Random random = new Random();
                for (int year = goal.StartYear; year <= goal.EndYear; year++)
                {
                    YearlyAmount yearlyAmount = new YearlyAmount
                    {
                        Year = year,
                        //Amount = goal.Amount * Math.Pow(1 + _inflationRate * 0.01, year - currentYear)
                        Amount = goal.Amount * Math.Pow(1 + random.Next(2,4) * 0.01, year - currentYear)
                    };
                    yearlyAmounts.Add(yearlyAmount);
                }
                goal.YearlyAmounts = yearlyAmounts;
            }
        }
    }

}
