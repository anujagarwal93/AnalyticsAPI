using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class AnalyzePlan
    {
        public static int CalculatePos()
        {
            int retYear = 2032, endOfAnalysis = 2055;
            int salary = 7500;
            List<Account> lstAccounts = new List<Account>
            {
                new Account
                {
                    AccountId = "1",
                    AccountName = "Acc1",
                    Amount = 8000,
                    Allocations = new List<Allocation>
                    {
                        new Allocation
                        {
                            AssetClassCode = "C",
                            AssetClassPercent = 45
                        },
                        new Allocation
                        {
                            AssetClassCode = "E",
                            AssetClassPercent = 55
                        }
                    }

                },
                new Account
                {
                    AccountId = "2",
                    AccountName = "Acc2",
                    Amount = 12000,
                    Allocations = new List<Allocation>
                    {
                        new Allocation
                        {
                            AssetClassCode = "C",
                            AssetClassPercent = 45
                        },
                        new Allocation
                        {
                            AssetClassCode = "E",
                            AssetClassPercent = 55
                        }
                    }

                }
            };
            List<Income> lstIncomes = new List<Income>
            {
                new Income
                {
                    IncomeId = "21",
                    IncomeName = "Inc1",
                    StartYear = DateTime.Now.Year,
                    EndYear = endOfAnalysis,
                    Amount = 12000
                }
            };
            List<Expense> lstExpense = new List<Expense>
            {
                new Expense
                {
                    Amount = 11500,
                    ExpenseId = "31",
                    ExpenseName = "Exp1",
                    StartYear = DateTime.Now.Year,
                    EndYear = endOfAnalysis
                },
                new Expense
                {
                    Amount = 3500,
                    ExpenseId = "32",
                    ExpenseName = "Exp2",
                    StartYear = retYear,
                    EndYear = endOfAnalysis
                }
            };
            Goal goal = new Goal
            {
                Amount = 41600,
                GoalName = "ret",
                StartYear = retYear,
                EndYear = endOfAnalysis
            };
            GrowCashflows.GrowExpenses(lstExpense);
            GrowCashflows.GrowIncomes(lstIncomes);
            GrowCashflows.GrowGoalAmount(goal);
            List<YearlyAmount> yearlyTotalExpenses =  Helper.CalculateTotalExpenseYearly(lstExpense, endOfAnalysis);
            List<YearlyAmount> yearlyTotalIncomes = Helper.CalculateTotalIncomeYearly(lstIncomes, endOfAnalysis);
            bool[] posIterationWise = new bool[100];
            double preRetirementValue = Helper.ProcessPreRetirement(yearlyTotalIncomes, yearlyTotalExpenses, salary, retYear);
            for(int iteration = 0; iteration < 99; iteration++)
            {
                bool[] posYearWise = new bool[endOfAnalysis - retYear + 1];
                List<YearlyAmount> yearlyTotalAccountValue = GrowAccounts.GrowAccount(lstAccounts,iteration, endOfAnalysis,retYear); //get yearly values for total value of all Accounts
                for(int year = retYear; year <= endOfAnalysis; year++)
                {
                    var inc = yearlyTotalIncomes.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                    var exp = yearlyTotalExpenses.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                    var acctVal = yearlyTotalAccountValue.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                    var goalAmt = goal.YearlyAmounts.FirstOrDefault(x => x.Year == year)?.Amount ?? 0;
                    var random = new Random();
                    var preRetAdj = preRetirementValue + preRetirementValue * 0.01 * random.Next(0,4);
                    posYearWise[year - retYear] = preRetirementValue + acctVal + inc - exp - goalAmt > 0 ? true : false;
                }
                int falseCount = posYearWise.Where(x => !x).ToList().Count;
                posIterationWise[iteration] = falseCount > 0 ? false: true;
            }
            return posIterationWise.Where(x => x).ToList().Count;
        }
    }
}
