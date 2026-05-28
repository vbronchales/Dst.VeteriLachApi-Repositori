using FluentValidation;

namespace VeteriLach.ReadApi.Application.MedicalHistory.Queries;

public class GetAnimalVisitsListQueryValidator : AbstractValidator<GetAnimalVisitsListQuery>
{
    public GetAnimalVisitsListQueryValidator()
    {
        RuleFor(x => x.IdAnimal)
            .NotEmpty().WithMessage("L'identificador de l'animal és obligatori.");

        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("El número de pàgina ha de ser major que 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("La mida de pàgina ha d'estar entre 1 i 100.");

        RuleFor(x => x.DataFi)
            .GreaterThanOrEqualTo(x => x.DataInici)
            .When(x => x.DataInici.HasValue && x.DataFi.HasValue)
            .WithMessage("La data de fi ha de ser posterior o igual a la data d'inici.");
    }
}
