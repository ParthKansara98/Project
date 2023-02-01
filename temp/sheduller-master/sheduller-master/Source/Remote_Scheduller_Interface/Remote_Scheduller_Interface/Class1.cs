using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remote_Scheduller_Interface
{
    public interface RemoteIR
    {
        void db_Connection(string date, int number_of_Line);

        void add_date_firstDay(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe);

        void update_first_table_data(string date, int line, string first, string sec, string thi, string four, string fiv, string six, string sev, string eig, string nin, string ten, string ele,string twe);

        bool search_date_of_left_day(string date, int li);

        string reading_right_date(string date, int number_of_line);

        string reading_left_date(string date,int n);

        //user Login
        Boolean user_connection(string user, string pass);
    }
}
