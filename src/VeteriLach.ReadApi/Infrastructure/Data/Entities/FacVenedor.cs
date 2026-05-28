using System;
using System.Collections.Generic;

namespace VeteriLach.ReadApi.Infrastructure.Data.Entities;

public partial class FacVenedor
{
    public Guid IdVenedor { get; set; }

    public virtual ICollection<FacAcompte> FacAcomptes { get; set; } = new List<FacAcompte>();

    public virtual ICollection<FacArticleReservat> FacArticleReservatIdUsuariRecepcioNavigations { get; set; } = new List<FacArticleReservat>();

    public virtual ICollection<FacArticleReservat> FacArticleReservatIdUsuariReservaNavigations { get; set; } = new List<FacArticleReservat>();

    public virtual ICollection<FacPressupost> FacPressuposts { get; set; } = new List<FacPressupost>();

    public virtual ICollection<FacTancament> FacTancaments { get; set; } = new List<FacTancament>();

    public virtual ICollection<FacTraspa> FacTraspas { get; set; } = new List<FacTraspa>();

    public virtual ICollection<FacVendum> FacVenda { get; set; } = new List<FacVendum>();

    public virtual SlcUsuari IdVenedor1 { get; set; } = null!;

    public virtual SlcPersona IdVenedorNavigation { get; set; } = null!;
}
