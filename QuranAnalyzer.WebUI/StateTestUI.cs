using System;
using System.Collections.Generic;
using System.Text;

namespace ReactDotNet;

class ModelA
    {
        public string PropA { get; set; }
        public int ClickCount { get; set; }
    }
    class ComponentA: ReactComponent<ModelA>
    {
        public override void constructor()
        {
            state = new ModelA { PropA = "A" };
        }

        public override Element render()
        {
            return new div
            {
                style   = { width = "200px", height = "100px", border = "1px solid blue", textAlign = "center", paddingTop = "20px" },
                innerText    = state.PropA + state.ClickCount,
                onClick = _ => state.ClickCount++
            };
        }
    }


    class ModelB
    {
        public string PropB { get; set; }
        public int ClickCount { get; set; }
    }
    class ComponentB : ReactComponent<ModelB>
    {
        public override void constructor()
        {
            state = new ModelB { PropB = "B" };
        }

        public override Element render()
        {
            return new div
            {
                style   = { width = "250px", height = "150px" ,border = "1px solid brown", textAlign = "center", paddingTop = "40px" },
                innerText    = state.PropB + state.ClickCount,
                onClick = _ => state.ClickCount++
            };
        }
    }


class ModelC
{
    public string PropC { get; set; }
    public int ClickCount { get; set; }
}
class ComponentC : ReactComponent<ModelC>
{
    public override void constructor()
    {
        state = new ModelC { PropC = "C" };
    }

    public override Element render()
    {
        return new div
        {
            style   = { width = "300px", height = "200px", border = "1px solid red", textAlign = "center", paddingTop = "50px" },
            innerText    = state.PropC + state.ClickCount,
            onClick = _ => state.ClickCount++
        };
    }
}


class ModelContainer1
{
    public ModelA A { get; set; }
    public ModelB B { get; set; }
    public ModelC C { get; set; }
    public string Container1Text { get; set; }

    public int ClickCount { get; set; }
}
class Container1 : ReactComponent<ModelContainer1>
{
    public override void constructor()
    {
        state = new ModelContainer1
        {
            A              = new ModelA{PropA  = "A"},
            B              = new ModelB{PropB  = "B"},
            C              = new ModelC{ PropC = "C"},
            Container1Text = "Container1Text"
        };
    }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex"},
            children =
            {
                new ComponentA {state = state.A},
                new ComponentB{state  = state.B},
                new ComponentC{state  = state.C},
                new div(state.Container1Text + state.ClickCount),
                
            },
            onClick = _ => state.ClickCount++
        };
    }
}


class ModelContainer2
{
    public ModelA A { get; set; }
    public ModelB B { get; set; }
    public ModelC C { get; set; }
    public string Container2Text { get; set; }

    public int ClickCount { get; set; }
}
class Container2 : ReactComponent<ModelContainer2>
{
    public override void constructor()
    {
        state = new ModelContainer2
        {
            A              = new ModelA { PropA = "A" },
            B              = new ModelB { PropB = "B" },
            C              = new ModelC { PropC = "C" },
            Container2Text = "Container2Text"
        };
    }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex" },
            children =
            {
                new ComponentA {state = state.A},
                new ComponentB{state  = state.B},
                new ComponentC{state  = state.C},
                new div(state.Container2Text + state.ClickCount++)
            },
            onClick = _ => state.ClickCount++
        };
    }
}

class ModelContainer3
{
    public ModelContainer1 ContainerModel1 { get; set; }
    public ModelContainer2 ContainerModel2 { get; set; }

    public string Container3Text { get; set; }
    public int ClickCount { get; set; }
}
class Container3 : ReactComponent<ModelContainer3>
{
    public override void constructor()
    {
        state = new ModelContainer3
        {
            ContainerModel1 = new ModelContainer1
            {
                A              = new ModelA { PropA = "A" },
                B              = new ModelB { PropB = "B" },
                C              = new ModelC { PropC = "C" },
                Container1Text = "Container1_"
            },
            ContainerModel2 = new ModelContainer2
            {
                A              = new ModelA { PropA = "A" },
                B              = new ModelB { PropB = "B" },
                C              = new ModelC { PropC = "C" },
                Container2Text = "Container2_"
            },
            Container3Text = "Container3_"
        };
    }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "column"},
            children =
            {
                new Container1{ state = state.ContainerModel1},
                new Container2{ state = state.ContainerModel2},
                new div{innerText     = state.Container3Text + state.ClickCount}

            },
            onClick = _ => state.ClickCount++
        };
    }
}