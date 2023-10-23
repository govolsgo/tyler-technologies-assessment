using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tyler_Tech_Test.Pages
{
    public class add_employeeModel : PageModel
    {
        private DAL dal = new DAL();

        public List<Role> roleList;
        public List<Employee> employeeList;

        public void OnGet()
        {
            roleList = dal.RoleList;
            employeeList = DAL.GetAllEmployees();
        }

        public void OnPostAddNewEmployee(Employee employee)
        {
            dal.AddEmployee(employee);
        }
    }
}
