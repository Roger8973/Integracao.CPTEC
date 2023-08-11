using System.Text.Json.Serialization;

namespace Integracao.CPTEC.Application.DTOs
{
    public class CityDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("estado")]
        public string State { get; set; }
    }
}
