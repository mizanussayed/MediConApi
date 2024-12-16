using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.WebUI.Services.Generals.Models;

public sealed class CollectionTypeRequestModel
{
    public long? CollectionTypeId { get; set; }
    public string? IsRFCollection { get; set; } = string.Empty;
}
