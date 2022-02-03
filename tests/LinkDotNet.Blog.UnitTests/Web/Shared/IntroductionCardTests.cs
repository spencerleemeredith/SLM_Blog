using System.Linq;
using AngleSharp.Css.Dom;
using Bunit;
using FluentAssertions;
using LinkDotNet.Blog.Domain;
using LinkDotNet.Blog.Web.Shared;
using Xunit;

namespace LinkDotNet.Blog.UnitTests.Web.Shared;

public class IntroductionCardTests : TestContext
{
    [Fact]
    public void ShouldSetBackgroundWhenSet()
    {
        var introduction = new Introduction()
        {
            BackgroundUrl = "something_but_null",
        };

        var cut = RenderComponent<IntroductionCard>(
            p => p.Add(s => s.Introduction, introduction));

        var background = cut.FindAll(".introduction-background");
        background.Should().NotBeNullOrEmpty();
        background.Should().HaveCount(1);
        background.Single().GetStyle().CssText.Should().Contain(introduction.BackgroundUrl);
    }
}