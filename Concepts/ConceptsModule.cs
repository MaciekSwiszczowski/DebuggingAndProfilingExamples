using System;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Concepts.ViewModels;
using Concepts.Views;

namespace Concepts
{
    [ModuleExport(typeof(ConceptsModule))]
    public class ConceptsModule : IModule, IDisposable
    {
        private readonly CompositionContainer _localContainer;
        private ConceptsView _conceptsView;

        public IRegionManager RegionManager { get; set; }


        [ImportingConstructor]
        public ConceptsModule(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ConceptsModule).Assembly));
            _localContainer = new CompositionContainer(catalog);

            _localContainer.ComposeExportedValue(RegionManager);
            
            _localContainer.ComposeParts(this);
        }

        public void Initialize()
        {
            _conceptsView = _localContainer.GetExportedValue<ConceptsView>();
            _conceptsView.DataContext = _localContainer.GetExportedValue<ConceptsViewModel>();

            RegionManager.RegisterViewWithRegion("ConceptsRegion", () => _conceptsView);
        }

        public void Dispose()
        {
            _conceptsView.DataContext = null;
            RegionManager.Regions.Remove("ConceptsRegion");

            _localContainer?.Dispose();
        }
    }
}