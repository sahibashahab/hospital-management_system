using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BLL
{
    public class patient : Info
    {
        public int CreatedBy { get; set; }
        public DateTime Date { get; set; }
        public patient() : base() { }
        public patient(string iD, string name, string email, string pass, string gender, string tel, string address, int createdby, DateTime date) : base(iD, name, email, pass, gender, tel, address)
        {
            this.CreatedBy = createdby;
            this.Date = date;
        }

        public void insertpatient(string _pName, string _Tel, string _Email, string _gender, string _Address, int CreatedBy, DateTime date)
        {
            operation op = new operation();
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_InsertPatient", _pName, _Tel, _Email, _gender, _Address, CreatedBy, date);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void deletepatient(int _id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_DeletePatient", _id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void updatepatient(string Id, string _pName, string _Tel, string _Email, string _gender, string _Address, int CreatedBy, DateTime date)
        {
            operation op = new operation();
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_UpdatePatient", Id, _pName, _Tel, _Email, _gender, _Address, CreatedBy, date);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }

        public DataTable Selectpatient()
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_SelectPaitents");
            dalObj.ExecuteQuery();
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

            return data;
        }
    }
}
