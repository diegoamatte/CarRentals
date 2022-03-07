namespace CarRentals.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public CarState State { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
