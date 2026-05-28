using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcTextNotificacio
{
    public Guid IdNotificacio { get; set; }

    public string TextPla { get; set; } = null!;

    public byte[]? TextRft { get; set; }

    public virtual SlcNotificacio IdNotificacioNavigation { get; set; } = null!;
}
