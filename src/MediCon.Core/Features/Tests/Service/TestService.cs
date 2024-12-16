using System.Net.Http.Json;

using MediCon.Core.Configurations.Constants;
using MediCon.Core.Configurations.ResiliencePipelines;
using MediCon.Core.Configurations.UnitOfWorks;
using MediCon.Core.Features.Tests.Entity;
using MediCon.Core.Features.Tests.Repository;

using Lib.ErrorOr;

namespace MediCon.Core.Features.Tests.Service;

public class TestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpClientFactory _httpClientFactory;

    public TestService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
    {
        _unitOfWork = unitOfWork;
        _httpClientFactory = httpClientFactory;
    }

    public Task<ErrorOr<ApplicationUser>> GetApplicationUserAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<ITestRepository>().GetApplicationUserAsync(cancellationToken);
    }

    public Task<ErrorOr<IEnumerable<ProductTest>>> GetAllProductTestAsync(CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<ITestRepository>().GetAllProductTestAsync(cancellationToken);
    }

    public Task<ErrorOr<IEnumerable<Vendor>>> GetVendors(CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<ITestRepository>().GetVendors(cancellationToken);
    }

    public Task<ErrorOr<bool>> CreateMarketVisit(CancellationToken cancellationToken)
    {
        return _unitOfWork.Repository<ITestRepository>().CreateMarketVisit(cancellationToken);
    }

    public async Task<ErrorOr<IEnumerable<Post>>> GetPostsAsync(CancellationToken cancellationToken)
    {
        const string url = "/posts";

        var client = _httpClientFactory.CreateClient(HttpClientKey.JsonPlaceHolder);
        var result = await DefaultHttpPipeline.Pipeline.ExecuteAsync(
            async _ => await client.GetFromJsonAsync<IEnumerable<Post>>(url, cancellationToken: cancellationToken).ConfigureAwait(false),
            cancellationToken)
        .ConfigureAwait(false);

        return ErrorOr.From(result ?? Enumerable.Empty<Post>());
    }
}
