using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacEntitatBancarium
{
    public Guid IdEntitatBancaria { get; set; }

    public string Nom { get; set; } = null!;

    public Guid? IdLogo { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<FacCaixa> FacCaixas { get; set; } = new List<FacCaixa>();

    public virtual ICollection<FacClient> FacClients { get; set; } = new List<FacClient>();
}
