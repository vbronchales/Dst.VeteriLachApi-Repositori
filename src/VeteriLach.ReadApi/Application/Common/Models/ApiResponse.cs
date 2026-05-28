namespace VeteriLach.ReadApi.Application.Common.Models;

/// <summary>
/// Resposta genèrica de l'API amb metadades
/// </summary>
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public ResponseMetadata Meta { get; set; } = new();

    public ApiResponse()
    {
    }

    public ApiResponse(T data)
    {
        Data = data;
        Meta = new ResponseMetadata
        {
            RetrievedAt = DateTime.UtcNow,
            Source = "VeteriLach Database"
        };
    }
}

public class ResponseMetadata
{
    public DateTime RetrievedAt { get; set; }
    public string Source { get; set; } = "VeteriLach Database";
    public bool CacheHit { get; set; } = false;
    public int? ResponseTimeMs { get; set; }
}
