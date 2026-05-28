using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacEmpresa
{
    public Guid IdEmpresa { get; set; }

    public string CodiEmpresa { get; set; } = null!;

    public Guid? IdIva { get; set; }

    public Guid? IdFoto { get; set; }

    public bool DeclaraIva { get; set; }

    public Guid IdCentreCost { get; set; }

    public virtual ICollection<FacAreaNegoci> FacAreaNegocis { get; set; } = new List<FacAreaNegoci>();

    public virtual ICollection<FacConfiguracio> FacConfiguracios { get; set; } = new List<FacConfiguracio>();

    public virtual ICollection<FacFactura> FacFacturas { get; set; } = new List<FacFactura>();

    public virtual ICollection<FacFamilium> FacFamilia { get; set; } = new List<FacFamilium>();

    public virtual HosHospital? HosHospital { get; set; }

    public virtual FacCentreCost IdCentreCostNavigation { get; set; } = null!;

    public virtual SlcPersona IdEmpresaNavigation { get; set; } = null!;

    public virtual FacIva? IdIvaNavigation { get; set; }
}
