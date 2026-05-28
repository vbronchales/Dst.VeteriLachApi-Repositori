using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosHospital
{
    public Guid IdHospital { get; set; }

    public virtual ICollection<HosConfiguracio> HosConfiguracios { get; set; } = new List<HosConfiguracio>();

    public virtual ICollection<HosDoctor> HosDoctors { get; set; } = new List<HosDoctor>();

    public virtual ICollection<HosPacient> HosPacients { get; set; } = new List<HosPacient>();

    public virtual SlcPersona IdHospital1 { get; set; } = null!;

    public virtual FacEmpresa IdHospitalNavigation { get; set; } = null!;
}
