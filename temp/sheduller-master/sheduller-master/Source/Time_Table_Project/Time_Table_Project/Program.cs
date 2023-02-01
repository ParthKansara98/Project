using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Table_Project
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
        /*static public System.Timers.Timer MyKillTimer = new System.Timers.Timer();

        protected void Application_Start(Object sender, EventArgs e)
        {
            try
            {
                MyKillTimer.Interval = 180000; // check sleeping connections every 3 minutes
                MyKillTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyKillTimer_Event);
                MyKillTimer.AutoReset = true;
                MyKillTimer.Enabled = true;
            }
            catch (Exception excep)
            {
            }
        }
        static public int KillSleepingConnections(int iMinSecondsToExpire)
        {
            string strSQL = "show processlist";
            System.Collections.ArrayList m_ProcessesToKill = new System.Collections.ArrayList();

            OdbcConnection myConn = new OdbcConnection(Global.strDBServer);
            OdbcCommand myCmd = new OdbcCommand(strSQL, myConn);
            OdbcDataReader MyReader = null;

            try
            {
                my//conn.Open();

                // Get a list of processes to kill.
                MyReader = myCmd.ExecuteReader();
                while (MyReader.Read())
                {
                    // Find all processes sleeping with a timeout value higher than our threshold.
                    int iPID = Convert.ToInt32(MyReader["Id"].ToString());
                    string strState = MyReader["Command"].ToString();
                    int iTime = Convert.ToInt32(MyReader["Time"].ToString());

                    if (strState == "Sleep" && iTime >= iMinSecondsToExpire && iPID > 0)
                    {
                        // This connection is sitting around doing nothing. Kill it.
                        m_ProcessesToKill.Add(iPID);
                    }
                }

                MyReader.Close();

                foreach (int aPID in m_ProcessesToKill)
                {
                    strSQL = "kill " + aPID;
                    myCmd.CommandText = strSQL;
                    myCmd.ExecuteNonQuery();
                }
            }
            catch (Exception excep)
            {
            }
            finally
            {
                if (MyReader != null && !MyReader.IsClosed)
                {
                    MyReader.Close();
                }

                if (myConn != null && myConn.State == ConnectionState.Open)
                {
                    my//conn.Close();
                }
            }

            return m_ProcessesToKill.Count;
        }
        private void MyKillTimer_Event(object source, System.Timers.ElapsedEventArgs e)
        {
            KillSleepingConnections(30);
        } */


    }
}
