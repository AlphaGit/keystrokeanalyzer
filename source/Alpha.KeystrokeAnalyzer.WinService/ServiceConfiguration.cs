using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Alpha.KeystrokeAnalyzer.WinService
{
    public static class ServiceConfiguration
    {
        public static int FlushIntervalSeconds
        {
            get
            {
                var configuration = ConfigurationManager.AppSettings["FlushIntervalInSeconds"];
                var flushInterval = 0;

                return int.TryParse(configuration, out flushInterval)
                    ? flushInterval
                    : 60;
            }
        }
    }
}
