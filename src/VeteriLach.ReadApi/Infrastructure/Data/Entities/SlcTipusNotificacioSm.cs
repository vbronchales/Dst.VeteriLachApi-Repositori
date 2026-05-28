using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcTipusNotificacioSm
{
    public Guid IdTipusNotificacio { get; set; }

    public string TextSms { get; set; } = null!;

    public virtual SlcTipusNotificacio IdTipusNotificacioNavigation { get; set; } = null!;
}
