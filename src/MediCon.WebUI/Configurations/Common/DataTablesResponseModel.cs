using System.Text.Json.Serialization;

namespace MediCon.WebUI.Configurations.Common;

public class DataTablesResponseModel<T>
{
    [JsonPropertyName("recordsTotal")]
    public long TotalItems { get; set; }

    [JsonPropertyName("recordsFiltered")]
    public long TotalDisplayItems { get; set; }

    [JsonPropertyName("data")]
    public IList<T> Data { get; set; } = [];
}
