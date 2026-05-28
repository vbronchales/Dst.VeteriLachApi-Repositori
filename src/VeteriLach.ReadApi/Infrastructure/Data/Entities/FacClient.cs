using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacClient
{
    public Guid IdClient { get; set; }

    public Guid? IdTipusTarifa { get; set; }

    public decimal? Descompte { get; set; }

    public Guid? IdEntitatBancaria { get; set; }

    public int? DiaFacturacio { get; set; }

    public int? DiesFacturacio { get; set; }

    public string? NumeroCompte { get; set; }

    public decimal? RetencioIrpf { get; set; }

    public string? AvisIntern { get; set; }

    public int? OpcionsRecordatori { get; set; }

    public int FormaPagament { get; set; }

    public string? DadesPagament { get; set; }

    public virtual ICollection<FacAcompte> FacAcomptes { get; set; } = new List<FacAcompte>();

    public virtual ICollection<FacArticleReservat> FacArticleReservats { get; set; } = new List<FacArticleReservat>();

    public virtual ICollection<FacConfiguracio> FacConfiguracios { get; set; } = new List<FacConfiguracio>();

    public virtual ICollection<FacFacturaClient> FacFacturaClients { get; set; } = new List<FacFacturaClient>();

    public virtual ICollection<FacNotificacioClient> FacNotificacioClients { get; set; } = new List<FacNotificacioClient>();

    public virtual ICollection<FacPressupost> FacPressuposts { get; set; } = new List<FacPressupost>();

    public virtual ICollection<FacRelTarifaFamilium> FacRelTarifaFamilia { get; set; } = new List<FacRelTarifaFamilium>();

    public virtual ICollection<FacVendum> FacVenda { get; set; } = new List<FacVendum>();

    public virtual HosPacient? HosPacient { get; set; }

    public virtual SlcPersona IdClientNavigation { get; set; } = null!;

    public virtual FacEntitatBancarium? IdEntitatBancariaNavigation { get; set; }

    public virtual FacTipusTarifa? IdTipusTarifaNavigation { get; set; }

    public virtual VetPropietari? VetPropietari { get; set; }

    public virtual ICollection<FacPerfilTarifa> IdPerfilTarifas { get; set; } = new List<FacPerfilTarifa>();
}
