using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Prism.Mvvm;

namespace Concepts.ViewModels
{
    [Export]
    public class PlotsViewModel : BindableBase
    {
        private PlotViewModel _selectedTab;

        public ObservableCollection<PlotViewModel> Plots { get; }

        public PlotViewModel SelectedTab
        {
            get => _selectedTab;
            set
            {
                _selectedTab = value;
                RaisePropertyChanged();

                if (_selectedTab.Color == Colors.Black)
                {
                    ((PlotViewModel)null).Color = Colors.DarkGray;
                }
            }
        }

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