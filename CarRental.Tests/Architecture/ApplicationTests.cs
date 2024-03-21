using CarRental.Application;
using MediatR;
using NetArchTest.Rules;

namespace CarRental.UnitTests.Architecture;
[TestFixture]
public class ApplicationTests
{
    [Test]
    public void HandlersShouldHaveProperNameTest()
    {
        var result = Types
            .InAssembly(typeof(ApplicationServicesRegistration).Assembly)
            .That()
            .ImplementInterface(typeof(IRequestHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .Or()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }
}
