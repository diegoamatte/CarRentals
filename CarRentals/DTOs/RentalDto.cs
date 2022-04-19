namespace CarRentals.DTOs
{
    public class RentalDto
    {
        public Guid? Id { get; set; }
        public IEnumerable<CarDto> RentedCars { get; set; }
        public ClientDto Client { get; set; }
        public DateTime RentStartTime { get; set; }
        public DateTime RentEndTime { get; set; }
    }
}
