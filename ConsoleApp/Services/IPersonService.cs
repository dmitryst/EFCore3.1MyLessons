using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScalarFunctionConsoleApp.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetAsync();

        /// <summary>
        /// LINQ expression can not be translated to SQL
        /// </summary>
        /// <param name="nameStartsWith"></param>
        /// <param name="isSpecial"></param>
        /// <returns></returns>
        Task<List<Person>> GetAsync(string nameStartsWith, bool isSpecial);

        /// <summary>
        /// ColName1 LIKE '%searchText%' OR ColName2 LIKE '%searchText%'...OR ColNameN LIKE '%searchText%'
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchText"></param>
        /// <returns></returns>
        List<T> Search<T>(string searchText) where T : class;
    }
}
