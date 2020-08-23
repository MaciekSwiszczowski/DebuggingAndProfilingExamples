using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Timers;
using System.Windows;
using Concepts;
using Prism.Mef;
using Prism.Modularity;

namespace WpfExamples
{
    public class Bootstrapper : MefBootstrapper, IDisposable
    {
        private readonly List<int[]> _buffer = new List<int[]>();
        private CompositionContainer _container;
        private readonly Timer _timer = new Timer();

        public ConceptsModule ConceptsModule { get; private set; }

        public void Dispose()
        {
            _container?.Dispose();
            ConceptsModule?.Dispose();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            /*
            double sum = 0;
            for(var i = 0; i < 100_000_000; i++)
            {
                sum += Math.Sin(i);
            }

            Console.WriteLine(sum);
            */

            base.InitializeShell();

            ConceptsModule = _container.GetExportedValue<IModule>() as ConceptsModule;

            if (Application.Current == null)
                // We're in tests
                return;

            Application.Current.MainWindow = (Shell) Shell;
            Application.Current.MainWindow?.Show();

            OnInitialized();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ConceptsModule).Assembly));
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var conceptsModule = typeof(ConceptsModule);
            ModuleCatalog.AddModule(new ModuleInfo(conceptsModule.Name, conceptsModule.AssemblyQualifiedName));
        }

        protected override CompositionContainer CreateContainer()
        {
            _container = base.CreateContainer();
            _container.ComposeExportedValue(_container);

            return _container;
        }

        private void OnInitialized()
        {
            _timer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
            _timer.Elapsed += (sender, e) => _buffer.Add(new int[100_000]);
            _timer.Start();
        }
    }
}