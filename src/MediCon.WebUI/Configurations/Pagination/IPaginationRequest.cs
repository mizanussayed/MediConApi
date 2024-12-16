namespace MediCon.WebUI.Configurations.Pagination;

public interface IPaginationRequest
{
    public int PageSize { get; set; }
    public int PageNo { get; set; }

}
