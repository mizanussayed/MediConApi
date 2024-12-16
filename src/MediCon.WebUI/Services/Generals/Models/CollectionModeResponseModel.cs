using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediCon.WebUI.Services.Generals.Models;

public sealed class CollectionModeResponseModel
{
    public long CollectionModeId { get; set; }
    public string CollectionModeCode { get; set; } = string.Empty;
    public string CollectionModeName { get; set; } = string.Empty;
    public string IsBankIdMandatory { get; set; } = string.Empty;
    public string ISBranchNameMandatory { get; set; }  = string.Empty;
}
