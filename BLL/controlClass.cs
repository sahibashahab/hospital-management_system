using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BLL
{
    public class controlClass
    {
        public static void ShowControl(System.Windows.Forms.Control control, System.Windows.Forms.Control content)
        {
            content.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BringToFront();
            content.Controls.Add(control);
        }
    }
}
