using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DoctorInfo : Info
    {
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Departmental_ID { get; set; }

        public string Password { get; set; }

        public int PricePerAppointment { get; set; }

        public DateTime starttime { get; set; }

        public DateTime endtime { get; set; }
        public DoctorInfo() : base() { }
        public DoctorInfo(string iD, string name, string email, string pass, string gender, string tel, string address, string doc_desig, string depart) : base(iD, name, email, pass, gender, tel, address)
        {
            this.Designation = doc_desig;
            this.Department = depart;
        }
    }

}
