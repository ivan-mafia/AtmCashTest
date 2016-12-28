namespace AtmCashTest.WebApi.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http.Tracing;

    using Newtonsoft.Json;

    using NLog;

    /// <summary>
    /// Public class to log Error/info messages to the access log file
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class NLogger : ITraceWriter
    {
        #region Private, Protected Fields
        private static readonly Logger ClassLogger = LogManager.GetCurrentClassLogger();

        private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> LoggingMap = new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>> { { TraceLevel.Info, ClassLogger.Info }, { TraceLevel.Debug, ClassLogger.Debug }, { TraceLevel.Error, ClassLogger.Error }, { TraceLevel.Fatal, ClassLogger.Fatal }, { TraceLevel.Warn, ClassLogger.Warn } });
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets property for Logger
        /// </summary>
        private Dictionary<TraceLevel, Action<string>> Logger => LoggingMap.Value;
        #endregion

        #region Public Methods
        /// <summary>
        /// Implementation of TraceWriter to trace the logs.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="category"></param>
        /// <param name="level"></param>
        /// <param name="traceAction"></param>
        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level != TraceLevel.Off)
            {
                if (traceAction?.Target != null)
                {
                    category = category + Environment.NewLine + "Action Parameters : " + JsonConvert.SerializeObject(traceAction.Target);
                }
                var record = new TraceRecord(request, category, level);
                traceAction?.Invoke(record);
                Log(record);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Logs info/Error to Log file
        /// </summary>
        /// <param name="record"></param>
        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(record.Message))
                message.Append("").Append(record.Message + Environment.NewLine);

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append("Method: " + record.Request.Method + Environment.NewLine);

                if (record.Request.RequestUri != null)
                    message.Append("").Append("URL: " + record.Request.RequestUri + Environment.NewLine);

                if (record.Request.Headers != null && record.Request.Headers.Contains("Token") && record.Request.Headers.GetValues("Token") != null && record.Request.Headers.GetValues("Token").FirstOrDefault() != null)
                    message.Append("").Append("Token: " + record.Request.Headers.GetValues("Token").FirstOrDefault() + Environment.NewLine);
            }

            if (!string.IsNullOrWhiteSpace(record.Category))
                message.Append("").Append(record.Category);

            if (!string.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (!string.IsNullOrWhiteSpace(record.Exception?.GetBaseException().Message))
            {
                // ReSharper disable once UnusedVariable
                var exceptionType = record.Exception.GetType();
                message.Append(Environment.NewLine);
                message.Append("").Append("Error: " + record.Exception.GetBaseException().Message + Environment.NewLine);
            }

            Logger[record.Level](Convert.ToString(message) + Environment.NewLine);
        }
        #endregion
    }
}