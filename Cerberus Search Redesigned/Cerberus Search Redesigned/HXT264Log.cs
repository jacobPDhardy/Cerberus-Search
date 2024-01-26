using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public class HXT264Log
    {
        public HXT264Log(long id, DateTime timestamp, string level, string exception, string properties, string renderedMessage)
        {
            Id = id;
            Timestamp = timestamp;
            Level = level;
            Exception = exception;
            Properties = properties;
            RenderedMessage = renderedMessage;
        }

        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public string RenderedMessage { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nTimestamp: {Timestamp}\nLevel: {Level}\nException: {Exception}\nProperties: {Properties}\nRenderedMessage: {RenderedMessage}\nTimestamp: {Timestamp}\n";
        }
    }
}
