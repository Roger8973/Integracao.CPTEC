using AutoMapper;
using Integracao.CPTEC.Application.Cities.Commands;
using Integracao.CPTEC.Application.Cities.Queries;
using Integracao.CPTEC.Application.DTOs;
using Integracao.CPTEC.Application.Excpetions;
using Integracao.CPTEC.Application.Interfaces;
using Integracao.CPTEC.Application.Services.LogService;
using Integracao.CPTEC.Domain.Exceptions;
using Integracao.CPTEC.Domain.Utils;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Integracao.CPTEC.Application.Services
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogService _logService;
        private readonly ILogger<AirportService> _loggerConsole;

        public CityService(IMapper mapper,
                           IMediator mediator,
                           ILogService logService,
                           ILogger<AirportService> loggerConsole)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logService = logService;
            _loggerConsole = loggerConsole;
        }

        public async Task<Response> GetCityByNome(string cityName)
        {
            try
            {
                UserMessageException.When(Helper.IsNotValidString(cityName), "CityName is not valid.");

                var getCityByNameQuery = new GetCityByNameQuery(cityName);

                var city = await _mediator.Send(getCityByNameQuery);

                return new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IEnumerable<CityDto>>(city),
                };
            }
            catch (UserMessageException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch (ExternalApiException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.StatusCode == HttpStatusCode.NotFound ? "City ​​not found." : ex.Message,
                };
            }
            catch (DomainException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                    Message = "Validation failed.",
                };
            }
            catch (AutoMapperMappingException ex)
            {
                return ResponseHelper.AutoMapperValidationException(ex);
            }
            catch (Exception ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Internal server error.",
                };
            }
        }

        public async Task<Response> GetWeatherForecastByCity(int cityId)
        {
            try
            {
                UserMessageException.When(cityId <= 0, "Cityid must be greater than zero.");

                var createWeatherForecastByCityCommand = new CreateWeatherForecastByCityCommand { CityId = cityId };

                var cityWeatherForecast = await _mediator.Send(createWeatherForecastByCityCommand);

                return new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<CityWeatherForecastDto>(cityWeatherForecast),
                };
            }
            catch (UserMessageException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = ex.Message,
                };
            }
            catch (DomainException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = HttpStatusCode.UnprocessableEntity,
                    Message = "Validation failed.",
                };
            }
            catch (ExternalApiException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.StatusCode == HttpStatusCode.NotFound ? "Weather forecast for city not found." : ex.Message,
                };
            }
            catch (AutoMapperMappingException ex)
            {
                return ResponseHelper.AutoMapperValidationException(ex);
            }
            catch (Exception ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Internal server error.",
                };
            }
        }
        private async Task HandleLogError(Exception ex)
        {
            try
            {
                await _logService.SaveLog(ex);
            }
            catch (Exception logEx)
            {
                _loggerConsole.LogError($"Failed to save log: {logEx.Message} || {ex.StackTrace}");
            }
        }
    }
}
