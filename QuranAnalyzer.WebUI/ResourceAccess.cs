using System.Collections.Generic;

namespace QuranAnalyzer.WebUI;

public static class ResourceAccess
{
    public  static  readonly CommonDataModel CommonData = ResourceHelper.Read<CommonDataModel>("CommonData.json");

    public static readonly IReadOnlyList<FactModel> Facts = ResourceHelper.Read<FactModel[]>("Facts.json");
}