using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DAL;


namespace BLL
{
    public class connection
    {


        public string servername;
        public string database;
        public string serverusername;
        public string serverpass;

        protected DAL.Database dalObj;
        protected string storedProcedure;
        public void connect(string serverName, string Database, string serverUsername, string serverPass)
        {
            DAL.Database dalObj = new DAL.Database();
            dalObj.connect(serverName, Database, serverUsername, serverPass);
            dalObj.OpenConnection();
            MessageBox.Show("Successfully! Connected.....");
            dalObj.CloseConnection();
        }

        public void check()
        {
            string path = "server_con.txt";
            string line = "";
            int i = 0;
            if (!File.Exists(path))
            {
                servername = Properties.Settings.Default.servername;
                database = Properties.Settings.Default.databasename;
                serverusername = Properties.Settings.Default.username;
                serverpass = Properties.Settings.Default.serverpass;
            }
            else
            {
                StreamReader sr = new StreamReader(path);

                while (line != null)
                {
                    i++;
                    line = sr.ReadLine();
                    if (i == 1)
                    {
                        servername = line;
                    }
                    else if (i == 2)
                    {
                        database = line;
                    }
                    else if (i == 3)
                    {
                        serverusername = line;
                    }
                    else if (i == 4)
                    {
                        serverpass = line;
                    }

                    Properties.Settings.Default.servername = servername;
                    Properties.Settings.Default.databasename = database;
                    Properties.Settings.Default.username = serverusername;
                    Properties.Settings.Default.serverpass = serverpass;

                    Properties.Settings.Default.Save();
                }
            }
        }

        
        public bool log_check { get; set; }
        public virtual void log(string username, string pass)
        {
            dalObj = new DAL.Database();
            dalObj.OpenConnection();
            bool isMatch;

                dalObj.LoadSpParameters(storedProcedure, username, pass);
                dalObj.ExecuteQuery();
                isMatch = dalObj.GetDataReader().HasRows;
                
            
            dalObj.UnLoadSpParameters();
                dalObj.CloseConnection();
            
            
            if (isMatch)
            {
                log_check = true;
            }
            else
            {
                log_check = false;
            }

        }
    }
}
