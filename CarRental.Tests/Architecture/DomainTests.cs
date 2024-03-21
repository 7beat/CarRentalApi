using CarRental.Domain.Common;

namespace CarRental.UnitTests.Architecture;
[TestFixture]
public class DomainTests : BaseTest
{
    [Test]
    public void VehicleTypesAreUnique()
    {
        var vehicleTypes = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>();

        foreach (var type in vehicleTypes)
        {
            var count = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && typeof(Vehicle).IsAssignableFrom(t))
                .Select(v => (Vehicle)Activator.CreateInstance(v))
                .Count(v => v.VehicleType == type);

            Assert.That(count, Is.EqualTo(1), $"Expected one vehicle with type {type}, but found {count}");
        }
    }
}