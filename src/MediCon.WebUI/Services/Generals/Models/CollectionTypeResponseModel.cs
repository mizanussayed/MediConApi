using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.WebUI.Services.Generals.Models;

public sealed class CollectionTypeResponseModel
{
    public long CollectionTypeId { get; set; }
    public string CollectionTypeName { get; set; } = string.Empty;
    public string HasInvoiceId { get; set; } = string.Empty;
    public string HasCustomerId { get; set; }  = string.Empty;
    public string IsInvoiceIdMandatory { get; set; }  = string.Empty;
    public string IsCustomerIdMandatory { get; set; }  = string.Empty;
    public string IsRFCollection { get; set; }  = string.Empty;
}
