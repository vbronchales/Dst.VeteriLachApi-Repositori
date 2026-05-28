using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetEspecie
{
    public Guid IdEspecie { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public Guid? IdIcona { get; set; }

    public int TipusEspecie { get; set; }

    public virtual ICollection<HosValoracioTipusProva> HosValoracioTipusProvas { get; set; } = new List<HosValoracioTipusProva>();

    public virtual ICollection<VetRasa> VetRasas { get; set; } = new List<VetRasa>();

    public virtual ICollection<VetTaulaPe> VetTaulaPes { get; set; } = new List<VetTaulaPe>();

    public virtual ICollection<HosTipusVacuna> IdTipusVacunas { get; set; } = new List<HosTipusVacuna>();
}
