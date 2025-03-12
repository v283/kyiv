using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kyiv.Models;
using SQLite;

namespace kyiv.Services
{
    public class LocalDataService
    {
        private SQLiteConnection _database;

        public LocalDataService()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "Notes.db");

            _database = new SQLiteConnection(dbPath);

            _database.CreateTable<NoteModel>();
        }


        public List<NoteModel> GetNotes()
        {
            return _database.Table<NoteModel>().ToList();
        }

        public NoteModel GetNote(int id)
        {
            return _database.Table<NoteModel>().FirstOrDefault(n => n.Id == id);
        }

        public void AddNote(NoteModel note)
        {
            _database.Insert(note);
        }

        public void UpdateNote(NoteModel note)
        {
            _database.Update(note);
        }

        public void DeleteNote(int id)
        {
            _database.Delete<NoteModel>(id);
        }
    }
}
