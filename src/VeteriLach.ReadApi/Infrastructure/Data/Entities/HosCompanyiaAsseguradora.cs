using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosCompanyiaAsseguradora
{
    public Guid IdCompanyiaAsseguradora { get; set; }

    public virtual ICollection<HosExpedientAsseguradora> HosExpedientAsseguradoras { get; set; } = new List<HosExpedientAsseguradora>();

    public virtual SlcPersona IdCompanyiaAsseguradoraNavigation { get; set; } = null!;
}
