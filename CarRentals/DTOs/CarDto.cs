using CarRentals.Models;
using System.Text.Json.Serialization;

namespace CarRentals.DTOs
{
    public class CarDto
    {
        public Guid? Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CarState State { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var carDto = (CarDto)obj;
            return LicensePlate.Equals(carDto.LicensePlate);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
