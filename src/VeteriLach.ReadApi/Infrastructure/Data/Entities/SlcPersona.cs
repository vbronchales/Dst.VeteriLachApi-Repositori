using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcPersona
{
    public Guid IdPersona { get; set; }

    public string? Nom { get; set; }

    public string? Cognom1 { get; set; }

    public string? Cognom2 { get; set; }

    public string? Adresa { get; set; }

    public string? CodiPostal { get; set; }

    public string? Poblacio { get; set; }

    public string? Provincia { get; set; }

    public string? Pais { get; set; }

    public int? Sexe { get; set; }

    public string? Nif { get; set; }

    public string? Email { get; set; }

    public bool Actiu { get; set; }

    public string? Observacions { get; set; }

    public string? CodiExtern { get; set; }

    public DateTime? Naixement { get; set; }

    public bool AmbWhatsApp { get; set; }

    public virtual ArtMagatzem? ArtMagatzem { get; set; }

    public virtual FacClient? FacClient { get; set; }

    public virtual FacEmpresa? FacEmpresa { get; set; }

    public virtual FacProveidor? FacProveidor { get; set; }

    public virtual FacVenedor? FacVenedor { get; set; }

    public virtual HosCompanyiaAsseguradora? HosCompanyiaAsseguradora { get; set; }

    public virtual HosDoctor? HosDoctor { get; set; }

    public virtual HosHospital? HosHospital { get; set; }

    public virtual HosPacient? HosPacient { get; set; }

    public virtual ICollection<SlcComunicat> SlcComunicats { get; set; } = new List<SlcComunicat>();

    public virtual ICollection<SlcTelefon> SlcTelefons { get; set; } = new List<SlcTelefon>();

    public virtual VetPropietari? VetPropietari { get; set; }
}
