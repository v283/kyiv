using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using static SQLite.TableMapping;

namespace kyiv.Data
{
    public static class DbProvider
    {
        public static string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydb.db");

        public static SQLiteConnection database;
        public static void FirstRunDb()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("kyiv.mydb.db"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    File.WriteAllBytes(DbPath, memoryStream.ToArray());
                }
            }
        }

        public static void ConnctToDb()
        {
            database = new SQLiteConnection(DbPath);

        }

        //public static List<QuestionModel> GetQuestionsListFomDbTable(string nameOfTable)
        //{
        //    return DbProvider.database.Query<QuestionModel>($"SELECT *FROM {nameOfTable}");
        //}


        //public static List<TopicModel> GetTopicsListFromDbTable(string nameOfTable)
        //{
        //    return DbProvider.database.Query<TopicModel>($"SELECT *FROM {nameOfTable}");
        //}
        //public static void UpdateTopicsTable(string table, string isdone, string referenceTable)
        //{
        //    DbProvider.database.Execute($"UPDATE {table} SET IsDone = '{isdone}'  WHERE Reference = '{referenceTable}' ");
        //}


        //public static string SelectIsDoneFromTopicsTable(string table, string referenceTable)
        //{
        //    return DbProvider.database.Query<TopicModel>($"SELECT *FROM {table} WHERE Reference = '{referenceTable}' ")[0].IsDone;
        //}


        //public static string SelectCurrentTopicFromTopicsTable(string table, string referenceTable)
        //{
        //    return DbProvider.database.Query<TopicModel>($"SELECT *FROM {table} WHERE Reference = '{referenceTable}' ")[0].Topic;
        //}

        //public static List<QuoteModel> GetGuotesFromDbTable(string nameOfTable)
        //{
        //    return DbProvider.database.Query<QuoteModel>($"SELECT *FROM {nameOfTable}");
        //}
        //public static List<WeekModel> GetWeekFromDbTable(string nameOfTable)
        //{
        //    return DbProvider.database.Query<WeekModel>($"SELECT *FROM {nameOfTable}");
        //}
        //public static int GetDayTime()
        //{
        //    return DbProvider.database.Query<WeekModel>($"SELECT *FROM MyWeek WHERE Id = 1")[0].DayTime;             
        //}
        //public static void SetDayTime(int time)
        //{
        //    DbProvider.database.Execute($"UPDATE MyWeek SET DayTime = {time}  WHERE Id = 1 ");
        //}
        //public static string GetCurrentDay()
        //{
        //    return DbProvider.database.Query<WeekModel>($"SELECT CurrentDay FROM MyWeek WHERE Id = 1")[0].CurrentDay;
        //}

        //public static void SetCurrentDay(string day)
        //{
        //    DbProvider.database.Execute($"UPDATE MyWeek SET CurrentDay = '{day}'  WHERE Id = 1");
        //}
        //public static List<SubjectsStatisticsModel> GetSubjectsStatistics()
        //{
        //    return DbProvider.database.Query<SubjectsStatisticsModel>($"SELECT *FROM SubjectsStatistics");
        //}

        //public static void SetSubjectsStatistics(string coloumn, int number)
        //{
        //    DbProvider.database.Execute($"UPDATE SubjectsStatistics SET {coloumn} = {number}  WHERE Id = 1 ");
        //}

        //public static List<SavedModel> GetSavedTests()
        //{
        //    return DbProvider.database.Query<SavedModel>($"SELECT *FROM SavedTests");
        //}
        //public static void SetSavedTestList(string table, string topic, string userAnswers, string aditionaldata)
        //{
        //    DbProvider.database.Execute($@"INSERT INTO SavedTests(RefTable, Topic, UserAnswers, AditionalData) VALUES('{table}', '{topic}', '{userAnswers}', '{aditionaldata}')");

        //}
        //public static void DeleteSavedTestList(int id)
        //{
        //    DbProvider.database.Execute($@"DELETE FROM SavedTests WHERE Id = {id}");

        //}

        //public static List<SettingsModel> GetProjectSettings()
        //{
        //    return DbProvider.database.Query<SettingsModel>($"SELECT *FROM ProjectSettings WHERE Id = 1");
        //}
        //public static void SetProjectSettings(int size)
        //{
        //    DbProvider.database.Execute($"UPDATE ProjectSettings SET MainFontSize = {size}  WHERE Id = 1");
        //}

    }
    //private void SaveDbInCorrectRepo()
    //{
    //       string DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydb.db");
    //       // TODO Only do this when app first runs
    //       var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
    //       using (Stream stream = assembly.GetManifestResourceStream("NMT_tests.mydb.db"))
    //       {
    //           using (MemoryStream memoryStream = new MemoryStream())
    //           {
    //               stream.CopyTo(memoryStream);

    //               File.WriteAllBytes(DbPath, memoryStream.ToArray());
    //           }
    //       }
    //       SQLiteConnection _database = new SQLiteConnection(DbPath);
    //       list = _database.Query<QuestionModel>("SELECT *FROM Hist6");
    //   }
}
