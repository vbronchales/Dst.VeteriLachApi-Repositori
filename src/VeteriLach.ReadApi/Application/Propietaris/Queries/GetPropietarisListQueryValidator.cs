using FluentValidation;

namespace VeteriLach.ReadApi.Application.Propietaris.Queries;

/// <summary>
/// Validador per a GetPropietarisListQuery
/// </summary>
public class GetPropietarisListQueryValidator : AbstractValidator<GetPropietarisListQuery>
{
    public GetPropietarisListQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("El número de pàgina ha de ser major que 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("La mida de pàgina ha de ser major que 0.")
            .LessThanOrEqualTo(50)
            .WithMessage("La mida de pàgina no pot ser superior a 50.");

        RuleFor(x => x.SearchTerm)
            .MinimumLength(2)
            .When(x => !string.IsNullOrWhiteSpace(x.SearchTerm))
            .WithMessage("El terme de cerca ha de tenir almenys 2 caràcters.");

        RuleFor(x => x.Poblacio)
            .MinimumLength(2)
            .When(x => !string.IsNullOrWhiteSpace(x.Poblacio))
            .WithMessage("La població ha de tenir almenys 2 caràcters.");
    }
}
