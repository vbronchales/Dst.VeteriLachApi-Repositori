using VeteriLach.ReadApi.Domain;
using VeteriLach.ReadApi.Infrastructure.Data.Entities;

namespace VeteriLach.ReadApi.Mapper
{
    public static class SalesMapper
    {
        public static DebtDto ToDebtDto(this FacVendum venda)
        {
            return new DebtDto(
                venda.IdVenda,
                venda.IdClient,
                venda.IdClientNavigation?.NomClient,
                venda.DiaVenda,
                venda.TotalVenda,
                venda.TotalPagat,
                venda.IdVenedor,
                venda.IdVenedorNavigation?.NomVenedor,
                venda.IdCaixa,
                venda.IdCaixaNavigation?.NomCaixa,
                venda.IdReferencia,
                venda.IdReferenciaNavigation?.NomReferencia,
                venda.FacArticleVenuts.Select(av => new ArticleSoldDto
                {
                    Id = av.IdArticleVenut,
                    ArticleId = av.IdArticle,
                    ArticleName = av.IdArticleNavigation?.NomArticle,
                    Quantity = av.Quantitat,
                    UnitPrice = av.PreuUnitari,
                    TotalPrice = av.PreuTotal
                }).ToList()
            );
        }
    }
}
