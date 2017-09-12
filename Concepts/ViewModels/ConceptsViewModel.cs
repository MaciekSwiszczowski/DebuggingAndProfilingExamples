using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using System.Windows.Media;
using OxyPlot;
using OxyPlot.Axes;
using Prism.Commands;
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





    [Export]
    public class PlotsViewModel : BindableBase
    {

        private readonly string[] _headers = {"Red", "Green", "Blue", "Black"};
        private readonly Color[] _colors = { Colors.Red, Colors.Green, Colors.Blue, Colors.Black };

        public ObservableCollection<PlotViewModel> Plots { get; set; }

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

    [Export]
    public class PlotViewModel : BindableBase, IDisposable
    {
        private readonly IDataSource _dataSource;
        //private ObservableCollection<Measurement> _measurements;
        public string Header { get; set; }
        public Color Color { get; set; }


        //public ReadOnlyObservableCollection<Measurement> Measurements { get; private set; }
        public ObservableCollection<Measurement> Measurements { get; private set; }
        public SliderViewModel Slider { get; }


        [ImportingConstructor]
        public PlotViewModel(IDataSource dataSource, SliderViewModel slider)
        {
            _dataSource = dataSource;
            Slider = slider;
            Slider.GenerateNewData += OnGenerateNewData;

            //_measurements = new ObservableCollection<Measurement>(dataSource.Get(slider.Start, slider.End));

            //Measurements = new ObservableCollection<Measurement>(dataSource.Get(slider.Start, slider.End));

            OnGenerateNewData();
        }

        private void OnGenerateNewData()
        {
            //_measurements = new ObservableCollection<Measurement>(_dataSource.Get(Slider.Start, Slider.End));
            //Measurements = new ReadOnlyObservableCollection<Measurement>(_measurements);
            Measurements = new ObservableCollection<Measurement>(_dataSource.Get(Slider.Start, Slider.End));
        }

        public void Dispose()
        {
            Slider.GenerateNewData -= OnGenerateNewData;
        }


    }

    public interface ISliderViewModel : IDisposable
    {
        double Start { get; }
        double End { get; }

        double RangeStart { get; }
        double RangeEnd { get; }

        event Action GenerateNewData;
    }


    [Export]
    public class SliderViewModel : ISliderViewModel
    {
        public event Action GenerateNewData;

        public ICommand GenerateNewDataCommand { get; }

        public double Start { get; set; }

        public double End { get; set; }

        public double RangeStart { get; set; }

        public double RangeEnd { get; set; }



        [ImportingConstructor]
        public SliderViewModel()
        {
            Start = 0;
            End = 100;

            RangeStart = 10;
            RangeEnd = 90;

            GenerateNewDataCommand = new DelegateCommand(() => GenerateNewData?.Invoke());
        }

        public void Dispose()
        {
            
        }


    }
}