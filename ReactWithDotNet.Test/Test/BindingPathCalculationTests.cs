using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test
{
    [TestClass]
    public class BindingPathCalculation
    {
        SampleClassA[] Models { get; set; }

        [TestMethod]
        public void _1_()
        {
            var state = new SampleClassA();

            Extensions.AsBindingPath(() => state.PropA1).path.Should().BeEquivalentTo("PropA1");
            Extensions.AsBindingPath(() => state.PropA1).isConnectedToState.Should().BeTrue();

            Extensions.AsBindingPath(() => state.NestedB.NestedA.PropA2).path.Should().BeEquivalentTo("NestedB.NestedA.PropA2".Split('.'));
            Extensions.AsBindingPath(() => state.NestedB.NestedA.PropA2).isConnectedToState.Should().BeTrue();

            var expected = new List<string> { "Blist", "[", "4", "]", "NestedA", "NestedB", "PropB2" };
            expected.Reverse();

            Extensions.AsBindingPath(() => state.Blist[4].NestedA.NestedB.PropB2).path.Should().BeEquivalentTo(expected);
            Extensions.AsBindingPath(() => state.Blist[4].PropB1).isConnectedToState.Should().BeTrue();

            expected = new List<string> { "Models", "[", "3", "]", "Blist", "[", "4", "]", "NestedA", "NestedB", "PropB2" };

            Extensions.AsBindingPath(() => Models[3].Blist[4].NestedA.NestedB.PropB2).path.Should().BeEquivalentTo(expected);
        }

        class SampleClassA
        {
            public IReadOnlyList<SampleClassB> Blist { get; set; }
            public SampleClassB NestedB { get; set; }
            public string PropA1 { get; set; }
            public string PropA2 { get; set; }
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        class SampleClassB
        {
            public SampleClassA NestedA { get; set; }
            public string PropB1 { get; set; }
            public string PropB2 { get; set; }
        }
    }
}