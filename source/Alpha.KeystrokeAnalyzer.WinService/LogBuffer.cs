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
            var writer = File.CreateText(path);
            writer.Write(
                string.Join("\t", _logItems
                    .Where(x => x is Keystroke)
                    .Cast<Keystroke>()
                    .GroupBy(x => x.ScanCode)
                    .Select(x => string.Format("{0}:{1}", x.Key, x.Count()))
                )
            );
            writer.Flush();
            writer.Close();
            writer.Dispose();

            _logItems.Clear();
        }
    }
}
