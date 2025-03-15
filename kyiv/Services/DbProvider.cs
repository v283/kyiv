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
        public static string mainDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UserData.db");
        public static string userDataDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tests.db");
        public static string dictionaryDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dictionary.db");

        public static SQLiteConnection mainDatabase;
        public static SQLiteConnection userDatabase;
        public static SQLiteConnection dictionaryDatabase;

        public static void FirstRunDb()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("kyiv.tests.db"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    File.WriteAllBytes(mainDbPath, memoryStream.ToArray());
                }
            }

            //this shoyld be executed only once, when app is firstly downloaded

            var assembly2 = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly2.GetManifestResourceStream("kyiv.UserData.db"))
            {
                using (MemoryStream memoryStream2 = new MemoryStream())
                {
                    stream.CopyTo(memoryStream2);

                    File.WriteAllBytes(userDataDbPath, memoryStream2.ToArray());
                }
            }

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

        public static void ResetTestsDb()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("kyiv.tests.db"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    File.WriteAllBytes(mainDbPath, memoryStream.ToArray());
                }
            }
        }

        public static int GetDbVersion()
        {
            DbVersionModel v = (mainDatabase.Query<DbVersionModel>($"SELECT *FROM DbVersion"))[0];
            return v.Version;
        }

        public static void SetDbVersion(int v)
        {
            mainDatabase.Execute($"CREATE TABLE DbVersion (Id INTEGER, Version INTEGER)");
            mainDatabase.Execute($"""INSERT INTO DbVersion (Id, Version) VALUES (1, {v})""");

        }

        public static void ConnctToDb()
        {
            mainDatabase = new SQLiteConnection(mainDbPath);
            userDatabase = new SQLiteConnection(userDataDbPath);
            dictionaryDatabase = new SQLiteConnection(dictionaryDbPath);

        }
        public static async Task<List<TopicModel>> GetTopicsByLevel(string nameOfTable)
        {
            List<TopicModel> topics = mainDatabase.Query<TopicModel>($"SELECT *FROM {nameOfTable}");
            List<UserTopicModel> userData;
            try
            {
                userData = userDatabase.Query<UserTopicModel>($"SELECT *FROM {nameOfTable}");
            }
            catch (Exception)
            {
                userDatabase.Execute($"CREATE TABLE {nameOfTable} (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, IsDoneImage TEXT)");
                userData = userDatabase.Query<UserTopicModel>($"SELECT *FROM {nameOfTable}");
            }

            for (int i = 0; i < topics.Count; i++)
            {
                if (i < userData.Count)
                {
                    topics[i].IsDoneImage = userData[i].IsDoneImage;
                }
                else
                {
                    userDatabase.Execute($"""INSERT INTO {nameOfTable} (IsDoneImage) VALUES ("{topics[i].IsDoneImage}")""");
                }
            }
            return topics;
        }

        public static async Task<List<TestsModel>> GetTestsByTopicAsync(string nameOfTable)
        {
            List<TestsModel> tests = mainDatabase.Query<TestsModel>($"SELECT *FROM {nameOfTable}");
            List<UserTestsModel> userTests;
            try
            {
                userTests = userDatabase.Query<UserTestsModel>($"SELECT *FROM {nameOfTable}");
            }
            catch (Exception)
            {
                userDatabase.Execute($"CREATE TABLE {nameOfTable} (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Result INTEGER NOT NULL)");
                userTests = userDatabase.Query<UserTestsModel>($"SELECT *FROM {nameOfTable}");
            }
            for (int i = 0; i < tests.Count; i++)
            {
                if (i < userTests.Count)
                {
                    tests[i].Result = userTests[i].Result;
                }
                else
                {
                    userDatabase.Execute($"""INSERT INTO {nameOfTable} (Result) VALUES ("{tests[i].Result}")""");
                }
            }

            return tests;
        }

        public static async Task<List<TestModel>> GetTestByNameAsync(string nameOfTable)
        {
            return mainDatabase.Query<TestModel>($"SELECT *FROM {nameOfTable}");
        }

        public static void UpdateTopicTestResult(string tableTopicName, int id, int result)
        {
            userDatabase.Execute($"UPDATE {tableTopicName} SET Result = {result}  WHERE Id = {id} ");
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

        public static void AddDictionaryWords(DictionaryWordsModel word, string tableRef)
        {
            dictionaryDatabase.Execute($""" INSERT INTO {tableRef} ( Image, Word,Translation) VALUES ( "{word.Image}", "{word.Word}", "{word.Translation}" ) """);
        }

        public static void RemoveDictionaryWords(DictionaryWordsModel word, string tableRef)
        {
            dictionaryDatabase.Execute($""" DELETE FROM {tableRef} WHERE Id={word.Id} """);
        }

        public static void DeleteWordsTable(string tableRef, int id)
        {
            dictionaryDatabase.Execute($""" DROP TABLE IF EXISTS {tableRef} """);
            dictionaryDatabase.Execute($""" DELETE FROM DictFolders WHERE Id={id} """);
        }
    }
}
