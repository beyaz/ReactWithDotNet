using System.IO;

namespace ReactWithDotNet.WebSite.Components;

class MainPageContentSample : PureComponent
{
    protected override Element render()
    {

        return new Playground
        {
            Files                 = Directory.GetFiles(nameof(_1_HelloWorld)).Select(fi => (Path.GetFileName(fi), File.ReadAllText(fi))).ToList(),
            TypeOfTargetComponent = typeof(ReactWithDotNet.WebSite._1_HelloWorld.HomePageDemoComponent)
        };
    }
}