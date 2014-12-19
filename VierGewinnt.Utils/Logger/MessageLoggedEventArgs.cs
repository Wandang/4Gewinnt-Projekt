using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VierGewinnt.Utils.Logger
{
    public class MessageLoggedEventArgs
    {
        /// <summary>
        /// Type of the Logged Message
        /// </summary>
        public LogLevel Type { get; set; }

        /// <summary>
        /// Namespace of the Logged Message
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Message of the Logged Text
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Event Args of Logger Events
        /// </summary>
        /// <param name="Type">Type of the Logged Message</param>
        /// <param name="Namespace">Namespace of the Logged Message</param>
        /// <param name="Message">Message of the Logged Text</param>
        public MessageLoggedEventArgs(LogLevel Type, string Namespace, string Message)
        {
            this.Type = Type;
            this.Namespace = Namespace;
            this.Message = Message;
        }
    }
}
