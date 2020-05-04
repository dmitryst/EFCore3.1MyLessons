using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public List<T> Search<T>(string searchText) where T : class
        {
            var baseQuery = _context
                .Set<T>()
                .AsQueryable()
                .Where(CreateExpression<T>(searchText));

            return baseQuery.ToList();
        }

        /// <summary>
        /// Creates Expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchText">searchText</param>
        /// <returns>            
        /// Output query:
        // SELECT[p].[BusinessEntityID], [p].[FirstName], [p].[LastName]
        // FROM[Person].[Person]
        // AS[p]
        //WHERE([p].[FirstName] LIKE N'%Ken%') OR([p].[LastName] LIKE N'%Ken%')</returns>
        private Expression<Func<T, bool>> CreateExpression<T>(string searchText) where T : class
        {
            var cols = new List<string>
            {
                "FirstName",
                "LastName"
            };

            var pattern = Expression.Constant($"%{searchText}%");

            var parameter = Expression.Parameter(typeof(T), "x");

            var dbLikeCalls = cols.Select(colName =>
            {
                var property = Expression.PropertyOrField(parameter, colName);

                var callLike = Expression.Call(
                    typeof(DbFunctionsExtensions),
                    nameof(DbFunctionsExtensions.Like),
                    Type.EmptyTypes,
                    Expression.Constant(EF.Functions),
                    property, 
                    pattern);

                return callLike;
            });

            var aggregateCalls = dbLikeCalls
                .Skip(1)
                .Aggregate(
                    (Expression)dbLikeCalls.First(),
                    (accum, call) => Expression.OrElse(accum, call));

            return Expression.Lambda<Func<T, bool>>(aggregateCalls, parameter);
        }

        private bool IsSpecial(Person p) => p.FirstName.Length > 5;
    }
}
