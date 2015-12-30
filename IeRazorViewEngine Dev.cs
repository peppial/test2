using System.Globalization;
using System.IO;
using System.Web.Mvc;

namespace CashWaveWeb.Infrastructure
{
    public class IeRazorViewEngine 
    {
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            float browserVersion;
            string viewPath = viewName;

            if (controllerContext.RequestContext.HttpContext.Request.Browser.Browser == "IE"
                && float.TryParse(controllerContext.RequestContext.HttpContext.Request.Browser.Version, NumberStyles.Number, CultureInfo.InvariantCulture, out browserVersion)
                && browserVersion <= 8.0F)
            {
                viewPath = "/Views/IE/" + GetControllerName(controllerContext) + "/" + viewName + ".cshtml";
                
                if (this.GetControllerName(controllerContext) != "RefreshAfToken")
                {
                    masterName = "/Views/IE/Shared/_BillaLayout.cshtml";
                }

                if (!File.Exists(controllerContext.RequestContext.HttpContext.Request.MapPath("~" + viewPath)))
                {
                    viewPath = "/Views/IE/Shared/" + viewName + ".cshtml";

                    if (!File.Exists(controllerContext.RequestContext.HttpContext.Request.MapPath("~" + viewPath)))
                    {
                        viewPath = viewName;
                    }
                }
            }

            return base.FindView(controllerContext, viewPath, masterName, useCache);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            float browserVersion;
            string partialViewPath = partialViewName;
            
            if (controllerContext.RequestContext.HttpContext.Request.Browser.Browser == "IE"
                && float.TryParse(controllerContext.RequestContext.HttpContext.Request.Browser.Version, NumberStyles.Number, CultureInfo.InvariantCulture, out browserVersion)
                && browserVersion <= 8.0F)
            {
                partialViewPath = "/Views/IE/" + GetControllerName(controllerContext) + "/" + partialViewName + ".cshtml";

                if (!File.Exists(controllerContext.RequestContext.HttpContext.Request.MapPath("~" + partialViewPath)))
                {
                    partialViewPath = "/Views/IE/Shared/" + partialViewName + ".cshtml";

                    if (!File.Exists(controllerContext.RequestContext.HttpContext.Request.MapPath("~" + partialViewPath)))
                    {
                        partialViewPath = partialViewName;
                    }
                }
            }

            return base.FindPartialView(controllerContext, partialViewPath, useCache);
        }

        private string GetControllerName(ControllerContext controllerContext)
        {
            return controllerContext.RouteData.Values["controller"].ToString();
        }
    }
}
