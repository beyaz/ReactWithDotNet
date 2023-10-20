//namespace ReactWithDotNet.NullChildTest;

//class ModelA
//{
//    public string PropA { get; set; }
    
//    public int ClickCount { get; set; }
//}

//class ComponentA : Component<ModelA>
//{
//    protected override Task constructor()
//    {
//        state = new ModelA { PropA = "A" };

//        return Task.CompletedTask;
//    }

//    protected override Element render()
//    {
//        return new div
//        {
//            style =
//            {
//                width     = "200px",
//                height    = "100px",
//                border    = "1px solid blue", 
//                textAlign = "center",
//                padding   = "50px"
//            },
//            innerText    = state.PropA + state.ClickCount,
//            onClick      = _ => state.ClickCount++,
//        };
//    }
//}

//class ModelB
//{
//    public string PropB { get; set; }
    
//    public int ClickCount { get; set; }
//}

//class ComponentB : Component<ModelB>
//{
   

//    protected override Task constructor()
//    {
//        state = new ModelB { PropB = "B" };

//        return Task.CompletedTask;
//    }

//    protected override Element render()
//    {
//        return new div
//        {
//            style     = { 
//                width = "250px", 
//                height = "150px", 
//                border = "1px solid brown", 
//                textAlign = "center", 
//                padding = "50px"
//            },
//            innerText = state.PropB + state.ClickCount,
//            onClick   = _ => state.ClickCount++
//        };
//    }
//}

//class ContainerModel
//{
//    public int ClickCount { get; set; }
//}
//class Container : Component<ContainerModel>
//{
//    protected override Element render()
//    {
//        return new div
//        {
//            style = { display = "flex" },
//            children =
//            {
//                new ComponentA(),
//                state.ClickCount % 3 == 0 ? null : new ComponentB()
//            },
//            onClick = _ => state.ClickCount++
//        };
//    }
//}


///*
// https://jsfiddle.net/boilerplate/react-jsx

//class ComponentA extends React.Component
//{
//    constructor(props) 
//    {
//        super(props);

//        this.state = { PropA: "A", ClickCount: 0 };

//        this.handleClick = this.handleClick.bind(this);
//    }

//    handleClick() 
//    {
//        this.setState({ClickCount: this.state.ClickCount + 1});
//    }
    
//    render()
//    {
//        return (
//          <div style={{ width: "200px", height : "100px", border : "1px solid blue", textAlign : "center", paddingTop: "20px" }} onClick={this.handleClick}>
//              {this.state.PropA + this.state.ClickCount}
//          </div>
//        );        
//    }
//}

//class ComponentB extends React.Component
//{
//    constructor(props) 
//    {
//        super(props);

//        this.state = { PropB: "B", ClickCount: 0 };

//        this.handleClick = this.handleClick.bind(this);

//console.log("B created");
//    }

//    handleClick() 
//    {
//        this.setState({ClickCount: this.state.ClickCount + 1});
//    }
    
//    render()
//    {
//        return (
//          <div style={{ width: "250px", height : "150px", border : "1px solid blue", textAlign : "center", paddingTop: "40px" }} onClick={this.handleClick}>
//              {this.state.PropB + this.state.ClickCount}
//          </div>
//        );        
//    }
//}



//class Container extends React.Component
//{
//    constructor(props) 
//    {
//        super(props);

//        this.state = { Container1Text: "Container1Text", ClickCount: 0 };

//        this.handleClick = this.handleClick.bind(this);
//    }

//    handleClick() 
//    {
//        this.setState({ClickCount: this.state.ClickCount + 1});
//    }
        
//    render()
//    {
//        return (
//          <div style={{ display: "flex" }} onClick={this.handleClick}>
//              <ComponentA />
//              { this.state.ClickCount % 3 === 0 ? null :  <ComponentB /> }
//          </div>
//        );        
//    }
//}



//ReactDOM.render(<Container />, document.querySelector("#app"))


// */