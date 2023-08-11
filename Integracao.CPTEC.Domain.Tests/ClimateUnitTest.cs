using FluentAssertions;
using Integracao.CPTEC.Domain.Entities;
using Integracao.CPTEC.Domain.Exceptions;

namespace Integracao.CPTEC.Domain.Tests
{
    public class ClimateUnitTest
    {
        [Fact]
        public void ValidateState_ValidData_Success()
        {
            Action action = () => new Climate(DateTime.Now, "T", "TS", 20, 30, 2);

            action.Should().NotThrow<DomainException>();
        }

        [Fact]
        public void ValidateState_ValidData_DomainException()
        {
            Action action = () => new Climate(DateTime.Now, "T", "TS", -220, 30, 2);

            action.Should().Throw<DomainException>();
        }

        #region Method ValidateStrings
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_ConditionProperty_DomainException(string condition)
        {
            Action action = () => new Climate(DateTime.Now, condition, "TS", 20, 30, 2);

            action.Should().Throw<DomainException>().WithMessage("Condition must not be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ValidateStrings_ConditionDescriptionProperty_DomainException(string conditionDescription)
        {
            Action action = () => new Climate(DateTime.Now, "T", conditionDescription, 20, 30, 2);

            action.Should().Throw<DomainException>().WithMessage("ConditionDescription must not be empty.");
        }
        #endregion

        #region Method ValidateIntegers
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ValidateIntegers_CityIdProperty_DomainException(int cityId)
        {
            var climate = new Climate(DateTime.Now, "T", "TS", 20, 30, 2);

            Action action = () => climate.SetCityId(cityId);

            action.Should().Throw<DomainException>().WithMessage("CityId must be greater than or equal to 0.");
        }

        [Fact]
        public void ValidateIntegers_MinimumTemperatureProperty_DomainException()
        {
            Action action = () => new Climate(DateTime.Now, "T", "TS", -101, 30, 2);

            action.Should().Throw<DomainException>().WithMessage("MinimumTemperature must be less than 100.");
        }

        [Fact]
        public void ValidateIntegers_MaximumTemperatureProperty_DomainException()
        {
            Action action = () => new Climate(DateTime.Now, "T", "TS", 20, 81, 2);

            action.Should().Throw<DomainException>().WithMessage("MaximumTemperature must be less than 80.");
        }

        [Fact]
        public void ValidateIntegers_UvIndexProperty_DomainException()
        {
            Action action = () => new Climate(DateTime.Now, "T", "TS", 80, 30, 21);

            action.Should().Throw<DomainException>().WithMessage("UvIndex must be less than 20.");
        }
        #endregion
    }
}
