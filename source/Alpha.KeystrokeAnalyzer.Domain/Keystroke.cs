using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alpha.KeystrokeAnalyzer.Domain
{
    public class Keystroke: LogItem
    {
        public Keystroke(int scanCode, long timestamp): base(timestamp)
        {
            this.ScanCode = scanCode;
        }

        public int ScanCode { get; set; }
    }
}
