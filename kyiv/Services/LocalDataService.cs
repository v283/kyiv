using System;
using System.Collections.Generic;
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

            _database.CreateTable<Note>();
        }


        public List<Note> GetNotes()
        {
            return _database.Table<Note>().ToList();
        }

        public Note GetNote(int id)
        {
            return _database.Table<Note>().FirstOrDefault(n => n.Id == id);
        }

        public void AddNote(Note note)
        {
            _database.Insert(note);
        }

        public void UpdateNote(Note note)
        {
            _database.Update(note);
        }

        public void DeleteNote(int id)
        {
            _database.Delete<Note>(id);
        }
    }
}
