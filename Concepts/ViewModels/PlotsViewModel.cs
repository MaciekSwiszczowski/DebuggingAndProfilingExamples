using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Prism.Mvvm;

namespace Concepts.ViewModels
{
    [Export]
    public class PlotsViewModel : BindableBase
    {
        public ObservableCollection<PlotViewModel> Plots { get; }

        [ImportingConstructor]
        public PlotsViewModel(ExportFactory<PlotViewModel> plotViewModelFactory)
        {
            Plots = new ObservableCollection<PlotViewModel>
            {
                GetPlotViewModel(plotViewModelFactory, Colors.Red, "Red"),
                GetPlotViewModel(plotViewModelFactory, Colors.Green, "Green"),
                GetPlotViewModel(plotViewModelFactory, Colors.Blue, "Blue"),
                GetPlotViewModel(plotViewModelFactory, Colors.Black, "Black")
            };
        }

        private static PlotViewModel GetPlotViewModel(ExportFactory<PlotViewModel> plotViewModelFactory, Color color, string header)
        {
            var plot = plotViewModelFactory.CreateExport().Value;
            plot.Color = color;
            plot.Header = header;
            return plot;
        }
    }
}