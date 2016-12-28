// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggingFilterAttribute.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Defines the LoggingFilterAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WebApi.ActionFilters
{
    using System;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Http.Tracing;

    using AtmCashTest.WebApi.Helpers;

    /// <summary>
    /// The logging filter attribute. This filter needed to log <c>api</c> actions in <c>nlog</c>.
    /// </summary>
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            var category = "Controller : "
                           + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                           + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName;
            trace.Info(filterContext.Request, category, "JSON", filterContext.ActionArguments);
        }
    }
}
