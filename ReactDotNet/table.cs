namespace ReactDotNet;

public class table : HtmlElement
{

}
public class tbody : HtmlElement
{

}
public class tr : HtmlElement
{
    [React]
    public int? rowspan { get; set; }

    [React]
    public int? colspan { get; set; }
}

public class th : HtmlElement
{
    [React]
    public int? rowspan { get; set; }

    [React]
    public int? colspan { get; set; }
}

public class td : HtmlElement
{
    [React]
    public int? rowspan { get; set; }

    [React]
    public int? colspan { get; set; }
}