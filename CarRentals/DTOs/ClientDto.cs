namespace CarRentals.DTOs
{
    public class ClientDto 
    {
        public Guid? Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public int DNI { get; set; }
        public string Address { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var client = (ClientDto)obj;
            return DNI.Equals(client.DNI);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
