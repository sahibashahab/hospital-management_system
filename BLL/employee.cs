using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace BLL
{
    public class employee : connection
    {
        public override void log(string username, string pass)
        {
            storedProcedure = "sp_LogEmployee";
            base.log(username, pass);
        }

        public void insertEmp(string _pName, string _gender, string _pass, string _Department, string _Tel, string _Email, string _Address, string _Designation)
        {
            operation op = new operation();
            int Deptid = op.GetIDbyName("Department", _Department, "DepartmentName", "ID");
            int Desgid = op.GetIDbyName("EmployeeRoles", _Designation, "RoleName", "ID");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_InsertEmployee", _pName, _gender, _pass, Deptid, _Tel, _Email, _Address, Desgid);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void deleteEmp(int _id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_DeleteEmployee", _id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void updateEmp(string Id, string _pName, string _gender, string _pass, string _Department, string _Tel, string _Email, string _Address, string _Designation)
        {
            operation op = new operation();
            int Deptid = op.GetIDbyName("Department", _Department, "DepartmentName", "ID");
            int Desgid = op.GetIDbyName("EmployeeRoles", _Designation, "RoleName", "ID");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_UpdateEmployee", Id, _pName, _gender, _pass, Deptid, _Tel, _Email, _Address, Desgid);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }

        public DataTable SelectEmp()
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_GetDoctors");
            dalObj.ExecuteQuery();
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

            return data;
        }

    }
}
