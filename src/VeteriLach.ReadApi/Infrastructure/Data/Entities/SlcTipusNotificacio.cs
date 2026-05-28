using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcTipusNotificacio
{
    public Guid IdTipusNotificacio { get; set; }

    public string Nom { get; set; } = null!;

    public int Tipus { get; set; }

    public int? CodiSistema { get; set; }

    public string? Observacions { get; set; }

    public virtual ICollection<SlcNotificacio> SlcNotificacios { get; set; } = new List<SlcNotificacio>();

    public virtual SlcTipusNotificacioCartum? SlcTipusNotificacioCartum { get; set; }

    public virtual SlcTipusNotificacioMail? SlcTipusNotificacioMail { get; set; }

    public virtual SlcTipusNotificacioSm? SlcTipusNotificacioSm { get; set; }
}
