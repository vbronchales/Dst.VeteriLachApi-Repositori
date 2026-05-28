using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcArxiuBinari
{
    public Guid IdArxiu { get; set; }

    public byte[]? Binari { get; set; }

    public virtual SlcArxiu IdArxiuNavigation { get; set; } = null!;
}
