using FluentValidation;

namespace VeteriLach.ReadApi.Application.Animals.Queries;

/// <summary>
/// Validador per a GetAnimalsListQuery
/// </summary>
public class GetAnimalsListQueryValidator : AbstractValidator<GetAnimalsListQuery>
{
    public GetAnimalsListQueryValidator()
    {
        // PageNumber: mínim 1
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("El número de pàgina ha de ser major que 0");

        // PageSize: entre 1 i 50
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("La mida de pàgina ha de ser major que 0")
            .LessThanOrEqualTo(50)
            .WithMessage("La mida de pàgina no pot ser superior a 50 items");

        // SearchTerm: mínim 2 caràcters si es proporciona
        When(x => !string.IsNullOrWhiteSpace(x.SearchTerm), () =>
        {
            RuleFor(x => x.SearchTerm)
                .MinimumLength(2)
                .WithMessage("El terme de cerca ha de tenir almenys 2 caràcters");
        });

        // IdPropietari: format GUID vàlid (ja validat pel tipus, però assegurem que no sigui Guid.Empty)
        When(x => x.IdPropietari.HasValue, () =>
        {
            RuleFor(x => x.IdPropietari!.Value)
                .NotEqual(Guid.Empty)
                .WithMessage("L'ID del propietari no pot ser un GUID buit");
        });

        // IdEspecie: format GUID vàlid
        When(x => x.IdEspecie.HasValue, () =>
        {
            RuleFor(x => x.IdEspecie!.Value)
                .NotEqual(Guid.Empty)
                .WithMessage("L'ID de l'espècie no pot ser un GUID buit");
        });
    }
}
