using Integracao.CPTEC.Domain.Exceptions;

namespace Integracao.CPTEC.Domain.Entities
{
    public sealed class Climate : Entity
    {
        public int CityId { get; private set; }
        public DateTime Date { get; private set; }
        public string Condition { get; private set; }
        public string ConditionDescription { get; private set; }
        public int MinimumTemperature { get; private set; }
        public int MaximumTemperature { get; private set; }
        public int UvIndex { get; private set; }

        public Climate(DateTime date,
                       string condition,
                       string conditionDescription,
                       int minimumTemperature,
                       int maximumTemperature,
                       int uvIndex)
        {
            Date = date;
            Condition = condition;
            ConditionDescription = conditionDescription;
            MinimumTemperature = minimumTemperature;
            MaximumTemperature = maximumTemperature;
            UvIndex = uvIndex;

            ValidadeState();
        }

        public void SetCityId(int cityId)
        {
            DomainException.When(cityId <= 0, "CityId must be greater than or equal to 0.");

            this.CityId = cityId;
        }

        public void SetDate(DateTime date)
        {
            this.Date = date;
        }

        public void ValidadeState()
        {
            ValidadeStrings();
            ValidadeIntegers();
            ValidadeErrors();
        }

        private void ValidadeStrings()
        {
            if (string.IsNullOrEmpty(Condition))
                Errors.Add("Condition must not be empty.");

            if (string.IsNullOrEmpty(ConditionDescription))
                Errors.Add("ConditionDescription must not be empty.");
        }

        private void ValidadeIntegers()
        {
            if (MinimumTemperature < -100)
                Errors.Add("MinimumTemperature must be less than 100.");

            if (MaximumTemperature > 80)
                Errors.Add("MaximumTemperature must be less than 80.");

            if (UvIndex > 20)
                Errors.Add("UvIndex must be less than 20.");
        }
    }
}
