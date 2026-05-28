using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class HosExpedientAsseguradora
{
    public Guid IdExpedientAsseguradora { get; set; }

    public Guid IdPacient { get; set; }

    public Guid IdCompanyiaAsseguradora { get; set; }

    public string CodiExpedient { get; set; } = null!;

    public string? Observacions { get; set; }

    public bool Actiu { get; set; }

    public virtual HosCompanyiaAsseguradora IdCompanyiaAsseguradoraNavigation { get; set; } = null!;

    public virtual HosPacient IdPacientNavigation { get; set; } = null!;
}
