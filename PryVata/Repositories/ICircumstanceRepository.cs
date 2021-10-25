using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface ICircumstanceRepository
    {
        void AddCircumstance(Circumstance circumstance);
        void DeleteCircumstance(int id);
        List<Circumstance> GetAllCircumstances();
        Circumstance GetCircumstanceById(int id);
        void UpdateCircumstance(Circumstance circumstance);
    }
}