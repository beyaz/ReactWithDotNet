using ReactWithDotNet;
using static ReactWithDotNet.Mixin;

namespace Preview;

class SampleComponent: Component
{
  protected override Element render()
  {
    return 
      // s t a r t 
      new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", paddingBottom = "96px", paddingTop = "96px" })
      {
          new h1
          {
              text = "A simple CMS for your Next.js app",
              // disabled = "",
              contenteditable = "false",
              style =
              {
                  border = "0px solid rgb(229, 231, 235)",
                  boxSizing = "border-box",
                  color = "rgb(55, 65, 81)",
                  fontSize = "60px",
                  fontWeight = "800",
                  lineHeight = "70px",
                  margin = "24px 0px 0px",
                  marginTop = "24px",
                  textAlign = "left"
              }
          },
          new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", marginTop = "12px" })
          {
              new p(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(55, 65, 81)", fontSize = "20px", lineHeight = "28px", margin = "20px 0px 0px", marginTop = "20px", textAlign = "left" })
              {
                  "Suncel is a powerful and versatile content platform, with a simple ",
                  new strong(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", fontWeight = "700" })
                  {
                      "visual builder"
                  },
                  " for marketers and publishers. It provides an SEO-optimized headless CMS based on Next.js."
              }
          },
          new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "flex", gap = "20px", justifyContent = "flex-start", marginTop = "48px" })
          {
              new span(new Style { alignItems = "center", border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(219, 39, 119)", display = "flex", flexDirection = "row", justifyContent = "center" })
              {
                  new svg(Data("v-1923673d", ""), Width("22"), Height("17"), ViewBox("0 0 33 26"), Fill("currentColor"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "block", verticalAlign = "middle" })
                  {
                      new path
                      {
                          d = "M32.1452 1.32151C32.9745 2.05115 33.0552 3.3149 32.3256 4.14418L13.8485 25.1442C13.4696 25.5749 12.9239 25.8221 12.3502 25.823C11.7764 25.8239 11.23 25.5784 10.8496 25.1489L1.32669 14.394C0.594444 13.567 0.671236 12.303 1.49821 11.5708C2.32519 10.8385 3.58918 10.9153 4.32143 11.7423L12.3423 20.8007L29.3225 1.5019C30.0522 0.67263 31.3159 0.591865 32.1452 1.32151Z",
                          style =
                          {
                              border = "0px solid rgb(229, 231, 235)",
                              boxSizing = "border-box",
                              clipRule = "evenodd",
                              fillRule = "evenodd"
                          }
                      }
                  },
                  new p
                  {
                      text = "Free plan",
                      // disabled = "",
                      contenteditable = "false",
                      style =
                      {
                          border = "0px solid rgb(229, 231, 235)",
                          boxSizing = "border-box",
                          color = "rgb(17, 24, 39)",
                          fontSize = "16px",
                          lineHeight = "24px",
                          margin = "0px 0px 0px 4px",
                          marginLeft = "4px",
                          textAlign = "center"
                      }
                  }
              },
              new span(new Style { alignItems = "center", border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(219, 39, 119)", display = "flex", flexDirection = "row", justifyContent = "center" })
              {
                  new svg(Data("v-1923673d", ""), Width("22"), Height("17"), ViewBox("0 0 33 26"), Fill("currentColor"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "block", verticalAlign = "middle" })
                  {
                      new path
                      {
                          d = "M32.1452 1.32151C32.9745 2.05115 33.0552 3.3149 32.3256 4.14418L13.8485 25.1442C13.4696 25.5749 12.9239 25.8221 12.3502 25.823C11.7764 25.8239 11.23 25.5784 10.8496 25.1489L1.32669 14.394C0.594444 13.567 0.671236 12.303 1.49821 11.5708C2.32519 10.8385 3.58918 10.9153 4.32143 11.7423L12.3423 20.8007L29.3225 1.5019C30.0522 0.67263 31.3159 0.591865 32.1452 1.32151Z",
                          style =
                          {
                              border = "0px solid rgb(229, 231, 235)",
                              boxSizing = "border-box",
                              clipRule = "evenodd",
                              fillRule = "evenodd"
                          }
                      }
                  },
                  new p
                  {
                      text = "No credit card required",
                      // disabled = "",
                      contenteditable = "false",
                      style =
                      {
                          border = "0px solid rgb(229, 231, 235)",
                          boxSizing = "border-box",
                          color = "rgb(17, 24, 39)",
                          fontSize = "16px",
                          lineHeight = "24px",
                          margin = "0px 0px 0px 4px",
                          marginLeft = "4px",
                          textAlign = "center"
                      }
                  }
              },
              new span(new Style { alignItems = "center", border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(219, 39, 119)", display = "flex", flexDirection = "row", justifyContent = "center" })
              {
                  new svg(Data("v-1923673d", ""), Width("22"), Height("17"), ViewBox("0 0 33 26"), Fill("currentColor"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "block", verticalAlign = "middle" })
                  {
                      new path
                      {
                          d = "M32.1452 1.32151C32.9745 2.05115 33.0552 3.3149 32.3256 4.14418L13.8485 25.1442C13.4696 25.5749 12.9239 25.8221 12.3502 25.823C11.7764 25.8239 11.23 25.5784 10.8496 25.1489L1.32669 14.394C0.594444 13.567 0.671236 12.303 1.49821 11.5708C2.32519 10.8385 3.58918 10.9153 4.32143 11.7423L12.3423 20.8007L29.3225 1.5019C30.0522 0.67263 31.3159 0.591865 32.1452 1.32151Z",
                          style =
                          {
                              border = "0px solid rgb(229, 231, 235)",
                              boxSizing = "border-box",
                              clipRule = "evenodd",
                              fillRule = "evenodd"
                          }
                      }
                  },
                  new p
                  {
                      text = "Setup in minutes",
                      // disabled = "",
                      contenteditable = "false",
                      style =
                      {
                          border = "0px solid rgb(229, 231, 235)",
                          boxSizing = "border-box",
                          color = "rgb(17, 24, 39)",
                          fontSize = "16px",
                          lineHeight = "24px",
                          margin = "0px 0px 0px 4px",
                          marginLeft = "4px",
                          textAlign = "center"
                      }
                  }
              }
          },
          new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", marginTop = "48px" })
          {
              new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", marginLeft = "0px", marginRight = "0px", maxWidth = "576px" })
              {
                  new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "flex", justifyContent = "flex-start" })
                  {
                      new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", marginTop = "0px" })
                      {
                          new a(Href("https://app.suncel.io/signup"), TargetBlank, Rel("noreferrer"), new Style { backgroundColor = "rgb(203, 3, 111)", border = "0px solid rgb(229, 231, 235)", borderRadius = "6px", boxShadow = "rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0) 0px 0px 0px 0px, rgba(0, 0, 0, 0.1) 0px 1px 3px 0px, rgba(0, 0, 0, 0.1) 0px 1px 2px -1px", boxSizing = "border-box", color = "rgb(255, 255, 255)", display = "block", fontWeight = "500", paddingBottom = "12px", paddingLeft = "16px", paddingRight = "16px", paddingTop = "12px", textAlign = "center", textDecoration = "none solid rgb(255, 255, 255)", width = "100%" })
                          {
                              new span
                              {
                                  text = "Get started",
                                  // disabled = "",
                                  contenteditable = "false",
                                  style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" }
                              }
                          }
                      },
                      new FlexRowCentered(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(17, 24, 39)", cursor = "pointer", marginLeft = "32px", marginTop = "0px" })
                      {
                          new svg(Data("v-1923673d", ""), Width("40"), Height("28"), ViewBox("0 0 60 42"), Fill("currentColor"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "block", verticalAlign = "middle" })
                          {
                              new path { d = "M-26.7-51.9a7.515,7.515,0,0,0-5.3-5.3c-4.679-1.254-23.442-1.254-23.442-1.254s-18.762,0-23.442,1.254a7.515,7.515,0,0,0-5.3,5.3c-1.254,4.679-1.254,14.442-1.254,14.442s0,9.763,1.254,14.442a7.514,7.514,0,0,0,5.3,5.3c4.679,1.254,23.442,1.254,23.442,1.254s18.762,0,23.442-1.254a7.514,7.514,0,0,0,5.3-5.3C-25.45-27.7-25.45-37.461-25.45-37.461S-25.45-47.224-26.7-51.9ZM-61.45-28.461v-18l15.588,9Z", /* transform = "translate(85.45 58.461)"*/ style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" } }
                          },
                          new p
                          {
                              text = "product video",
                              // disabled = "",
                              contenteditable = "false",
                              style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", marginLeft = "4px" }
                          }
                      }
                  },
                  new p
                  {
                      text = "Publish content faster, improve page speed and SEO ranking.",
                      // disabled = "",
                      contenteditable = "false",
                      style =
                      {
                          border = "0px solid rgb(229, 231, 235)",
                          boxSizing = "border-box",
                          color = "rgb(107, 114, 128)",
                          fontSize = "14px",
                          lineHeight = "20px",
                          margin = "16px 0px 0px",
                          marginTop = "16px",
                          textAlign = "left"
                      }
                  }
              },
              new div(new Style { alignItems = "center", border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", display = "flex", justifyContent = "flex-start", marginTop = "48px" })
              {
                  new a(Href("https://www.capterra.com/p/275259/Suncel/"), TargetBlank, Rel("noopener noreferrer"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(17, 24, 39)", textDecoration = "none solid rgb(17, 24, 39)" })
                  {
                      new img(Data("nimg", "1"))
                      {
                          alt = "capterra 5 stars",
                          loading = "lazy",

                          // decoding = "async",
                          src = "https://assets.suncel.io/61bf5e233c962a862faf209f/7R0uh-capterra-logo.svg",
                          style =
                          {
                              border = "0px solid rgb(229, 231, 235)",
                              boxSizing = "border-box",
                              display = "block",
                              height = "auto",
                              maxWidth = "100%",
                              verticalAlign = "middle"
                          }
                      },
                      new FlexRow(new Style { alignItems = "center", border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", marginLeft = "8px", marginTop = "8px" })
                      {
                          new svg(Stroke("currentColor"), Fill("currentColor"), ViewBox("0 0 1024 1024"), Height("1em"), Width("1em"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(250, 204, 21)", display = "block", fontSize = "20px", lineHeight = "28px", strokeWidth = "0", verticalAlign = "middle" })
                          {
                              new path { d = "M908.1 353.1l-253.9-36.9L540.7 86.1c-3.1-6.3-8.2-11.4-14.5-14.5-15.8-7.8-35-1.3-42.9 14.5L369.8 316.2l-253.9 36.9c-7 1-13.4 4.3-18.3 9.3a32.05 32.05 0 0 0 .6 45.3l183.7 179.1-43.4 252.9a31.95 31.95 0 0 0 46.4 33.7L512 754l227.1 119.4c6.2 3.3 13.4 4.4 20.3 3.2 17.4-3 29.1-19.5 26.1-36.9l-43.4-252.9 183.7-179.1c5-4.9 8.3-11.3 9.3-18.3 2.7-17.5-9.5-33.7-27-36.3z", style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" } }
                          },
                          new svg(Stroke("currentColor"), Fill("currentColor"), ViewBox("0 0 1024 1024"), Height("1em"), Width("1em"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(250, 204, 21)", display = "block", fontSize = "20px", lineHeight = "28px", marginLeft = "8px", marginRight = "0px", strokeWidth = "0", verticalAlign = "middle" })
                          {
                              new path { d = "M908.1 353.1l-253.9-36.9L540.7 86.1c-3.1-6.3-8.2-11.4-14.5-14.5-15.8-7.8-35-1.3-42.9 14.5L369.8 316.2l-253.9 36.9c-7 1-13.4 4.3-18.3 9.3a32.05 32.05 0 0 0 .6 45.3l183.7 179.1-43.4 252.9a31.95 31.95 0 0 0 46.4 33.7L512 754l227.1 119.4c6.2 3.3 13.4 4.4 20.3 3.2 17.4-3 29.1-19.5 26.1-36.9l-43.4-252.9 183.7-179.1c5-4.9 8.3-11.3 9.3-18.3 2.7-17.5-9.5-33.7-27-36.3z", style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" } }
                          },
                          new svg(Stroke("currentColor"), Fill("currentColor"), ViewBox("0 0 1024 1024"), Height("1em"), Width("1em"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(250, 204, 21)", display = "block", fontSize = "20px", lineHeight = "28px", marginLeft = "8px", marginRight = "0px", strokeWidth = "0", verticalAlign = "middle" })
                          {
                              new path { d = "M908.1 353.1l-253.9-36.9L540.7 86.1c-3.1-6.3-8.2-11.4-14.5-14.5-15.8-7.8-35-1.3-42.9 14.5L369.8 316.2l-253.9 36.9c-7 1-13.4 4.3-18.3 9.3a32.05 32.05 0 0 0 .6 45.3l183.7 179.1-43.4 252.9a31.95 31.95 0 0 0 46.4 33.7L512 754l227.1 119.4c6.2 3.3 13.4 4.4 20.3 3.2 17.4-3 29.1-19.5 26.1-36.9l-43.4-252.9 183.7-179.1c5-4.9 8.3-11.3 9.3-18.3 2.7-17.5-9.5-33.7-27-36.3z", style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" } }
                          },
                          new svg(Stroke("currentColor"), Fill("currentColor"), ViewBox("0 0 1024 1024"), Height("1em"), Width("1em"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(250, 204, 21)", display = "block", fontSize = "20px", lineHeight = "28px", marginLeft = "8px", marginRight = "0px", strokeWidth = "0", verticalAlign = "middle" })
                          {
                              new path { d = "M908.1 353.1l-253.9-36.9L540.7 86.1c-3.1-6.3-8.2-11.4-14.5-14.5-15.8-7.8-35-1.3-42.9 14.5L369.8 316.2l-253.9 36.9c-7 1-13.4 4.3-18.3 9.3a32.05 32.05 0 0 0 .6 45.3l183.7 179.1-43.4 252.9a31.95 31.95 0 0 0 46.4 33.7L512 754l227.1 119.4c6.2 3.3 13.4 4.4 20.3 3.2 17.4-3 29.1-19.5 26.1-36.9l-43.4-252.9 183.7-179.1c5-4.9 8.3-11.3 9.3-18.3 2.7-17.5-9.5-33.7-27-36.3z", style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" } }
                          },
                          new svg(Stroke("currentColor"), Fill("currentColor"), ViewBox("0 0 1024 1024"), Height("1em"), Width("1em"), new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(250, 204, 21)", display = "block", fontSize = "20px", lineHeight = "28px", marginLeft = "8px", marginRight = "0px", strokeWidth = "0", verticalAlign = "middle" })
                          {
                              new path { d = "M908.1 353.1l-253.9-36.9L540.7 86.1c-3.1-6.3-8.2-11.4-14.5-14.5-15.8-7.8-35-1.3-42.9 14.5L369.8 316.2l-253.9 36.9c-7 1-13.4 4.3-18.3 9.3a32.05 32.05 0 0 0 .6 45.3l183.7 179.1-43.4 252.9a31.95 31.95 0 0 0 46.4 33.7L512 754l227.1 119.4c6.2 3.3 13.4 4.4 20.3 3.2 17.4-3 29.1-19.5 26.1-36.9l-43.4-252.9 183.7-179.1c5-4.9 8.3-11.3 9.3-18.3 2.7-17.5-9.5-33.7-27-36.3z", style = { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box" } }
                          },
                          new div(new Style { border = "0px solid rgb(229, 231, 235)", boxSizing = "border-box", color = "rgb(55, 65, 81)", fontSize = "18px", fontWeight = "600", lineHeight = "28px", marginLeft = "8px", marginRight = "0px" })
                          {
                              "4.9"
                          }
                      }
                  }
              }
          }
      }
      // e n d
    ;
  }
}
