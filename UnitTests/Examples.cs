using System.Windows.Media;
using Concepts;
using Concepts.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using System.IO;
using Jil;
using System.Linq;
using System.ComponentModel.Composition;
using System;
using Shouldly;

namespace UnitTests
{
    [TestClass]
    public class Examples
    {
        private int _value;
        private int Value
        {
            get => _value;

            set
            {
                _value = value;
            }
        }

        [TestMethod]
        public void JsonVisualizerTest()
        {
            var fixture = new Fixture();

            fixture.Register<IDataSource>(() => fixture.Create<DataSource>());
            fixture.Register(() => new ExportFactory<PlotViewModel>(new Func<Tuple<PlotViewModel, Action>>(() => new Tuple<PlotViewModel, Action>(fixture.Create<PlotViewModel>(), null))));
            fixture.Inject(Colors.Brown);

           
            var sut = fixture.Create<ConceptsViewModel>();

            using (var output = new StringWriter())
            {
                var json = JSON.Serialize(sut);
            }
        }

        [TestMethod]
        public void DebugerDisplayExample()
        {
            var fixture = new Fixture();

            var measurements = fixture.CreateMany<Concepts.Measurement>(1000).ToArray();
            var testMeasurements = fixture.CreateMany<Measurement>(1000).ToArray();
        }

        [TestMethod]
        public void CallStackExample()
        {
            GetLength("Maciek").ShouldBe(6);
        }

        private int GetLength(string label)
        {
            return label == string.Empty ? 
                1 :
                1 + GetLength(label.Substring(1));
        }


        [TestMethod]
        public void ConditionalExpressionExample()
        {
            Value = 4;

            Value = 4;

            Value = 5;
        }

    }

}