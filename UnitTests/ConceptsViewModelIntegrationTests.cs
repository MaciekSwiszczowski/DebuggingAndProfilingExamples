using Concepts.ViewModels;
using Concepts.Views;
using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JetBrains.dotMemoryUnit;
using WpfExamples;

namespace UnitTests
{
    [TestClass]
    public class ConceptsViewModelIntegrationTests
    {
        [TestMethod]
        [DotMemoryUnit(FailIfRunWithoutSupport = false, SavingStrategy = SavingStrategy.Never)]
        public void Initialization_should_be_successful()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<ConceptsView>()).ObjectsCount.ShouldBe(1));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<ConceptsViewModel>()).ObjectsCount.ShouldBe(1));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<PlotsViewModel>()).ObjectsCount.ShouldBe(1));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<SliderViewModel>()).ObjectsCount.ShouldBe(1));
        }


        [TestMethod]
        [DotMemoryUnit(FailIfRunWithoutSupport = false, SavingStrategy = SavingStrategy.Never)]
        public void After_Dispose_objects_should_freed()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();

            bootstrapper.ConceptsModule.Dispose();

            //dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<ConceptsView>()).ObjectsCount.ShouldBe(0));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<PlotViewModel>()).ObjectsCount.ShouldBe(0));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<ConceptsViewModel>()).ObjectsCount.ShouldBe(0));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<PlotsViewModel>()).ObjectsCount.ShouldBe(0));
            dotMemory.Check(memory => memory.GetObjects(where => where.Type.Is<SliderViewModel>()).ObjectsCount.ShouldBe(0));
        }

    }
}