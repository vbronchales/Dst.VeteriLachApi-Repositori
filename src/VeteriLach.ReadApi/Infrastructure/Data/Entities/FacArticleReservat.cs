using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacArticleReservat
{
    public Guid IdArticleReservat { get; set; }

    public Guid IdArticleComanda { get; set; }

    public Guid? IdClient { get; set; }

    public string NomClient { get; set; } = null!;

    public string? Cognom1 { get; set; }

    public string? Cognom2 { get; set; }

    public string? Telefon { get; set; }

    public string? Mail { get; set; }

    public DateTime DiaReserva { get; set; }

    public DateTime? DiaRecepcio { get; set; }

    public string? Observacions { get; set; }

    public Guid IdUsuariReserva { get; set; }

    public Guid? IdUsuariRecepcio { get; set; }

    public int Unitats { get; set; }

    public Guid? IdReferencia { get; set; }

    public virtual FacArticleComandum IdArticleComandaNavigation { get; set; } = null!;

    public virtual FacClient? IdClientNavigation { get; set; }

    public virtual FacVenedor? IdUsuariRecepcioNavigation { get; set; }

    public virtual FacVenedor IdUsuariReservaNavigation { get; set; } = null!;
}
