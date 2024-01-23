using Microsoft.Data.SqlClient;
using NuGet.Protocol.Plugins;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ADOPractice.Models
{
    public class EmployeeActions
    {
        string connectionString = @"data source=GEDU-LP343\SQLEXPRESS;initial catalog=TestDB;
                        TrustServerCertificate=True;integrated security=true";

        public IEnumerable<Employee> GetAllEmsployees()
        {
            List<Employee> listemployee = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "SELECT * FROM EMPLOYEE";
                    command.Connection = connection;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Employee employee = new Employee()
                            {
                                Id = (int)reader["id"],
                                Name = (string)reader["name"],
                                Address = (string)reader["address"],
                                DOJ =   (DateTime)(reader["DOJ"]),
                                Salary = (int)reader["salary"]

                            };
                            listemployee.Add(employee);

                        }
                    }
                }
                connection.Close();
            }
            return listemployee;
        }

        public void AddEmployee(Employee emp)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    
                    command.CommandText = $"INSERT INTO EMPLOYEE (NAME, ADDRESS,DOJ, SALARY) VALUES('{emp.Name}','{emp.Address}','{emp.DOJ.ToString("yyyy-MM-dd hh:mm:ss")}',{emp.Salary})";
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var list = GetAllEmsployees();
                var employeeobj = list.Where(x => x.Id == emp.Id).FirstOrDefault();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText =
                        $"UPDATE EMPLOYEE SET NAME= '{emp.Name}' , ADDRESS = '{emp.Address}' , DOJ = '{emp.DOJ}' , SALARY ={emp.Salary} WHERE ID = {employeeobj.Id}";
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }

        public void DeleteEmployee(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //var list = GetAllEmsployees();
                //var employeeobj = list.Where(x => x.Id ==id).FirstOrDefault();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = $"DELETE FROM EMPLOYEE WHERE ID = {id}";
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }

        public Employee GetEmployeeById(int? id)
        {
            Employee employee = new Employee();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM EMPLOYEE WHERE ID = " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    employee.Id = Convert.ToInt32(reader["id"]);
                    employee.Name = reader["name"].ToString();
                    employee.DOJ = (DateTime)(reader["doj"]);
                    employee.Address = reader["address"].ToString();
                    employee.Salary = Convert.ToInt32(reader["salary"]);

                }
                return employee;
            }

        }
    }
}
