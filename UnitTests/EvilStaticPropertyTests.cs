using Shouldly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTests
{
    public class ExampleClass
    {
        internal static int MagicNumber = 666;

        public static int StaticProperty => MagicNumber++;
        public static int SecondStaticProperty => MagicNumber++;


        public int Normalize(int value)
        {
            if (MagicNumber > 100)
            {
                MagicNumber = 100;
            }

            return value / MagicNumber;
        }
    }


    [TestClass]
    public class EvilStaticPropertyTests
    {

        [TestMethod]
        public void NormalizationTest()
        {
            var sut = new ExampleClass();
            const int value = 1000;
            var normalizedValue = sut.Normalize(value);

            normalizedValue.ShouldBe(10);
        }

        [TestMethod]
        public void InitializationTest()
        {
            var sut = new ExampleClass();

            sut.ShouldNotBeNull();

            ExampleClass.StaticProperty.ShouldBe(666);
            ExampleClass.SecondStaticProperty.ShouldBe(667);
        }

    }
}