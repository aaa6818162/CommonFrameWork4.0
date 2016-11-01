﻿using System;
using CommonFrameWork.Logging;
using log4net;


namespace CommonFrameWork.Extensions.Log4Net
{
    /// <summary>Log4Net based logger implementation.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        /// <summary>Parameterized constructor.
        /// </summary>
        /// <param name="log"></param>
        public Log4NetLogger(ILog log)
        {
            _log = log;
        }

        #region ILogger Members

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public void Debug(object message)
        {
            _log.Debug(message);
        }
        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void DebugFormat(string format, params object[] args)
        {
            _log.DebugFormat(format, args);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Debug(object message, System.Exception exception)
        {
            _log.Debug(message, exception);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public void Info(object message)
        {
            _log.Info(message);
        }
        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void InfoFormat(string format, params object[] args)
        {
            _log.InfoFormat(format, args);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(object message, System.Exception exception)
        {
            _log.Info(message, exception);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public void Error(object message)
        {
            _log.Error(message);
        }
        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void ErrorFormat(string format, params object[] args)
        {
            _log.ErrorFormat(format, args);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(object message, System.Exception exception)
        {
            _log.Error(message, exception);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public void Warn(object message)
        {
            _log.Warn(message);
        }
        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void WarnFormat(string format, params object[] args)
        {
            _log.WarnFormat(format, args);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Warn(object message, System.Exception exception)
        {
            _log.Warn(message, exception);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(object message)
        {
            _log.Fatal(message);
        }
        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void FatalFormat(string format, params object[] args)
        {
            _log.FatalFormat(format, args);
        }
        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Fatal(object message, System.Exception exception)
        {
            _log.Fatal(message, exception);
        }

        #endregion
    }
}
