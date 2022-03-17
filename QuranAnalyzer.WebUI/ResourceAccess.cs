using System;
using System.Collections.Generic;

namespace QuranAnalyzer.WebUI;

public static class ResourceAccess
{
    public  static  readonly CommonDataModel CommonData = ResourceHelper.Read<CommonDataModel>("CommonData.json");

    public static readonly IReadOnlyList<FactModel> Facts = ResourceHelper.Read<FactModel[]>("Facts.json");

    public static readonly MainPageModel MainPage = ResourceHelper.ReadPage<MainPageModel>(nameof(MainPage));

    public static readonly QuestionAnswerPageModel QuestionAnswerPage = ResourceHelper.ReadPage<QuestionAnswerPageModel>(nameof(QuestionAnswerPage) );

}




[Serializable]
public sealed class MainPageModel
{
    public string Title { get; set; }
    public string Content { get; set; }
}

[Serializable]
public sealed class QuestionAnswerPageModel 
{
    public string Title { get; set; }
    public string Summary { get; set; }

    public QuestionAnswerPair[] QuestionsAndAnswers { get; set; }

}

[Serializable]
public sealed class QuestionAnswerPair
{
    public string Question { get; set; }
    public string Answer { get; set; }

}