using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcPrivilegi
{
    public Guid IdPrivilegi { get; set; }

    public int CodiPrivilegi { get; set; }

    public virtual ICollection<SlcGrup> IdGrups { get; set; } = new List<SlcGrup>();

    public virtual ICollection<SlcUsuari> IdUsuaris { get; set; } = new List<SlcUsuari>();
}
