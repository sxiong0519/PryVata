using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IExceptionRepository
    {
        void AddException(Exceptions exception);
        void DeleteException(int id);
        List<Exceptions> GetAllExceptions();
        Exceptions GetExceptionById(int id);
        void UpdateException(Exceptions exception);
    }
}