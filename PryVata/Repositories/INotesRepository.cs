using PryVata.Models;
using System.Collections.Generic;

namespace PryVata.Repositories
{
    public interface INotesRepository
    {
        void AddNote(Notes note);
        void DeleteNote(int id);
        List<Notes> GetAllNotes();
        Notes GetNoteById(int id);
        void UpdateNote(Notes note);
    }
}