﻿using Prism.Mef;
using Prism.Modularity;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System;
using Concepts;

namespace WpfExamples 
{
    public class Bootstrapper : MefBootstrapper, IDisposable
    {
        private CompositionContainer _container;

        public ConceptsModule ConceptsModule { get; private set; }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            ConceptsModule = _container.GetExportedValue<IModule>() as ConceptsModule;

            if (Application.Current == null)
            {
                // We're in tests
                return;
            }

            Application.Current.MainWindow = (Shell)Shell;
            Application.Current.MainWindow.Show();
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

        public void Dispose()
        {
            _container?.Dispose();
            ConceptsModule?.Dispose();
        }
    }
}