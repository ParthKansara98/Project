using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Table_Project
{
    class Database_Connection
    {
        dataBase db = new dataBase();
        public Boolean user_connection(string user, string pass)
        {
            try
            {
                //Connecting to database
                //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
                //MySqlConnection conn = new MySqlConnection(connect);
                //Querey to select username and password
                MySqlConnection conn = db.GetConnection();
                MySqlCommand sda = new MySqlCommand(@"Select * from shedulling.users where Username = '" + user + "' and password = '" + pass + ";'", conn);
                MySqlDataReader reader;
                //conn.Open();
                reader = sda.ExecuteReader();
                int count = 0;
                //Reading the Username and Password from database
                //if the username and password are found mor than 1 or less than 0
                //then it will not allow you to enter to the software 
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count <= 1)
                {
                    conn.Close();
                    return true;
                }
                else if (count > 1)
                {
                    MessageBox.Show("User name Duplicated");
                    conn.Close();
                    return false;
                }
                else
                {
                    MessageBox.Show("User not exist");
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
                return false;
            }
        }

        public void store_data_in_DB()
        {
 
        }
    }
}
