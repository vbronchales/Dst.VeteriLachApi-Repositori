using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcTipusNotificacioCartum
{
    public Guid IdTipusNotificacio { get; set; }

    public int CodiReport { get; set; }

    public virtual SlcTipusNotificacio IdTipusNotificacioNavigation { get; set; } = null!;
}
