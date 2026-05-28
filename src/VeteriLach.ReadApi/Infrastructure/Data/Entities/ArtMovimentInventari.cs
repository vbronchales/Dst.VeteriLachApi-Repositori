using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtMovimentInventari
{
    public Guid IdInventari { get; set; }

    public Guid IdMoviment { get; set; }

    public int Ordre { get; set; }

    public virtual ArtInventari IdInventariNavigation { get; set; } = null!;

    public virtual ArtMoviment IdMovimentNavigation { get; set; } = null!;
}
