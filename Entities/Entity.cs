using System;
using System.Collections.Generic;

namespace Entities
{
    public class Expense
    {
        public string ExpenseName { get; set; }
        public string ExpenseId { get; set; }
        public double Amount { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public List<YearlyAmount>? YearlyAmounts { get; set; }
    }
    public class YearlyAmount
    {
        public int Year { get; set; }
        public double Amount { get; set; }
    }
    public class Income
    {
        public string IncomeName { get; set; }
        public string IncomeId { get; set; }
        public double Amount { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public List<YearlyAmount>? YearlyAmounts { get; set; }
    }
    public class Account
    {
        public string? AccountName { get; set; }
        public string? AccountId { get; set; }
        public double Amount { get; set; }
        public List<Allocation>? Allocations { get; set; }
        public List<YearlyAmount>? YearlyAmounts { get; set; }
    }
    public class Allocation
    {
        public string? AssetClassCode { get; set; }
        public int AssetClassPercent { get; set; }
    }
}
