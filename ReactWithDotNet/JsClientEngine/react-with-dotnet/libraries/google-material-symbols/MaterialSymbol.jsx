// MaterialSymbol.jsx
import React from "react";

export default class MaterialSymbol extends React.PureComponent {
  static defaultProps = {
    styleVariant: "outlined",
    size: 24,
    color: "currentColor",
    className: "",
    fill: undefined,
    weight: undefined,
    grade: undefined,
    opticalSize: undefined,
  };

  sizeToCss(size) {
    return typeof size === "number" ? `${size}px` : size;
  }

  buildStyle() {
    const { size, color, fill, weight, grade, opticalSize } = this.props;
    const style = {
      fontSize: this.sizeToCss(size),
      lineHeight: 1,
      display: "inline-block",
      color,
      verticalAlign: "middle",
    };

    const axes = [];
    if (fill !== undefined) axes.push(`"FILL" ${Number(fill)}`);
    if (weight !== undefined) axes.push(`"wght" ${Number(weight)}`);
    if (grade !== undefined) axes.push(`"GRAD" ${Number(grade)}`);
    if (opticalSize !== undefined) axes.push(`"opsz" ${Number(opticalSize)}`);
    if (axes.length) style.fontVariationSettings = axes.join(", ");
    return style;
  }

  getClassName() {
    const { styleVariant, className } = this.props;
    return `material-symbols-${styleVariant} ${className}`.trim();
  }

  render() {
    const { name, ariaLabel } = this.props;
    return (
      <span
        role={ariaLabel ? "img" : undefined}
        aria-label={ariaLabel}
        className={this.getClassName()}
        style={this.buildStyle()}
      >
        {name}
      </span>
    );
  }
}


function RegisterGlobalStyles()
{
    ReactWithDotNet.TryLoadCssByHref("https://fonts.googleapis.com/icon?family=Material+Icons");
    ReactWithDotNet.TryLoadCssByHref("https://fonts.googleapis.com/icon?family=Material+Icons+Outlined");
    ReactWithDotNet.TryLoadCssByHref("https://fonts.googleapis.com/icon?family=Material+Icons+Round");
    ReactWithDotNet.TryLoadCssByHref("https://fonts.googleapis.com/icon?family=Material+Icons+Sharp");
    ReactWithDotNet.TryLoadCssByHref("https://fonts.googleapis.com/icon?family=Material+Icons+Two+Tone");
}


var isFirstLoad = false;

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.BeforeAnyThirdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent =>
{
    if (isFirstLoad)
    {
        return;
    }

    if (dotNetFullClassNameOf3rdPartyComponent === 'ReactWithDotNet.ThirdPartyLibraries.GoogleMaterialSymbols.MaterialSymbol' )
    {
        isFirstLoad = true;

        RegisterGlobalStyles();
        RegisterComponents();
    }
});