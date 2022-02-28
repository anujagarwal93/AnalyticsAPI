using AnalyticsAPI.Entities;
using AnalyticsAPI.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
namespace AnalyticsAPI.BusinessLayer
{
    public class AnalyzePlan
    {

        public static ILoggerFactory LoggerFactory1 { get; } = new LoggerFactory();
        public static ILogger analyzeLog = null;

        public static ILogger CreateLogger<AnalyzePlan>()
        {
            var logger = LoggerFactory1.CreateLogger<AnalyzePlan>();
            return logger;
        }

        public AnalyzePlan()
        {
            analyzeLog = CreateLogger<AnalyzePlan>();
        }

        public static int CalculatePos(Plan plan)
        {
            analyzeLog.LogInformation("Calculating POS");
            int retYear = plan.client.goal.startYear, endOfAnalysis = plan.client.goal.endYear;
            double salary = plan.client.salary;
            List<Account> lstAccounts = plan.accounts;
            List<Cashflow> lstCashflows = plan.cashflows;
            
            
            GrowCashflows.GrowExpenses(lstCashflows, plan.client);
            Goal goal = new Goal();
            goal.startYear = plan.client.goal.startYear;
            goal.endYear = plan.client.goal.endYear;
            GrowCashflows.GrowGoalAmount(goal,plan.client);
            List<YearlyAmount> yearlyTotalExpenses =  Helper.CalculateTotalExpenseYearly(lstCashflows.Where(x => x.type.Equals("Expense")).ToList(), endOfAnalysis);
            List<YearlyAmount> yearlyTotalIncomes = Helper.CalculateTotalIncomeYearly(lstCashflows.Where(x => x.type.Equals("Income")).ToList(), endOfAnalysis);
            bool[] posIterationWise = new bool[100];
            double preRetirementValue = Helper.ProcessPreRetirement(yearlyTotalIncomes, yearlyTotalExpenses, salary, retYear);
            for(int iteration = 0; iteration < 99; iteration++)
            {
                analyzeLog.LogInformation("Starting Iteration - {0}", iteration);
                bool[] posYearWise = new bool[endOfAnalysis - retYear + 1];
                List<YearlyAmount> yearlyTotalAccountValue = GrowAccounts.GrowAccount(lstAccounts,iteration, endOfAnalysis,retYear,plan.allocations); //get yearly values for total value of all Accounts
                for(int year = retYear; year <= endOfAnalysis; year++)
                {
                    analyzeLog.LogInformation("Iteration - {0}, Year - {1)", iteration,year);
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
