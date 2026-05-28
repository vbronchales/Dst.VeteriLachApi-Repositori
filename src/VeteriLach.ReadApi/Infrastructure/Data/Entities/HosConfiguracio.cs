using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosConfiguracio
{
    public int IdConfiguracio { get; set; }

    public Guid IdHospital { get; set; }

    public bool? MostrarBaixa { get; set; }

    public int? TipusVisita { get; set; }

    public bool CronologicAsc { get; set; }

    public bool PesEnGrams { get; set; }

    public int PeriodeMostrarVisites { get; set; }

    public bool GenId { get; set; }

    public Guid? IdTarifaHospitalitzacio { get; set; }

    public virtual HosHospital IdHospitalNavigation { get; set; } = null!;

    public virtual FacTipusTarifa? IdTarifaHospitalitzacioNavigation { get; set; }
}
