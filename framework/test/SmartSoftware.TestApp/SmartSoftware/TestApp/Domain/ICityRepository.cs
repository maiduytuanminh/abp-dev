using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories;

namespace SmartSoftware.TestApp.Domain;

public interface ICityRepository : IBasicRepository<City, Guid>
{
    Task<City> FindByNameAsync(string name);

    Task<List<Person>> GetPeopleInTheCityAsync(string cityName);
}
