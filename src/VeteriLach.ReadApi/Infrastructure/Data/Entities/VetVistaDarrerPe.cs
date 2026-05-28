using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetVistaDarrerPe
{
    public decimal? Pes { get; set; }

    public Guid IdPacient { get; set; }
}
