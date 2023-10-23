using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Tyler_Tech_Test.Pages
{
	public class subordinate_viewerModel : PageModel
    {
        public List<Employee> managerList;

        public void OnGet()
        {
            managerList = DAL.GetManagers();
        }

        public IActionResult OnPostGetSubordinates(Guid Manager)
        {
            var employees = DAL.GetEmployeesByManager(Manager);

            return new JsonResult(employees);
        }
    }
}
