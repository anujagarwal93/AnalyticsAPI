using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyticsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalyzeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
        [Route("analyzePlan")]
        
        public int AnalyzePlan()
        {
            int result = BusinessLayer.AnalyzePlan.CalculatePos();
            return result;
        }
    }
}
