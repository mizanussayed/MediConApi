using MediCon.WebUI.Configurations.Pagination;

using Microsoft.AspNetCore.Mvc;

namespace MediCon.WebUI.Configurations.Common;

public class DataTablesRequestModel : IPaginationRequest
{
 
    [BindProperty(Name = "length")]
    public int DataTableLength { get; set; } = 10;

    [BindProperty(Name = "start")]
    public int DataTableStart { get; set; }

    [BindProperty(Name = "search[value]")]
    public string? SearchQuery { get; set; }

    public int PageSize { get { return DataTableLength; } set { DataTableLength = value; } }
    public int PageNo { get { return (DataTableStart / DataTableLength) + 1; } set { DataTableStart = value; } }
    public long? IsFilterInProgress { get; set; }

}
