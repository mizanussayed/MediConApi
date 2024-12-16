namespace MediCon.WebUI.Services.Generals.Models
{
    public sealed class ApprovalStatusUpdateRequestModel
    {
        public long Id { get; set; }
        public long? ApprovalHierarchyId { get; set; }
        public string? Remarks { get; set; }
    }
}