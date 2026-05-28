using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetVistaDarreraVisitum
{
    public Guid IdPacient { get; set; }

    public DateTime? DarrerDia { get; set; }
}
