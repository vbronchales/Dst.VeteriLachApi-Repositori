using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosHospitalitzacio
{
    public Guid IdHospitalitzacio { get; set; }

    public Guid IdPacient { get; set; }

    public DateTime DiaIngres { get; set; }

    public Guid IdDoctorIngres { get; set; }

    public Guid IdMotiuIngres { get; set; }

    public string? ObservacionsIngres { get; set; }

    public DateTime? DiaAlta { get; set; }

    public Guid? IdDoctorAlta { get; set; }

    public Guid? IdMotiuAlta { get; set; }

    public string? ObservacionsAlta { get; set; }

    public Guid? IdBoxHospitalitzacio { get; set; }

    public virtual ICollection<HosDetallHospitalitzacio> HosDetallHospitalitzacios { get; set; } = new List<HosDetallHospitalitzacio>();

    public virtual HosBoxHospitalitzacio? IdBoxHospitalitzacioNavigation { get; set; }

    public virtual HosDoctor? IdDoctorAltaNavigation { get; set; }

    public virtual HosDoctor IdDoctorIngresNavigation { get; set; } = null!;

    public virtual HosMotiuHospitalitzacio? IdMotiuAltaNavigation { get; set; }

    public virtual HosMotiuHospitalitzacio IdMotiuIngresNavigation { get; set; } = null!;

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;
}
