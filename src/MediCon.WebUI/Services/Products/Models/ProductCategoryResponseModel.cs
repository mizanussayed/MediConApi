using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.WebUI.Services.Products.Models;

public sealed class ProductCategoryResponseModel
{
    public long CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public long ProductFamilyId { get; set; }
    public string PFCode { get; set; } = string.Empty;
}
