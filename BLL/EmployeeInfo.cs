using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class EmployeeInfo : Info
    {
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Username { get; set; }
        public string Departmental_ID { get; set; }
        public EmployeeInfo() : base() { }

        public EmployeeInfo(string iD, string name, string email, string pass, string gender, string tel, string address, string doc_desig, string depart) : base(iD, name, email, pass, gender, tel, address)
        {
            this.Designation = doc_desig;
            this.Department = depart;



        }
    }
}
