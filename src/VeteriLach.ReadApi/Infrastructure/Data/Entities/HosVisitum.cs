using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosVisitum
{
    public Guid IdVisita { get; set; }

    public Guid IdPacient { get; set; }

    public DateTime DiaVisita { get; set; }

    public Guid IdDoctor { get; set; }

    public string? Resum { get; set; }

    public decimal? Pes { get; set; }

    public decimal? Alsada { get; set; }

    public int TipusVisita { get; set; }

    public int? TipusPrograma { get; set; }

    public virtual ICollection<HosArxiuVisitum> HosArxiuVisita { get; set; } = new List<HosArxiuVisitum>();

    public virtual ICollection<HosProva> HosProvas { get; set; } = new List<HosProva>();

    public virtual ICollection<HosReceptum> HosRecepta { get; set; } = new List<HosReceptum>();

    public virtual ICollection<HosTextVisitum> HosTextVisita { get; set; } = new List<HosTextVisitum>();

    public virtual ICollection<HosVacuna> HosVacunas { get; set; } = new List<HosVacuna>();

    public virtual HosDoctor IdDoctorNavigation { get; set; } = null!;

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;

    public virtual VetVisitum? VetVisitum { get; set; }
}
