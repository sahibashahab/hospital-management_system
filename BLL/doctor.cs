using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DAL;
using System.Windows.Forms;

namespace BLL
{
    public class doctor : connection
    {
        public int ID;
        public string deptid;
        public string Role;
        public string Username;
        public override void log(string username, string pass)
        {
            storedProcedure = "sp_LogDoctor";
            base.log(username, pass);
            Username = username;
        }

        public int GetDocId()
        {
            Database dalObj = new Database();
            dalObj.OpenConnection();
            ID = Convert.ToInt32(dalObj.ExecuteValue("select ID from DOCTORS where DOC_EMAIL = 'hiba1@gmail.com'"));
            //dalObj.ExecuteQuery();


            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return ID;
        }

        public void insertDoc(string _pName, string _Department, string _Tel, string _Email, string _pass, string _gender, string _Address, string _Designation, int _PricePerAppointment)
        {
            operation op = new operation();
            int Deptid = op.GetIDbyName("Department", _Department, "DepartmentName", "ID");
            int Desgid = op.GetIDbyName("DoctorRoles", _Designation, "Rolename", "ID");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_InsertDoctor", _pName, Deptid, _Tel, _Email, _pass, _gender, _Address, Desgid, _PricePerAppointment);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void deleteDoc(int _id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_DeleteDoctor", _id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void updateDoc(string Id, string _pName, string _Department, string _Tel, string _Email, string _pass, string _gender, string _Address, string _Designation, int _PricePerAppointment)
        {
            operation op = new operation();
            int Deptid = op.GetIDbyName("Department", _Department, "DepartmentName", "ID");
            int Desgid = op.GetIDbyName("DoctorRoles", _Designation, "Rolename", "ID");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_UpdateDoctor", Id, _pName, Deptid, _Tel, _Email, _pass, _gender, _Address, Desgid, _PricePerAppointment);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }

        public DataTable SelectDoc()
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


        public DataTable DepartmentList()
        {
            Database dalObj = new Database();
            try
            {
                dalObj.OpenConnection();
                dalObj.ExecuteValue("SELECT * FROM Department ORDER BY DepartmentName");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return data;
        }

        public DataTable RoleList()
        {
            Database dalObj = new Database();
            try
            {
                dalObj.OpenConnection();
                dalObj.ExecuteValue("SELECT * FROM DoctorRoles ORDER BY Rolename");
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return data;
        }

        public DataTable getEmployeeDetail(string id)
        {
            Database dalObj = new Database();
            
            dalObj.OpenConnection();
            dalObj.ExecuteValue(@"SELECT Doctors.ID,Doctors.DOC_ID,Doctors.DOC_NAME,Department.DepartmentName,Doctors.DOC_TEL,Doctors.DOC_EMAIL,Doctors.DOC_PASS,Doctors.DOC_GENDER,Doctors.DOC_ADDRESS,DoctorRoles.Rolename,DOCTORS.PricePerAppointment from Doctors 
                                                        inner join Department on Department.ID = Doctors.DOC_DEP_ID
                                                        inner join DoctorRoles on DoctorRoles.ID = Doctors.DOC_Role_ID WHERE Doctors.ID = " + int.Parse(id) + "");
            
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return data;
        }


        public DataTable doctosPatient(string docID)
        {
            Database dalObj = new Database();

            dalObj.OpenConnection();
            dalObj.ExecuteValue("SELECT PAT_ID, PAT_NAME, PAT_GENDER, PAT_TEL, PAT_EMAIL, PAT_ADDRESS FROM PATIENTS INNER JOIN APPOINTMENT ON APPOINTMENT.PAT_CODE =PATIENTS.ID  where  DOC_CODE = '" + docID + "'");

            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return data;
        }
        public DataTable GetAppointmentofDoctor(int docid)
        {
            Database dalObj = new Database();

            dalObj.OpenConnection();
            dalObj.ExecuteValue(@"select slotstart as Start_Timings,slotend as End_Timings,CHECKUP_DATE,PATIENTS.PAT_NAME,DOCTORS.DOC_NAME from APPOINTMENT inner join timeSlots on timeSlots.id =Slot_ID
                                                   inner join DOCTORS on DOCTORS.ID = DOC_CODE inner join PATIENTS on PATIENTS.ID = PAT_CODE where APPOINTMENT.DOC_CODE = " + docid + "");


            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return data;
        }

        public void insertDoc(string _pName, string _Department, string _Tel, string _Email, string _pass, string _gender, string _Address, string _Designation, int _PricePerAppointment)
        {
            operation op = new operation();
            int Deptid = op.GetIDbyName("Department", _Department, "DepartmentName", "ID");
            int Desgid = op.GetIDbyName("DoctorRoles", _Designation, "Rolename", "ID");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_InsertDoctor", _pName, Deptid, _Tel, _Email, _pass, _gender, _Address, Desgid, _PricePerAppointment);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void deleteDoc(int _id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_DeleteDoctor", _id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void updateDoc(string Id, string _pName, string _Department, string _Tel, string _Email, string _pass, string _gender, string _Address, string _Designation, int _PricePerAppointment)
        {
            operation op = new operation();
            int Deptid = op.GetIDbyName("Department", _Department, "DepartmentName", "ID");
            int Desgid = op.GetIDbyName("DoctorRoles", _Designation, "Rolename", "ID");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_UpdateDoctor", Id, _pName, Deptid, _Tel, _Email, _pass, _gender, _Address, Desgid, _PricePerAppointment);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }

        public DataTable SelectDoc()
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
