using System;
using Microsoft.Data.SqlClient;

namespace Tyler_Tech_Test
{
	public class DAL
	{
		public DAL()
		{
            static List<Employee> GetEmployeesByManager(Guid manager)
			{
                var employees = new List<Employee>();

                using (var cn = new SqlConnection("connection string would go here"))
                {
                    var cmd = new SqlCommand("GetEmployeesByManager", cn);
                    cmd.Parameters.AddWithValue("@Manager", manager);

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

            static Employee GetEmployeeRoles(Employee employee)
            {
                using (var cn = new SqlConnection("connection string would go here"))
                {
                    var cmd = new SqlCommand("GetRolesByEmployee", cn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

                    cn.Open();
                    var results = cmd.ExecuteReader();

                    if (results.HasRows)
                    {
                        while (results.Read())
                        {
                            employee.Roles.Add((string)results["RoleName"]);
                        }
                    }

                    return employee;
                }
            }

            static void AddEmployee(Employee employee)
            {
                using (var cn = new SqlConnection("connection string would go here"))
                {
                    var cmd = new SqlCommand("AddEmployee", cn);
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Manager", employee.Manager);

                    cn.Open();
                    var result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        AddRoles(employee);
                        return;
                    }
                    else
                        throw new Exception("Unable to add employee!");
                }
            }

            static void AddRoles(Employee employee)
            {
                using (var cn = new SqlConnection("connection string would go here"))
                {
                    cn.Open();

                    foreach (var role in employee.Roles)
                    {
                        var cmd = new SqlCommand("GetRoleIDByName", cn);
                        cmd.Parameters.AddWithValue("@RoleName", role);

                        var roleID = (Guid)cmd.ExecuteScalar();

                        cmd = new SqlCommand("AddRole");
                        cmd.Parameters.AddWithValue("@ID", Guid.NewGuid());
                        cmd.Parameters.AddWithValue("@UserID", employee.ID);
                        cmd.Parameters.AddWithValue("@RoleID", roleID);

                        var result = cmd.ExecuteNonQuery();

                        if (result != 1)
                            throw new Exception("Unable to add role!");
                    }
                }
            }
        }
	}
}
