namespace ReactWithDotNet.StateTests.PropSerializationTests;

class ModelA
{
    public string StateValueA { get; set; }
    
    public int ClickCount { get; set; }

}

class ComponentA : ReactComponent<ModelA>
{
    public string A_Prop_1 { get; set; }

    public string A_Prop_2 { get; set; }


    public ComponentA()
    {
        state = new ModelA { StateValueA = "A" };
    }

    public override Element render()
    {
        return new div
        {
            style =
            {
                border     = "1px solid blue", 
                padding = "20px"
            },
            text    = "state.A: " + state.StateValueA + ", state.ClickCount: "+ state.ClickCount + ", A_Prop_1:"+ A_Prop_1 + ", A_Prop_2:"+ A_Prop_2,
            onClick = _ => state.ClickCount++,
        };
    }
}

class ModelB
{
    public string StateValueB { get; set; }
    public int ClickCount { get; set; }
}

class ComponentB : ReactComponent<ModelB>
{
    public string B_Prop_1 { get; set; }

    public string B_Prop_2 { get; set; }

    
    public ComponentB()
    {
        state = new ModelB { StateValueB = "B" };
    }

    public override Element render()
    {
        return new div
        {
            style =
            {
                border  = "1px solid red",
                padding = "20px"
            },
            text    = "state.B: " + state.StateValueB + ", state.ClickCount: " + state.ClickCount + ", B_Prop_1:" + B_Prop_1 + ", B_Prop_2:" + B_Prop_2,
            onClick = _ => state.ClickCount++,
        };
    }
}


class ModelContainer1
{
    public int ClickCount { get; set; }
}

class Container1 : ReactComponent<ModelContainer1>
{
    public Container1()
    {
        state = new ModelContainer1
        {
        };
    }
    
    
    public override Element render()
    {
        return new div
        {
            children =
            {
                new ComponentA{A_Prop_1 = "A_Prop_1", A_Prop_2 = "A_Prop_2"},
                new ComponentB{B_Prop_1 = "B_Prop_1", B_Prop_2 = "B_Prop_2"}
            },
            //onClick = _ => state.ClickCount++
        };
    }
}


/*
 https://jsfiddle.net/boilerplate/react-jsx

class ComponentA extends React.Component
{
    constructor(props) 
    {
        super(props);

        this.state = { PropA: "A", ClickCount: 0 };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() 
    {
        this.setState({ClickCount: this.state.ClickCount + 1});
    }
    
    render()
    {
        return (
          <div style={{ width: "200px", height : "100px", border : "1px solid blue", textAlign : "center", paddingTop: "20px" }} onClick={this.handleClick}>
              {this.state.PropA + this.state.ClickCount}
          </div>
        );        
    }
}

class ComponentB extends React.Component
{
    constructor(props) 
    {
        super(props);

        this.state = { PropB: "B", ClickCount: 0 };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() 
    {
        this.setState({ClickCount: this.state.ClickCount + 1});
    }
    
    render()
    {
        return (
          <div style={{ width: "250px", height : "150px", border : "1px solid blue", textAlign : "center", paddingTop: "40px" }} onClick={this.handleClick}>
              {this.state.PropB + this.state.ClickCount}
          </div>
        );        
    }
}

class ComponentC extends React.Component
{
    constructor(props) 
    {
        super(props);

        this.state = { PropC: "C", ClickCount: 0 };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() 
    {
        this.setState({ClickCount: this.state.ClickCount + 1});
    }
    
    render()
    {
        return (
          <div style={{ width: "300px", height : "200px", border : "1px solid blue", textAlign : "center", paddingTop: "50px" }} onClick={this.handleClick}>
              {this.state.PropC + this.state.ClickCount}
          </div>
        );        
    }
}

class Container1 extends React.Component
{
    constructor(props) 
    {
        super(props);

        this.state = { Container1Text: "Container1Text", ClickCount: 0 };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() 
    {
        this.setState({ClickCount: this.state.ClickCount + 1});
    }
    
    conditionalRender()
    {
        if( this.state.ClickCount % 3 == 0)
        {
            return (
                <div>
                    <div>Mod3</div>
                    <ComponentC />
                </div>            
            );
        }
        
        return (<ComponentC />);        
    }
    
    render()
    {
        return (
          <div style={{ display: "flex" }} onClick={this.handleClick}>
              <ComponentA />
              <ComponentB />
              {this.conditionalRender()},
              <div onClick={this.handleClick}>
                  {this.state.Container1Text + this.state.ClickCount}
              </div>
          </div>
        );        
    }
}

class Container2 extends React.Component
{
    constructor(props) 
    {
        super(props);

        this.state = { Container2Text: "Container2Text", ClickCount: 0 };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() 
    {
        this.setState({ClickCount: this.state.ClickCount + 1});
    }
    
    render()
    {
        return (
          <div style={{ display: "flex" }} onClick={this.handleClick}>
              <ComponentA />
              <ComponentB />
              <ComponentC />
              <div onClick={this.handleClick}>
                  {this.state.Container2Text + this.state.ClickCount}
              </div>
          </div>
        );        
    }
}

class Container3 extends React.Component
{
    constructor(props) 
    {
        super(props);

        this.state = { Container3Text: "Container3Text", ClickCount: 0 };

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() 
    {
        this.setState({ClickCount: this.state.ClickCount + 1});
    }
    
    render()
    {
        return (
          <div style={{ display: "flex", flexDirection: "column" }} onClick={this.handleClick}>
              <Container1 />
              <Container2 />
              <div onClick={this.handleClick}>
                  {this.state.Container3Text + this.state.ClickCount}
              </div>
          </div>
        );        
    }
}


ReactDOM.render(<Container3 />, document.querySelector("#app"))


 */