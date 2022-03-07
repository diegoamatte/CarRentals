namespace CarRentals.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int DNI { get; set; }
        public string Address { get; set; }
    }
}
