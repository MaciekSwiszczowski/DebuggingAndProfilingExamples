using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Prism.Mvvm;

namespace Concepts.ViewModels
{
    [Export]
    public class PlotsViewModel : BindableBase
    {

        private readonly string[] _headers = {"Red", "Green", "Blue", "Black"};
        private readonly Color[] _colors = { Colors.Red, Colors.Green, Colors.Blue, Colors.Black };

        public ObservableCollection<PlotViewModel> Plots { get; }

        [ImportingConstructor]
        public PlotsViewModel(ExportFactory<PlotViewModel> plotViewModelFactory)
        {
            Plots = new ObservableCollection<PlotViewModel>();
            for (var i = 0; i < 4; i++)
            {
                var viewModel = plotViewModelFactory.CreateExport();

                Plots.Add(viewModel.Value);

                viewModel.Value.Header = _headers[i];
                viewModel.Value.Color = _colors[i];
            }
        }
    }
}