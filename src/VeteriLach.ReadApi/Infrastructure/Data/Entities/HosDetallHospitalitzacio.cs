using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosDetallHospitalitzacio
{
    public Guid IdDetallHospitalitzacio { get; set; }

    public Guid IdHospitalitzacio { get; set; }

    public string Nom { get; set; } = null!;

    public DateTime HoraInici { get; set; }

    public int Pediode { get; set; }

    public byte Tipus { get; set; }

    public Guid? IdArticle { get; set; }

    public int? Unitats { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<HosValorHospitalitzacio> HosValorHospitalitzacios { get; set; } = new List<HosValorHospitalitzacio>();

    public virtual FacArticle? IdArticleNavigation { get; set; }

    public virtual HosHospitalitzacio IdHospitalitzacioNavigation { get; set; } = null!;
}
