namespace CarRentals.Models
{
    public class Car
    {
        public Guid? Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public CarState State { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null)
                return false;
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return LicensePlate == ((Car)obj).LicensePlate;
        }
    }
}
