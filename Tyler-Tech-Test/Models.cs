using System;
namespace Tyler_Tech_Test
{
    public class Employee
    {
        public Employee()
        {
            Roles = new List<string>();
        }

        public Guid ID;
        public required string EmployeeID;
        public required string FirstName;
        public required string LastName;
        public Guid Manager;
        public List<string> Roles;
    }

    public class Role
    {
        public Guid ID;
        public required string Name;
    }
}

