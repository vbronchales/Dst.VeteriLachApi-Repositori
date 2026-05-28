using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosReceptum
{
    public Guid IdRecepta { get; set; }

    public Guid IdDoctor { get; set; }

    public DateTime DiaRecepta { get; set; }

    public Guid IdPacient { get; set; }

    public string? Observacions { get; set; }

    public Guid? IdVisita { get; set; }

    public int? CodiUnic { get; set; }

    public byte? TipusOrdinaria { get; set; }

    public byte? TipusProfilactic { get; set; }

    public byte? TipusDispensacio { get; set; }

    public virtual ICollection<HosMedicamentReceptum> HosMedicamentRecepta { get; set; } = new List<HosMedicamentReceptum>();

    public virtual HosDoctor IdDoctorNavigation { get; set; } = null!;

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;

    public virtual HosVisitum? IdVisitaNavigation { get; set; }
}
