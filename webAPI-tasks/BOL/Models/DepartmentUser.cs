using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL.Models
{
    public class DepartmentUser
    {
        public DepartmentUser()
        {
            HourForDepartments = new List<HourForDepartment>();
            Users = new List<User>();
        }
        public int Id { get; set; }
        public string Status { get; set; }
        public  List<HourForDepartment> HourForDepartments { get; set; }
        public List<User>Users { get; set; }

    }
}
