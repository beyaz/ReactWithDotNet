namespace ReactWithDotNet;

partial class Mixin
{
    /// <summary>
    ///     style.whiteSpace = break-spaces
    /// </summary>
    public static StyleModifier WhiteSpaceBreakSpaces => WhiteSpace("break-spaces");

    /// <summary>
    ///     style.whiteSpace = normal
    /// </summary>
    public static StyleModifier WhiteSpaceNormal => WhiteSpace("normal");

    /// <summary>
    ///     style.whiteSpace = nowrap
    /// </summary>
    public static StyleModifier WhiteSpaceNoWrap => WhiteSpace("nowrap");

    /// <summary>
    ///     style.whiteSpace = pre
    /// </summary>
    public static StyleModifier WhiteSpacePre => WhiteSpace("pre");

    /// <summary>
    ///     style.whiteSpace = pre-line
    /// </summary>
    public static StyleModifier WhiteSpacePreLine => WhiteSpace("pre-line");

    /// <summary>
    ///     style.whiteSpace = pre-wrap
    /// </summary>
    public static StyleModifier WhiteSpacePreWrap => WhiteSpace("pre-wrap");

    /// <summary>
    ///     <para>style.wordBreak = "break-all"</para>
    /// </summary>
    public static StyleModifier WordBreakAll => WordBreak("break-all");

    /// <summary>
    ///     <para>style.wordBreak = "keep-all"</para>
    /// </summary>
    public static StyleModifier WordBreakKeepAll => WordBreak("keep-all");

    /// <summary>
    ///     <para>style.wordBreak = "normal"</para>
    /// </summary>
    public static StyleModifier WordBreakNormal => WordBreak("normal");

    /// <summary>
    ///     <para>style.wordBreak = "break-word"</para>
    /// </summary>
    public static StyleModifier WordBreakWord => WordBreak("break-word");

    /// <summary>
    ///     style.whiteSpace = <paramref name="value" />
    /// </summary>
    public static StyleModifier WhiteSpace(string value) => new(style => style.whiteSpace = value);

    /// <summary>
    ///     style.wordBreak = <paramref name="value" />
    /// </summary>
    public static StyleModifier WordBreak(string value) => new(style => style.wordBreak = value);
}