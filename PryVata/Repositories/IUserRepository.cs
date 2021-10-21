using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetByFirebaseUserId(string firebaseUserId);
    }
}