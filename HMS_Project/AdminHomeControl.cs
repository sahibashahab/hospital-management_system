using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace HMS_Project
{
    public partial class AdminHomeControl : UserControl
    {
        public AdminHomeControl()

        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CRUDDoctor cRUDDoctor = new CRUDDoctor();
            controlClass.ShowControl(cRUDDoctor, Content);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PatientCRUD cRUDPatient = new PatientCRUD();
            controlClass.ShowControl(cRUDPatient, Content);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InpatientUserControl inpatientUserControl = new InpatientUserControl();
            controlClass.ShowControl(inpatientUserControl, Content);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RoomCRUD roomCRUD = new RoomCRUD();
            controlClass.ShowControl(roomCRUD, Content);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CrudEmployee CRUDemp = new CrudEmployee();
            controlClass.ShowControl(CRUDemp, Content);

        }

        private void buttonAppointments_Click(object sender, EventArgs e)
        {
            DoctorsAppointmentUserControl doctorsAppointmentUserControl = new DoctorsAppointmentUserControl();
            controlClass.ShowControl(doctorsAppointmentUserControl, Content);
        }

        private void Content_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
