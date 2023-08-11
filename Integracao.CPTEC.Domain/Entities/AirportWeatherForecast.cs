using Integracao.CPTEC.Domain.Exceptions;
using Integracao.CPTEC.Domain.Utils;

namespace Integracao.CPTEC.Domain.Entities
{
    public sealed class AirportWeatherForecast : Entity
    {
        public int Moisture { get; private set; }
        public string Visibility { get; private set; }
        public string ICAOCode { get; private set; }
        public int AtmosphericPressure { get; private set; }
        public int Wind { get; private set; }
        public int WindDirection { get; private set; }
        public string Condition { get; private set; }
        public string ConditionDescription { get; private set; }
        public int Temperature { get; private set; }
        public string Updated { get; private set; }

        public AirportWeatherForecast(int moisture,
                                      string visibility,
                                      string icaoCode,
                                      int atmosphericPressure,
                                      int wind,
                                      int windDirection,
                                      string condition,
                                      string conditionDescription,
                                      int temperature,
                                      string updated)
        {
            Moisture = moisture;
            Visibility = visibility;
            ICAOCode = icaoCode;
            AtmosphericPressure = atmosphericPressure;
            Wind = wind;
            WindDirection = windDirection;
            Condition = condition;
            ConditionDescription = conditionDescription;
            Temperature = temperature;
            Updated = updated;

            ValidadeState();
        }

        public void ValidadeState()
        {
            ValidateStrings();
            ValidadeIntegers();
            ValidadeErrors();
        }

        private void ValidateStrings()
        {
            if (string.IsNullOrEmpty(Visibility))
                Errors.Add("Visibility must not be empty.");

            if (Helper.IsNotValidIcao(ICAOCode))
                Errors.Add("ICAO code is not valid.");

            if (string.IsNullOrEmpty(Condition))
                Errors.Add("Condition must not be empty.");

            if (string.IsNullOrEmpty(ConditionDescription))
                Errors.Add("Condition description must not be empty.");

            if (string.IsNullOrEmpty(Updated) || !DateTime.TryParse(Updated, out _))
                Errors.Add("Updated must be a valid date time.");
        }

        private void ValidadeIntegers()
        {
            if (Moisture < 0)
                Errors.Add("Moisture must be greater than or equal to 0.");

            if (AtmosphericPressure < 0)
                Errors.Add("AtmosphericPressure must be greater than or equal to 0.");

            if (Wind < 0)
                Errors.Add("Wind must be greater than or equal to 0.");

            if (WindDirection < 0)
                Errors.Add("WindDirection must be greater than or equal to 0.");

            if (Temperature < -80)
                Errors.Add("Temperature should not be less than 80.");

            if (Temperature > 80)
                Errors.Add("Temperature should not be higher than 80.");

            if (Errors.Any())
                throw new DomainException(string.Join(" ", Errors));
        }
    }
}
