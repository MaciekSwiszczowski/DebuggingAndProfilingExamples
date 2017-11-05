using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Media;
using Prism.Mvvm;

namespace Concepts.ViewModels
{
    [Export]
    public class PlotViewModel : BindableBase, IDisposable
    {
        private readonly IDataSource _dataSource;
        private ObservableCollection<Measurement> _measurements;

        public string Header { get; set; }
        public Color Color { get; set; }


        public ObservableCollection<Measurement> Measurements
        {
            get => _measurements;
            private set
            {
                _measurements = value;
                RaisePropertyChanged();
            }
        }

        public SliderViewModel Slider { get; }


        [ImportingConstructor]
        public PlotViewModel(IDataSource dataSource, SliderViewModel slider)
        {
            _dataSource = dataSource;
            Slider = slider;
            Slider.GenerateNewData += OnGenerateNewData;
            
            OnGenerateNewData();
        }

        private void OnGenerateNewData()
        {
            Measurements = new ObservableCollection<Measurement>(_dataSource.Get(Slider.Minimum, Slider.Maximum));
        }

        public void Dispose()
        {
            Slider.GenerateNewData -= OnGenerateNewData;
        }

    }
}