using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcElementAlocat
{
    public Guid IdElement { get; set; }

    public string NomElement { get; set; } = null!;

    public Guid IdConnexio { get; set; }

    public DateTime HoraAlocat { get; set; }

    public virtual SlcConnexio IdConnexioNavigation { get; set; } = null!;
}
