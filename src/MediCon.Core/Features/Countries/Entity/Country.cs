using MediCon.Core.Configurations.CommonModel;

namespace MediCon.Core.Features.Countries.Entity;
public sealed class Country : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
