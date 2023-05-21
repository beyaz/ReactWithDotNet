using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReactWithDotNet.TypeScriptCodeAnalyzer;

[TestClass]
public class TsParserTests
{
    [TestMethod]
    public void ParseMembers()
    {
        var code = @"
  /**
   * The variant to use.
   * @default 'standard'
   */
  variant?: OverridableStringUnion<'standard' | 'filled' | 'outlined', AlertPropsVariantOverrides>;

 /**
   * Override the icon displayed before the children.
   * Unless provided, the icon is mapped to the value of the `severity` prop.
   * Set to `false` to remove the `icon`.
   */
  icon?: React.ReactNode;
  /**
   * The ARIA role attribute of the element.
   * @default 'alert'
   */
  role?: string;

";
        var tokens = TsLexer.ParseTokens(code, 0).tokens;

        var (hasRead, memberInfo, newIndex) = TsParser.TryReadMemberInfo(tokens, 0);
        hasRead.Should().BeTrue();
        memberInfo.Name.Should().Be("variant");
        newIndex.Should().Be(23);

        (hasRead, memberInfo, newIndex) = TsParser.TryReadMemberInfo(tokens, newIndex + 1);
        hasRead.Should().BeTrue();
        memberInfo.Name.Should().Be("icon");
        newIndex.Should().Be(35);

        (hasRead, memberInfo, newIndex) = TsParser.TryReadMemberInfo(tokens, newIndex + 1);
        hasRead.Should().BeTrue();
        memberInfo.Name.Should().Be("role");
        newIndex.Should().Be(44);

        tokens = TsLexer.ParseTokens("{" + code + "}", 0).tokens;

        // ReSharper disable once RedundantAssignment
        (hasRead, var members, newIndex) = TsParser.TryReadMembers(tokens, 0);

        hasRead.Should().BeTrue();
        members.Count.Should().Be(3);
    }

    [TestMethod]
    public void ParseTypeReference()
    {
        var (hasRead, tsTypeReference, newIndex) = TsParser.TryReadTypeReference(TsLexer.ParseTokens("Partial<AlertClasses>;", 0).tokens, 0);

        hasRead.Should().BeTrue();
        tsTypeReference.Name.Should().Be("Partial");
        newIndex.Should().Be(4);
    }
}