using System.Windows;

namespace WpfExamples
{
    public partial class App : Application
    {
        private Bootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Run();

            Current.Exit += OnExit;
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            _bootstrapper.Dispose();
        }
    }
}