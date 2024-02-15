using CarRental.Application;
using CarRental.Domain.Common;
using CarRental.Infrastructure;
using NetArchTest.Rules;

namespace CarRental.Tests.Architecture;
[TestFixture]
public class LayerTests
{

    [Test]
    public void GivenDomainLayerShouldNotHaveDependencyOnApplicationLayer()
    {
        var result = Types.InAssembly(typeof(EntityBase).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(ApplicationServicesRegistration).Assembly.FullName)
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }

    [Test]
    public void GivenApplicationLayerShouldNotHaveDependencyOnInfrastructureLayer()
    {
        var result = Types.InAssembly(typeof(ApplicationServicesRegistration).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(InfrastructureServicesRegistration).Assembly.FullName)
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }

    [Test]
    public void GivenInfrastructureLayerShouldNotHaveDependencyOnPresentationLayer()
    {
        var result = Types.InAssembly(typeof(InfrastructureServicesRegistration).Assembly)
            .Should()
            .NotHaveDependencyOn("CarRental.Api")
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }

}
