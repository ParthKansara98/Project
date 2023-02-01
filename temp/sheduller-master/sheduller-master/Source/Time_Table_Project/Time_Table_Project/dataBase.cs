using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Table_Project
{
    class dataBase
    {
        string connect = "datasource = localhost; port = 3306;Connection Timeout=30; pooling=true; Max Pool Size=32767;username = root; password = ;";
        static int count = 0,closeCount=0;
       
        public MySqlConnection GetConnection()
        {
            MySqlConnection conn=null;
            try
            {
                conn = new MySqlConnection(connect);
               
                if (conn.State.ToString() == "Open")
                {
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    count++;
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
            return conn;
            
        }


        public void add_date_firstDay(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe)
        {
            MySqlConnection conn = GetConnection();
            if (line == 0)
            {
                if (sec == "")
                { sec = "****"; }
                if (thi=="")
                { thi = "****"; }
                if(four=="")
                { four = "****"; }
                if(fiv=="")
                { fiv = "****"; }
                if(six=="")
                { six = "****"; }
                if(sev=="")
                { sev = "****"; }
                if(eig=="")
                { eig = "****"; }
                if(nin=="")
                { nin = "****"; }
                if(ten=="")
                { ten = "****"; }
                if(ele=="")
                { ele = "****"; }
                
            }
            
            


                //GetConnection();
                MySqlCommand sda = new MySqlCommand(@"insert into shedulling.tablelayout1 values
                    ('" + date + "','" + line + "','" + first + "','" + sec + "','" + thi + "','" + four + "','" + fiv + "', '" + six + "','" + sev + "','" + eig + "','" + nin + "', '" + ten + "', '" + ele + "', '" + twe + "')", conn);

                MySqlDataReader reader;
                try
                {
                    //conn.Open();
                    reader = sda.ExecuteReader();
                    while (reader.Read())
                    {

                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                //conn.Close();
                conn.Close();
                closeCount++;
            
        }

        public void add_data_secondDay(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe)
        {
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"insert into shedulling.tablelayout1 values
                    ('" + date + "','" + line + "','" + first + "','" + sec + "','" + thi + "','" + four + "','" + fiv + "', '" + six + "','" + sev + "','" + eig + "','" + nin + "','" + ten + "','" + ele + "','"+twe+"')", conn);

            MySqlDataReader reader;
            try
            {
                //conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //conn.Close();
        }

        public void update_first_table_data(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe)
        {
            //DateTime dt = Convert.ToDateTime(date);
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            if (line == 0)
            {
                if (sec == "")
                { sec = "****"; }
                if (thi == "")
                { thi = "****"; }
                if (four == "")
                { four = "****"; }
                if (fiv == "")
                { fiv = "****"; }
                if (six == "")
                { six = "****"; }
                if (sev == "")
                { sev = "****"; }
                if (eig == "")
                { eig = "****"; }
                if (nin == "")
                { nin = "****"; }
                if (ten == "")
                { ten = "****"; }
                if (ele == "")
                { ele = "****"; }

            }
            MySqlCommand sda = new MySqlCommand(@"Update shedulling.tablelayout1 set date =
                    '" + date + "',line ='" + line + "',col1 ='" + first + "', col2 ='" + sec + "',col3 ='" + thi + "',col4 ='" + four + "',col5 ='" + fiv + "', col6 ='" + six + "',col7 ='" + sev + "',col8 ='" + eig + "',col9 ='" + nin + "', col10 ='" + ten + "',col11 ='" + ele + "' ,col12='"+twe+"'where date = '" + date + "' AND line ='" + line + "'", conn);
            MySqlDataReader reader;
            try
            {
                //conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {
                   
                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //conn.Close();
            conn.Close();
            closeCount++;
        }

        // prolem only in left side data updation :/
        public void update_secondtable_data(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe)
        {
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"Update shedulling.tablelayout1 set date =
                    '" + date + "',line ='" + line + "',col1 ='" + first + "', col2 ='" + sec + "',col3 ='" + thi + "',col4 ='" + four + "',col5 ='" + fiv + "', col6 ='" + six + "',col7 ='" + sev + "',col8 ='" + eig + "',col9 ='" + nin + "', col10 ='" + ten + "',col11 ='" + ele + "',col12='"+twe+"' where date = '" + date + "' AND line ='" + line + "'", conn);
            MySqlDataReader reader;
            try
            {
                //conn.Open();

                reader = sda.ExecuteReader();
                while (reader.Read())
                {

                }
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //conn.Close();
            conn.Close();
            closeCount++;
        }

        Boolean found = false;
        Boolean date_of_Table2 = false;
        string s;

        public string reading_left_date(string date, int number_of_Line)
        {
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"select * from shedulling.tablelayout1 where date = '" + date + "' AND line = '" + number_of_Line + "'", conn);
            MySqlDataReader reader;

            //int r = tableLayoutPanel1.RowCount;

            string a, b, c, d, e, f, g, h, i, j, k,l; // storing data from database
            try
            {
                //conn.Open();
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

                    s = a + " !" + b + " !" + c + " !" + d + " !" + e + " !" + f + "! " + g + " !" + h + "! " + i + " !" + j + " !" + k + " !"+l+"!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conn.Close();
            closeCount++;
            return s;
        }
        
        public string reading_rightt_date(string date, int number_of_Line)
        {
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"select * from shedulling.tablelayout1 where date = '" + date + "' AND line = '" + number_of_Line + "'", conn);
            MySqlDataReader reader;

            //int r = tableLayoutPanel1.RowCount;

            //int r = 0; // count rows
            //int cl = 11; // number of columns
            string a, b, c, d, e, f, g, h, i, j, k,l; // storing data from database
            try
            {
                //conn.Open();
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

                    s = a + " !" + b + " !" + c + " !" + d + " !" + e + " !" + f + "! " + g + " !" + h + "! " + i + " !" + j + " !" + k + " !"+l+"!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return s;
        }
        
        //searching for date of right day if it already exist in database
        public bool search_date_of_right_day(string date, int l)
        {
            // add Line integer as i did in finding dates in left date in db :)  
            string first_table;
            MySqlConnection conn = GetConnection();
            try
            {
                //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
                //MySqlConnection conn = new MySqlConnection(connect);
                //Query to select Maximum ID from Database;

                MySqlCommand sda = new MySqlCommand(@"Select DATE_FORMAT(date,'%Y-%m-%d'),line from shedulling.tablelayout1", conn);
                MySqlDataReader reader;
                //openingthe Connection
                //conn.Open();
                //executing the querey;
                reader = sda.ExecuteReader();
                //read data from database untill the read object has no data
                while (reader.Read())
                {
                    first_table = reader.GetString(0);
                    string ll = reader.GetString(1);
                    if (first_table == date && Convert.ToInt32(ll) == l)
                    {
                        found = true;
                        break;
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            conn.Close();
            closeCount++;
            return found;
        }

        //searching for date of left day if it already exist in database
        public bool search_date_of_left_day(string date, int li)
        {
            string first_table;
            
            MySqlConnection conn = GetConnection();
            try
            {
                //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
                //MySqlConnection conn = new MySqlConnection(connect);
                //Query to select Maximum ID from Database;

                MySqlCommand sda1 = new MySqlCommand(@"Select  DATE_FORMAT(date,'%Y-%m-%d'),line from shedulling.tablelayout1", conn);
                MySqlDataReader reader;
                //openingthe Connection
              
                    //conn.Open();

                
               
                //executing the querey;
                reader = sda1.ExecuteReader();

                //read data from database untill the read object has no data
                while (reader.Read())
                {
                    first_table = reader.GetString(0);
                    first_table = string.Format("{0:yyyy-MM-dd}", first_table);
                    string ll = reader.GetString(1);
                    if (first_table == date && Convert.ToInt32(ll) == li)
                    {
                        date_of_Table2 = true;
                        break;
                    }
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            conn.Close();
            closeCount++;
            return date_of_Table2;

        }

        // adding names to database
        public void adding_names()
        {
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"insert into shedulling.names values
                    (' ',' Pablo',' Drop Offs','Juan','Brad','Kevin','bobalu','Brandon', 'Javier','Micheal','Pending')", conn);

            MySqlDataReader reader;
            try
            {
                //conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //conn.Close();
            conn.Close();
            closeCount++;
        }

        // adding Dates to database
        public void adding_dates()
        {
            //string connect = "datasource = localhost; port = 3306; username = root; password = ;";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"insert into shedulling.dates values
                    (' 8:00
-----
8:30','9:00
-----
9:30','10:00
-----
10:30','11:00
-----
11:30','12:00
-----
12:30','1:00
-----
1:30','2:00
-----
2:30','3:00
-----
3:30', '4:00
-----
4:30','5:00
-----
5:30','6:00
-----
6:30')", conn);

            MySqlDataReader reader;
            try
            {
                //conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //conn.Close();
            conn.Close();
            closeCount++;
        }

        /*
         * Created By :- PriTesh D. Sortee
         * Created Date :- 29 Oct 2015
         * Purpose:- get date and line no to place yellow and pink label
         */
        #region---------------------------------getplace()------------------------------------------------------------------------------------------
        //two day
        public string GetPlace(string dt)
        {
            string sp = "";
            string no = "11";
            //MySqlConnection conn = new MySqlConnection(connect);
            MySqlConnection conn = GetConnection();
            MySqlCommand sda = new MySqlCommand(@"SELECT DATE(DATE_FORMAT(date,'%Y-%m-%d')) AS 'dd',  CONVERT( line, UNSIGNED INTEGER ) AS line
                                                    FROM shedulling.tablelayout1
                                    WHERE date >= '" + dt  + "' AND col12 = '" + sp + "' AND line != '" + no + "' AND 1= (SELECT 1 FROM shedulling.tablelayout1 WHERE col2='"+sp+"' OR col3='"+sp+"' OR col4='" +sp+ "' OR col5='"+sp+"' OR col6='"+sp+"' OR col7='"+sp+"' OR col8='"+sp+"'  OR col9='"+sp+"' OR col10='"+sp+"'  OR col11='"+sp+"' AND col12='"+sp+"' limit 1) Order By dd ,line limit 1", conn);

            MySqlDataReader reader;

            //int r = tableLayoutPanel1.RowCount;

            //int r = 0; // count rows
            //int cl = 11; // number of columns
            string a, b; // storing data from database
            try
            {
                //conn.Open();
                reader = sda.ExecuteReader();
                while (reader.Read())
                {
                    a = reader.GetString(0);
                    b = reader.GetString(1);
                    

                    s = a + " !" + b + " !" ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return s;
        }
        #endregion

        /*
         * Created By :- PriTesh D. Sortee
         * Created Date :- 29 Oct 2015
         * Purpose:- Get futeure date with data to display name
         */
        #region-----------------------------------------------------------GetFutureDate With data---------------------------------------------------
        public string GetMinFutureDate(string dt)
        {

            string s = "";
           
           
                int c = 0;
                MySqlConnection conn = GetConnection();
                MySqlCommand sd = new MySqlCommand(@"SELECT count(*) From shedulling.tablelayout1", conn);
                MySqlDataReader ed;
                ed = sd.ExecuteReader();
                while (ed.Read())
                {
                    c = Convert.ToInt32(ed.GetString(0));
                }
                ed.Close();


                if (c > 0)
                {
                    MySqlCommand sda = new MySqlCommand(@"SELECT  DATE_FORMAT(MIN(date),'%Y-%m-%d') From shedulling.tablelayout1 WHERE date >='" + dt + "'", conn);

                    MySqlDataReader reader;
                    reader = sda.ExecuteReader();
                    while (reader.Read())
                    {
                        s = reader.GetString(0).ToString();
                    }
                    reader.Close();

                }
                conn.Close();
            
             return s;
        }
        #endregion
        /*
         * Created By :- PriTesh D. Sortee
         * Created Date :- 29 Oct 2015
         * Purpose:- Get past date with data to display name
         */
        #region-----------------------------------------------------------GetPastDate With data----------------------------------------------------------
        public string GetMinPastDate(string dt)
        {

            string s = "";
           
           
                int c = 0;
                MySqlConnection conn = GetConnection();
                MySqlCommand sd = new MySqlCommand(@"SELECT count(*) From shedulling.tablelayout1", conn);
                MySqlDataReader ed;
                ed = sd.ExecuteReader();
                while (ed.Read())
                {
                    c = Convert.ToInt32(ed.GetString(0));
                }
                ed.Close();


                if (c > 0)
                {
                    MySqlCommand sda = new MySqlCommand(@"SELECT  DATE_FORMAT(MAX(date),'%Y-%m-%d') From shedulling.tablelayout1 WHERE date <='" + dt + "'", conn);

                    MySqlDataReader reader;
                    reader = sda.ExecuteReader();
                    while (reader.Read())
                    {
                        s = reader.GetString(0).ToString();
                    }
                    reader.Close();

                }
                conn.Close();
            
             return s;
        }
        #endregion

    }

}                

