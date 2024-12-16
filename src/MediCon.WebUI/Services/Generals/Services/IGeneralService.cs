using MediCon.WebUI.Configurations.Common;
using MediCon.WebUI.Services.Generals.Models;

namespace MediCon.WebUI.Services.Generals.Services;

public interface IGeneralService
{
    Task<ApiResponse<IList<InvoiceDurationResponseModel>>> GetInvoiceDuration(CancellationToken cancellationToken = default);
    Task<ApiResponse<IList<BankResponseModel>>> GetBankDropdown(CancellationToken cancellationToken = default);
    Task<ApiResponse<IList<BankResponseModel>>> GetDisburseBankDropdown(CancellationToken cancellationToken = default);
    Task<ApiResponse<IList<CenterResponseModel>>> GetCenter(CenterRequestModel requestModel, CancellationToken cancellationToken);
    Task<ApiResponse<IList<RegionResponseModel>>> GetRegion(RegionRequestModel requestModel, CancellationToken cancellationToken);
    Task<ApiResponse<IList<CollectionModeResponseModel>>> GetCollectionMode(CollectionModeRequestModel requestModel, CancellationToken cancellationToken);
    Task<ApiResponse<IList<CollectionTypeResponseModel>>> GetCollectionType(CollectionTypeRequestModel requestModel, CancellationToken cancellationToken);
    Task<ApiResponse<IList<SubCreditAccountResponseModel>>> GetSubCreditAccount(CancellationToken cancellationToken);   
    Task<ApiResponse<DistributorResponseModel>> GetDistributorById(DistributorRequestModel requestModel, CancellationToken cancellationToken);
    Task<ApiResponse<ConfigurationResponseModel>> GetConfiguration(CancellationToken cancellationToken = default);

}
