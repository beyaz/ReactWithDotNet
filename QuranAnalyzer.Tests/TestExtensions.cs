namespace QuranAnalyzer;

static class TestExtensions
{
    public static void ShouldBe(this Response<int> actual, int expected)
    {
        actual.Value.Should().Be(expected);
    }
}