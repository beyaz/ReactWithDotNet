namespace ReactWithDotNet;

partial class Mixin
{
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
    ///     style.wordBreak = <paramref name="value" />
    /// </summary>
    public static StyleModifier WordBreak(string value) => new(style => style.wordBreak = value);
}