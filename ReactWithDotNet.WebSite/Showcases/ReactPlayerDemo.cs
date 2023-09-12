using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.ThirdPartyLibraries.React_Player;

namespace ReactWithDotNet.WebSite.Showcases
{
    public class ReactPlayerDemo : ReactPureComponent
    {
        protected override Element render()
        {
            return new div
            {
                style =
                {
                    width = "400px",
                    height = "400px"
                },
                children =
                {
                    new ReactPlayer
                    {
                        url    = "https://www.youtube.com/watch?v=pU1vlTtvRmQ",
                        width  ="100%",
                        height ="100%",
                        volume = 0,
                        controls = true,
                        playsinline = true,
                        config =
                        {
                            youtube=new
                            {
                                playerVars = new {showinfo = 1}
                            }
                        }
                    }
                }
            };
        }
    }
}
