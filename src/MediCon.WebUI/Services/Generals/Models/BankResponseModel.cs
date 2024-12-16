namespace MediCon.WebUI.Services.Generals.Models
{
    public sealed class BankResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string RoutingNumber { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
    }
}
