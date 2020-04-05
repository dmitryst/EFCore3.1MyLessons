using Domain;
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
    }
}
