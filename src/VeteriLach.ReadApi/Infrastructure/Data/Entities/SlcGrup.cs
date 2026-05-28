using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcGrup
{
    public Guid IdGrup { get; set; }

    public string Nom { get; set; } = null!;

    public string Observacions { get; set; } = null!;

    public virtual ICollection<SlcPrivilegi> IdPrivilegis { get; set; } = new List<SlcPrivilegi>();

    public virtual ICollection<SlcUsuari> IdUsuaris { get; set; } = new List<SlcUsuari>();
}
