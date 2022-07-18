namespace ReactWithDotNet;

class ModelA
{
    public string PropA { get; set; }
    public int ClickCount { get; set; }
}

class ComponentA : ReactComponent<ModelA>
{
    public ComponentA()
    {
        state = new ModelA { PropA = "A" };
    }

    public override Element render()
    {
        return new div
        {
            style     = { width = "200px", height = "100px", border = "1px solid blue", textAlign = "center", paddingTop = "20px" },
            innerText = state.PropA + state.ClickCount,
            onClick   = _ => state.ClickCount++
        };
    }

    public void ComponentDidMount()
    {
        state.PropA += "-DidMountA-";
    }
}

class ModelB
{
    public string PropB { get; set; }
    public int ClickCount { get; set; }
}

class ComponentB : ReactComponent<ModelB>
{
    public ComponentB()
    {
        state = new ModelB { PropB = "B" };
    }

    public override Element render()
    {
        return new div
        {
            style     = { width = "250px", height = "150px", border = "1px solid brown", textAlign = "center", paddingTop = "40px" },
            innerText = state.PropB + state.ClickCount,
            onClick   = _ => state.ClickCount++
        };
    }

    public void ComponentDidMount()
    {
        state.PropB += "-DidMountB-";
    }
}

class ModelC
{
    public string PropC { get; set; }
    public int ClickCount { get; set; }
}

class ComponentC : ReactComponent<ModelC>
{
    public ComponentC()
    {
        state = new ModelC { PropC = "C" };
    }

    public override Element render()
    {
        return new div
        {
            style     = { width = "300px", height = "200px", border = "1px solid red", textAlign = "center", paddingTop = "50px" },
            innerText = state.PropC + state.ClickCount,
            onClick   = _ => state.ClickCount++
        };
    }
}

class ModelContainer1
{
    public string Container1Text { get; set; }
    public int ClickCount { get; set; }
}

class Container1 : ReactComponent<ModelContainer1>
{
    public Container1()
    {
        state = new ModelContainer1
        {
            Container1Text = "Container1Text"
        };
    }

    Element conditionalRender()
    {
        if (this.state.ClickCount % 3 == 0)
        {
            return new div
            {
                new div { innerText = "Mod3" },
                new ComponentC()
            };
        }

        return new ComponentC();
    }
    
    public override Element render()
    {
        return new div
        {
            style = { display = "flex" },
            children =
            {
                new ComponentA(),
                new ComponentB(),
                conditionalRender(),
                new ComponentC(),
                new div(state.Container1Text + state.ClickCount),
            },
            onClick = _ => state.ClickCount++
        };
    }
}

class ModelContainer2
{
    public string Container2Text { get; set; }
    public int ClickCount { get; set; }
}

class Container2 : ReactComponent<ModelContainer2>
{
    public Container2()
    {
        state = new ModelContainer2
        {
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
                new ComponentA(),
                new ComponentB(),
                new ComponentC(),
                new div(state.Container2Text + state.ClickCount++)
            },
            onClick = _ => state.ClickCount++
        };
    }
}

class ModelContainer3
{

    public string Container3Text { get; set; }
    public int ClickCount { get; set; }
}

class Container3 : ReactComponent<ModelContainer3>
{
    public Container3()
    {
        state = new ModelContainer3
        {
            Container3Text = "Container3_"
        };
    }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "column" },
            children =
            {
                new Container1(),
                new Container2(),
                new div { innerText = state.Container3Text + state.ClickCount }
            },
            onClick = _ => state.ClickCount++
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