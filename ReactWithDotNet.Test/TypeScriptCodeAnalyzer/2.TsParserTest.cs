using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactWithDotNet.Exporting;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.TsLexer;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

[TestClass]
public class TsParserTests
{
    [TestMethod]
    public void FullMatchTest()
    {
        var tokens = ParseTokens("Partial<    AlertClasses >  ;", 0).tokens;

        tokens.FullMatch("  Partial<    AlertClasses>     ;").Should().BeTrue();
        tokens.FullMatch("  Partial<AlertClasses").Should().BeFalse();
    }
    
    [TestMethod]
    public void IsEqualsTest()
    {
        var tokensA = ParseTokens("Partial<    AlertClasses >  ;", 0).tokens;
        var tokensB = ParseTokens(" Partial  <    AlertClasses >  ;", 0).tokens;
        IsEquals(tokensA, 0, tokensA.Count - 1, tokensB, 0, tokensB.Count - 1).Should().BeTrue();
        
        
        tokensA = ParseTokens("<>Partial< ", 0).tokens;
        tokensB = ParseTokens(" Partial  <", 0).tokens;
        IsEquals(tokensA, 2, tokensA.Count - 1, tokensB, 0, tokensB.Count - 1).Should().BeTrue();
    }
    
    [TestMethod]
    public void ParseTypeReference_0()
    {
        var tokens = ParseTokens("React.ReactNode", 0).tokens;
        
        var (hasRead, tsTypeReference, newIndex) = Ast.TryReadTypeReference(tokens, 0);

        hasRead.Should().BeTrue();
        tsTypeReference.Name.Should().Be("React.ReactNode");
    }
    
    [TestMethod]
    public void ParseTypeReference()
    {
        var tokens = ParseTokens("Partial<AlertClasses>;", 0).tokens;
        
        var (hasRead, tsTypeReference, newIndex) = Ast.TryReadTypeReference(tokens, 0);

        hasRead.Should().BeTrue();
        tsTypeReference.Name.Should().Be("Partial");
        newIndex.Should().Be(4);
    }


    [TestMethod]
    public void __typeReference__parsing__1()
    {
        var tokens = ParseTokens(" abc.def", 0).tokens;

        var typeReference = Ast.TryReadOnlyOneTypeReference(tokens, 0).tsTypeReference;

        typeReference.Name.Should().Be("abc.def");
        typeReference.IsSimpleNamedType.Should().BeTrue();
    }

    [TestMethod]
    public void __typeReference__parsing__2()
    {
        var tokens = ParseTokens(" React.ReactNode |  undefined; ", 0).tokens;

        var typeReference = Ast.TryReadOnlyOneTypeReference(tokens, 0).tsTypeReference;

        typeReference.Name.Should().Be("React.ReactNode");
        typeReference.IsSimpleNamedType.Should().BeTrue();
    }

    [TestMethod]
    public void __typeReference__parsing__3()
    {
        var tokens = ParseTokens(" React.ReactNode |  undefined | 'fixed'; ", 0).tokens;

        var typeReference = Ast.TryReadUnionTypeReference(tokens, 0).tsTypeReference;

        typeReference.UnionTypes[0].Name.Should().Be("React.ReactNode");
        typeReference.UnionTypes[1].Name.Should().Be("undefined");
        typeReference.UnionTypes[2].StringValue.Should().Be("fixed");
        typeReference.IsUnionType.Should().BeTrue();
    }

    [TestMethod]
    public void __typeReference__parsing__4()
    {
        var tokens = ParseTokens(" YYY<React.ReactNode |  undefined | 'fixed'>; ", 0).tokens;

        var typeReference = Ast.TryReadTypeReference(tokens, 0).tsTypeReference;

        typeReference.IsGeneric.Should().BeTrue();
        
        typeReference.GenericArguments[0].Name.Should().Be("React.ReactNode");
        typeReference.GenericArguments[1].Name.Should().Be("undefined");
        typeReference.GenericArguments[2].StringValue.Should().Be("fixed");
        typeReference.Name.Should().Be("YYY");
    }


    [TestMethod]
    public void __function_parsing_1()
    {
        var tokens = ParseTokens("   (event: React.SyntheticEvent) => void", 0).tokens;

         var (parameters, _) = Ast.TryReadFunctionParameters(tokens, 0).Value;
         
         parameters.Count.Should().Be(1);
    }

    


    [TestMethod]
    public void __3__()
    {
        var tokens = ParseTokens(@"
/**
   * The components used for each slot inside.
   *
   * This prop is an alias for the `components` prop, which will be deprecated in the future.
   *
   * @default {}
   */
  slots?: {
    popper?: React.ElementType<PopperProps>;
    transition?: React.ElementType;
    tooltip?: React.ElementType;
    arrow?: React.ElementType;
  };
  /**
   * The system prop that allows defining system overrides as well as additional CSS styles.
   */
  sx?: SxProps<Theme>;
  /**
   * Tooltip title. Zero-length titles string, undefined, null and false are never displayed.
   */
  title: React.ReactNode;
  /**
   * The component used for the transition.
   * [Follow this guide](/material-ui/transitions/#transitioncomponent-prop) to learn more about the requirements for this component.
   * @default Grow
   */
  TransitionComponent?: React.JSXElementConstructor<
    TransitionProps & { children: React.ReactElement<any, any> }
  >;
  /**
   * Props applied to the transition element.
   * By default, the element is based on this [`Transition`](http://reactcommunity.org/react-transition-group/transition/) component.
   */
  TransitionProps?: TransitionProps;


", 0).tokens;

        var memberTokens = Ast.ParseToMemberTokens(tokens, 0,tokens.Count).Value;

        memberTokens.Count.Should().Be(5);

        memberTokens.All(x => Exporter.ParseMemberTokens(x).Success).Should().BeTrue();

    }
}