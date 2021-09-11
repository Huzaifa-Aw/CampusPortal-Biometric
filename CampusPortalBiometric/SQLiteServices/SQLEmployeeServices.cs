using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CampusPortalBiometric.Utils.Entities;

namespace CampusPortalBiometric.SQLiteServices
{
    class SQLEmployeeServices
    {

        private SQLiteConnection connection;
        private CampusPortalDB PortalDB;
        public SQLEmployeeServices()
        {
            PortalDB = new CampusPortalDB();
            connection = PortalDB.GetConnection();
            connection.Open();
        }

        public List<Employee> GetRegisteredEmployees()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "Select * FROM RegEmployee";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<Employee> AllEmployees = new List<Employee>();

            while (sqlite_datareader.Read())
            {
                Employee tempEmployee = new Employee();
                tempEmployee.Id = sqlite_datareader["Id"].ToString();
                tempEmployee.Name = sqlite_datareader["Name"].ToString();
                tempEmployee.Father_Name = sqlite_datareader["Father_Name"].ToString();
                tempEmployee.Designation = sqlite_datareader["Designation"].ToString();
                tempEmployee.Fingerprint = sqlite_datareader["Fingerprint"].ToString();

                AllEmployees.Add(tempEmployee);
            }
            return AllEmployees.ToList();
        }
        public List<Employee> GetNonRegisteredEmployees()
        {
            SQLiteCommand sqlite_cmd = connection.CreateCommand();
            sqlite_cmd.CommandText = "Select * FROM NonRegEmployee";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            List<Employee> AllEmployees = new List<Employee>();

            while (sqlite_datareader.Read())
            {
                Employee tempEmployee = new Employee();
                tempEmployee.Id = sqlite_datareader["Id"].ToString();
                tempEmployee.Name = sqlite_datareader["Name"].ToString();
                tempEmployee.Father_Name = sqlite_datareader["Father_Name"].ToString();
                tempEmployee.Designation = sqlite_datareader["Designation"].ToString();
                tempEmployee.Fingerprint = sqlite_datareader["Fingerprint"].ToString();

                AllEmployees.Add(tempEmployee);
            }
            return AllEmployees.ToList();
        }
        public void UpdateEmployeeFPrint(string Id, string FPrint)
        {
            String query = "Update RegEmployee set Fingerprint=@Fingerprint WHERE Id=@Id";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Fingerprint", FPrint);
                command.ExecuteNonQuery();
                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Updating Employee!");
            }
        }
        public void SaveRegisteredEmployees(List<Employee> Employees)
        {
            String query = "INSERT INTO RegEmployee (Id ,Name,Father_Name,Designation,Fingerprint) VALUES (@Id ,@Name,@Father_Name, @Designation,@Fingerprint)";

            foreach (var employee in Employees)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Father_Name", employee.Father_Name);
                    command.Parameters.AddWithValue("@Designation", employee.Designation);
                    command.Parameters.AddWithValue("@Fingerprint", employee.Fingerprint);

                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        Console.WriteLine("Error in Registering Employee!");
                }
            }
        }
        
        public void SaveNonRegisteredEmployees(List<Employee> Employees)
        {
            String query = "INSERT INTO NonRegEmployee (Id ,Name,Father_Name,Designation,Fingerprint) VALUES (@Id ,@Name,@Father_Name, @Designation,@Fingerprint)";

            foreach (var employee in Employees)
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Father_Name", employee.Father_Name);
                    command.Parameters.AddWithValue("@Designation", employee.Designation);
                    command.Parameters.AddWithValue("@Fingerprint", employee.Fingerprint);

                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        Console.WriteLine("Error in Registering Employee!");
                }
            }
        }

        public void ClearEmployees()
        {
            DeleteEmployee("RegEmployee");
            DeleteEmployee("NonRegEmployee");
        }

        private void DeleteEmployee(string TableName)
        {
            String query = "DELETE FROM " + TableName;
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Deleting Employee!");
            }
        }

        public void RegisterEmployee(Employee employee,string XMLPrint)
        {
            DeleteNonReg(employee.Id);
            Register(employee, XMLPrint);
        }
        private void DeleteNonReg(string Id)
        {
            String query = "DELETE FROM NonRegEmployee WHERE Id=@Id";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", Id);
                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Deleting Student!");
            }
        }
        private void Register(Employee employee, string XMLPrint)
        {
            String query = "INSERT INTO RegEmployee (Id ,Name,Father_Name, Designation,Fingerprint) VALUES (@Id ,@Name,@Father_Name, @Designation,@Fingerprint)";

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", employee.Id);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Father_Name", employee.Father_Name);
                command.Parameters.AddWithValue("@Designation", employee.Designation);
                command.Parameters.AddWithValue("@Fingerprint", XMLPrint);

                int result = command.ExecuteNonQuery();
                if (result < 0)
                    Console.WriteLine("Error in Registering Employee!");
            }
        }
    }
}
