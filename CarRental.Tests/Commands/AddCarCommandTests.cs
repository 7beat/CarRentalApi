using AutoMapper;
using CarRental.Application.Contracts.Persistence;
using CarRental.Application.Features.Cars.Commands;
using CarRental.Domain.Entities;
using Moq;

namespace CarRental.Tests.Commands;
[TestFixture]
internal class AddCarCommandTests
{
    private Mock<IUnitOfWork> unitOfWorkMock;
    private Mock<IMapper> mapperMock;

    [SetUp]
    public void Setup()
    {
        unitOfWorkMock = new();
        mapperMock = new();
        mapperMock.Setup(x => x.Map<Car>(It.IsAny<object>()))
            .Returns(new Car() { Brand = "TestBrand", Model = "TestModel", DateOfProduction = new DateOnly(2020, 01, 01), NumberOfDoors = 5 });

        unitOfWorkMock.Setup(x => x.Cars.CreateAsync(It.IsAny<Car>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Car() { Id = Guid.Parse("ede491a4-f82e-4e40-88d2-59bb290826a9"), Brand = "TestBrand", Model = "TestModel" });
    }

    [Test]
    public async Task GivenTestingCreateCarCommand()
    {
        // Arrange
        var command = new AddCarCommand()
        {
            Brand = "TestBrand",
            Model = "TestModel",
            NumberOfDoors = 5,
            DateOfProduction = new DateOnly(2020, 01, 01),
            EngineId = Guid.NewGuid(),
            CreatedBy = Guid.NewGuid(),
        };

        var handler = new AddCarCommandHandler(unitOfWorkMock.Object, mapperMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(Guid.Parse("ede491a4-f82e-4e40-88d2-59bb290826a9")));
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
