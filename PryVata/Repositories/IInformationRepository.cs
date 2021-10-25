using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IInformationRepository
    {
        void AddInformation(Information information);
        void DeleteInformation(int id);
        List<Information> GetAllInformation();
        Information GetInformationById(int id);
        void UpdateInformation(Information information);
    }
}