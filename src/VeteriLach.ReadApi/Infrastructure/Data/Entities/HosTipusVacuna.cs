using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosTipusVacuna
{
    public Guid IdTipusVacuna { get; set; }

    public string Nom { get; set; } = null!;

    public int Frequencia { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<HosVacuna> HosVacunas { get; set; } = new List<HosVacuna>();

    public virtual ICollection<VetEspecie> IdEspecies { get; set; } = new List<VetEspecie>();
}
