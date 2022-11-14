namespace QuranAnalyzer.WebUI.Components;

// Taken from https://www.w3schools.com/howto/tryit.asp?filename=tryhow_css_loader
public class LoadingIcon : ReactComponent
{
    protected override Element render()
    {
        return new div
        {
            new style
            {
                text = @"

.loader {
  border: 1px solid #f3f3f3;
  border-radius: 50%;
  border-top: 1px solid #A08139;

  -webkit-animation: spin 1s linear infinite; /* Safari */
  animation: spin 1s linear infinite;
}

/* Safari */
@-webkit-keyframes spin {
  0% { -webkit-transform: rotate(0deg); }
  100% { -webkit-transform: rotate(360deg); }
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

"
            },

            new div { className = "loader", style = { WidthHeight("100%") } }
        };
    }
}
