using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactWithDotNet.Libraries.UIDesigner.Components;

namespace ReactWithDotNet.Test
{
    [TestClass]
    public class InstanceEditorTests
    {
        class SampleClassA
        {
            public string PropA1 { get; set; }
            public string PropA2 { get; set; }
            int Field3;
            public SampleClassB NestedB { get; set; }

            public IReadOnlyList<SampleClassB> Blist { get; set; }
        }

        class SampleClassB
        {
            public string PropB1 { get; set; }
            public string Field1;

            public SampleClassD InnerD { get; set; }
        }

        class SampleClassC
        {
            public string PropC0 { get; set; }
            public int PropC1 { get; set; }
        }

        class SampleClassD
        {
            public string PropD0 { get; set; }
            public int PropD1 { get; set; }

            public SampleClassC InnerC { get; set; }
        }

        


        [TestMethod]
        public void _2_()
        {
            var instanceC = new SampleClassC();

            var items = TypeInspector.GetValueInfoList(typeof(SampleClassC), instanceC, "y.");

            items.Count.Should().Be(2);


            var instanceD = new SampleClassD
            {
                InnerC = new SampleClassC
                {
                    PropC0 = "0",
                    PropC1 = 5
                },
                PropD0 = "1",
                PropD1 = 3
            };

            items = TypeInspector.GetValueInfoList(typeof(SampleClassD), instanceD, "y.");

            items.Count.Should().Be(4);

            var instanceB = new SampleClassB
            {
                Field1 = "_a",
                PropB1 = "7y",
                InnerD = new SampleClassD
                {
                    InnerC = new SampleClassC
                    {
                        PropC0 = "0",
                        PropC1 = 5
                    },
                    PropD0 = "1",
                    PropD1 = 3
                }
            };

            items = TypeInspector.GetValueInfoList(typeof(SampleClassB), instanceB, "b.");

            items.Count.Should().Be(6);


            var instanceA = new SampleClassA
            {
                PropA1 = "Prop1",
                PropA2 = "preop2",
                NestedB = new SampleClassB
                {
                    Field1 = "_a",
                    PropB1 = "7y",
                    InnerD = new SampleClassD
                    {
                        InnerC = new SampleClassC
                        {
                            PropC0 = "0",
                            PropC1 = 5
                        },
                        PropD0 = "1",
                        PropD1 = 3
                    }
                },
                
                Blist = new List<SampleClassB>()
            };

            items = TypeInspector.GetValueInfoList(typeof(SampleClassA), instanceA, "a.");

            items.Count.Should().Be(9);
        }

    }
}