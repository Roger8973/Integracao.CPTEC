using Integracao.CPTEC.Domain.Entities;
using MediatR;

namespace Integracao.CPTEC.Application.Cities.Queries
{
    public class GetCityByNameQuery : IRequest<IEnumerable<City>>
    {
        public string CityName { get; set; }

        public GetCityByNameQuery(string name)
        {
            CityName = name;
        }
    }
}
