using FluentValidation;
using MediatR;

namespace CarRental.Application.Features.Common.Commands;
public abstract record AddBaseCommand : IRequest<Guid>
{
    public string Brand { get; init; } = default!;
    public string Model { get; init; } = default!;
    public DateOnly DateOfProduction { get; init; }
    public Guid EngineId { get; init; }
    public required Guid CreatedBy { get; init; }
}

public class AddBaseValidator<T> : AbstractValidator<T> where T : AddBaseCommand
{
    public AddBaseValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.Model)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(c => c.CreatedBy)
            .NotEmpty();

        RuleFor(c => c.DateOfProduction)
            .LessThan(DateOnly.FromDateTime(DateTime.Today).AddYears(-20))
            .WithMessage("Vehicle cant be older than 20 years");
    }
}