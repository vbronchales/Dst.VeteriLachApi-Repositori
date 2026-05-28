using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacCompteBanc
{
    public Guid IdCompteBanc { get; set; }

    public Guid IdClient { get; set; }

    public string CodiCompte { get; set; } = null!;

    public bool Defecte { get; set; }

    public string? Observacions { get; set; }

    public virtual FacClient IdClientNavigation { get; set; } = null!;
}
