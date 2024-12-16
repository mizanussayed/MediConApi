using Swashbuckle.AspNetCore.Annotations;

namespace MediCon.Core.Configurations.CommonModel;

public class CommonRequestModel
{
    [SwaggerSchema(ReadOnly = true)]
    public long CurrentUserId { get; set; }
}
