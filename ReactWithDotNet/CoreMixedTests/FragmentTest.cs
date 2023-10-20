//namespace ReactWithDotNet.FragmentTest;

//class ModelA
//{
//    public string PropA { get; set; }
    
//    public int ClickCount { get; set; }
    
//    public int ClickCount2 { get; set; }
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
//        return new Fragment
//        {
//            new div
//            {
//                Width(200), Height(100),Border("1px solid blue"),TextAlignCenter, PaddingTop(20),
//                OnClick(_=>state.ClickCount++),
//                Text(state.PropA + state.ClickCount)
//            },

//            new div
//            {
//                Width(200), Height(100),Border("1px solid blue"),TextAlignCenter, PaddingTop(20),
//                OnClick(_=>state.ClickCount2++),
//                Text(state.PropA + state.ClickCount2)
//            }
//        };
//    }
//}



//class ContainerModel
//{
//    public string PropC { get; set; }
//    public int ClickCount { get; set; }
//}
//class Container : Component<ContainerModel>
//{
//    protected override Element render()
//    {
//        return new div
//        {
//            new Fragment
//            {
//                new div { text = "A" },
//                new div { text = "B" },
//                new Fragment
//                {
//                    new div { text = "C" },
//                    new div { text = "D" },
//                    new div
//                    {
//                        new Fragment
//                        {
//                            new div
//                            {
//                                Width(200), Height(100),Border("1px solid blue"),TextAlignCenter, PaddingTop(20),
//                                OnClick(_=>state.ClickCount++),
//                                Text(state.PropC + state.ClickCount)
//                            },

//                            new ComponentA()
//                        }
//                    }
//                }
//            }
//        };
//    }
//}


///*
//https://frontarm.com/james-k-nelson/react-fragments-in-practice/




//class ComponentA extends React.Component
//{
//    constructor(props) 
//    {
//        super(props);

//        this.state = { PropA: "A", ClickCount: 0, ClickCount2: 0 };

//        this.handleClick = this.handleClick.bind(this);
//        this.handleClick2 = this.handleClick2.bind(this);
//    }

//    handleClick() 
//    {
//        this.setState({ClickCount: this.state.ClickCount + 1});
//    }
     
//    handleClick2()
//    {
//        this.setState({ClickCount2: this.state.ClickCount2 + 1});
//    }
//    render()
//    {
//        return (
//        <React.Fragment>
//          <div style={{ width: "200px", height : "100px", border : "1px solid blue", textAlign : "center", paddingTop: "20px" }} onClick={this.handleClick}>
//              {this.state.PropA + this.state.ClickCount}
//          </div>
//          <div style={{ width: "200px", height : "100px", border : "1px solid blue", textAlign : "center", paddingTop: "20px" }}
//          onClick={this.handleClick2}>
//              {this.state.PropA + this.state.ClickCount2}
//          </div>
//        </React.Fragment>
//        );        
//    }
//}


//class Container extends React.Component
//{
//    constructor(props) 
//    {
//        super(props);

//        this.state = { PropC: "C", ClickCount: 0};
//        this.handleClick = this.handleClick.bind(this);
//    }

//    handleClick() 
//    {
//        this.setState({ClickCount: this.state.ClickCount + 1});
//    }
     
//    render()
//    {
//        return (
//        <React.Fragment>
//          <div style={{ width: "200px", height : "100px", border : "1px solid blue", textAlign : "center", paddingTop: "20px" }} onClick={this.handleClick}>
//              {this.state.PropC + this.state.ClickCount}
//          </div>
          
//          <ComponentA />
          
//        </React.Fragment>
//        );        
//    }
//}

//ReactDOM.render(
//  <>
//    <h1>Yo Dawgu,</h1>
//    <p>
//      I heard you like fragments so
//      I <>put a <>fragment</> in your fragment</>.
//    </p>
//    <Container />
//  </>,
//  document.querySelector("#root")
//)


// */