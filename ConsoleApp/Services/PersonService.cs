using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Person>> GetAsync(string nameStartsWith, bool isSpecial)
        {
            var persons =
                _context.Persons
                .Where(p => p.FirstName.StartsWith(nameStartsWith))
                .AsEnumerable()  // switches to LINQ to Objects
                .Where(p => isSpecial ? IsSpecial(p) : !IsSpecial(p)) // as it can not be translated to SQL
                .Take(10)
                .ToList();

            return persons;
        }

        private bool IsSpecial(Person p) => p.FirstName.Length > 5;
    }
}
