using BlazorApp1.Pages;
using Bunit;
using FluentAssertions;

namespace BlazorTests;

public class FetchDataIntegrationTests : BlazorIntegrationTestContext
{
    [Fact]
    public async Task CanMakeComponent()
    {
        var cut = RenderComponent<FetchData>();

        cut.Find("p em").InnerHtml.Should().Be("Loading...");

        cut.WaitForElements("tbody tr").Should().HaveCount(5);
    }

    [Fact]
    public async Task RenderingTwiceMakes10Forecasts()
    {
        var cut = RenderComponent<FetchData>();

        cut.Find("p em").InnerHtml.Should().Be("Loading...");

        cut.WaitForElements("tbody tr").Should().HaveCount(5);

        var c2 = RenderComponent<FetchData>();
        c2.WaitForElements("tbody tr").Should().HaveCount(10);

        var c3 = RenderComponent<FetchData>();
        c3.WaitForElements("tbody tr").Should().HaveCount(15);
    }
}
