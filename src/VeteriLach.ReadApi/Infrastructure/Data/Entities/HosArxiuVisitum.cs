using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosArxiuVisitum
{
    public Guid IdVisita { get; set; }

    public Guid IdArxiu { get; set; }

    public int Ordre { get; set; }

    public virtual SlcArxiu IdArxiuNavigation { get; set; } = null!;

    public virtual HosVisitum IdVisitaNavigation { get; set; } = null!;
}
