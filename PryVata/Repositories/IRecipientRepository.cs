using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface IRecipientRepository
    {
        void AddRecipient(Recipient recipient);
        void DeleteRecipient(int id);
        List<Recipient> GetAllRecipients();
        Recipient GetRecipientById(int id);
        void UpdateRecipient(Recipient recipient);
    }
}