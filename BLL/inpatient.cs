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
    public class inpatient
    {
        public DataTable displayInPat()
        {
            Database dalObj = new Database();
            dalObj.OpenConnection();
            dalObj.ExecuteValue("SELECT INPATIENT.ID, PATIENTS.ID As Patient_ID, PAT_NAME, PAT_TEL, DATE_OF_AD, DATE_OF_DIS, ROOM.ID AS ROOM_NUMBER, ROOM_TYPE, TotalAmount FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE");
            DataTable dataTable = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return dataTable;
        }

        public DataTable roomList(string roomType)
        {
            Database dalObj = new Database();
            
            try
            {
                dalObj.OpenConnection();
                dalObj.ExecuteValue("SELECT * FROM ROOM WHERE ID not in (SELECT ROOM_CODE FROM INPATIENT INNER JOIN PATIENTS ON PATIENTS.ID = PAT_CODE INNER JOIN ROOM ON ROOM.ID = ROOM_CODE where DATE_OF_DIS >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ) AND ROOM_TYPE = '" + roomType + "'");
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


        public int PatID { get; set; }
        public int InPatID { get; set; }
        public string RoomNo { get; set; }
        public DateTime Admission { get; set; }
        public DateTime Discharge { get; set; }
        public int TotalAmount { get; set; }


        public void insertPAT(DateTime DATE_OF_AD, DateTime DATE_OF_DIS, int pat_code, string room, int amount)
        {
            operation op = new operation();
            int room_code = op.GetIDbyName("ROOM", room, "ROOM_TYPE", "ROOM_NO");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_InsertInpatient", DATE_OF_AD,DATE_OF_DIS,pat_code,room_code,amount);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }

        public void updatePAT(int id,DateTime DATE_OF_AD, DateTime DATE_OF_DIS, int pat_code, string room, int amount)
        {
            operation op = new operation();
            int room_code = op.GetIDbyName("ROOM", room, "ROOM_TYPE", "ROOM_NO");
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_UpdateInpatient",id, DATE_OF_AD, DATE_OF_DIS, pat_code, room_code, amount);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void deletepat(int _id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_DeleteInpatient", _id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }


        public bool ifinpatientalreadyexisted(int pid, DateTime stdate)
        {
            
            DataTable dt = new DataTable();
            dt = displayInPat();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (pid == Convert.ToInt32(dt.Rows[i][1].ToString()))
                {
                    if (stdate > Convert.ToDateTime(dt.Rows[i]["DATE_OF_DIS"]))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

            }

            return true;
        }

        public DataTable patientList()
        {
            Database dalObj = new Database();
            
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_SelectPaitents");
            dalObj.ExecuteQuery();
            
            DataTable dataTable = dalObj.GetDataTable();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();
            return dataTable;
        }

    }
}
