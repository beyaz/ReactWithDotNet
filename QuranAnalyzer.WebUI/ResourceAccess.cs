using System;
using System.Collections.Generic;

namespace QuranAnalyzer.WebUI;

public static class ResourceAccess
{
    public  static  readonly CommonDataModel CommonData = ResourceHelper.Read<CommonDataModel>("CommonData.json");

    public static readonly IReadOnlyList<FactModel> Facts = ResourceHelper.Read<FactModel[]>("Facts.json");

    public static readonly MainPageModel MainPage = ResourceHelper.Read<MainPageModel>("MainPage.yaml");
    
}




[Serializable]
public sealed class MainPageModel
{
    public string Title { get; set; }
    public string Content { get; set; }
}