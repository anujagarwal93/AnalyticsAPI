using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyticsAPI.Entities
{
    
    public class Cashflow
    {
        public string id { get; set; }
        public string ClientId { get; set; }
        public string type { get; set; }
        public double amount { get; set; }

        public List<YearlyAmount> YearlyAmounts { get; set; }
    }
}
