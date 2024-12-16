using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediCon.WebUI.Configurations.Common;

namespace MediCon.WebUI.Services.Products.Models;

public sealed class ProductRequestModel
{
    public long? ProductId { get; set; }
    public long? CategoryId { get; set; }
    public long? SubCategoryId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
}
