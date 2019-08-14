using CSASPNETHighlightCodeInPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FuncGeneretor.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string InputText = "var x = 10;\nx *= 5;\ndocument.getElementById(\"demo\").innerHTML = x;\nif( x == 8 ){\n}\nelse{\nwhile(true)//111test comment\n}";
            Hashtable _htb = CSASPNETHighlightCodeInPage.CodeManager.Init();

            // Initialize the suitable collection object.
            RegExp _rg = new RegExp();
            _rg = (RegExp)_htb["js"];

            // Display the highlighted code in a label control.
            ViewBag.GeneratedCode = CodeManager.Encode(
                CodeManager.HighlightCode(
                InputText.Replace("&quot;", "\""),
                "js", _rg)
                );
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.GeneratedDesc = "/*...  ...*/";
            ViewBag.GeneratedCode = "{...  ...}";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult UpdateText(int commandNum, int conditionNum)
        {
            CodeBuilder codebuilder = new CodeBuilder();
            FuncCodeAndDesc InputText = codebuilder.PrintRandomFunction(commandNum - 1, conditionNum);
            InputText.FuncCode = CodeManager.Beautiffy(InputText.FuncCode);
            Hashtable _htb = CSASPNETHighlightCodeInPage.CodeManager.Init(); 

            // Initialize the suitable collection object.
            RegExp _rg = new RegExp();
            _rg = (RegExp)_htb["js"];

            // Display the highlighted code in a label control.
            var result = CodeManager.Encode(
                CodeManager.HighlightCode(
                InputText.FuncCode.Replace("&quot;", "\""),
                "js", _rg)
                );

            InputText.FuncCode = result;

            return new JsonResult() { Data = InputText };
        }

        [HttpPost]
        public ActionResult DownloadFile(int commandNum, int conditionNum, int dataCount)
        {  
            string[] lines;
            List<string> linesList = new List<string>();
            CodeBuilder codebuilder = new CodeBuilder();
            for (int i = 0; i < dataCount; i++)
            {
                FuncCodeAndDesc InputText = codebuilder.PrintRandomFunction(commandNum - 1, conditionNum);
                linesList.Add(InputText.FuncDesc + "\t" + InputText.FuncCode);
                Thread.Sleep(5);
            }

            lines = linesList.ToArray();

            System.IO.File.WriteAllLines(@"C:\Users\yehuda_da\Desktop\פרוייקט גמר\eng-js.txt", lines);
     
            return new JsonResult();
        }
    }
}