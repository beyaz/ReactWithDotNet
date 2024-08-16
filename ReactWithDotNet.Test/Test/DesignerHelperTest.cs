using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReactWithDotNet.Tokenizing;

namespace ReactWithDotNet.Test;

[TestClass]
public class DesignerHelperTest
{
    [TestMethod]
    public void _0_()
    {
        const string inputCode = "Margin(1, 2, 3, 4), Margin(4), Padding(22.56), BackgroundWhite,  Background(\"yellow\")";
        const string expectedCode = "Margin(1, 2, 3, 4), Margin(4), Padding(22.56), BackgroundWhite, Background(\"yellow\")";

        Assert(inputCode, expectedCode);
    }
    
    [TestMethod]
    public void _1_()
    {
        const string inputCode = "Opacity(0.5), Focus(DisplayNone),   Border(1, solid, Gray100), Active(FontSize23, TextAlign(\"center\")), Hover(Margin(1, 2, 3, 4), Margin(4), Padding(22.56), BackgroundWhite,  Background(\"yellow\"))";
        const string expectedCode = "Opacity(0.5), Focus(DisplayNone), Border(1, solid, Gray100), Active(FontSize23, TextAlign(\"center\")), Hover(Margin(1, 2, 3, 4), Margin(4), Padding(22.56), BackgroundWhite, Background(\"yellow\"))";

        Assert(inputCode, expectedCode);
    }
    
    [TestMethod]
    public void _3_()
    {
        var node = new DesignerHelper.Node
        {
            Name = "Opacity",
            Parameters =
            [
                new()
                {
                    IsDoubleNode = true,
                    DoubleValue  = 0.7
                }
            ]
        };

        var (methodInfo, methodParameters) = DesignerHelper.ToModifier(node).Value;
        
        methodInfo.Name.Should().Be("Opacity");

        methodParameters[0].Should().Be(0.7);

    }
    
     
    [TestMethod]
    public void _2_()
    {
        var node = new DesignerHelper.Node
        {
            Name = "DisplayNone"
        };

        var (methodInfo, methodParameters) = DesignerHelper.ToModifier(node).Value;
        
        methodInfo.Name.Should().Be("get_DisplayNone");

        methodParameters.Length.Should().Be(0);

    }
    
    [TestMethod]
    public void _4_()
    {
        var tokens = Lexer.ParseTokens("Hover(DisplayNone)",0).tokens.Where(x=>x.tokenType != TokenType.Space).ToList();
        
        var node = DesignerHelper.NodeReader.TryReadNode(tokens,0,tokens.Count-1).node;

        var (methodInfo, methodParameters) = DesignerHelper.ToModifier(node).Value;
        
        methodInfo.Name.Should().Be("Hover");

        methodParameters.Length.Should().Be(1);

    }

    [TestMethod]
    public void _5_()
    {
        DesignerHelper.Reader.ReadInt64Array(GetTokens("[4,6,8,987]"),0).Value.value.Should().BeEquivalentTo([4,6,8,987]);
    }

    static IReadOnlyList<Token> GetTokens(string text)
    {
        return Lexer.ParseTokens(text, 0).tokens.Where(x => x.tokenType != TokenType.Space).ToList();
    }
    
    [TestMethod]
    public void _6_()
    {
        var classDefinitionCode = """
                                  class Deneme : PureComponent
                                  {
                                      public string aaa { get; set; }
                                  
                                      #region Designer Code [Do not edit manually]
                                  
                                      protected override DesignerCode Designer => new()
                                      {
                                          { [0], [Padding(22), Background("yellow"), WidthFitContent] },
                                          { [0, 0], [Size(200), Background("green"), Hover(Background("blue"), BorderRadius(8))] }
                                      };
                                  
                                      #endregion Designer Code [Do not edit manually]
                                  
                                      protected override Element render()
                                      {
                                          return new div
                                          {
                                              new div()
                                          };
                                      }
                                  }
                                  """;

        DesignerHelper.ReadDesignerCode(classDefinitionCode);

    }
    static void Assert(string inputCode, string expectedCode)
    {
        var (hasRead, _, tokens) = Lexer.ParseTokens(inputCode, 0);

        hasRead.Should().BeTrue();

        tokens = tokens.Where(x=>x.tokenType != TokenType.Space).ToList();

        var (success, nodes, _) = DesignerHelper.NodeReader.TryReadNodes(tokens,0,tokens.Count-1);
       
        success.Should().BeTrue();

        string.Join(", ", nodes).Should().Be(expectedCode);
    }
    
    
}