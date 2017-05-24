// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalExceptionAttribute.cs" company="Ivan">
//   Starikov Ivan, 2016
// </copyright>
// <summary>
//   Action filter to handle for Global application errors.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AtmCashTest.WebApi.ActionFilters
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using System.Web.Http.Tracing;

    using AtmCashTest.WebApi.Helpers;

    /// <summary>
    /// Action filter to handle for Global application errors.
    /// </summary>
    public class GlobalExceptionAttribute : ExceptionFilterAttribute
    {
        #region Public Methods
        /// <summary>
        /// The on exception. This filter needed to log exception in <c>nlog</c>.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <exception cref="HttpResponseException">
        /// Http Response Exception.
        /// </exception>
        public override void OnException(HttpActionExecutedContext context)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            var category = "Controller : "
                           + context.ActionContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                           + Environment.NewLine + "Action : " + context.ActionContext.ActionDescriptor.ActionName;
            trace.Error(
                context.Request,
                category,
                context.Exception);

            var exceptionType = context.Exception.GetType();

            if (exceptionType == typeof(ValidationException))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(context.Exception.Message), ReasonPhrase = "ValidationException", };
                throw new HttpResponseException(resp);
            }
            else if (exceptionType == typeof(UnauthorizedAccessException))
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                throw new HttpResponseException(context.Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }
        #endregion
    }
}
