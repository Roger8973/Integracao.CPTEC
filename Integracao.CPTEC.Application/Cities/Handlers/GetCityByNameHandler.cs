using AutoMapper;
using Integracao.CPTEC.Application.Cities.Queries;
using Integracao.CPTEC.Application.Excpetions;
using Integracao.CPTEC.Application.Services.HttpService;
using Integracao.CPTEC.Domain.Entities;
using MediatR;
using System.Net;

namespace Integracao.CPTEC.Application.Cities.Handlers
{
    public class GetCityByNameHandler : IRequestHandler<GetCityByNameQuery, IEnumerable<City>>
    {
        private readonly ICityApiService _cityApiService;
        private readonly IMapper _mapper;

        public GetCityByNameHandler(ICityApiService cityApiService, 
                                    IMapper mapper)
        {
            _cityApiService = cityApiService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<City>> Handle(GetCityByNameQuery request, CancellationToken cancellationToken)
        {
            var response = await _cityApiService.GetCityByName(request.CityName.Trim());

            return response.IsSuccessStatusCode ?
                   _mapper.Map<IEnumerable<City>>(response.Content)
                   : throw new ExternalApiException("Unable to establish a connection with the external service.", response.StatusCode ); 
        }
    }
}
