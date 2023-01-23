namespace ReactWithDotNet;

partial class Mixin
{
    public static StyleModifier AlignItemsBaseline => new(style => style.alignItems = "baseline");

    /// <summary>
    ///     <para>style.alignItems = "center"</para>
    /// </summary>
    public static StyleModifier AlignItemsCenter => new(style => style.alignItems = "center");

    public static StyleModifier AlignItemsFlexEnd => new(style => style.alignItems = "flex-end");
    public static StyleModifier AlignItemsFlexStart => new(style => style.alignItems = "flex-start");
    public static StyleModifier AlignItemsStretch => new(style => style.alignItems = "stretch");

    #region AlignContent
    /// <summary>
    ///     <para>style.alignContent = "center"</para>
    /// </summary>
    public static StyleModifier AlignContentCenter => new(style => style.alignContent = "center");

    /// <summary>
    ///     <para>style.alignContent = "flex-start"</para>
    /// </summary>
    public static StyleModifier AlignContentFlexStart => new(style => style.alignContent = "flex-start");

    /// <summary>
    ///     <para>style.alignContent = "flex-end"</para>
    /// </summary>
    public static StyleModifier AlignContentFlexEnd => new(style => style.alignContent = "flex-end");

    /// <summary>
    ///     <para>style.alignContent = "stretch"</para>
    /// </summary>
    public static StyleModifier AlignContentStretch => new(style => style.alignContent = "stretch");

    /// <summary>
    ///     <para>style.alignContent = "space-around"</para>
    /// </summary>
    public static StyleModifier AlignContentSpaceAround => new(style => style.alignContent = "space-around");

    /// <summary>
    ///     <para>style.alignContent = "space-between"</para>
    /// </summary>
    public static StyleModifier AlignContentSpaceBetween => new(style => style.alignContent = "space-between");
    #endregion
}