using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacNotificacioClient
{
    public Guid IdNotificacio { get; set; }

    public Guid IdClient { get; set; }

    public virtual FacClient IdClientNavigation { get; set; } = null!;
}
