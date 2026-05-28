using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcUsuari
{
    /// <summary>
    /// Identificador de l&apos;usuari
    /// </summary>
    public Guid IdUsuari { get; set; }

    public string Nom { get; set; } = null!;

    public string? Observacions { get; set; }

    public int Nivell { get; set; }

    public string Codi { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<AgnAgendesVisible> AgnAgendesVisibles { get; set; } = new List<AgnAgendesVisible>();

    public virtual AgnConfUsuari? AgnConfUsuari { get; set; }

    public virtual ICollection<ArtMoviment> ArtMoviments { get; set; } = new List<ArtMoviment>();

    public virtual ICollection<FacFactura> FacFacturas { get; set; } = new List<FacFactura>();

    public virtual FacVenedor? FacVenedor { get; set; }

    public virtual HosDoctor? HosDoctor { get; set; }

    public virtual ICollection<SlcComunicat> SlcComunicatIdUsuariComunicatNavigations { get; set; } = new List<SlcComunicat>();

    public virtual ICollection<SlcComunicat> SlcComunicatIdUsuariCreacioNavigations { get; set; } = new List<SlcComunicat>();

    public virtual ICollection<SlcConnexio> SlcConnexios { get; set; } = new List<SlcConnexio>();

    public virtual ICollection<SlcRecordatori> SlcRecordatoris { get; set; } = new List<SlcRecordatori>();

    public virtual ICollection<SlcGrup> IdGrups { get; set; } = new List<SlcGrup>();

    public virtual ICollection<SlcMaquina> IdMaquinas { get; set; } = new List<SlcMaquina>();

    public virtual ICollection<SlcPrivilegi> IdPrivilegis { get; set; } = new List<SlcPrivilegi>();
}
