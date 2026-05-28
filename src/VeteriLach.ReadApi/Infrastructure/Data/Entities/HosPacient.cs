using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosPacient
{
    public Guid IdPacient { get; set; }

    public Guid? IdFoto { get; set; }

    public string? Avis { get; set; }

    public string? AvisIntern { get; set; }

    public Guid? IdHospital { get; set; }

    public virtual HosBaixa? HosBaixa { get; set; }

    public virtual ICollection<HosExpedientAsseguradora> HosExpedientAsseguradoras { get; set; } = new List<HosExpedientAsseguradora>();

    public virtual ICollection<HosHospitalitzacio> HosHospitalitzacios { get; set; } = new List<HosHospitalitzacio>();

    public virtual ICollection<HosPacientConsultat> HosPacientConsultats { get; set; } = new List<HosPacientConsultat>();

    public virtual ICollection<HosPatologiaPacient> HosPatologiaPacients { get; set; } = new List<HosPatologiaPacient>();

    public virtual ICollection<HosReceptum> HosRecepta { get; set; } = new List<HosReceptum>();

    public virtual ICollection<HosSalaEspera> HosSalaEsperas { get; set; } = new List<HosSalaEspera>();

    public virtual ICollection<HosVacuna> HosVacunas { get; set; } = new List<HosVacuna>();

    public virtual ICollection<HosVisitum> HosVisita { get; set; } = new List<HosVisitum>();

    public virtual HosHospital? IdHospitalNavigation { get; set; }

    public virtual SlcPersona IdPacient1 { get; set; } = null!;

    public virtual FacClient IdPacientNavigation { get; set; } = null!;

    public virtual VetAnimal? VetAnimal { get; set; }

    public virtual ICollection<SlcNotificacio> IdNotificacios { get; set; } = new List<SlcNotificacio>();
}
