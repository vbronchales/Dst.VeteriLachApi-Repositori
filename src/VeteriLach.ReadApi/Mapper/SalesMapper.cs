using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper
{
    public static class SalesMapper
    {
        public static SaleDto ToSalesDto(this FacVendum venda)
        {
            var personaClient = venda.IdClientNavigation?.IdClientNavigation;
            return new SaleDto(
                venda.IdVenda,
                venda.IdClient,
                personaClient?.GetFullName() ?? string.Empty,
                venda.IdVenedor,
                venda.IdVenedorNavigation?.IdVenedorNavigation?.GetFullName() ?? string.Empty,
                venda.DiaVenda,
                venda.TotalVenda,
                venda.TotalPagat,
                venda.TotalCanvi,
                venda.IdCaixa,
                venda.IdCaixaNavigation?.Nom ?? string.Empty,
                venda.IdCaixaNavigation?.Efectiu ?? false,
                venda.IdReferencia?? null,
                venda.Referencia,
                venda.Resum,
                venda.Observacions,
                venda.FacArticleVenuts.Count
                );
        }
    }
}
