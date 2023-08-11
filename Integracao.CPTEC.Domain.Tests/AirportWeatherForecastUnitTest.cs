using Integracao.CPTEC.Domain.Entities;
using FluentAssertions;
using Integracao.CPTEC.Domain.Exceptions;

namespace Integracao.CPTEC.Domain.Tests
{
    public class AirportWeatherForecastUnitTest
    {
        [Fact]
        public void ValidateState_ValidData_Success()
        {
            Action action = () => new AirportWeatherForecast(1, "1000", "SBAR", 1, 1, 1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().NotThrow<DomainException>();
        }

        [Fact]
        public void ValidateState_ValidData_DomainException()
        {
            Action action = () => new AirportWeatherForecast(-1, "SBAR", "SBAR", 1, 1, 1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().Throw<DomainException>();
        }

        #region Method ValidateStrings
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_VisibilityProperty_DomainException(string visibility)
        {
            Action act = () => new AirportWeatherForecast(1, visibility, "SBAR", 1, 1, 1, "T", "TS", 20, DateTime.Now.ToString());

            act.Should().Throw<DomainException>().WithMessage("Visibility must not be empty.");
        }

        [Theory]
        [InlineData("S")]
        [InlineData("SU")]
        [InlineData("SBU")]
        [InlineData("AEU")]
        [InlineData("AEUR")]
        public void ValidateStrings_ICAOCodeProperty_DomainException(string icaoCode)
        {
            Action action = () => new AirportWeatherForecast(1, "1000", icaoCode, 1, 1, 1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("ICAO code is not valid.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_ConditionProperty_DomainException(string condition)
        {
            Action act = () => new AirportWeatherForecast(1, "1000", "SBAR", 1, 1, 1, condition, "TS", 20, DateTime.Now.ToString());

            act.Should().Throw<DomainException>().WithMessage("Condition must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_ConditionDescriptionProperty_DomainException(string conditionDescription)
        {
            Action act = () => new AirportWeatherForecast(1, "1000", "SBAR", 1, 1, 1, "T", conditionDescription, 20, DateTime.Now.ToString());

            act.Should().Throw<DomainException>().WithMessage("Condition description must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("32/12/2020")]
        [InlineData("29/02/2021")]
        [InlineData("12/13/2020")]
        public void ValidateStrings_UpdatedProperty_DomainException(string updated)
        {
            Action act = () => new AirportWeatherForecast(1, "1000", "SBAR", 1, 1, 1, "T", "TS", 20, updated);

            act.Should().Throw<DomainException>().WithMessage("Updated must be a valid date time.");
        }
        #endregion

        #region Method ValidateIntegers
        [Fact]
        public void ValidateIntegers_MoistureProperty_DomainException()
        {
            Action action = () => new AirportWeatherForecast(-1, "SBAR", "SBAR", 1, 1, 1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("Moisture must be greater than or equal to 0.");
        }

        [Fact]
        public void ValidateIntegers_AtmosphericPressureProperty_DomainException()
        {
            Action action = () => new AirportWeatherForecast(1, "SBAR", "SBAR", -1, 1, 1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("AtmosphericPressure must be greater than or equal to 0.");
        }

        [Fact]
        public void ValidateIntegers_WindProperty_DomainException()
        {
            Action action = () => new AirportWeatherForecast(1, "SBAR", "SBAR", 1, -1, 1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("Wind must be greater than or equal to 0.");
        }

        [Fact]
        public void ValidateIntegers_WindDirectionProperty_DomainException()
        {
            Action action = () => new AirportWeatherForecast(1, "SBAR", "SBAR", 1, 1, -1, "T", "TS", 20, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("WindDirection must be greater than or equal to 0.");
        }

        [Fact]
        public void ValidateIntegers_TemperatureProperty_DomainExceptionForNegativeTemperature()
        {
            Action action = () => new AirportWeatherForecast(1, "SBAR", "SBAR", 1, 1, 1, "T", "TS", -81, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("Temperature should not be less than 80.");
        }

        [Fact]
        public void ValidateIntegers_TemperatureProperty_DomainExceptionForPositiveTemperature()
        {
            Action action = () => new AirportWeatherForecast(1, "SBAR", "SBAR", 1, 1, 1, "T", "TS", 81, DateTime.Now.ToString());

            action.Should().Throw<DomainException>().WithMessage("Temperature should not be higher than 80.");
        }
        #endregion
    }
}