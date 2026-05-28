using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class VetVistaDeute
{
    public Guid IdClient { get; set; }

    public decimal? Total { get; set; }
}
