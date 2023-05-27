using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ReactWithDotNet.TypeScriptCodeAnalyzer.TsLexer;

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
        var tokens = ParseTokens(code, 0).tokens;

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

        tokens = ParseTokens("{" + code + "}", 0).tokens;

        // ReSharper disable once RedundantAssignment
        (hasRead, var members, newIndex) = TsParser.TryReadMembers(tokens, 0);

        hasRead.Should().BeTrue();
        members.Count.Should().Be(3);
    }

    [TestMethod]
    public void ParseTypeReference()
    {
        var tokens = ParseTokens("Partial<AlertClasses>;", 0).tokens;
        
        var (hasRead, tsTypeReference, newIndex) = TsParser.TryReadTypeReference(tokens, 0);

        hasRead.Should().BeTrue();
        tsTypeReference.Name.Should().Be("Partial");
        newIndex.Should().Be(4);
    }


    [TestMethod]
    public void __typeReference__parsing__1()
    {
        var tokens = ParseTokens(" abc.def", 0).tokens;

        var typeReference = TsParser.TryReadOnlyOneTypeReference(tokens, 0).tsTypeReference;

        typeReference.Name.Should().Be("abc.def");
        typeReference.IsSimpleNamedType.Should().BeTrue();
    }

    [TestMethod]
    public void __typeReference__parsing__2()
    {
        var tokens = ParseTokens(" React.ReactNode |  undefined; ", 0).tokens;

        var typeReference = TsParser.TryReadOnlyOneTypeReference(tokens, 0).tsTypeReference;

        typeReference.Name.Should().Be("React.ReactNode");
        typeReference.IsSimpleNamedType.Should().BeTrue();
    }

    [TestMethod]
    public void __typeReference__parsing__3()
    {
        var tokens = ParseTokens(" React.ReactNode |  undefined | 'fixed'; ", 0).tokens;

        var typeReference = TsParser.TryReadUnionTypeReference(tokens, 0).tsTypeReference;

        typeReference.UnionTypes[0].Name.Should().Be("React.ReactNode");
        typeReference.UnionTypes[1].Name.Should().Be("undefined");
        typeReference.UnionTypes[2].StringValue.Should().Be("fixed");
        typeReference.IsUnionType.Should().BeTrue();
    }

    [TestMethod]
    public void __typeReference__parsing__4()
    {
        var tokens = ParseTokens(" YYY<React.ReactNode |  undefined | 'fixed'>; ", 0).tokens;

        var typeReference = TsParser.TryReadTypeReference(tokens, 0).tsTypeReference;

        typeReference.IsGeneric.Should().BeTrue();
        
        typeReference.GenericArguments[0].Name.Should().Be("React.ReactNode");
        typeReference.GenericArguments[1].Name.Should().Be("undefined");
        typeReference.GenericArguments[2].StringValue.Should().Be("fixed");
        typeReference.Name.Should().Be("YYY");
    }

}