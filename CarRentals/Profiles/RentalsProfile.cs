using AutoMapper;
using CarRentals.DTOs;
using CarRentals.Models;

namespace CarRentals.Profiles
{
    public class RentalsProfile : Profile
    {
        public RentalsProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<ClientDto, Client>();
            CreateMap<Client, ClientDto>();
        }
    }
}
