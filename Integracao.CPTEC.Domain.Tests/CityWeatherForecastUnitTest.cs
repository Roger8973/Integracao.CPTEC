using FluentAssertions;
using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Exceptions;

namespace Integracao.CPTEC.Domain.Tests
{
    public class CityWeatherForecastUnitTest
    {
        [Fact]
        public void ValidateState_ValidData_Success()
        {
            Action action = () => new CityWeatherForecast("Sao Paulo", "SP", DateTime.Now.ToString(), new List<Climate>());

            action.Should().NotThrow<DomainException>();
        }

        [Fact]
        public void ValidateState_ValidData_DomainException()
        {
            Action action = () => new CityWeatherForecast("", "SP", DateTime.Now.ToString(), new List<Climate>());

            action.Should().Throw<DomainException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_CityProperty_DomainException(string city)
        {
            Action action = () => new CityWeatherForecast(city, "SP", DateTime.Now.ToString(), new List<Climate>());

            action.Should().Throw<DomainException>().WithMessage("City must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_StateProperty_DomainException(string state)
        {
            Action action = () => new CityWeatherForecast("Sao Paulo", state, DateTime.Now.ToString(), new List<Climate>());

            action.Should().Throw<DomainException>().WithMessage("State must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("32/12/2020")]
        [InlineData("29/02/2021")]
        [InlineData("12/13/2020")]
        public void ValidateStrings_UpdatedProperty_DomainException(string updated)
        {
            Action action = () => new CityWeatherForecast("Sao Paulo", "SP", updated, new List<Climate>());

            action.Should().Throw<DomainException>().WithMessage("Updated must be a valid date time.");
        }
    }
}
