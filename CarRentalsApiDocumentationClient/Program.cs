var client = new CarsRentalsOpenApiClient.CarsClient(new HttpClient());

var allCars = await client.GetCarsAsync();

Console.WriteLine(allCars);