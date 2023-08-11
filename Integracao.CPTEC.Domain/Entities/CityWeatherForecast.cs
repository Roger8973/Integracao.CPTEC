namespace Integracao.CPTEC.Domain.Entities
{
    public sealed class CityWeatherForecast : Entity
    {
        public string City { get; private set; }
        public string State { get; private set; }
        public string Updated { get; private set; }
        public IEnumerable<Climate> ListClimates { get; private set; }

        public CityWeatherForecast(string city,
                                   string state,
                                   string updated,
                                   IEnumerable<Climate> listClimates)
        {
            City = city;
            State = state;
            Updated = updated;
            ListClimates = listClimates;

            ValidadeState();
        }

        public void ValidadeState()
        {
            ValidadeStrings();
            ValidadeErrors();
        }

        private void ValidadeStrings()
        {
            if (string.IsNullOrEmpty(City))
                Errors.Add("City must not be empty.");

            if (string.IsNullOrEmpty(State))
                Errors.Add("State must not be empty.");

            if (string.IsNullOrEmpty(Updated) || !DateTime.TryParse(Updated, out _))
                Errors.Add("Updated must be a valid date time.");
        }
    }
}
