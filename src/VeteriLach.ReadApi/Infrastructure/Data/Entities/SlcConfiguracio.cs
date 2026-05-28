using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class SlcConfiguracio
{
    public int IdConfiguracio { get; set; }

    public string? CarpetaDades { get; set; }

    public string? MailNomHostSmtp { get; set; }

    public string? MailUsuariCorreu { get; set; }

    public string? MailPasswordCorreu { get; set; }

    public string? MailCompteCorreu { get; set; }

    public string? MailNomAmostrar { get; set; }

    public int? MailPort { get; set; }

    public string? DiscId { get; set; }

    public int FreqBackup { get; set; }

    public int? BinariAct { get; set; }

    public string? CompteSms { get; set; }

    public string? RemitentSms { get; set; }

    public int? ColumnesImpresoraTickets { get; set; }

    public bool UtilitzarBdBinary { get; set; }

    public bool ActualitzarVersions { get; set; }

    public bool ImprimirCita { get; set; }

    public bool DemanarUsuariCita { get; set; }
}
