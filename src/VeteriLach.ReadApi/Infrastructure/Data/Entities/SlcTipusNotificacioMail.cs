using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcTipusNotificacioMail
{
    public Guid IdTipusNotificacio { get; set; }

    public string PlantillaMail { get; set; } = null!;

    public virtual SlcTipusNotificacio IdTipusNotificacioNavigation { get; set; } = null!;
}
