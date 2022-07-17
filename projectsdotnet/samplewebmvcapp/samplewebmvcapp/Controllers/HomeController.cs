using Microsoft.AspNetCore.Mvc;
using samplewebmvcapp.Models;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace samplewebmvcapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<studypahse> lststudyhase = new List<studypahse>();
            studypahse varstudypahse = new studypahse();
            varstudypahse.studyid = "700-001";
            varstudypahse.studyperiod = "Lead-In";
            varstudypahse.dosageStratdate = DateTime.ParseExact("02/05/2015".ToString(), "MM/dd/yyyy",null);
            varstudypahse.dosageEnddate = DateTime.ParseExact("02/05/2015", "MM/dd/yyyy", null);
            lststudyhase.Add(varstudypahse);

            studypahse varstudypahse1 = new studypahse();
            varstudypahse1.studyid = "700-001";
            varstudypahse1.studyperiod = "Cycle 1";
            varstudypahse1.dosageStratdate = DateTime.ParseExact("02/06/2015", "MM/dd/yyyy", null);
            varstudypahse1.dosageEnddate = DateTime.ParseExact("02/06/2015", "MM/dd/yyyy", null);
            lststudyhase.Add(varstudypahse1);

            studypahse varstudypahse2 = new studypahse();
            varstudypahse2.studyid = "700-001";
            varstudypahse2.studyperiod = "Cycle 2";
            varstudypahse2.dosageStratdate = DateTime.ParseExact("02/10/2015", "MM/dd/yyyy", null);
            varstudypahse2.dosageEnddate = DateTime.ParseExact("02/10/2015", "MM/dd/yyyy", null);
            lststudyhase.Add(varstudypahse2);

            studypahse varstudypahse3 = new studypahse();
            varstudypahse3.studyid = "700-001";
            varstudypahse3.studyperiod = "Cycle 3";
            varstudypahse3.dosageStratdate = DateTime.ParseExact("02/15/2015", "MM/dd/yyyy", null);
            varstudypahse3.dosageEnddate = DateTime.ParseExact("02/15/2015", "MM/dd/yyyy", null);
            lststudyhase.Add(varstudypahse3);

            studypahse varstudypahse4 = new studypahse();
            varstudypahse4.studyid = "700-001";
            varstudypahse4.studyperiod = "Cycle 4";
            varstudypahse4.dosageStratdate = DateTime.ParseExact("02/25/2015", "MM/dd/yyyy", null);
            varstudypahse4.dosageEnddate = DateTime.ParseExact("02/25/2015", "MM/dd/yyyy", null);
            lststudyhase.Add(varstudypahse4);

            studypahse varstudypahse5 = new studypahse();
            varstudypahse5.studyid = "700-001";
            varstudypahse5.studyperiod = "After Cycle 5";
            varstudypahse5.dosageStratdate = DateTime.ParseExact("02/28/2015", "MM/dd/yyyy", null);
            varstudypahse5.dosageEnddate = DateTime.ParseExact("02/28/2015", "MM/dd/yyyy", null);
            lststudyhase.Add(varstudypahse5);

            List<string?> lststudyperiod = lststudyhase.Select(o => o.studyperiod).ToList();

            List<studyRegEx> lststudyregex = new List<studyRegEx>();

            studyRegEx studyregex1 = new studyRegEx();
            studyregex1.studyphase = "Pre Randomization";
            studyregex1.studyregex = "(^LEAD-IN)";
            lststudyregex.Add(studyregex1);

            studyRegEx studyregex2 = new studyRegEx();
            studyregex2.studyphase = "Randomization Phase";
            studyregex2.studyregex = "(^CYCLE\\s+[1-4]$)";
            lststudyregex.Add(studyregex2);

            studyRegEx studyregex3 = new studyRegEx();
            studyregex3.studyphase = "Maitenance Phase";
            studyregex3.studyregex = "(^CYCLE\\s+[5-9]|[1-9][0-9]{1,2}|1000$)";
            lststudyregex.Add(studyregex3);

            List<string?> lsteliminate = new List<string?>();
            foreach(string? str in lststudyperiod)
            {
                if (str != "")
                {
                    foreach (studyRegEx strreg in lststudyregex)
                    {
                        
                        bool regpass = Regex.IsMatch(str.ToUpper(), strreg.studyregex);
                        if (regpass)
                        {
                            if (strreg.studyphase == "Pre Randomization")
                            {
                                lsteliminate.Add(str);
                                break;
                            }

                        }
                        
                    }
                }
             }

            foreach (string str in lsteliminate)
            {
                studypahse? result = lststudyhase.Find(x => x.studyperiod == str);
                if (result != null)
                    lststudyhase.Remove(result);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}