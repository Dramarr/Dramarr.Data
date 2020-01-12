using System;
using System.Collections.Generic;
using System.Text;
using static Dramarr.Core.Enums.LogHelpers;

namespace Dramarr.Data.Model
{
    /// <summary>
    /// Log
    /// </summary>
    public class Log
    {
        public Guid Id { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
        public string Properties { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        public Log(LogType type, string message, string properties)
        {
            Id = Guid.NewGuid();
            Type = type;
            Message = message;
            Properties = properties;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
