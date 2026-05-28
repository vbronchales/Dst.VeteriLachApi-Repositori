using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtMagatzem
{
    public Guid IdMagatzem { get; set; }

    public virtual ICollection<ArtConfiguracio> ArtConfiguracios { get; set; } = new List<ArtConfiguracio>();

    public virtual ICollection<ArtMoviment> ArtMoviments { get; set; } = new List<ArtMoviment>();

    public virtual ICollection<ArtRelArticleMagatzem> ArtRelArticleMagatzems { get; set; } = new List<ArtRelArticleMagatzem>();

    public virtual ICollection<ArtTraspa> ArtTraspaIdMagatzemDestiNavigations { get; set; } = new List<ArtTraspa>();

    public virtual ICollection<ArtTraspa> ArtTraspaIdMagatzemOrigenNavigations { get; set; } = new List<ArtTraspa>();

    public virtual SlcPersona IdMagatzemNavigation { get; set; } = null!;
}
