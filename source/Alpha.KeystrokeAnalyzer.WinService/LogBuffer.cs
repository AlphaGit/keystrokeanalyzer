using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alpha.KeystrokeAnalyzer.Domain;

namespace Alpha.KeystrokeAnalyzer.WinService
{
    public class LogBuffer
    {
        #region Singleton
		private LogBuffer() 
        {
            this._logItems = new List<LogItem>();
        }

        
        private static LogBuffer _instance;
        public static LogBuffer Instance
        {
            get
            {
                return _instance ?? (_instance = new LogBuffer());
            }
        } 
	    #endregion

        private List<LogItem> _logItems;

        public void AddLogItem(LogItem logItem) 
        {
            _logItems.Add(logItem);
        }

        public void FlushToFile(string path)
        {
            var summary = 
                "{timestamp:\"" + DateTime.Now.ToString("s") + "\","
                + "data:[" +
                string.Join(",", _logItems
                    .Where(x => x is Keystroke)
                    .Cast<Keystroke>()
                    .GroupBy(x => x.ScanCode)
                    .OrderBy(x => x.Key)
                    .Select(x => string.Format("{{{0}:{1}}}", x.Key, x.Count()))
                ) + "]}";
            File.AppendAllText(path, Environment.NewLine + summary);

            _logItems.Clear();
        }
    }
}
