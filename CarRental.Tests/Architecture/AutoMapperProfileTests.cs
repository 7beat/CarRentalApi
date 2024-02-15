using AutoMapper;

namespace CarRental.Tests.Architecture;
public class AutoMapperProfileTests
{
    private readonly IConfigurationProvider configurationProvider;
    private IMapper mapper;

    public AutoMapperProfileTests()
    {
        configurationProvider = new MapperConfiguration(cfg =>
        {
        });

        mapper = configurationProvider.CreateMapper();
    }

    [Test]
    public void GivenMapperShouldHaveValidConfiguration()
    {
        configurationProvider.AssertConfigurationIsValid();
    }
}
