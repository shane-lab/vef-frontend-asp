using MvcWeb.App_Start;
using MvcWeb.Models.Promises;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Collections;
using System.Text.RegularExpressions;

namespace MvcWeb.Controllers
{
    public abstract class BaseController : Controller
    {
        private static readonly String _SPF_HEADER = "X-SPF-Referer";

        private static readonly String[] _SPF_TYPES = { "navigate", "prefetch", "navigate-back" };

        private static readonly String[] _RENDER_SECTIONS = { "imports", "scripts" };

        protected IAuthenticated Authenticated { private set; get; }

        protected virtual bool IsSPFRequest
        {
            get
            {
                var query = Request.QueryString;
                var headers = Request.Headers;

                return query.AllKeys.Contains("spf") &&
                        _SPF_TYPES.Contains(query.GetValues("spf").FirstOrDefault()) &&
                        headers.AllKeys.Contains(_SPF_HEADER);
            }
        }

        protected String ControllerName
        {
            get
            {
                return ControllerContext?.RouteData?.Values["controller"]?.ToString();
            }
        }

        protected String ActionName
        {
            get
            {
                return ControllerContext?.RouteData?.Values["action"]?.ToString();
            }
        }

        public BaseController(IAuthenticated authenticated)
        {
            Authenticated = authenticated;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            if (filterContext.Controller is AccountController)
            {
                return;
            }

            RedirectToRouteResult result = null;

            if (Authenticated == null)
            {
                result = RedirectToAction("Login", "Account");
            }
            else
            {
                // TODO check access token, create verification route (e.g. /api/token/:token) or use the existing route /api/users/me/ + jwt header ?
            }

            if (result != null)
            {
                filterContext.Result = result;
            }
        }

        protected virtual ActionResult SPFView(object model = null)
        {
            if (IsSPFRequest)
            {
                var spf = renderPartial(model);

                var result = new
                {
                    head = spf.Head,
                    body = new
                    {
                        target = spf.Content ?? "404"
                    },
                    foot = spf.Foot
                };

                return Content(JsonConvert.SerializeObject(result));
            }

            return View(model);
        }

        private SPFPartial renderPartial(object model = null)
        {
            ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var result = ViewEngines.Engines.FindPartialView(ControllerContext, ActionName);

                String virtualPath = (result.View as RazorView)?.ViewPath;
                using (var output = new HtmlTextWriter(writer))
                {
                    var ctx = new ViewContext(ControllerContext, result.View, ViewData, TempData, writer);

                    result.View.Render(ctx, writer);
                    result.ViewEngine.ReleaseView(ControllerContext, result.View);

                    var spf = new SPFPartial
                    {
                        Content = writer.GetStringBuilder().ToString()
                    };

                    if (virtualPath != null)
                    {
                        var fileContents = System.IO.File.ReadAllText(Server.MapPath($@"{virtualPath}"))?.Replace(Environment.NewLine, "");

                        Regex importsExpr = sectionExpression("imports");

                        var match = importsExpr.Match(fileContents);
                        if (match?.Success == true && match.Groups.Count == 2) // inner group are html imports / css styles
                        {
                            var capture = match.Groups[1];

                            // catching custom-style styletags attributes to resolve dynamic Polymer styles loading
                            Regex polymerExpr = new Regex(@"<style\s+is\s*=""custom-style""\s*>(.*)<\/\s*style\s*>");

                            var parentImport = capture.Value;
                            match = polymerExpr.Match(parentImport);

                            var groups = match.Groups;
                            if (match?.Success == true && groups.Count >= 2)
                            {
                                parentImport = polymerExpr.Replace(parentImport, "");

                                for (var i = 1; i < groups.Count; i++)
                                {
                                    capture = match.Groups[1];

                                    // script to dynamically load Polymer style tags to the dom document
                                    spf.AddScript(polymerPatternFix(capture.Value));
                                }
                            }

                            // prepend styles and imports
                            spf.AddImport(parentImport.Replace("href=\"~/", "href=\"/")); // resolve virtual path
                        }

                        Regex scriptsExpr = sectionExpression("scripts");

                        match = scriptsExpr.Match(fileContents);
                        if (match?.Success == true && match.Groups.Count == 2)
                        {
                            var capture = match.Groups[1];

                            spf.AddScript(capture.Value);
                        }
                    }

                    return spf;
                }
            }
        }

        private Regex sectionExpression(String section)
        {
            // temporary requires the postfix @end@ to indicate the ending of a section
            return new Regex($@"@section\s+{section}\s*{{(.*)}}@\*end\*@", RegexOptions.IgnoreCase);
        }

        private String polymerPatternFix(String content)
        {
            return $"<script> var s = document.createElement('style', 'custom-style'); s.textContent = '{content}'; document.body.appendChild(s); </script>";
        }

        private struct SPFPartial
        {
            private StringList _imports, _scripts;

            public String Head
            {
                get
                {
                    return _imports?.ToString() ?? "";
                }
            }

            public String Foot
            {
                get
                {
                    return _scripts?.ToString() ?? "";
                }
            }

            public string Content { get; set; }

            public void AddImport(String import)
            {
                if (_imports == null)
                {
                    _imports = new StringList();
                }

                _imports.Add(import);
            }

            public void AddScript(String script)
            {
                if (_scripts == null)
                {
                    _scripts = new StringList();
                }

                _scripts.Add(script);
            }
        }

        private class StringList : List<String>
        {
            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();

                this.ForEach(s => builder.Append(s));

                return builder.ToString();
            }
        }
    }
}