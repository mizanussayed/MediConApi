using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.OperatorInfo.Models;

namespace MediCon.WebUI.Services.OperatorInfo.Services
{
    public interface IOperatorInfoService
    {
        Task<ApiResponse<IList<OperatorInfoResponseModel>>> Get(CancellationToken cancellationToken = default);
        Task<ApiResponse<OperatorInfoResponseModel>> GetById(long id, CancellationToken cancellationToken = default);
        Task<EmptyApiResponse> Create(OperatorInfoRequestModel model, CancellationToken cancellationToken = default);
        Task<EmptyApiResponse> Update(long id, OperatorInfoRequestModel model, CancellationToken cancellationToken = default);
        Task<EmptyApiResponse> Delete(long id, CancellationToken cancellationToken = default);
    }
}
