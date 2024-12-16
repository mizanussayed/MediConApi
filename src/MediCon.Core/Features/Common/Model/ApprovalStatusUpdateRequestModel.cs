namespace MediCon.Core.Features.Common.Model;

public sealed class ApprovalStatusUpdateRequestModel
{
    public long Id { get; set; }
    public long? ApprovalHierarchyId { get; set; }
    public string? Remarks { get; set; }
}
