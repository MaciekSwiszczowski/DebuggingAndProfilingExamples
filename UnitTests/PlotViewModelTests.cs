using System.Windows.Media;
using Concepts;
using Concepts.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Shouldly;

namespace UnitTests
{
    [TestClass]
    public class PlotViewModelTests
    {
        [TestMethod]
        public void Initialization_should_be_successful()
        {
            var sut = GetPlotViewModel();

            sut.ShouldNotBeNull();
            sut.Measurements.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void On_GenerateNewData_event_Measurements_should_be_changed()
        {
            var sut = GetPlotViewModel();

            var measurementsOnStart = sut.Measurements;

            var slider = sut.Slider;
            slider.GenerateNewDataCommand.Execute(null);


            sut.Measurements.ShouldNotBeSameAs(measurementsOnStart);
        }

        [TestMethod]
        public void Dispose_should_be_successful()
        {
            var sut = GetPlotViewModel();

            var measurementsOnStart = sut.Measurements;

            sut.Dispose();

            var slider = sut.Slider;
            slider.GenerateNewDataCommand.Execute(null);


            sut.Measurements.ShouldBeSameAs(measurementsOnStart);
        }

        private static PlotViewModel GetPlotViewModel()
        {
            var fixture = new Fixture();

            fixture.Register<IDataSource>(() => new DataSource());

            fixture.Inject(Colors.Brown);

            fixture.Customize<SliderViewModel>(
                _ => _
                    .With(x => x.Minimum, 0)
                    .With(x => x.RangeStart, 10)
                    .With(x => x.RangeEnd, 90)
                    .With(x => x.Maximum, 100));
            
            var sut = fixture.Create<PlotViewModel>();
            return sut;
        }
    }
}
