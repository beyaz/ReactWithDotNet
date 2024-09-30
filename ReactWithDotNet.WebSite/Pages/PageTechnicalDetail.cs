namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageTechnicalDetail : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        {
            new h1{"Technical Details"},
            SpaceY(16),
            new p
            {
                "How is it working? How to connect React and c# language?"
            },

            new p
            {
                "ReactWithDotNet is working on the .net core. Creates ReactNode hierarchy in c# language then serialize to client.",
                br,
                "Our client engine recalculates React nodes from incoming c# generated nodes",
                br,
                "If any react event occurs then serialize only sub react nodes values to server"
            },
            
            SpaceY(50),
            new img(WidthFull, ObjectFitContain)
            {
                Src(Asset("TechnicalDetail1.drawio.png"))
            },
            
            SpaceY(50),
            new p
            {
                "Lets see what is incoming from server to client"
            },
            
            SpaceY(20),
            new img(WidthFull, ObjectFitContain)
            {
                Src(Asset("TechnicalDetail.I.drawio.png"))
            },
            
            SpaceY(50),
            new p
            {
                "Lets see what is outgoing from client to server"
            },
            
            SpaceY(20),
            new img(WidthFull, ObjectFitContain)
            {
                Src(Asset("TechnicalDetail.O.drawio.png"))
            },
            SpaceY(20),
            new p
            {
                "As you can see at images, main idea is server driven UI, just like react server components.",
                br,
                "Main differences server is not nodejs, language is not typescript or js.",
                br,
                "Server is .NetCore server and language is c# language.",
                br,
                "As a result; power of two technology is combined. " +
                "We have many benefits of c# language and serverside approach and react components flexibility."
            },
            SpaceY(50)
        };
    }
}