using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosSalaEspera
{
    public Guid IdSalaEspera { get; set; }

    public DateTime HoraEntrada { get; set; }

    public string Motiu { get; set; } = null!;

    public Guid? IdDoctor { get; set; }

    public Guid? IdPacient { get; set; }

    public bool Urgencia { get; set; }

    public DateTime? HoraSortida { get; set; }

    public string? MotiuSortida { get; set; }

    public virtual HosDoctor? IdDoctorNavigation { get; set; }

    public virtual HosPacient? IdPacientNavigation { get; set; }
}
