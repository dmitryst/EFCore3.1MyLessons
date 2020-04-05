using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScalarFunctionConsoleApp.Services
{
    public interface IPersonService
    {
        Task<List<Person>> GetAsync();
    }
}
