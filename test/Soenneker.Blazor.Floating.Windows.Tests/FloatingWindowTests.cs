using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Floating.Windows.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class FloatingWindowTests : HostedUnitTest
{
    public FloatingWindowTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {
    }
}
