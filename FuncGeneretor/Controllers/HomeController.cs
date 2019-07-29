using CSASPNETHighlightCodeInPage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            // Display the highlighted code in a label control.
            //Request.Form["GeneratedCode"] = CodeManager.Encode(
            //CodeManager.HighlightHTMLCode(InputText, _htb)
            //);
            //ViewBag.GeneratedCode = CodeManager.Encode(
            //CodeManager.HighlightHTMLCode(InputText, _htb)
            //);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult UpdateText(string text)
        {
            text += "\n";
            string InputText = text + "var x = 10;\nx *= 5;\ndocument.getElementById(\"demo\").innerHTML = x;\nif( x == 8 ){\n}\nelse{\nwhile(true)//111test comment\n}\n//with new comment";
            Hashtable _htb = CSASPNETHighlightCodeInPage.CodeManager.Init();

            // Initialize the suitable collection object.
            RegExp _rg = new RegExp();
            _rg = (RegExp)_htb["js"];

            // Display the highlighted code in a label control.
            var result = CodeManager.Encode(
                CodeManager.HighlightCode(
                InputText.Replace("&quot;", "\""),
                "js", _rg)
                );

            // Display the highlighted code in a label control.
            //Request.Form["GeneratedCode"] = CodeManager.Encode(
            //CodeManager.HighlightHTMLCode(InputText, _htb)
            //);
            //ViewBag.GeneratedCode = CodeManager.Encode(
            //CodeManager.HighlightHTMLCode(InputText, _htb)
            //);

            return new JsonResult() { Data = result };
        }

        protected void btnHighLight_Click(object sender, EventArgs e)
        {
            string InputText = String.Format("{0}", Request.Form["GeneratedCode"]);
            Hashtable _htb = CSASPNETHighlightCodeInPage.CodeManager.Init();

            // Initialize the suitable collection object.
            RegExp _rg = new RegExp();
            _rg = (RegExp)_htb["js"];

            // Display the highlighted code in a label control.
            Request.Form["GeneratedCode"] = CodeManager.Encode(
                CodeManager.HighlightHTMLCode(InputText, _htb)
                );

        }
    }
}