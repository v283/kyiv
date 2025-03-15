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
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "NoteModels.db");

            _database = new SQLiteConnection(dbPath);

            _database.CreateTable<NoteModel>();
        }


        public List<NoteModel> GetNoteModels()
        {
            return _database.Table<NoteModel>().ToList();
        }

        public NoteModel GetNoteModel(int id)
        {
            return _database.Table<NoteModel>().FirstOrDefault(n => n.Id == id);
        }

        public void AddNoteModel(NoteModel NoteModel)
        {
            _database.Insert(NoteModel);
        }

        public void UpdateNoteModel(NoteModel NoteModel)
        {
            _database.Update(NoteModel);
        }

        public void DeleteNoteModel(int id)
        {
            _database.Delete<NoteModel>(id);
        }
    }
}
