using System.Text.Json.Serialization;

namespace Integracao.CPTEC.Application.DTOs
{
    public class GetAllWeatherForecastAirportDto
    {
        [JsonPropertyName("umidade")]
        public int Moisture { get; set; }

        [JsonPropertyName("intensidade")]
        public string Visibility { get; set; }

        [JsonPropertyName("codigo_icao")]
        public string ICAOCode { get; set; }

        [JsonPropertyName("pressao_atmosferica")]
        public int AtmosphericPressure { get; set; }

        [JsonPropertyName("vento")]
        public int Wind { get; set; }

        [JsonPropertyName("direcao_vento")]
        public int WindDirection { get; set; }

        [JsonPropertyName("condicao")]
        public string Condition { get; set; }

        [JsonPropertyName("condicao_desc")]
        public string ConditionDescription { get; set; }

        [JsonPropertyName("temp")]
        public int Temperature { get; set; }

        [JsonPropertyName("atualizado_em")]
        public string Updated { get; set; }
    }
}
