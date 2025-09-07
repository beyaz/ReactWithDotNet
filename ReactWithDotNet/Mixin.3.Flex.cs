﻿namespace ReactWithDotNet;

partial class Mixin
{
    public static StyleModifier AlignItemsBaseline => new(style => style.alignItems = "baseline");

    /// <summary>
    ///     <para>style.alignItems = "normal"</para>
    /// </summary>
    public static StyleModifier AlignItemsNormal => new(style => style.alignItems = "normal");
    
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

    #region AlignSelf

    

    /// <summary>
    ///     <para>style.alignSelf = "auto"</para>
    /// </summary>
    public static StyleModifier AlignSelfaAuto => AlignSelf("auto");

    /// <summary>
    ///     <para>style.alignSelf = "stretch"</para>
    /// </summary>
    public static StyleModifier AlignSelfStretch => AlignSelf("stretch");

    /// <summary>
    ///     <para>style.alignSelf = "center"</para>
    /// </summary>
    public static StyleModifier AlignSelfCenter => AlignSelf("center");

    /// <summary>
    ///     <para>style.alignSelf = "flex-start"</para>
    /// </summary>
    public static StyleModifier AlignSelfFlexStart => AlignSelf("flex-start");

    /// <summary>
    ///     <para>style.alignSelf = "flex-end"</para>
    /// </summary>
    public static StyleModifier AlignSelfFlexEnd => AlignSelf("flex-end");

    /// <summary>
    ///     <para>style.alignSelf = "baseline"</para>
    /// </summary>
    public static StyleModifier AlignSelfBaseline => AlignSelf("baseline");

    /// <summary>
    ///     <para>style.alignSelf = "initial"</para>
    /// </summary>
    public static StyleModifier AlignSelfInitial => AlignSelf("initial");

    /// <summary>
    ///     <para>style.alignSelf = "inherit"</para>
    /// </summary>
    public static StyleModifier AlignSelfInherit => AlignSelf("inherit");

    #endregion
    
    
    public static StyleModifier GridRow(int rowNumber) => new(style => style.gridRow = rowNumber.ToString());
    public static StyleModifier GridColumn(int columnNumber) => new(style => style.gridColumn = columnNumber.ToString());
    
}