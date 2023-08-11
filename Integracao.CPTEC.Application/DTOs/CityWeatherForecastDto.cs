using System.Text.Json.Serialization;

namespace Integracao.CPTEC.Application.DTOs
{
    public class CityWeatherForecastDto
    {
        [JsonPropertyName("cidade")]
        public string City { get; set; }

        [JsonPropertyName("estado")]
        public string State { get; set; }

        [JsonPropertyName("atualizado_em")]
        public string Updated { get; set; }

        [JsonPropertyName("clima")]
        public IEnumerable<ClimateDto> ListClimates { get; set; }
    }
}
