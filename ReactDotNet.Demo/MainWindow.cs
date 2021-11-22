using System;
using System.Collections.Generic;
using Bridge.Html5;
using ReactDotNet;
using ReactElement = ReactDotNet.ReactElement;

namespace ReactDotNet.Demo
{
    [Serializable]
    public class CityInfo
    {
        public string Name { get; set; } = "T";
        public int Id { get; set; } = 22;
    }

    [Serializable]
    public class AppModel
    {
        public string A { get; set; } = "T";
        public int Height { get; set; } = 22;
        public int SelectedCityId { get; set; } = 3;
        public IReadOnlyList<CityInfo> CityList { get; set; } = new[] { new CityInfo { Id = 1, Name = "Newyork" }, new CityInfo { Id = 3, Name = "washingTon" } };
    }

    [Serializable]
    public class AppProps
    {

    }
    class MainWindow : ReactComponent<AppProps,AppModel>
    {
        public void onChange(SyntheticEvent<HTMLElement> e)
        {
            SetState(model =>
            {
                model.A = e.Target["value"]?.ToString();

                model.Height = 0;

                if (int.TryParse(model.A, out var result))
                {
                    model.Height = result;
                }
            });
        }

        public void onClick(SyntheticEvent<HTMLElement> e)
        {
            void action()
            {
                SetState(x => { x.A += "1"; });
            }

            Window.SetTimeout(action, 200);
        }

        public void OnCityChanged(SyntheticEvent<HTMLElement> e)
        {
            SetState(s => s.SelectedCityId = Convert.ToInt32(e.Target["value"].ToString()));
        }

        public override ReactElement render()
        {

            return new ReactDotNet.PrimeReact.Button() { label = "Aloha" };

        }
    }
}