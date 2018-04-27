using NUnit.Framework;

namespace ApplicationEssentials.Tests.ArgumentsProcessing
{
    [TestFixture]
    public class ArgumentsProcessorTests
    {
        private ArgumentsProcessor processor;

        [SetUp]
        public void SetUp()
        {
            processor = new ArgumentsProcessor();
        }

        [Test]
        public void ParseArguments_EmptyArgs_ReturnsEmptyObject()
        {
            var args = new string[0];

            var result = processor.ParseArguments<object>(args);

            Assert.That(result, Is.TypeOf(typeof(object)));
        }

        [Test]
        public void ParseArguments_When_PositionalParametersAreInCorrectOrder_Then_ReturnInitializedClass()
        {
            var args = new string[] { "first", "second", "third" };

            var result = processor.ParseArguments<PositionalParameters>(args);

            Assert.That(result.First, Is.EqualTo("first"));
            Assert.That(result.Second, Is.EqualTo("second"));
            Assert.That(result.Third, Is.EqualTo("third"));
        }
    }
}