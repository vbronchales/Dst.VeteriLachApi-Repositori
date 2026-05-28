using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetVistaDarreraAlsadum
{
    public decimal? Alsada { get; set; }

    public Guid IdPacient { get; set; }
}
