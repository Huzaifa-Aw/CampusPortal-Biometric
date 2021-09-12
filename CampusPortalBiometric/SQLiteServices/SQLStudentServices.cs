using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric.SQLiteServices
{
    public class SQLStudentServices
    {
        private SQLiteConnection connection;
        private CampusPortalDB PortalDB;
        public SQLStudentServices()
        {
            PortalDB = new CampusPortalDB();
            connection = PortalDB.GetConnection();
            connection.Open();
        }

        public List<Student> GetRegisteredStudents()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "Select * FROM RegStudent";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<Student> AllStudents = new List<Student>();

            while (sqlite_datareader.Read())
            {
                Student tempStudent = new Student();
                tempStudent.Id = sqlite_datareader["Id"].ToString();
                tempStudent.Name = sqlite_datareader["Name"].ToString();
                tempStudent.Father_Name = sqlite_datareader["Father_Name"].ToString();
                tempStudent.Class = sqlite_datareader["Class"].ToString();
                tempStudent.Fingerprint = sqlite_datareader["Fingerprint"].ToString();

                AllStudents.Add(tempStudent);
            }
            return AllStudents.ToList();
        }
        public void UpdateStudentFPrint(string Id, string FPrint)
        {
            String query = "Update RegStudent set Fingerprint=@Fingerprint WHERE Id=@Id";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Fingerprint", FPrint);
                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Updating Fingerprint!");
            }
        }
        public void SaveRegisteredStudents(List<Student> Students)
        {
            String query = "INSERT INTO RegStudent (Id ,Name, Father_Name, Class,Fingerprint) VALUES (@Id ,@Name, @Father_Name, @Class,@Fingerprint)";
            
            foreach (var student in Students)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Father_Name", student.Father_Name);
                    command.Parameters.AddWithValue("@Class", student.Class);
                    command.Parameters.AddWithValue("@Fingerprint", student.Fingerprint);

                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        Console.WriteLine("Error in Registering Student!");
                }
            }
        }

        public void ClearStudents()
        {
            DeleteStudents("RegStudent");
        }

        private void DeleteStudents(string TableName)
        {
            String query = "DELETE FROM " + TableName;
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Deleting Student!");
            }
        }

        public void RegisterStudent(Student student, string XMLPrint)
        {
            String query = "INSERT INTO RegStudent (Id ,Name, Father_Name, Class,Fingerprint) VALUES (@Id ,@Name, @Father_Name, @Class,@Fingerprint)";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@Father_Name", student.Father_Name);
                command.Parameters.AddWithValue("@Class", student.Class);
                command.Parameters.AddWithValue("@Fingerprint", XMLPrint);

                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Registering Student!");
            }
        }
    }
}

