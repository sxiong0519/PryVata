using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IDBRARepository
    {
        void AddDBRA(DBRA DBRA, int userId);
        void DeleteDBRA(int id);
        List<DBRA> GetAllDBRAs();
        DBRA GetDBRAById(int id);
        void UpdateDBRA(DBRA DBRA, int userId);
    }
}