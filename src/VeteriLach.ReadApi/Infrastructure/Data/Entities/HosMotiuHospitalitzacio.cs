using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosMotiuHospitalitzacio
{
    public Guid IdMotiuHospitalitzacio { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual ICollection<HosHospitalitzacio> HosHospitalitzacioIdMotiuAltaNavigations { get; set; } = new List<HosHospitalitzacio>();

    public virtual ICollection<HosHospitalitzacio> HosHospitalitzacioIdMotiuIngresNavigations { get; set; } = new List<HosHospitalitzacio>();
}
