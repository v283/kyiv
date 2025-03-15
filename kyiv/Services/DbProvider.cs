using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using kyiv.Models;


namespace kyiv.Services
{
    public static class DbProvider
    {
        public static string dictionaryDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dictionary.db");


        public static SQLiteConnection dictionaryDatabase;

        public static void FirstRunDb()
        {
            var assembly3 = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly3.GetManifestResourceStream("kyiv.dictionary.db"))
            {
                using (MemoryStream memoryStream3 = new MemoryStream())
                {
                    stream.CopyTo(memoryStream3);

                    File.WriteAllBytes(dictionaryDbPath, memoryStream3.ToArray());
                }
            }

        }
        public static void ConnctToDb()
        {
            dictionaryDatabase = new SQLiteConnection(dictionaryDbPath);

        }

        // dictionary

        public static List<DictionaryModel> GetDictionaryFolders(string nameOfTable)
        {
            List<DictionaryModel> folders = dictionaryDatabase.Query<DictionaryModel>($"SELECT *FROM {nameOfTable}");

            return folders;
        }
        public static void AddDictionaryFolder(DictionaryModel dictionary)
        {
            dictionaryDatabase.Execute($""" INSERT INTO DictFolders ( FolderImage, FolderName,DictRef) VALUES ( "{dictionary.FolderImage}", "{dictionary.FolderName}", "{dictionary.DictRef}" ) """);
        }

        public static List<DictionaryWordsModel> GetDictionaryWords(string nameOfTable)
        {
            List<DictionaryWordsModel> words = dictionaryDatabase.Query<DictionaryWordsModel>($"SELECT *FROM {nameOfTable}");
            return words;
        }
        public static void CreateDictionaryWordsTable(string nameOfTable)
        {
            dictionaryDatabase.Execute($"CREATE TABLE {nameOfTable} (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Image TEXT,Word TEXT,Translation TEXT )");           
        }

        public static void AddDictionaryWords(DictionaryWordsModel word , string tableRef)
        {
            dictionaryDatabase.Execute($""" INSERT INTO {tableRef} ( Image, Word,Translation) VALUES ( "{word.Image}", "{word.Word}", "{word.Translation}" ) """);
        }

        public static void RemoveDictionaryWords(DictionaryWordsModel word, string tableRef)
        {
            dictionaryDatabase.Execute($""" DELETE FROM {tableRef} WHERE Id={word.Id} """);
        }

        public static void DeleteWordsTable( string tableRef, int id)
        {
            dictionaryDatabase.Execute($""" DROP TABLE IF EXISTS {tableRef} """);
            dictionaryDatabase.Execute($""" DELETE FROM DictFolders WHERE Id={id} """);
        }
    }
}
