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
        const string inputCode = """

                                 BackgroundWhite, Padding(22),  WidthFitContent

                                 """;

        var (hasRead, _, tokens) = Lexer.ParseTokens(inputCode, 0);

        hasRead.Should().BeTrue();

        tokens = tokens.Where(x=>x.tokenType != TokenType.Space).ToList();

        var (success, nodes, i) = DesignerHelper.TryReadNodes(tokens,0,tokens.Count);
       
        success.Should().BeTrue();
        string.Join(", ", nodes).Should().Be("BackgroundWhite, Padding(22), WidthFitContent");
    }
}