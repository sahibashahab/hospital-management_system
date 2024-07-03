using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class Info
    {
        protected Info()
        {

        }
        protected Info(string iD, string name, string email, string pass, string gender, string tel, string address)
        {
            ID = iD;
            Name = name;
            Email = email;
            Pass = pass;
            Gender = gender;
            Tel = tel;
            Address = address;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Gender { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public static string Role { get; set; }
    }

}
