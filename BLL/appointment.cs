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
    public class appointment
    {

        public string AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TimeslotID { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DataTable doctorAppointment()
        {
            Database db = new Database();
            db.OpenConnection();
            db.ExecuteValue("select AID,DOC_NAME,PAT_NAME,CHECKUP_DATE,slotstart,slotend,DOCTORS.PricePerAppointment from APPOINTMENT inner join timeSlots on APPOINTMENT.Slot_ID = timeSlots.id inner join PATIENTS on APPOINTMENT.PAT_CODE = PATIENTS.ID inner join DOCTORS on DOCTORS.ID = APPOINTMENT.DOC_CODE");
            db.ExecuteQuery();
            DataTable data = db.GetDataTable();
            db.UnLoadSpParameters();
            db.CloseConnection();

            return data;
        }

        public DataTable timeslotlist(int docid)
        {
            Database db = new Database();
            try
            {
                db.OpenConnection();
                db.ExecuteValue("select id,Concat(slotstart,' ' ,slotend) as slotdec from timeSlots where slotdocid = " + docid + " and isavailable = 1");

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable data = db.GetDataTable();
            db.UnLoadSpParameters();
            db.CloseConnection();
            return data;
        }
        

        public int gettimeslotidfromappointment(string apid)
        {
            Database db = new Database();
            try
            {
                db.OpenConnection();
                db.ExecuteValue("select Slot_ID from APPOINTMENT where AID = '" + apid + "'");

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable dataTable = db.GetDataTable();
            db.UnLoadSpParameters();
            db.CloseConnection();

            return Convert.ToInt32(dataTable.Rows[0][0].ToString());
        }

        public void updatetimeslotavailability(int id, int yesorno)
        {
            Database db = new Database();
            try
            {
                db.OpenConnection();
                db.ExecuteValue("UPDATE timeSlots SET isavailable = " + yesorno + "  WHERE id ='" + id+"'");

                int a = db.ExecuteQuery();
                if (a > 0)
                {
                    MessageBox.Show("Data Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Unable to updated Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                db.CloseConnection();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateappoint(string Id,DateTime check,int slot, int pat_id, int doc_id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_UpdateDoctor", Id, check,slot,pat_id,doc_id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }

        public void insertappoint(DateTime check, int slot, int pat_id, int doc_id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_InsertAppointment", check, slot, pat_id, doc_id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        public void deleteappoint(string _id)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.OpenConnection();
            dalObj.LoadSpParameters("sp_DeleteAppointment", _id);
            dalObj.ExecuteQuery();
            dalObj.UnLoadSpParameters();
            dalObj.CloseConnection();

        }
        
        public DataTable patientList()
        {
            Database db = new Database();
            try
            {
                db.OpenConnection();
                db.ExecuteValue("SELECT * FROM PATIENTS ORDER BY PAT_NAME");

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable data = db.GetDataTable();
            db.UnLoadSpParameters();
            db.CloseConnection();
            return data;
        }

        public DataTable doctorList()
        {
            Database db = new Database();
            try
            {
                db.OpenConnection();
                db.ExecuteValue("SELECT * FROM DOCTORS ORDER BY DOC_NAME");

            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            DataTable data = db.GetDataTable();
            db.UnLoadSpParameters();
            db.CloseConnection();
            return data;
        }
    }
}
