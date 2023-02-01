using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduller_Server
{
    class Remote_Sheduller:MarshalByRefObject,Remote_Scheduller_Interface.RemoteIR
    {

        Remote_Sheduller()
        {
            Console.WriteLine("Remote Server Object Created");
        }

        //~Remote_Sheduller()
        //{
        //    conn.Close();
        //}
        string s;
        public MySqlDataReader reader;
        public MySqlCommand sda;
        public MySqlConnection conn;

        string connect = "datasource = localhost; port = 3306;Connection Timeout=30; Min Pool Size=20; Max Pool Size=2000;  username = root; password = ;";

        public void db_Connection(string date, int number_of_Line)
        {
            //string connect = "datasource = 127.0.0.1; port = 3306;Connection Timeout=30; Min Pool Size=20; Max Pool Size=200;  username = root; password = ;";
            conn = new MySqlConnection(connect);
            //MessageBox.Show("onnection Developed");
        }


        public string reading_left_date(string date,int number_of_line)
        {
            sda = new MySqlCommand(@"select * from shedulling.tablelayout1 where date = '" + date + "' AND line = '" + number_of_line.ToString() + "'", conn);
            //int r = tableLayoutPanel1.RowCount;
            string a, b, c, d, e, f, g, h, i, j, k,l; // storing data from database
            try
            {
                conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {
                    a = reader.GetString(2);
                    b = reader.GetString(3);
                    c = reader.GetString(4);
                    d = reader.GetString(5);
                    e = reader.GetString(6);
                    f = reader.GetString(7);
                    g = reader.GetString(8);
                    h = reader.GetString(9);
                    i = reader.GetString(10);
                    j = reader.GetString(11);
                    k = reader.GetString(12);
                    l = reader.GetString(13);
                    s = a + "@" + b + "@" + c + "@" + d + "@" + e + "@" + f + "@" + g + "@" + h + "@" + i + "@" + j + "@" + k + "@"+l;
                    //MessageBox.Show(s);
               }
                conn.Close();
                conn.Dispose();
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return s;
        }

        //All Funtions related to Database

        public string reading_right_date(string date, int number_of_line)
        {
            sda = new MySqlCommand(@"select * from shedulling.tablelayout1 where date = '" + date + "' AND line = '" + number_of_line.ToString() + "'", conn);
            string a, b, c, d, e, f, g, h, i, j, k,l; // storing data from database
            try
            {
                conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {
                    a = reader.GetString(2);
                    b = reader.GetString(3);
                    c = reader.GetString(4);
                    d = reader.GetString(5);
                    e = reader.GetString(6);
                    f = reader.GetString(7);
                    g = reader.GetString(8);
                    h = reader.GetString(9);
                    i = reader.GetString(10);
                    j = reader.GetString(11);
                    k = reader.GetString(12);
                    l = reader.GetString(13);

                    s = a + "@" + b + "@" + c + "@" + d + "@" + e +  "@" + f + "@ " + g + "@" + h + "@" + i + "@" + j + "@" + k + "@"+l+"@";
                }
                if(reader.Read() == null)
                {
                    s = "";
                }
                conn.Close();
                conn.Dispose();
                reader.Close();   
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return s;
        }

        public void add_date_firstDay(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe)
        {
            DateTime dt = Convert.ToDateTime(date);
            //string connect = "datasource = 127.0.0.1; port = 3306;Connection Timeout=30; Min Pool Size=20; Max Pool Size=200;  username = root; password = ;";
            MySqlConnection conn = new MySqlConnection(connect);
            MySqlCommand sda = new MySqlCommand(@"insert into shedulling.tablelayout1 values
                    ('" + dt + "','" + line + "','" + first + "','" + sec + "','" + thi + "','" + four + "','" + fiv + "','" + six + "','" + sev + "','" + eig + "','" + nin + "','" + ten + "', '" + ele + "','"+twe+ "')", conn);
                    


            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
                    }
        
        public void update_first_table_data(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe)
        {
            string da = date;
            int l = line;
            //string connect = "datasource = 127.0.0.1; port = 3306;Connection Timeout=30; Min Pool Size=20; Max Pool Size=200;  username = root; password = ;";
            MySqlConnection conn = new MySqlConnection(connect);

            MySqlCommand sda = new MySqlCommand(@"Update shedulling.tablelayout1 set col1 ='" + first + "', col2 ='" + sec + "',col3 ='" + thi + "',col4 ='" + four + "',col5 ='" + fiv + "', col6 ='" + six + "',col7 ='" + sev + "',col8 ='" + eig + "',col9 ='" + nin + "', col10 ='" + ten + "',col11 ='" + ele + "' ,col12='" + twe + "'where date = '" + da + "' AND line = '" + l.ToString() + "';", conn);

            MySqlDataReader reader;
            try
            {
                conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
                conn.Close();
                conn.Dispose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
      }
        Boolean found = false;
        Boolean date_of_Table2 = false;

        string right_table;
        string lll;

        string ll;
        string first_table;
        //searching for date of left day if it already exist in database
        public bool search_date_of_left_day(string date, int li)
        {
            try
            {
                first_table = null;
                ll = null;
               // MessageBox.Show("Searching date = "+date);
                //string connect = "datasource = 127.0.0.1; port = 3306;Connection Timeout=30; Min Pool Size=20; Max Pool Size=200;  username = root; password = ;";
                MySqlConnection conn = new MySqlConnection(connect);
                //Query to select Maximum ID from Database;
                MySqlCommand sda1 = new MySqlCommand(@"Select DATE_FORMAT(date,'%Y-%m-%d'),line,col1,col2,col3,col4,col5,col6,col7,col8,col9,col10,col11,col12 from shedulling.tablelayout1 where date = '" + date + "'ORDER BY line limit 1", conn);
                MySqlDataReader reader;
                //openingthe Connection
                conn.Open();
                //executing the querey;
                reader = sda1.ExecuteReader();

                //read data from database untill the read object has no data
                while (reader.Read())
                {
                    first_table = reader.GetString(0);
                    ll = reader.GetString(1);
                   // MessageBox.Show(first_table + " AND " + ll);
                }
                conn.Close();
                conn.Dispose();
                reader.Close();
               // MessageBox.Show("Connection is closed");
                if (first_table != null)
                {
                    if (first_table.Equals(date) && Convert.ToInt32(ll) == li)
                    {
                        date_of_Table2 = true;
                        Console.WriteLine("On Server data found for " + date);
                        //return date_of_Table2;
                    }
                    else
                    {
                        date_of_Table2 = false;
                        Console.WriteLine("On Server data found for " + date);
                        //return date_of_Table2;
                    }                    
                }
                else
                {
                    date_of_Table2 = false;
                    Console.WriteLine("On Server data could not found for " + date);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }


            return date_of_Table2;
        }


        string db_user, db_pass;
        public Boolean user_connection(string user, string pass)
        {
            try
            {
                //string connect = "datasource = 127.0.0.1; port = 3306;Connection Timeout=30; Min Pool Size=20; Max Pool Size=200;  username = root; password = ;";
                MySqlConnection conn = new MySqlConnection(connect);
                //Querey to select username and password
                MySqlCommand sda = new MySqlCommand(@"Select Username,password from shedulling.users where Username = '" + user + "' ", conn);
                MySqlDataReader reader;
                conn.Open();
                reader = sda.ExecuteReader();
                int count = 0;
                //Reading the Username and Password from database
                //if the username and password are found mor than 1 or less than 0
                //then it will not allow you to enter to the software 
                while (reader.Read())
                {
                    db_user = reader.GetString(0);
                    db_pass = reader.GetString(1);
                }
                conn.Close();
                conn.Dispose();
                reader.Close();
                if (db_user.Equals(user) && db_pass.Equals(pass))
                {
                    return true;
                }
                else if (count > 1)
                {
                    //MessageBox.Show("User name Duplicated");
                    return false;
                }
                else
                {
                    //MessageBox.Show("User not exist");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }

}
