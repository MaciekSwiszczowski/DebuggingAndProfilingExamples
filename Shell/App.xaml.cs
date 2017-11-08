using System;
using System.Diagnostics;
using System.Windows;

namespace WpfExamples
{
    public partial class App
    {
        private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Run();

            Exit += OnExit;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            _bootstrapper.Dispose();
        }
    }
}