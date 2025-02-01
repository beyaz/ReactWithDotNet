using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Exporting.ExporterForMui;

[TestClass]
public class ExporterTest
{
    static string GetTsCode(string className)
    {
        // main directory: https://github.com/mui/material-ui/blob/master/packages/mui-material/src/
        
        var rawUrlInGithub = $"https://raw.githubusercontent.com/mui/material-ui/refs/heads/master/packages/mui-material/src/{className}/{className}.d.ts";

        if (className == "SwitchBase")
        {
            rawUrlInGithub = $"https://raw.githubusercontent.com/mui/material-ui/refs/heads/master/packages/mui-material/src/internal/{className}.d.ts";
        }

        return new HttpClient().GetStringAsync(rawUrlInGithub).GetAwaiter().GetResult();
    }
    [TestMethod]
    public void Paper()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Paper)),
            StartFrom        = "props: P & {",
            ClassName        = "Paper",
            ClassModifier    = " ",
            SkipMembers      = ["children"],
            ExtraProps       = ["string component"]
        });
    }

    [TestMethod]
    public void Card()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Card)),
            StartFrom        = "DistributiveOmit<PaperProps, 'classes'> & {",
            ClassName        = "Card",
            SkipMembers      = ["children", "classes", "sx"],
            BaseClassName    = "Paper"
        });
    }

    [TestMethod]
    public void Tooltip()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Tooltip)),
            StartFrom        = "  TooltipSlotsAndSlotProps {",
            ClassName        = "Tooltip",
            SkipMembers      = ["children", "TransitionComponent", "TransitionProps", "PopperComponent"],
            IsContainer      = true
        });
    }
    [TestMethod]
    public void Accordion()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Accordion)),
            StartFrom        = "AdditionalProps & {",
            ClassName        = "Accordion",
            SkipMembers      = ["children","TransitionProps"],
            IsContainer      = true
        });
    }

    [TestMethod]
    public void CardMedia()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CardMedia)),
            StartFrom        = "props: P & {",
            ClassName        = "CardMedia",
            SkipMembers      = ["children"],
            ExtraProps       = ["string title"]
        });
    }


    [TestMethod]
    public void Divider()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Divider)),
            StartFrom        = "props: P & {",
            ClassName        = "Divider",
            SkipMembers      = ["children"]
        });
    }

    [TestMethod]
    public void Typography()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Typography)),
            StartFrom        = "SystemProps<Theme> & {",
            ClassName        = "Typography",
            SkipMembers      = ["children"],
            ExtraProps       = ["string color", "string component"]
        });
    }

    [TestMethod]
    public void CardContent()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CardContent)),
            StartFrom        = "props: P & {",
            ClassName        = "CardContent",
            SkipMembers      = ["children"]
        });
    }

    [TestMethod]
    public void CardActions()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CardActions)),
            StartFrom        = "<HTMLDivElement>> {",
            ClassName        = "CardActions",
            SkipMembers      = ["children"]
        });
    }


    [TestMethod]
    public void TextField()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(TextField)),
            StartFrom        = "> {",
            ClassName        = "TextField",
            SkipMembers      = ["children", "inputRef", "onClick"],
            ClassModifier    = "partial"
        });
    }

    [TestMethod]
    public void ButtonBase()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(ButtonBase)),
            StartFrom        = "props: P & {",
            ClassName        = "ButtonBase",
            SkipMembers      = ["children", "action", "touchRippleRef", "LinkComponent", "onFocusVisible", "tabIndex"],
            ClassModifier    = string.Empty
            
        });
    }

    [TestMethod]
    public void IconButton()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(IconButton)),
            StartFrom        = "props: P & {",
            ClassName        = "IconButton",
            SkipMembers      = ["children", "classes", "disabled","sx"],
            BaseClassName    = nameof(ButtonBase),
            ExtraProps       = ["string type"]

        });
    }


    [TestMethod]
    public void SwitchBase()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(SwitchBase)),
            StartFrom        = "SwitchBaseSlotsAndSlotProps {",
            ClassName        = "SwitchBase",
            SkipMembers      = ["-", "classes", "disabled", "disableRipple"],
            ClassModifier    = string.Empty,
            BaseClassName    = "ButtonBase"

        });
    }

    [TestMethod]
    public void Switch()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Switch)),
            StartFrom        = "> {",
            ClassName        = "Switch",
            SkipMembers      = ["children", "inputRef","checkedIcon", "disabled", "icon", "sx","value", "classes"],
            BaseClassName    = "SwitchBase",
            ClassModifier    = "partial"
        });
    }
    
    [TestMethod]
    public void CircularProgress()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(CircularProgress)),
            StartFrom        = "<HTMLSpanElement>, 'children'> {",
            ClassName        = "CircularProgress",
            SkipMembers      = ["children", "classes", "sx"]
        });
    }
    
    [TestMethod]
    public void Autocomplete()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Autocomplete)),
            StartFrom        = " ChipComponent> {",
            ClassName        = "Autocomplete",
            SkipMembers      = ["children", "classes"],
            ClassModifier    = "partial"
        });
    }
    
    [TestMethod]
    public void Slider()
    {
        Exporter.ExportToCSharpFile(new ExportInput
        {
            DefinitionTsCode = GetTsCode(nameof(Slider)),
            StartFrom        = " | 'children'> {",
            ClassName        = "Slider",
            SkipMembers      = ["children", "classes"],
            ClassModifier    = "partial"
        });
    }
    
}