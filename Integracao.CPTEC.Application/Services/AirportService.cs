using AutoMapper;
using Integracao.CPTEC.Application.Airports.Commands;
using Integracao.CPTEC.Application.Airports.Queries;
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
    public class AirportService : IAirportService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogService _logService;
        private readonly ILogger<AirportService> _loggerConsole;

        public AirportService(IMapper mapper,
                              IMediator mediator,
                              ILogService logService,
                              ILogger<AirportService> loggerConsole)
                              
        {
            _mapper = mapper;
            _mediator = mediator; 
            _logService = logService;
            _loggerConsole = loggerConsole;
        }

        public async Task<Response> GetAllAirportWeatherForecasts()
        {
            try
            {
                var createWeatherForecastByAirportCommand = new GetAllAirportWeatherForecastsQuery();

                var airportWeatherForecastList = await _mediator.Send(createWeatherForecastByAirportCommand);

                return new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<IEnumerable<AirportWeatherForecastDto>>(airportWeatherForecastList),
                };
            }
            catch (ExternalApiException ex)
            {
                await HandleLogError(ex);

                return new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.StatusCode == HttpStatusCode.NotFound ? "Weather forecast for all airports not found" : ex.Message,
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
            catch(AutoMapperMappingException ex)
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

        public async Task<Response> GetAirportWeatherForecast(string icaoCode)
        {
            try
            {
                UserMessageException.When(Helper.IsNotValidIcao(icaoCode), "IcaoCode is not valid.");

                var createWeatherForecastByAirportCommand = new CreateWeatherForecastByAirportCommand(icaoCode);

                var airportWeatherForecast = await _mediator.Send(createWeatherForecastByAirportCommand);

                return new SuccessResponse
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = _mapper.Map<AirportWeatherForecastDto>(airportWeatherForecast),
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
                    Message = ex.StatusCode == HttpStatusCode.NotFound ? "Airport weather forecast not found" : ex.Message,
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
