using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using Concepts.ViewModels;
using Concepts.Views;

namespace Concepts
{
    [ModuleExport(typeof(ConceptsModule))]
    public class ConceptsModule : IModule
    {
        private readonly ConceptsViewModel _concepts;

        [Import]
        public IRegionManager RegionManager { get; set; }

        [Import]
        public ConceptsView ConceptsView { get; set; }

        [ImportingConstructor]
        public ConceptsModule(ConceptsViewModel concepts)
        {
            _concepts = concepts;
        }

        public void Initialize()
        {
            ConceptsView.DataContext = _concepts;
            RegionManager.RegisterViewWithRegion("ConceptsRegion", () => ConceptsView);
        }
    }
}