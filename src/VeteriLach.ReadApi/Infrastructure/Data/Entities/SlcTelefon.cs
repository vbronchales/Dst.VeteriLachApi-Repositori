using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcTelefon
{
    public Guid IdTelefon { get; set; }

    public Guid IdPersona { get; set; }

    public string Numero { get; set; } = null!;

    public int TipusTelefon { get; set; }

    public int Ordre { get; set; }

    public string? Observacions { get; set; }

    public virtual SlcPersona IdPersonaNavigation { get; set; } = null!;
}
