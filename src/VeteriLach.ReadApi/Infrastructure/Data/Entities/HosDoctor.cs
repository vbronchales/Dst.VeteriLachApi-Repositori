using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosDoctor
{
    public Guid IdDoctor { get; set; }

    public Guid IdHospital { get; set; }

    public string Especialitat { get; set; } = null!;

    public Guid? IdFoto { get; set; }

    public string? NumColegiat { get; set; }

    public int TabOrdreCerca { get; set; }

    public virtual ICollection<HosHospitalitzacio> HosHospitalitzacioIdDoctorAltaNavigations { get; set; } = new List<HosHospitalitzacio>();

    public virtual ICollection<HosHospitalitzacio> HosHospitalitzacioIdDoctorIngresNavigations { get; set; } = new List<HosHospitalitzacio>();

    public virtual ICollection<HosPacientConsultat> HosPacientConsultats { get; set; } = new List<HosPacientConsultat>();

    public virtual ICollection<HosReceptum> HosRecepta { get; set; } = new List<HosReceptum>();

    public virtual ICollection<HosSalaEspera> HosSalaEsperas { get; set; } = new List<HosSalaEspera>();

    public virtual ICollection<HosVisitum> HosVisita { get; set; } = new List<HosVisitum>();

    public virtual SlcUsuari IdDoctor1 { get; set; } = null!;

    public virtual SlcPersona IdDoctorNavigation { get; set; } = null!;

    public virtual HosHospital IdHospitalNavigation { get; set; } = null!;
}
