using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyticsAPI.Entities
{
    public class Goal
    {
        public double amout { get; set; }
        public int startYear { get; set; }
        public int endYear { get; set; }

        public List<YearlyAmount> YearlyAmounts { get; set; }
    }
}
