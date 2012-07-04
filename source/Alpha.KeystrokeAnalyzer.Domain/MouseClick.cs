using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alpha.KeystrokeAnalyzer.Domain
{
    public class MouseClick: LogItem
    {
        public MouseClick(long timestamp): base(timestamp)
        { }
    }
}
