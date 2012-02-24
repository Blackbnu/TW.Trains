namespace Trains.Domain.Data
{
    public class City 
    {
        // I´m not happy with this class and constructor but I believe that the best solution would emerge later in this project
        // Already thought about using a simple string or char instead of a class but it might look confusing/not clear 
        // Already thought about use a builder to create the cities
        public City(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var city = (City)obj;
            return (Id == city.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }
    }
}