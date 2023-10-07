using ReactWithDotNet;
using static ReactWithDotNet.Mixin;

namespace Preview;

class SampleComponent : Component
{
    protected override Element render()
    {
        return
          // s t a r t 
          new div(BackgroundColor("rgb(255, 255, 255)"), BackgroundImage("None"), BorderBottom("1px solid"), BorderColor("rgb(229, 234, 242)"), BorderImageOutset(0), BorderImageRepeat("Stretch"), BorderImageSlice("100%"), BorderImageSource("None"), BorderImageWidth(1), BorderLeft("1px solid"), BorderRadius(12), BorderRight("1px solid"), BorderTop("1px solid"), BoxSizingBorderBox, BreakInside("Avoid"), Color("rgb(28, 32, 37)"), DisplayBlock, Height(364), MarginBottom(16), Padding(16), Transition("border 0.15s cubic-bezier(0.4, 0, 0.2, 1) 0s, box-shadow 0.15s cubic-bezier(0.4, 0, 0.2, 1) 0s"))
          {
              //new FlexRow(AlignItemsCenter, BoxSizingBorderBox, MarginBottom(8), WebkitBoxAlign("Center"))
              //{
              //    new svg(Focusable("False"), AriaHidden("True"), ViewBox("0 0 24 24"), Data("testid", "AcUnitIcon"), BoxSizingBorderBox, Color("rgb(0, 127, 255)"), DisplayBlock, Fill("rgb(0, 127, 255)"), FlexShrink(0), FontSize20, Height(20), Transition("fill 0.2s cubic-bezier(0.4, 0, 0.2, 1) 0s"), UserSelect("None"), Width("1em"))
              //    {
              //        new path { d = "M22 11h-4.17l3.24-3.24-1.41-1.42L15 11h-2V9l4.66-4.66-1.42-1.41L13 6.17V2h-2v4.17L7.76 2.93 6.34 4.34 11 9v2H9L4.34 6.34 2.93 7.76 6.17 11H2v2h4.17l-3.24 3.24 1.41 1.42L9 13h2v2l-4.66 4.66 1.42 1.41L11 17.83V22h2v-4.17l3.24 3.24 1.42-1.41L13 15v-2h2l4.66 4.66 1.41-1.42L17.83 13H22z", boxSizing = "border-box" }
              //    },
              //    new h3(BoxSizingBorderBox, Color("rgb(28, 32, 37)"), FontFamily("'IBM Plex Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'"), FontSize14, FontWeight700, LetterSpacingNormal, LineHeight21, Margin(0px 0px 0px 8), ScrollMarginTop(92))
              //    {
              //        "Perpetual license model"
              //    }
              //},
              //new div(BoxSizingBorderBox, Color("rgb(67, 77, 91)"), FontFamily("'IBM Plex Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'"), FontSize14, FontWeight400, LetterSpacingNormal, LineHeight21, Margin(0), ScrollMarginTop(92))
              //{
              //    "The Perpetual license model offers the right to keep using your licensed versions forever in production and development. It comes with 12 months of maintenance (free updates &amp; support).",
              //    br,
              //    br,
              //    "Upon expiration, you can renew your maintenance plan with a discount that depends on when you renew:",
              //    new ul(BoxSizingBorderBox)
              //    {
              //        new li(BoxSizingBorderBox)
              //        {
              //            "before the support expires: 50% discount"
              //        },
              //        new li(BoxSizingBorderBox)
              //        {
              //            "up to 60 days after the support has expired: 35% discount"
              //        },
              //        new li(BoxSizingBorderBox)
              //        {
              //            "more than 60 days after the support has expired: 15% discount"
              //        }
              //    }
              //}
          }
        // e n d
        ;
    }
}
