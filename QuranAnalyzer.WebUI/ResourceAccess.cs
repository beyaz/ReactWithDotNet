using System;
using System.Collections.Generic;
using QuranAnalyzer.WebUI.Pages.FactPage;

namespace QuranAnalyzer.WebUI;

public static class ResourceAccess
{

    public static readonly IReadOnlyList<FactModel> Facts = ResourceHelper.Read<FactModel[]>("FactPage.Facts.json");



}




