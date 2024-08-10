using System.IO;

namespace ReactWithDotNet.WebSite.Components;

class MainPageContentSample : PureComponent
{
    protected override Element render()
    {
        string[] files = [];
        try
        {
            files = Directory.GetFiles(nameof(_1_HelloWorld));
        }
        catch (Exception)
        {
            // ignored
        }

        return new Playground
        {
            Files                 = files.Select(fi => (Path.GetFileName(fi), File.ReadAllText(fi))).ToList(),
            TypeOfTargetComponent = typeof(_1_HelloWorld.HomePageDemoComponent)
        };
    }
}