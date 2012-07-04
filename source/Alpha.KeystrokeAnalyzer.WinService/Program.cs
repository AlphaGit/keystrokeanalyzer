using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.KeystrokeAnalyzer.WinService
{
    static class Program
    {
        static void Main()
        {
#if DEBUG
            var ksService = new KeystrokeAnalyzerService();
            ksService.Start();
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new KeystrokeAnalyzerService() 
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
