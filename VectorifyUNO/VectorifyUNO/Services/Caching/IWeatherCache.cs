using System.Collections.Immutable;
using VectorifyUNO.DataContracts;

namespace VectorifyUNO.Services.Caching;
public interface IWeatherCache
{
    ValueTask<IImmutableList<WeatherForecast>> GetForecast(CancellationToken token);
}
