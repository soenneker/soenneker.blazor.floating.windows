using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Blazor.Floating.Windows.Tests;

[Collection("Collection")]
public sealed class FloatingWindowTests : FixturedUnitTest
{
    public FloatingWindowTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {
    }
}