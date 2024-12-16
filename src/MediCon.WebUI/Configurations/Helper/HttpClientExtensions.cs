using System.Net;

using MediCon.WebUI.Configurations.Common;

namespace MediCon.WebUI.Configurations.Helper;

public static class HttpClientExtensions
{
    /// <summary>
    /// Check if json is Null or not if null it returns Invalid Response with No Content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<ApiResponse<T>> GetApiResponseFromJsonAsync<T>(this HttpResponseMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            if (message is null)
            {
                return ApiResponse.InvalidResponse<T>(StatusCodes.Status204NoContent);
            }
            if (message.StatusCode == HttpStatusCode.Unauthorized)
            {
                return ApiResponse.UnAuthorized<T>();
            }

#if DEBUG
            var x = message.Content.ReadAsStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
#endif

            var result = await message.Content.ReadFromJsonAsync<ApiResponse<T>>(cancellationToken: cancellationToken).ConfigureAwait(false);
            if (result is null)
            {
                return ApiResponse.InvalidResponse<T>(StatusCodes.Status204NoContent);
            }

            return result;
        }
        catch (Exception ex)
        {
            return ApiResponse.UnexpectedResponse<T>(message: ex.Message);
        }
    }

    /// <summary>
    /// Check if json is Null or not if null it returns Invalid Response with No Content
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<EmptyApiResponse> GetEmptyApiResponseFromJsonAsync(this HttpResponseMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            if (message is null)
            {
                return EmptyApiResponse.InvalidResponse(StatusCodes.Status204NoContent);
            }
            if (message.StatusCode == HttpStatusCode.Unauthorized)
            {
                return EmptyApiResponse.UnAuthorized();
            }

#if DEBUG
            var x = message.Content.ReadAsStringAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
#endif

            var result = await message.Content.ReadFromJsonAsync<EmptyApiResponse>(cancellationToken: cancellationToken).ConfigureAwait(false);
            if (result is null)
            {
                return EmptyApiResponse.InvalidResponse(StatusCodes.Status204NoContent);
            }

            return result;
        }
        catch (Exception ex)
        {
            return EmptyApiResponse.UnexpectedResponse(message: ex.Message);
        }
    }
}
