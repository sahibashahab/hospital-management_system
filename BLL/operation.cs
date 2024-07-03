using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Windows.Forms;

namespace BLL
{
    public class operation
    {
        

        public void Showincbx(ComboBox cbx, string tablename, string colname)
        {
            cbx.Items.Clear();
            Database dalObj = new Database();
            dalObj.OpenConnection();
            dalObj.ExecuteValue("SELECT * FROM " + tablename);
            dalObj.ExecuteQuery();
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                cbx.Items.Add(data.Rows[i][colname]);
            }

        }

        public DataTable display(string value)
        {
            Database dalObj = new Database();
            dalObj.OpenConnection();
            //if (value == "PATIENTS")
            //{
            //    sqlDataAdapter = new SqlDataAdapter("SELECT PATIENTS.ID, PAT_NAME, DOC_NAME, PAT_GENDER, PAT_TEL, PAT_EMAIL, PAT_ADDRESS, DOC_DESIG, DOC_DEPART FROM PATIENTS INNER JOIN DOCTORS ON DOC_CODE=DOCTORS.ID", sqlConnection);
            //}
            /*else */
            if (value == "DOCTORS")
            {
                dalObj.ExecuteValue(@"SELECT Doctors.ID,Doctors.DOC_ID,Doctors.DOC_NAME,Department.DepartmentName,Doctors.DOC_TEL,Doctors.DOC_EMAIL,Doctors.DOC_GENDER,Doctors.DOC_ADDRESS,DoctorRoles.Rolename,DOCTORS.PricePerAppointment from Doctors 
                                                        inner join Department on Department.ID = Doctors.DOC_DEP_ID
                                                        inner join DoctorRoles on DoctorRoles.ID = Doctors.DOC_Role_ID");

            }
            else if (value == "EMPLOYEE")
            {
                dalObj.ExecuteValue("select EMPLOYEE.ID,EMPLOYEE.EMP_ID,EMPLOYEE.EMP_NAME,EMPLOYEE.EMP_GENDER,EMPLOYEE.EMP_PASS,Department.DepartmentName,EMPLOYEE.EMP_TEL,EMPLOYEE.EMP_EMAIL,EMPLOYEE.EMP_ADDRESS,EmployeeRoles.RoleName from Employee inner join Department on EMPLOYEE.DepartmentID = Department.ID inner join EmployeeRoles on EMPLOYEE.RoleID = EmployeeRoles.ID");

            }

            else
            {
                dalObj.ExecuteValue("SELECT * FROM " + value + "");
            }
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

            return data;
        }

        public int GetIDbyName(string tablename, string name, string colname, string colname2)
        {

            Database dalObj = new Database();
            dalObj.OpenConnection();
            dalObj.ExecuteValue("SELECT * FROM " + tablename + " where " + colname + "=" + "'" + name + "'");
            DataTable data = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return Convert.ToInt32(data.Rows[0][colname2].ToString());
        }
        public DataTable search(string tableValue, string searchValue, string searchByValue)
        {
            Database dalObj = new Database();
            try
            {
                dalObj.OpenConnection();
                if (tableValue == "DOCTORS")
                {
                    if (searchByValue.ToLower() == "doctor id")
                    {
                        dalObj.ExecuteValue("SELECT * FROM " + tableValue + " WHERE DOC_ID LIKE '%" + searchValue + "%'");
                    }
                    else if (searchByValue.ToLower() == "number")
                    {
                        dalObj.ExecuteValue("SELECT * FROM " + tableValue + " WHERE DOC_TEL LIKE '%" + searchValue + "%'");
                    }

                    else
                    {
                        dalObj.ExecuteValue("SELECT * FROM " + tableValue + " WHERE DOC_NAME LIKE '%" + searchValue + "%'");
                    }
                }
                else if (tableValue == "PATIENTS")
                {
                    if (searchByValue.ToLower() == "patient name")
                    {
                        dalObj.ExecuteValue("SELECT * FROM PATIENTS WHERE PAT_NAME LIKE '%" + searchValue + "%'");
                    }

                    else if (searchByValue.ToLower() == "patient number")
                    {
                        dalObj.ExecuteValue("SELECT * FROM PATIENTS WHERE PAT_TEL LIKE '%" + searchValue + "%'");
                    }

                    else
                    {
                        dalObj.ExecuteValue("SELECT * FROM PATIENTS WHERE PAT_ID LIKE '%" + searchValue + "%'");
                    }
                }

                else if (tableValue == "EMPLOYEE")
                {
                    if (searchByValue.ToLower() == "name")
                    {
                        dalObj.ExecuteValue("select EMPLOYEE.ID,EMPLOYEE.EMP_ID,EMPLOYEE.EMP_NAME,EMPLOYEE.EMP_GENDER,EMPLOYEE.EMP_PASS,Department.DepartmentName,EMPLOYEE.EMP_TEL,EMPLOYEE.EMP_EMAIL,EMPLOYEE.EMP_ADDRESS,EmployeeRoles.RoleName from Employee inner join Department on EMPLOYEE.DepartmentID = Department.ID inner join EmployeeRoles on EMPLOYEE.RoleID = EmployeeRoles.ID WHERE EMP_NAME LIKE '%" + searchValue + "%'");
                    }

                    else if (searchByValue.ToLower() == "number")
                    {
                        dalObj.ExecuteValue("select EMPLOYEE.ID,EMPLOYEE.EMP_ID,EMPLOYEE.EMP_NAME,EMPLOYEE.EMP_GENDER,EMPLOYEE.EMP_PASS,Department.DepartmentName,EMPLOYEE.EMP_TEL,EMPLOYEE.EMP_EMAIL,EMPLOYEE.EMP_ADDRESS,EmployeeRoles.RoleName from Employee inner join Department on EMPLOYEE.DepartmentID = Department.ID inner join EmployeeRoles on EMPLOYEE.RoleID = EmployeeRoles.ID WHERE EMP_TEL LIKE '%" + searchValue + "%'");
                    }

                    else if (searchByValue.ToLower() == "email")
                    {
                        dalObj.ExecuteValue("select EMPLOYEE.ID,EMPLOYEE.EMP_ID,EMPLOYEE.EMP_NAME,EMPLOYEE.EMP_GENDER,EMPLOYEE.EMP_PASS,Department.DepartmentName,EMPLOYEE.EMP_TEL,EMPLOYEE.EMP_EMAIL,EMPLOYEE.EMP_ADDRESS,EmployeeRoles.RoleName from Employee inner join Department on EMPLOYEE.DepartmentID = Department.ID inner join EmployeeRoles on EMPLOYEE.RoleID = EmployeeRoles.ID WHERE EMP_EMAIL LIKE '%" + searchValue + "%'");
                    }

                    else if (searchByValue.ToLower() == "role")
                    {
                        dalObj.ExecuteValue("select EMPLOYEE.ID,EMPLOYEE.EMP_ID,EMPLOYEE.EMP_NAME,EMPLOYEE.EMP_GENDER,EMPLOYEE.EMP_PASS,Department.DepartmentName,EMPLOYEE.EMP_TEL,EMPLOYEE.EMP_EMAIL,EMPLOYEE.EMP_ADDRESS,EmployeeRoles.RoleName from Employee inner join Department on EMPLOYEE.DepartmentID = Department.ID inner join EmployeeRoles on EMPLOYEE.RoleID = EmployeeRoles.ID WHERE EMP_EMAIL LIKE '%" + searchValue + "%'");
                    }

                    else
                    {
                        dalObj.ExecuteValue("select EMPLOYEE.ID,EMPLOYEE.EMP_ID,EMPLOYEE.EMP_NAME,EMPLOYEE.EMP_GENDER,EMPLOYEE.EMP_PASS,Department.DepartmentName,EMPLOYEE.EMP_TEL,EMPLOYEE.EMP_EMAIL,EMPLOYEE.EMP_ADDRESS,EmployeeRoles.RoleName from Employee inner join Department on EMPLOYEE.DepartmentID = Department.ID inner join EmployeeRoles on EMPLOYEE.RoleID = EmployeeRoles.ID WHERE EMP_ID LIKE '%" + searchValue + "%'");
                    }
                }

                else if (tableValue == "ROOM")



                {
                    {
                    }
                    {
                    }
                    {
                    }
                    {
                    }
                }
                else if (tableValue == "INPATIENTS")
                {
                    if (searchByValue.ToLower() == "date of discharge")
                    {
                        dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE WHERE DATE_OF_DIS LIKE '%" + searchValue + "%'");
                    }
                    else if (searchByValue.ToLower() == "patient tel")
                    {
                        dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE WHERE PAT_TEL LIKE '%" + searchValue + "%'");
                    }
                    else if (searchByValue.ToLower() == "room no")
                    {
                        dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE WHERE ROOM_NO LIKE '%" + searchValue + "%'");
                    }
                    else if (searchByValue.ToLower() == "room type")
                    {
                        dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE WHERE ROOM_TYPE LIKE '%" + searchValue + "%'");
                    }
                    else if (searchByValue.ToLower() == "date of admission")
                    {
                        dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE WHERE DATE_OF_AD LIKE '%" + searchValue + "%'");
                    }
                    else
                    {
                        dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE WHERE PAT_NAME LIKE '%" + searchValue + "%'");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable dataTable = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return dataTable;
        }
    }
}
