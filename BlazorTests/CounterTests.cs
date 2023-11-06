using BlazorApp1.Pages;
using Bunit;
using FluentAssertions;

namespace BlazorTests;

public class BlazorUnitTestContext : TestContext
{
    public BlazorUnitTestContext()
    {
        //register services here:

        //var mockService = new Mock<IService>();
        //mockService.Setup(m=>m.DoSomething()).Returns(5);
        //Services.AddScoped<IService>(_ => mockService.Object);
    }
}

public class CounterTests : BlazorUnitTestContext
{
    [Fact]
    public async Task CanCount()
    {
        var cut = RenderComponent<Counter>();
        cut.Find("button").Click();
        cut.Find("p").InnerHtml.Should().Be("Current count: 1");
    }

    [Fact]
    public async Task LotsOfClicks()
    {
        var cut = RenderComponent<Counter>();
        cut.Find("button").Click();
        cut.Find("button").Click();
        cut.Find("button").Click();
        cut.Find("button").Click();
        cut.Find("p").InnerHtml.Should().Be("Current count: 4");
    }

    [Fact]
    public async Task CanCountBy5()
    {
        var cut = RenderComponent<Counter>(parameters =>
            parameters.Add(c => c.IncrementBy, 5));
        cut.Find("button").Click();
        cut.Find("p").InnerHtml.Should().Be("Current count: 5");
    }

    [Fact]
    public async Task CanCountBy5StartingAt2()
    {
        var cut = RenderComponent<Counter>(parameters => parameters
            .Add(c => c.IncrementBy, 5)
            .Add(c => c.StartingValue, 2)
        );
        cut.Find("button").Click();
        cut.Find("p").InnerHtml.Should().Be("Current count: 7");
    }
}
