using System;
using Microsoft.Data.Sqlite;
namespace TestProject.Model
{
    public class DbContext
    {
        public string connectionString = "Data Source=sample1.db";
        public void CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var dropTableCmd = connection.CreateCommand();
                dropTableCmd.CommandText =
                @"
                DROP TABLE IF  EXISTS Employee 
            ";
                dropTableCmd.ExecuteNonQuery();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText =
                @"
                CREATE TABLE IF NOT EXISTS Employee (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmployeeName VARCHAR(250) NOT NULL,
                    CompanyName VARCHAR(250) NOT NULL,
                    VerificationCode VARCHAR(250) NOT NULL
                );
            ";
                createTableCmd.ExecuteNonQuery();

                var insertCmd = connection.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Employee (EmployeeName, CompanyName,VerificationCode) VALUES ('A', 'ABC','E1');";
                insertCmd.ExecuteNonQuery();
                insertCmd.CommandText = "INSERT INTO Employee (EmployeeName, CompanyName,VerificationCode) VALUES ('B', 'ABC','E2');";
                insertCmd.ExecuteNonQuery();
                insertCmd.CommandText = "INSERT INTO Employee (EmployeeName, CompanyName,VerificationCode) VALUES ('C', 'XYZ','X1');";
                insertCmd.ExecuteNonQuery();
                insertCmd.CommandText = "INSERT INTO Employee (EmployeeName, CompanyName,VerificationCode) VALUES ('D', 'XYZ','X2');";
                insertCmd.ExecuteNonQuery();
                insertCmd.CommandText = "INSERT INTO Employee (EmployeeName, CompanyName,VerificationCode) VALUES ('E', 'XYZ','X3');";
                insertCmd.ExecuteNonQuery();
            }
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Id, EmployeeName, CompanyName,VerificationCode FROM Employee;";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = reader.GetInt32(0);
                        employee.EmployeeName = reader.GetString(1);
                        employee.CompanyName = reader.GetString(2);
                        employee.VerificationCode = reader.GetString(3);
                        employeeList.Add(employee);
                    }
                }
            }
            return employeeList;
        }
        public bool VerifyEmployee(int employeeId, string companyName, string verificationCode)
        {
            bool verified = false;
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                // Query the data
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Id, EmployeeName, CompanyName,VerificationCode FROM Employee" +
                    " where Id =" + employeeId + " and CompanyName = '" + companyName + "' " +
                    "and VerificationCode = '" + verificationCode + "';";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verified = true;
                    }
                }
                return verified;
            }
        }

    }
}



