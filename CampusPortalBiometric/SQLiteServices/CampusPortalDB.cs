using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusPortalBiometric.SQLiteServices
{
    public class CampusPortalDB
    {
        public SQLiteConnection connection;
        private string dbName;

        public CampusPortalDB()
        {
            dbName = "CampusPortalDB.db";
        }
        public SQLiteConnection GetConnection()
        {
            var ConnectionString = string.Format("Data Source={0};Version=3;New=True;Compress=True;", LoadConnectionString());
            connection = new SQLiteConnection(ConnectionString);
            return connection;
        }
        private string LoadConnectionString(/*string id = "Default"*/)
        {
            string relativePath = @"" + dbName;
            var parentdir = AppDomain.CurrentDomain.BaseDirectory;
            //var parentdir = Path.GetDirectoryName(Application.StartupPath);
            //string myString = parentdir.Remove(parentdir.Length - 3, 3);
            string absolutePath = Path.Combine(parentdir, relativePath);
            //return ConfigurationManager.ConnectionStrings[id].ConnectionString;
            return absolutePath;

        }
    }
}
