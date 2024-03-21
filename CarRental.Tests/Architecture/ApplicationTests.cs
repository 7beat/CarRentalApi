using MediatR;
using NetArchTest.Rules;

namespace CarRental.UnitTests.Architecture;
[TestFixture]
public class ApplicationTests : BaseTest
{
    [Test]
    public void RequestClassesShouldHaveProperName()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IRequest<>))
            .Should()
            .HaveNameEndingWith("Query")
            .Or()
            .HaveNameEndingWith("Command")
            .GetResult();

        Assert.That(result.IsSuccessful);
    }

    [Test]
    public void HandlersShouldHaveProperName()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .HaveNameEndingWith("Handler")
            .GetResult();

        Assert.That(result.IsSuccessful);
    }

    [Test]
    public void HandlersShouldHaveDependencyOnDomain()
    {
        var result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .HaveDependencyOn(ApplicationAssembly.FullName)
            .GetResult();

        Assert.That(result.IsSuccessful);
    }
}
