namespace MediCon.WebUI.Services.Generals.Models;

public sealed class InvoiceDurationResponseModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public long MonthCount { get; set; }
}
