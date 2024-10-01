using System.IO;

namespace ReactWithDotNet.WebSite.Components;

class MainPageContentSample : PureComponent
{
    protected override Element render()
    {
        string[] files = [Path.Combine(nameof(Components), nameof(HomePageDemoComponent) + ".cs")];

        return new Playground
        {
            Files                 = files.Select(fi => (Path.GetFileName(fi), File.ReadAllText(fi))).ToList(),
            TypeOfTargetComponent = typeof(HomePageDemoComponent)
        };
    }
}