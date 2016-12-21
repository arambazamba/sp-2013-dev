using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MileageRecorderRemoteWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //Keep the app web url and host web url for later
            if (Request.QueryString["SPAppWebUrl"] != null)
            {
                Session["SPAppWebUrl"] = Request.QueryString["SPAppWebUrl"];
            }
            if (Request.QueryString["SPHostUrl"] != null)
            {
                Session["SPHostUrl"] = Request.QueryString["SPHostUrl"];
            }

            return View();
        }

    }
}
