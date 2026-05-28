namespace VeteriLach.ReadApi.Application.Common.Models;

/// <summary>
/// Resultat paginat genèric per a respostes de llistes
/// </summary>
public class PaginatedResult<T>
{
    public List<T> Data { get; set; } = new();
    public PaginationMetadata Pagination { get; set; } = new();
    public Dictionary<string, string>? Links { get; set; }

    public PaginatedResult()
    {
    }

    public PaginatedResult(List<T> data, int totalItems, int page, int pageSize)
    {
        Data = data;
        Pagination = new PaginationMetadata
        {
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            HasNextPage = page < (int)Math.Ceiling(totalItems / (double)pageSize),
            HasPreviousPage = page > 1
        };
    }
}

public class PaginationMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasNextPage { get; set; }
    public bool HasPreviousPage { get; set; }
}
