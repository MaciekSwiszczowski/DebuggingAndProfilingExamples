using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Concepts.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]

    public class SliderViewModel : BindableBase
    {
        private double _rangeStart;
        private double _rangeEnd;
        public event Action GenerateNewData;

        public ICommand GenerateNewDataCommand { get; }

        public double Minimum { get; set; }

        public double Maximum { get; set; }

        public double RangeStart
        {
            get => _rangeStart;
            set
            {
                _rangeStart = value;
                RaisePropertyChanged();
            }
        }

        public double RangeEnd
        {
            get => _rangeEnd;
            set
            {
                _rangeEnd = value;
                RaisePropertyChanged();
            }
        }

        [ImportingConstructor]
        public SliderViewModel()
        {
            Minimum = 0;
            Maximum = 100;

            RangeStart = 10;
            RangeEnd = 90;

            GenerateNewDataCommand = new DelegateCommand(() => GenerateNewData?.Invoke());
        }

    }
}