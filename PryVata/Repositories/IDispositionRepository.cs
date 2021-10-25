using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IDispositionRepository
    {
        void AddDisposition(Dispositions disposition);
        void DeleteDisposition(int id);
        List<Dispositions> GetAllDispositions();
        Dispositions GetDispositionById(int id);
        void UpdateDisposition(Dispositions disposition);
    }
}