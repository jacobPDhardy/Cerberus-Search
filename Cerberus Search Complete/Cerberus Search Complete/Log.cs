﻿namespace Cerberus_Search_Complete
{
    public class Log //DONE - Needs to be made compatible with HXT264Log
    {
        public int Id { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public Levels Level { get; private set; }
        public string Exception { get; private set; }
        public string RenderedMessage { get; private set; }
        public string Properties { get; private set; }

        public Log(string id, string timestamp, string level,string exception,string renderedMessage,string properties)
        {
            Id = Int32.Parse(id);
            TimeStamp = DateTime.Parse(timestamp);
            Level = (Levels)Enum.Parse(typeof(Levels), level);
            Exception = exception;
            RenderedMessage = renderedMessage;  
            Properties = properties;
        }

        public override string ToString()
        {
            return $"Id={Id}\nTimeStamp:{TimeStamp}\nLevel:{Level}\nException:{Exception}\nRenderedMessage:{RenderedMessage}\nProperties:{Properties}\n";
        }
    }
}
