using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtInventari
{
    public Guid IdInventari { get; set; }

    public DateTime DiaInventari { get; set; }

    public string Nom { get; set; } = null!;

    public bool Tancat { get; set; }

    public virtual ICollection<ArtMovimentInventari> ArtMovimentInventaris { get; set; } = new List<ArtMovimentInventari>();
}
