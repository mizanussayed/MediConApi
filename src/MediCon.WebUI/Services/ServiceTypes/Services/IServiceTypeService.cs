using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.ServiceTypes.Models;

namespace MediCon.WebUI.Services.ServiceTypes.Services;

public interface IServiceTypeService
{
    Task<ApiResponse<IList<ServiceTypeResponseModel>>> Get(CancellationToken cancellationToken = default);
    Task<ApiResponse<ServiceTypeResponseModel>> GetById(long id, CancellationToken cancellationToken = default);
    Task<EmptyApiResponse> Create(ServiceTypeRequestModel model, CancellationToken cancellationToken = default);
    Task<EmptyApiResponse> Update(long id, ServiceTypeRequestModel model, CancellationToken cancellationToken = default);
    Task<EmptyApiResponse> Delete(long id, CancellationToken cancellationToken = default);
}
