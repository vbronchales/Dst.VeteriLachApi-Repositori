using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetVisitum
{
    public Guid IdVisita { get; set; }

    public virtual HosVisitum IdVisitaNavigation { get; set; } = null!;

    public virtual ICollection<VetTextVisitum> VetTextVisita { get; set; } = new List<VetTextVisitum>();
}
