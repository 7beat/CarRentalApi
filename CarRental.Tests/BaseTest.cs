using CarRental.Api.Controllers;
using CarRental.Application;
using CarRental.Domain.Common;
using CarRental.Infrastructure;
using System.Reflection;

namespace CarRental.UnitTests;
public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(EntityBase).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ApplicationServicesRegistration).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(InfrastructureServicesRegistration).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(BaseApiController).Assembly;
}
