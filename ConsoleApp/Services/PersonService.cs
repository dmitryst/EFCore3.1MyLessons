using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScalarFunctionConsoleApp.Services
{
    public class PersonService : IPersonService
    {
        private readonly AdvWorksDbContext _context;

        public PersonService(AdvWorksDbContext context)
        {
            _context = context;
        }

        public async Task<List<Person>> GetAsync()
        {
            var persons = await
                _context.Persons.Take(10).ToListAsync();

            return persons;
        }
    }
}
