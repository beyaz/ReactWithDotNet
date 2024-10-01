using System.IO;

namespace ReactWithDotNet.WebSite.Components;

class MainPageContentSample : Component
{
    protected override Element render()
    {
        string[] files = [Path.Combine(nameof(Components), nameof(HomePageDemoComponent) + ".cs")];

        return new Playground
        {
            Height = 300,
            
            TypeOfTargetComponent = typeof(HomePageDemoComponent),

            Files = files.Select(fi => (Path.GetFileName(fi), File.ReadAllText(fi))).ToList()
        };
    }
}