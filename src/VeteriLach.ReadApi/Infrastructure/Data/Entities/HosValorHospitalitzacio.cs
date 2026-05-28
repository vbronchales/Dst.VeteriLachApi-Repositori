using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosValorHospitalitzacio
{
    public Guid IdValorHospitalitzacio { get; set; }

    public Guid IdDetallHospitalitzacio { get; set; }

    public string Valor { get; set; } = null!;

    public int? Unitats { get; set; }

    public string? Observacions { get; set; }

    public virtual HosDetallHospitalitzacio IdDetallHospitalitzacioNavigation { get; set; } = null!;
}
