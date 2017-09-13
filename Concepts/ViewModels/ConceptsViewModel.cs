using System.ComponentModel.Composition;
using Prism.Mvvm;

namespace Concepts.ViewModels
{
    [Export]
    public class ConceptsViewModel : BindableBase
    {
        public SliderViewModel Slider { get;  }
        public PlotsViewModel Plots { get;  }

        [ImportingConstructor]
        public ConceptsViewModel(SliderViewModel slider, PlotsViewModel plots)
        {
            Slider = slider;
            Plots = plots;
        }
    }
}