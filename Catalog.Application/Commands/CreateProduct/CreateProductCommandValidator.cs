// Application/Validators/CreateProductCommandValidator.cs
using FluentValidation;
namespace Catalog.Application.Commands.CreateProduct;

public sealed class CreateProductCommandValidator 
    : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(5, 20);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.Sku)
            .NotEmpty()
            .Matches(@"^[A-Z0-9\-]+$"); // Example pattern
    }
}
