using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.Test;

[TestClass]
public class TsParserTests
{
    [TestMethod]
    public void A()
    {
        var input = @" /**
     * The system prop that allows defining system overrides as well as additional CSS styles.
     */
    sx?: SxProps<Theme>;

/**
     * The position of the icon relative to the label.
     * @default 'top'
     */
    iconPosition?: 'top' | 'bottom' | 'start' | 'end';

/**
     * Whether to automatically manage layering.
     * @defaultValue true
     */
    autoZIndex?: boolean | undefined;

    /**
     * Base zIndex value to use in layering.
     * @defaultValue 0
     */
    baseZIndex?: number | undefined;


 /**
   * Override or extend the styles applied to the component.
   */
  classes?: Partial<CircularProgressClasses>;
  /**
   * The color of the component.
   * It supports both default and custom theme colors, which can be added as shown in the
   * [palette customization guide](https://mui.com/material-ui/customization/palette/#adding-new-colors).
   * @default 'primary'
   */
  color?: OverridableStringUnion<
    'primary' | 'secondary' | 'error' | 'info' | 'success' | 'warning' | 'inherit',
    CircularProgressPropsColorOverrides
  >;

";


        var cursor = 0;
        
        var properties = ReactWithDotNet.TsLexer.ParseTokens(input, cursor);

        
    }

    
    
}