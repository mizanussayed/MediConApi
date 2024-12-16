using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.WebUI.Services.Products.Models;

public sealed class ProductResponseModel
{
    public long ProductId { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public long CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public long SubCategoryId { get; set; }
    public string SubCategoryName { get; set; } = string.Empty;
    public long ProductFamilyId { get; set; }
    public string IsSaleableYN { get; set; } = string.Empty;
}
