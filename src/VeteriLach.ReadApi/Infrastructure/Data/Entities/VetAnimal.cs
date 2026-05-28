using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetAnimal
{
    public Guid IdAnimal { get; set; }

    public Guid IdPropietari { get; set; }

    public Guid IdRasa { get; set; }

    public string? NumXip { get; set; }

    public string? Capa { get; set; }

    public string? Color { get; set; }

    public string? Tatuatge { get; set; }

    public string? Caracter { get; set; }

    public bool Castrat { get; set; }

    public DateTime? DiaCel { get; set; }

    public virtual ICollection<FacAcompte> FacAcomptes { get; set; } = new List<FacAcompte>();

    public virtual ICollection<FacVendum> FacVenda { get; set; } = new List<FacVendum>();

    public virtual HosPacient IdAnimalNavigation { get; set; } = null!;

    public virtual VetPropietari IdPropietariNavigation { get; set; } = null!;

    public virtual VetRasa IdRasaNavigation { get; set; } = null!;

    public virtual ICollection<VetPropietariAntic> VetPropietariAntics { get; set; } = new List<VetPropietariAntic>();

    public virtual ICollection<SlcNotificacio> IdNotificacios { get; set; } = new List<SlcNotificacio>();

    public virtual ICollection<SlcNotificacio> IdNotificaciosNavigation { get; set; } = new List<SlcNotificacio>();
}
