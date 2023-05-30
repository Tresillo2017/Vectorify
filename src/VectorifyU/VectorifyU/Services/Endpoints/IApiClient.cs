using System.Collections.Immutable;
using VectorifyU.DataContracts;

namespace VectorifyU.Services.Endpoints
{
    [Headers("Content-Type: application/json")]
    public interface IApiClient
    {
        [Get("/api/weatherforecast")]
        Task<ApiResponse<IImmutableList<WeatherForecast>>> GetWeather(CancellationToken cancellationToken = default);
    }
}