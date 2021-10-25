using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IMethodRepository
    {
        void AddMethod(Method method);
        void DeleteMethod(int id);
        List<Method> GetAllMethods();
        Method GetMethodById(int id);
        void UpdateMethod(Method method);
    }
}