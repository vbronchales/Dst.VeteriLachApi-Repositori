using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosMedicamentReceptum
{
    public Guid IdMedicamentRecepta { get; set; }

    public Guid IdRecepta { get; set; }

    public Guid? IdMedicament { get; set; }

    public string NomMedicament { get; set; } = null!;

    public string? Dosis { get; set; }

    public string? Frequencia { get; set; }

    public int Ordre { get; set; }

    public string? Observacions { get; set; }

    public string? Durada { get; set; }

    public string? QuantitatPrescrita { get; set; }

    public decimal? CimavetDosis { get; set; }

    public int? CimavetDurada { get; set; }

    public byte? CimavetTipusfrequencia { get; set; }

    public virtual HosMedicament? IdMedicamentNavigation { get; set; }

    public virtual HosReceptum IdReceptaNavigation { get; set; } = null!;
}
