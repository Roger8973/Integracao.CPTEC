using System.Text.Json.Serialization;

namespace Integracao.CPTEC.Application.DTOs
{
    public class ClimateDto
    {
        [JsonPropertyName("data")]
        public string Date { get; set; }

        [JsonPropertyName("condicao")]
        public string Condition { get; set; }

        [JsonPropertyName("condicao_desc")]
        public string ConditionDescription { get; set; }

        [JsonPropertyName("min")]
        public int MinimumTemperature { get; set; }

        [JsonPropertyName("max")]
        public int MaximumTemperature { get; set; }

        [JsonPropertyName("indice_uv")]
        public int UvIndex { get; set; }
    }
}
