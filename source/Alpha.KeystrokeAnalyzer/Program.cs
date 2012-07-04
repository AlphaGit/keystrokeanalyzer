using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using HookLib;
using HookLib.Windows;

namespace Alpha.KeystrokeAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var hookManager = new HookManager();
            hookManager.KeyPressed += hookManager_KeyPressed;
            hookManager.MouseClick += hookManager_MouseClick;

            var messagePump = new WindowsMessagePump();
            messagePump.Run();

            System.Console.WriteLine("Finished.");
            System.Console.ReadKey();
        }

        static void hookManager_MouseClick(object sender, GlobalMouseEventHandlerArgs args)
        {
            Console.WriteLine(args.Point);
        }

        static void hookManager_KeyPressed(object sender, GlobalKeyEventHandlerArgs args)
        {
            Console.WriteLine(args.Character);
        }
    }
}
