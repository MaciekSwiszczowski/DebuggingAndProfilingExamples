using System.ComponentModel.Composition;
using System.Windows.Media;
using Concepts;
using Concepts.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;
using Shouldly;

namespace UnitTests
{
    [TestClass]
    public class ConceptsViewModelTests
    {
        [TestMethod]
        public void Initialization_Should_Be_Successful()
        {
            var fixture = new Fixture();

            fixture.Register<IDataSource>(() => new DataSource());
            fixture.Inject(Colors.Brown);

           
            var sut = fixture.Create<ConceptsViewModel>();

            sut.ShouldNotBeNull();
        }
    }
}