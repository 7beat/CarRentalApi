using CarRental.UnitTests;
using NetArchTest.Rules;

namespace CarRental.Tests.Architecture;
[TestFixture]
public class ArchitectureTests : BaseTest
{
    [Test]
    public void DomainLayerShouldNotHaveDependencyOnApplicationLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.FullName)
            .GetResult();

        Assert.That(result.IsSuccessful);
    }

    [Test]
    public void ApplicationLayerShouldNotHaveDependencyOnInfrastructureLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.FullName)
            .GetResult();

        Assert.That(result.IsSuccessful);
    }

    [Test]
    public void InfrastructureLayerShouldNotHaveDependencyOnPresentationLayer()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn("CarRental.Api")
            .GetResult();

        Assert.That(result.IsSuccessful);
    }

    [Test]
    public void ControllersShouldHaveDependencyOnMediatR()
    {
        var result = Types
            .InAssembly(PresentationAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .Should()
            .HaveDependencyOn("MediatR")
            .GetResult();

        Assert.That(result.IsSuccessful);
    }
}
