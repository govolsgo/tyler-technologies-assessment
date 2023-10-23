using System;
using Microsoft.Data.SqlClient;

namespace Tyler_Tech_Test
{
	public class DAL
	{
        public List<Role> RoleList;

        public DAL()
        {
            RoleList = GetRoleList();
        }

        public static List<Employee> GetEmployeesByManager(Guid manager)
        {
            var employees = new List<Employee>();

            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("GetEmployeesByManager", cn);
                cmd.Parameters.AddWithValue("@Manager", manager);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                var results = cmd.ExecuteReader();

                if (results.HasRows)
                {
                    while (results.Read())
                    {
                        var employee = new Employee
                        {
                            ID = (Guid)results["ID"],
                            EmployeeID = (string)results["EmployeeID"],
                            FirstName = (string)results["FirstName"],
                            LastName = (string)results["LastName"],
                            Manager = (Guid)results["Manager"]
                        };

                        employees.Add(GetEmployeeRoles(employee));
                    }
                }

                return employees;
            };
        }

        public static Employee GetEmployeeRoles(Employee employee)
        {
            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("GetRolesByEmployee", cn);
                cmd.Parameters.AddWithValue("@EmployeeGUID", employee.ID);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                var results = cmd.ExecuteReader();

                if (results.HasRows)
                {
                    while (results.Read())
                        employee.Roles.Add((string)results["Name"]);
                }

                return employee;
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("AddEmployee", cn);
                cmd.Parameters.AddWithValue("@ID", employee.ID);
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@Manager", employee.Manager);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                var result = cmd.ExecuteNonQuery();

                if (result == 1)
                {
                    AddRoleMappings(employee);
                    return;
                }
                else
                    throw new Exception("Unable to add employee!");
            }
        }

        public void AddRoleMappings(Employee employee)
        {
            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                cn.Open();

                foreach (var role in employee.Roles)
                {
                    var roleID = RoleList.Where(x => x.Name == role);

                    var cmd = new SqlCommand("AddRoleMapping");
                    cmd.Parameters.AddWithValue("@ID", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@UserID", employee.ID);
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var result = cmd.ExecuteNonQuery();

                    if (result != 1)
                        throw new Exception("Unable to add role!");
                }
            }
        }

        public static List<Role> GetRoleList()
        {
            var roleList = new List<Role>();

            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("GetAllRoles", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var role = new Role
                        {
                            ID = (Guid)reader["ID"],
                            Name = (string)reader["Name"]
                        };

                        roleList.Add(role);
                    }
                }
            }

            return roleList;
        }

        public static List<Employee> GetManagers()
        {
            var managerList = new List<Employee>();

            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("GetManagers", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var manager = new Employee
                        {
                            ID = (Guid)reader["ID"],
                            EmployeeID = (string)reader["EmployeeID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Manager = reader["Manager"].GetType() != typeof(DBNull) ? (Guid)reader["Manager"] : Guid.Empty
                        };

                        managerList.Add(manager);
                    }
                }
            }

            return managerList;
        }

        public static List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();

            using (var cn = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=S-Squared-Enterprises;Trusted_Connection=True;TrustServerCertificate=True"))
            {
                var cmd = new SqlCommand("GetAllEmployees", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            ID = (Guid)reader["ID"],
                            EmployeeID = (string)reader["EmployeeID"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Manager = reader["Manager"].GetType() != typeof(DBNull) ? (Guid)reader["Manager"] : Guid.Empty
                        };

                        employees.Add(GetEmployeeRoles(employee));
                    }
                }
            }   
            
            return employees;
        }
    }
}
