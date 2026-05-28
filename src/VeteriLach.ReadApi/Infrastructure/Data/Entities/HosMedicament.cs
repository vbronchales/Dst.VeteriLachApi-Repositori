using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosMedicament
{
    public Guid IdMedicament { get; set; }

    public string Nom { get; set; } = null!;

    public decimal ValorDosis1 { get; set; }

    public string UnitatDosis1 { get; set; } = null!;

    public decimal ValorDosis2 { get; set; }

    public string UnitatDosis2 { get; set; } = null!;

    public decimal ValorConcentracio1 { get; set; }

    public string UnitatConcentracio1 { get; set; } = null!;

    public decimal ValorConcentracio2 { get; set; }

    public string UnitatConcentracio2 { get; set; } = null!;

    public string? Observacions { get; set; }

    public string? CimavetNregistro { get; set; }

    public string? CimavetJsondata { get; set; }

    public byte? TipusCimaVet { get; set; }

    public int? CimavetCodinacional { get; set; }

    public bool? Antibiotic { get; set; }

    public virtual ICollection<HosMedicamentReceptum> HosMedicamentRecepta { get; set; } = new List<HosMedicamentReceptum>();
}
