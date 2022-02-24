namespace CarRentals.Models
{
    public class Rental
    {
        public Guid Id { get; set; }
        public IEnumerable<Car> RentedCars { get; set; }
        public Client Client { get; set; }
        public DateTime RentStartTime { get; set; }
        public DateTime RentEndTime { get; set; }
    }
}
