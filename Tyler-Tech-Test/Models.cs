using System;
namespace Tyler_Tech_Test
{
    public class Employee
    {
        public Guid ID;
        public string EmployeeID;
        public string FirstName;
        public string LastName;
        public Guid Manager;
        public List<string> Roles;
    }

    public class Role
    {
        public Guid ID;
        public string Name;
    }
}

