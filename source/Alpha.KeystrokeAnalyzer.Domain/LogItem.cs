using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alpha.KeystrokeAnalyzer.Domain
{
    public abstract class LogItem
    {
        public LogItem(long timestamp)
        {
            this.Timestamp = timestamp;
        }

        public long Timestamp { get; set; }
    }
}
