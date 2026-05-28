using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosBoxHospitalitzacio
{
    public Guid IdBoxHospitalitzacio { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public virtual ICollection<HosHospitalitzacio> HosHospitalitzacios { get; set; } = new List<HosHospitalitzacio>();
}
