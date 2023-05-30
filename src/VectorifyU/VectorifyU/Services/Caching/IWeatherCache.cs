using System.Collections.Immutable;
using VectorifyU.DataContracts;

namespace VectorifyU.Services.Caching
{
    public interface IWeatherCache
    {
        ValueTask<IImmutableList<WeatherForecast>> GetForecast(CancellationToken token);
    }
}