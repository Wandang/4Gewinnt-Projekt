using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Utils.Logger
{
    /// <summary>
    /// Logger Util
    /// </summary>
    public static class Logger
    {
        private static Logger _l;

        /// <summary>
        /// Delegate to Handle Message Logged Events
        /// </summary>
        /// <param name="sender">Sender of this Event</param>
        /// <param name="args">Event Arguments</param>
        public static delegate void MessageLoggedEventHandler(object sender, MessageLoggedEventArgs args);

        /// <summary>
        /// Event that is triggered when a Message is Logged
        /// </summary>
        public static event MessageLoggedEventHandler OnMessageLogged;

        /// <summary>
        /// Log Level of the Logger
        /// </summary>
        public static LogLevel LogLevel = LogLevel.NORMAL;

        private static void Log(LogLevel Level, string Namespace, string Message)
        {


            var handler = OnMessageLogged;
            if (handler != null)
            {
                handler(typeof(Logger), new MessageLoggedEventArgs(Level, Namespace, Message));
            }
        }

        #region Error
        /// <summary>
        /// Log an Error for a Namespace
        /// </summary>
        /// <param name="Namespace">Namespace of the Error</param>
        /// <param name="Message">Error Message</param>
        public static void Error(string Namespace, string Message)
        {
            Log(LogLevel.ERROR, Namespace, Message);
        }

        /// <summary>
        /// Log an Error
        /// </summary>
        /// <param name="Message">Error Message</param>
        public static void Error(string Message)
        {
            Error("DEFAULT", Message);
        }
        #endregion

        #region Chat
        public static void Chat(string Message)
        {
            if ((int)Logger.LogLevel > 1)
            {
                Log(LogLevel.CHAT, "", Message);
            }
        }
        #endregion

        #region Log
        /// <summary>
        /// Log a Message
        /// </summary>
        /// <param name="Namespace">Namespace for this Log Message</param>
        /// <param name="Message">Message to Log</param>
        public static void Log(string Namespace, string Message)
        {
            if ((int)Logger.LogLevel >= 2)
            {
                Log(LogLevel.NORMAL, Namespace, Message);
            }
        }

        /// <summary>
        /// Log a Message
        /// </summary>
        /// <param name="Message">Message to Log</param>
        public static void Log(string Message)
        {
            Log("DEFAULT", Message);
        }
        #endregion

        #region Info
        public static void Info(string Namespace, string Message)
        {
            if ((int)Logger.LogLevel >= 3)
            {

            }
        }
        #endregion

        #region Debug
        public static void Debug(string Namespace, string Message)
        {
            if ((int)Logger.LogLevel >= 4)
            {
                Log(LogLevel.DEBUG, Namespace, Message);
            }
        }

        public static void Debug(string Namespace)
        {
            Debug("DEFAULT", Namespace);
        }
        #endregion
    }
}
