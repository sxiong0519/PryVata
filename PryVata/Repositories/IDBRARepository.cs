using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IDBRARepository
    {
        List<DBRA> GetAllDBRAs();
        DBRA GetDBRAById(int id);
    }
}