using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosPatologiaPacient
{
    public Guid IdPatologia { get; set; }

    public Guid IdPacient { get; set; }

    public string Nom { get; set; } = null!;

    public DateTime DiaAlta { get; set; }

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;
}
