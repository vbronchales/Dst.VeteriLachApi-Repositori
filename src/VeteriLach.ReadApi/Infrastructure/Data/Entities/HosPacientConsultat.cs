using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosPacientConsultat
{
    public Guid IdPacientConsultat { get; set; }

    public Guid IdPacient { get; set; }

    public DateTime DiaConsulta { get; set; }

    public Guid IdDoctor { get; set; }

    public virtual HosDoctor IdDoctorNavigation { get; set; } = null!;

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;
}
