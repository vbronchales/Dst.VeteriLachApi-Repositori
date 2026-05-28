using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class ArtConfiguracio
{
    public int IdConfiguracio { get; set; }

    public Guid? IdMagatzem { get; set; }

    public virtual ArtMagatzem? IdMagatzemNavigation { get; set; }
}
