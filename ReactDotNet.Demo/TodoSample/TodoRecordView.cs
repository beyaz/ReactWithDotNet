using System;
using System.Collections.Generic;
using System.Linq;
using ReactDotNet.PrimeReact;
using InputText = ReactDotNet.PrimeReact.InputText;
using  static ReactDotNet.Mixin;

namespace ReactDotNet.Demo.TodoSample
{

    [Serializable]
    public class TodoRecordList
    {
        public List<TodoRecord> Records { get; set; }
        public string AnyName { get; set; }

        public string B { get; set; }

        public string SelectedTabName { get; set; }

        public int SelectedTabIndex { get; set; }

    }

    class TodoRecordListView : ReactDotNet.ReactComponent<TodoRecordList>
    {

        public string Aloha { get; set; } = "ABC";

        #region Constructors
        public TodoRecordListView()
        {
            state = new TodoRecordList
            {
                Records = new List<TodoRecord>
                {
                    new TodoRecord { Id = 1, Text = "1" },
                    new TodoRecord { Id = 2, Text = "2" }
                },
                AnyName = "NameX",
                B = "B",
                SelectedTabName = "None"
            };
        }
        #endregion

        #region Public Methods

        public override Element render()
        {

            var countries = new[] {
            new { name= "Australia", code= "AU" },
            new { name = "Brazil", code = "BR" },
            new { name = "China", code = "CN" },
            new { name = "Egypt", code = "EG" },
            new { name = "France", code = "FR" },
            new { name = "Germany", code = "DE" },
            new { name = "India", code = "IN" },
            new { name = "Japan", code = "JP" },
            new { name = "Spain", code = "ES" },
            new { name = "United States", code = "US" }
            };


            return new div("className-" + state.Records.Count)
            {
                new InputText
                {
                    value =state.B
                    // valueBind = Bind(()=>state.AnyName)
                },
                new InputText
                {
                    valueBind = ()=>state.AnyName
                },
                new TodoRecordView{ MyProp1 = "A"},
                new TodoRecordView{ MyProp1 = "B"},
                new TodoRecordView{ MyProp1 = "C"},
                new Button{ label = "Update", onClick = Update},
                new InputTextarea
                {
                    value = state.B,
                    rows = 3

                },
                //new MaterialUI.Autocomplete{ 
                //    value  = "aa" , 
                //    options       = new []{"A","B"}, 
                //    renderInput =()=> new MaterialUI.TextField{}

                //},

                new Button{ label = state.SelectedTabName},
                new TabView()
                {
                    // onTabChange = OnTabChange,
                    onTabChange = (e)=>{ OnTabChange(e); },
                    activeIndex = state.SelectedTabIndex,
                    Children={
                         new TabPanel
                    {
                        header = "tab1",
                        Children =
                             {
                                 new VPanel
                                 {
                                    new Button(){label ="A"},
                                    new Button(){label ="BB"},
                                    new ListBox{
                                        options=countries,
                                        value="FR" ,                                        
                                        onChange= e => state.SelectedTabName ="Aloha"+ e.value,
                                        optionLabel = "name",
                                        optionValue =  "code",
                                        filter = true,
                                        //itemTemplate={this.countryTemplate} 
                                        //style={{ width: '15rem' }}
                                        listStyle={ MaxHeight = "250px" }
                                    }

                                 },
                               
                                 new HPanel
                                 {
                                     new Button(){label ="C"},
                                     new Button(){label ="d"}
                                 }
                             }
                    },
                    new TabPanel
                    {
                        header = "tab2"
                    }
                    }
                }

            }.Style(s=>s.Background = "#E7E9EB");
        }

        void OnTabChange(TabViewTabChangeParams e)
        {
            state.SelectedTabIndex = e.index;

            state.SelectedTabName = "SelectedIndexş:" + e.index;
        }

        void Update()
        {
            state.B = state.AnyName;
        }
        #endregion
    }


    public class TodoRecordView : ReactComponent<TodoRecord>
    {
        public TodoRecordView()
        {
            state = new TodoRecord();
        }

        public string MyProp1 { get; set; }

        void OnClicked()
        {
            
            state.ClickCount++;
        }

        public override Element render()
        {
            return new div("C4")
            {
                new button{ text   = MyProp1+"Click Count: " + state.ClickCount, onClick = OnClicked},
                new Button
                {
                    label = MyProp1+"Click Count: " + state.ClickCount, onClick = OnClicked,
                    tooltip = "My tooltip",
                    tooltipOptions = { position = TooltipPositionType.left}
                }
            };
        }
    }
}