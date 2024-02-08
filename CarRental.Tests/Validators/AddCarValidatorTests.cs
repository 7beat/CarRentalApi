using CarRental.Application.Features.Cars.Commands;
using FluentValidation.TestHelper;

namespace CarRental.Tests.Validators;
[TestFixture]
public class AddCarValidatorTests
{
    private AddCarCommandValidator validator;

    [SetUp]
    public void Setup()
    {
        this.validator = new AddCarCommandValidator();
    }

    [Test]
    public void GivenValidateAddCarCThenShouldHaveErrorWhenNumberOfDoorsIsTooBig()
    {
        var command = new AddCarCommand()
        {
            // Brand = "TestBrand",
            Model = "TestModel",
            NumberOfDoors = 20,
            DateOfProduction = new DateOnly(2024, 01, 01),
            EngineId = Guid.NewGuid(),
            CreatedBy = Guid.Empty
        };

        validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.Brand);
        validator.TestValidate(command).ShouldHaveValidationErrorFor(c => c.NumberOfDoors);
    }
}
