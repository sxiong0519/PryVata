using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IControlRepository
    {
        void AddControl(Controls control);
        void DeleteControl(int id);
        List<Controls> GetAllControls();
        Controls GetControlById(int id);
        void UpdateControl(Controls control);
    }
}