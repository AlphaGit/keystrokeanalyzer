using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Alpha.KeystrokeAnalyzer.Domain;
using HookLib;
using HookLib.Windows;

namespace Alpha.KeystrokeAnalyzer.WinService
{
    public partial class KeystrokeAnalyzerService : ServiceBase
    {
        public KeystrokeAnalyzerService()
        {
            InitializeComponent();
            this._timestampOffset = Environment.TickCount;
        }

        private Timer _processingTimer { get; set; }
        private HookManager _hookManager { get; set;}
        private LogBuffer _logBuffer { get; set; }
        private long _timestampOffset;

        private void BootstrapLogger()
        {
            _hookManager = new HookManager();
            _logBuffer = LogBuffer.Instance;
            _hookManager.KeyPressed += hookManager_KeyPressed;
            _hookManager.MouseClick += hookManager_MouseClick;
        }

        public void Start()
        {
            Start(null);
        }

        public void Start(string[] args)
        {
            BootstrapLogger();
            BootstrapProcessingTimer();

            var messagePump = new WindowsMessagePump();
            messagePump.Run();
        }

        private void BootstrapProcessingTimer()
        {
            _processingTimer = new Timer(1000 * ServiceConfiguration.FlushIntervalSeconds);
            _processingTimer.Elapsed += ProcessingTimer_Elapsed;
            _processingTimer.Start();
        }

        private void ProcessingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _logBuffer.FlushToFile("log.txt");
        }

        protected override void OnStart(string[] args)
        {
            BootstrapLogger();
        }

        private void hookManager_MouseClick(object sender, GlobalMouseEventHandlerArgs args)
        {
            _logBuffer.AddLogItem(new MouseClick(args.Time + Environment.TickCount - this._timestampOffset));
        }

        private void hookManager_KeyPressed(object sender, GlobalKeyEventHandlerArgs args)
        {
            _logBuffer.AddLogItem(new Keystroke(args.ScanCode, args.Time + Environment.TickCount - this._timestampOffset));
        }

        protected override void OnStop()
        {
            //TODO dispose hookManager
        }
    }
}
