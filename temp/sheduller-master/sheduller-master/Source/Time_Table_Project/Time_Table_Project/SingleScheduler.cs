using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Remote_Scheduller_Interface;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Threading;


namespace Time_Table_Project
{
    public partial class SingleScheduler : Form
    {
        TcpChannel chan = new TcpChannel();
        //  ChannelServices.RegisterChannel(chan);
        static int num = 0;
        int FormLoadCount = 0;
        int rownum, colnum;
        // add server Mac Adress and replace it with localhost
        RemoteIR obj = (RemoteIR)Activator.GetObject(
                typeof(Remote_Scheduller_Interface.RemoteIR),
                "tcp://localhost:8189/RemoteScheduller");

        string[] pos = new string[270];
        int kount;

        int pink = 0; int pink_change_Column = 11;
        int yellow = 0;
        int yellow_change_column = 11;
        //bool dragged;
        //private Point firstpoint = new Point();

        //PINK Button Mouse Click
        public Panel p1;
        public TextBox textBox_3;
        public TextBox textBox_2;
        public TextBox textBox_1;

        Point downPoint;
        bool moved;


        Dictionary<TableLayoutPanelCellPosition, Rectangle> dict = new Dictionary<TableLayoutPanelCellPosition, Rectangle>();

        int Counter_for_MouseClicks = 0;

        /*
         * Reading the Data From Table 
         */
        int iteration = 0;
        int col = 0;
        string[] storeing_firstday = new string[12];
        string[] storeing_firstday1 = new string[12];
        int line = 0;
        //int line1 = 0;

        dataBase db = new dataBase();

        Control c = new Control();

        //int co = 0;
        string kll;
        // string[] array = new string[243];
        string a, b, _c, d, e, f, g, h, i, j, k, l; // storing data from database

        int cl = 12;
        int r = 0;

        Control cc;

        //int ir = 0;


        public SingleScheduler(string d)
        {
            //Thread thr = new Thread(new ThreadStart(splash));
            //thr.Start();
            //Thread.Sleep(5000);
            textBox_1 = new TextBox();
            textBox_1.Select(0, 0);

            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.TransparencyKey = Color.FromKnownColor(KnownColor.Control);
            this.Update();

            InitializeComponent();
            timer1.Enabled = true;
            
            dateTimePicker1.Value = Convert.ToDateTime(d);
            //thr.Abort();
            typeof(TableLayoutPanel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(tableLayoutPanel1, true, null);
        }

        private void SingleScheduler_Load(object sender, EventArgs e)
        {
            if (FormLoadCount == 0)
            {
            //panel2.Visible = false;
            textBox_1.Select(0, 0);
            this.Visible = true;
            this.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            this.Location = new Point(0, 0);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            //panel2.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 10);
            tableLayoutPanel1.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 5);
            //panel2.Location = new System.Drawing.Point(0, 0);
            //pictureBox3.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width - 100, Screen.PrimaryScreen.WorkingArea.Height - 100);
           
                FormLoadCount++;
                string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
                loading_page_Date_Checker(kj);
                
            }
            
 
        }

        private void Universal_KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                Console.Write("\n\n Tab is Pressed");
            }
        }

        private void Pink_Post_Mouse_Click(object sender, MouseEventArgs e)
        {
            reading_firstDay_table();
            

            dataBase db = new dataBase();
            string date = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
            string read_data = db.GetPlace(date);
            if (read_data != null)
            {
                string[] arr = read_data.Split('!');
                string dt = arr[0];
                int ln = Convert.ToInt32(arr[1]);



                dateTimePicker1.Value = Convert.ToDateTime(dt);
                
                string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
                loading_page_Date_Checker(kj);
                

                pink = --ln;
                num = --ln;
            }
            if (num == 20)
            {

                // dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
                pictureBox2.Enabled = false;
                pictureBox1.Enabled = false;

                reading_firstDay_table();
                

                
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);

                string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
                loading_page_Date_Checker(kj);
                

                pictureBox2.Enabled = true;
                pictureBox1.Enabled = true;
                num = 10;
            }

            {
                if (pink == 10)
                {
                    pink_change_Column = pink_change_Column + 12;
                    yellow_change_column = yellow_change_column + 12;
                    pink = 0;
                }
                pink = pink + 1;
                num++;
                // label3.Enabled = false;
                p1 = new Panel();

                //            p1.Location = new System.Drawing.Point(0, 114);
                

                textBox_3 = new TextBox();
                textBox_2 = new TextBox();
                textBox_1 = new TextBox();

                Label l1 = new Label();
                Label l2 = new Label();
                Label l3 = new Label();

                textBox_1.BackColor = System.Drawing.Color.Pink;
                textBox_2.BackColor = System.Drawing.Color.Pink;
                textBox_3.BackColor = System.Drawing.Color.Pink;


                //p1.Location = new Point(400, 300);
                p1.Visible = true;
                p1.BackColor = System.Drawing.Color.Pink;
                tableLayoutPanel1.Controls.Add(p1, pink_change_Column, pink);

                p1.AllowDrop = true;
                p1.BorderStyle = System.Windows.Forms.BorderStyle.None;

                p1.Controls.Add(textBox_3);
                p1.Controls.Add(textBox_2);
                p1.Controls.Add(textBox_1);

                //
                // TextBox3 
                // 
                textBox_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox_3.Location = new System.Drawing.Point(-2, 22);
                textBox_3.Margin = new System.Windows.Forms.Padding(0);
                textBox_3.Multiline = true;
                textBox_3.Name = "textBox3";
                textBox_3.TextAlign = HorizontalAlignment.Left;
                textBox_3.Size = new System.Drawing.Size(p1.Width - 13, 42);
                textBox_3.TabIndex = 2;
                textBox_3.Text = "";
                textBox_3.BorderStyle = BorderStyle.None;

                textBox_3.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_3_KeyDown);

                // 
                // textBox2
                // 
                textBox_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox_2.Location = new System.Drawing.Point(0, 12);
                textBox_2.Margin = new System.Windows.Forms.Padding(0);
                textBox_2.Multiline = false;
                textBox_2.Name = "textBox2";
                textBox_2.TextAlign = HorizontalAlignment.Left;
                textBox_2.Size = new System.Drawing.Size(p1.Width - 15, 8);
                textBox_2.TabIndex = 1;
                textBox_2.Text = "";
                textBox_2.BorderStyle = BorderStyle.None;


                textBox_2.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_2_KeyDown);

                // 
                // textBox1
                // 
                textBox_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox_1.Location = new System.Drawing.Point(0, 0);
                textBox_1.Margin = new System.Windows.Forms.Padding(0);
                textBox_1.Multiline = false;
                textBox_1.Name = "textBox1";
                textBox_1.TextAlign = HorizontalAlignment.Left;
                textBox_1.ShortcutsEnabled = false;
                textBox_1.Size = new System.Drawing.Size(p1.Width, 12);
                textBox_1.TabIndex = 0;
                textBox_1.Text = "";
                textBox_1.BorderStyle = BorderStyle.None;

                textBox_1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_1_KeyDown);

                Graphics g = p1.CreateGraphics();

                //p1.Location = new System.Drawing.Point(132, 114);
                p1.Name = "p1";
                int height = tableLayoutPanel1.Height / 6;
                int width = tableLayoutPanel1.Width / 11;
                p1.Size = new System.Drawing.Size(width, height);
                //            MessageBox.Show("p1 Height = "+height +" and width = "+width);
                p1.TabIndex = 7;

                p1.Controls.Add(textBox_1);
                p1.Controls.Add(textBox_2);
                p1.Controls.Add(textBox_3);

                //
                // Label1
                //
                l1.Size = new System.Drawing.Size(p1.Width - 42, p1.Height - 45);
                l1.BackColor = System.Drawing.Color.Pink;
                l1.BorderStyle = BorderStyle.FixedSingle;
                l1.Location = new System.Drawing.Point(43, 12);

                p1.Controls.Add(l1);
                l1.Visible = true;

                //
                // Label2
                //
                l2.Size = new System.Drawing.Size(p1.Width - 42, p1.Height - 45);
                l2.BackColor = System.Drawing.Color.Pink;
                l2.BorderStyle = BorderStyle.FixedSingle;
                l2.Location = new System.Drawing.Point(43, 18);

                p1.Controls.Add(l2);
                l2.Visible = true;


                //
                // Label3
                //
                l3.Size = new System.Drawing.Size(p1.Width - 42, p1.Height - 45);
                l3.BackColor = System.Drawing.Color.Pink;
                l3.BorderStyle = BorderStyle.FixedSingle;
                l3.Location = new System.Drawing.Point(43, 24);

                p1.Controls.Add(l3);

                l3.Visible = true;

                l1.MouseClick += label1_MouseClick;
                l2.MouseClick += label2_MouseClick;
                l3.MouseClick += label3_MouseClick;

                // p1.Location = new System.Drawing.Point(132, 114);
                p1.Name = "p1";
                //p1.Size = new System.Drawing.Size(70, 75);
                p1.TabIndex = 7;

                p1.MouseClick += new System.Windows.Forms.MouseEventHandler(p1_MouseClick);
                p1.MouseDown += new System.Windows.Forms.MouseEventHandler(p1_MouseDown);
                p1.MouseMove += new System.Windows.Forms.MouseEventHandler(p1_MouseMove);
                p1.MouseUp += new System.Windows.Forms.MouseEventHandler(p1_MouseUp);

                // Delete Button pressed
                p1.KeyDown += new System.Windows.Forms.KeyEventHandler(p1_KeyDown);
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;

            reading_firstDay_table();
            

            


            dateTimePicker1.Value = dateTimePicker1.Value.Subtract(TimeSpan.FromDays(1));
            string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
            loading_page_Date_Checker(kj);
            
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            // dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
            pictureBox2.Enabled = false;
            pictureBox1.Enabled = false;

            reading_firstDay_table();
            

            
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);

            string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
            loading_page_Date_Checker(kj);
            

            pictureBox2.Enabled = true;
            pictureBox1.Enabled = true;
        }

        private void Yellow_label_mouse_Click(object sender, MouseEventArgs e)
        {
            reading_firstDay_table();
            

            dataBase db = new dataBase();
            string date = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
            string read_data = db.GetPlace(date);
            if (read_data != null)
            {
                string[] arr = read_data.Split('!');
                string dt = arr[0];
                int ln = Convert.ToInt32(arr[1]);



                dateTimePicker1.Value = Convert.ToDateTime(dt);
            
                string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
                loading_page_Date_Checker(kj);
            

                pink = --ln;
                num = --ln;
            }
            if (num == 20)
            {

                // dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
                pictureBox2.Enabled = false;
                pictureBox1.Enabled = false;

                reading_firstDay_table();
                

                
                dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
                string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
                loading_page_Date_Checker(kj);
                

                pictureBox2.Enabled = true;
                pictureBox1.Enabled = true;
                num = 10;
            }
            {
                if (pink == 10)
                {
                    yellow_change_column = yellow_change_column + 12;
                    pink_change_Column = pink_change_Column + 12;
                    pink = 0;
                }
                yellow = ++pink;
                num++;
                Console.WriteLine("Yellow Column Number = " + yellow);
                Panel p1 = new Panel();

                //            p1.Location = new System.Drawing.Point(0, 114);
                p1.Name = "p1";
                int height = tableLayoutPanel1.Height / 6;
                int width = tableLayoutPanel1.Width / 11;
                p1.Size = new System.Drawing.Size(width, height);
                p1.TabIndex = 7;


                // label4.Enabled = false;

                textBox_3 = new TextBox();
                textBox_2 = new TextBox();
                textBox_1 = new TextBox();

                Label l1 = new Label();
                Label l2 = new Label();
                Label l3 = new Label();

                textBox_1.BackColor = System.Drawing.Color.Yellow;
                textBox_2.BackColor = System.Drawing.Color.Yellow;
                textBox_3.BackColor = System.Drawing.Color.Yellow;

                //p1.Location = new Point(20, 20);
                p1.Visible = true;
                p1.BackColor = System.Drawing.Color.Yellow;
                tableLayoutPanel1.Controls.Add(p1, yellow_change_column, yellow);

                p1.AllowDrop = true;
                p1.BorderStyle = System.Windows.Forms.BorderStyle.None;

                p1.Controls.Add(textBox_3);
                p1.Controls.Add(textBox_2);
                p1.Controls.Add(textBox_1);

                //
                // TextBox3 
                // 
                textBox_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox_3.Location = new System.Drawing.Point(-2, 22);
                textBox_3.Margin = new System.Windows.Forms.Padding(0);
                textBox_3.Multiline = true;
                textBox_3.Name = "textBox3";
                textBox_3.TextAlign = HorizontalAlignment.Left;
                textBox_3.Size = new System.Drawing.Size(p1.Width - 13, 42);
                textBox_3.TabIndex = 2;
                textBox_3.Text = "";
                textBox_3.BorderStyle = BorderStyle.None;

                textBox_3.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_3_KeyDown);

                // 
                // textBox2
                // 
                textBox_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox_2.Location = new System.Drawing.Point(0, 12);
                textBox_2.Margin = new System.Windows.Forms.Padding(0);
                textBox_2.Multiline = false;
                textBox_2.Name = "textBox2";
                textBox_2.TextAlign = HorizontalAlignment.Left;
                textBox_2.Size = new System.Drawing.Size(p1.Width - 14, 8);
                textBox_2.TabIndex = 1;
                textBox_2.Text = "";
                textBox_2.BorderStyle = BorderStyle.None;

                textBox_2.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_2_KeyDown);

                // 
                // textBox1
                // 
                textBox_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                textBox_1.Location = new System.Drawing.Point(0, 0);
                textBox_1.Margin = new System.Windows.Forms.Padding(0);
                textBox_1.Multiline = false;
                textBox_1.Name = "textBox1";
                textBox_1.TextAlign = HorizontalAlignment.Left;
                textBox_1.ShortcutsEnabled = false;
                textBox_1.Size = new System.Drawing.Size(p1.Width, 12);
                textBox_1.TabIndex = 0;
                textBox_1.Text = "";
                textBox_1.BorderStyle = BorderStyle.None;

                textBox_1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_1_KeyDown);


                p1.Controls.Add(textBox_1);
                p1.Controls.Add(textBox_2);
                p1.Controls.Add(textBox_3);

                // Mouse Clicks
                p1.MouseClick += new System.Windows.Forms.MouseEventHandler(p1_yellow_MouseClick);
                p1.MouseDown += new System.Windows.Forms.MouseEventHandler(p1_MouseDown);
                p1.MouseMove += new System.Windows.Forms.MouseEventHandler(p1_yellow_MouseMove);
                p1.MouseUp += new System.Windows.Forms.MouseEventHandler(p1_MouseUp);

                //
                // Label1
                //
                l1.Size = new System.Drawing.Size(p1.Width - 42, p1.Height - 45);
                l1.BackColor = System.Drawing.Color.Yellow;
                l1.BorderStyle = BorderStyle.FixedSingle;
                l1.Location = new System.Drawing.Point(43, 12);

                p1.Controls.Add(l1);
                l1.Visible = true;

                //
                // Label2
                //
                l2.Size = new System.Drawing.Size(p1.Width - 42, p1.Height - 45);
                l2.BackColor = System.Drawing.Color.Yellow;
                l2.BorderStyle = BorderStyle.FixedSingle;
                l2.Location = new System.Drawing.Point(43, 18);

                p1.Controls.Add(l2);
                l2.Visible = true;


                //
                // Label3
                //
                l3.Size = new System.Drawing.Size(p1.Width - 42, p1.Height - 45);
                l3.BackColor = System.Drawing.Color.Yellow;
                l3.BorderStyle = BorderStyle.FixedSingle;
                l3.Location = new System.Drawing.Point(43, 24);


                p1.Controls.Add(l3);

                l3.Visible = true;

                l1.MouseClick += yellow_label1_MouseClick;
                l2.MouseClick += yellow_label2_MouseClick;
                l3.MouseClick += yellow_label3_MouseClick;


                // Delete Button Pressed
                p1.KeyDown += new System.Windows.Forms.KeyEventHandler(p1_KeyDown);
            }
        }

        private void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            dict[new TableLayoutPanelCellPosition(e.Column, e.Row)] = e.CellBounds;
            if (moved)
            {
                if (e.CellBounds.Contains(tableLayoutPanel1.PointToClient(MousePosition)))
                {
                    e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
                }
            }
        }

        private void reading_firstDay_table()
        {
            dataBase db = new dataBase();
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                for (int j = 0; j < this.tableLayoutPanel1.ColumnCount ; j++)
                {
                    Control c = this.tableLayoutPanel1.GetControlFromPosition(j, i);

                    if (c == null)
                    {
                        storeing_firstday[col++] = "";
                    }
                    else if (c != null && c is Label)
                    {
                        storeing_firstday[col++] = c.Text;
                    }
                    else if (c != null && c is TextBox)
                    {
                        storeing_firstday[col++] = c.Text;
                    }
                    else if (c != null && c is Panel)
                    {
                        string panel_Color = c.BackColor.ToString();
                        string first = panel_Color.Split('[', ']')[1];
                        kll = first;
                        string l;
                        foreach (Control control in c.Controls)
                        {
                            if (control is TextBox)
                            {
                                l = control.Text;
                                kll = kll + "!" + l;
                            }
                            else if (control is Label)
                            {
                                string s = control.BackColor.ToString();
                                string output = s.Split('[', ']')[1];
                                kll = kll + "!" + output;
                                iteration++;
                            }
                        }
                        storeing_firstday[col++] = kll;
                        first = null;
                        l = null;
                        kll = null;
                    }
                }

                string dat = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
                //DateTime dat1 = DateTime.ParseExact(dateTimePicker1.Value.ToShortDateString(), "dd/MM/yyyy", null);
                //DateTime dat1 = Convert.ToDateTime(dateTimePicker1.Value);
                //string dat1.ToShortDateString();

                col = 0;
                if (db.search_date_of_left_day(dat, line))
                {
                    //MessageBox.Show();
                    db.update_first_table_data(dat, line, storeing_firstday[0], storeing_firstday[1], storeing_firstday[2], storeing_firstday[3], storeing_firstday[4], storeing_firstday[5], storeing_firstday[6], storeing_firstday[7], storeing_firstday[8], storeing_firstday[9], storeing_firstday[10], storeing_firstday[11]);
                    line++;

                }
                else
                {
                    db.add_date_firstDay(dat, line, storeing_firstday[0], storeing_firstday[1], storeing_firstday[2], storeing_firstday[3], storeing_firstday[4], storeing_firstday[5], storeing_firstday[6], storeing_firstday[7], storeing_firstday[8], storeing_firstday[9], storeing_firstday[10], storeing_firstday[11]);
                    line++;
                }
            }
            line = 0;
        }

        private void loading_page_Date_Checker(string kj)
        {
            //check whether data is available for date if yes
            //kj = string.Format("{0:yyyy-MM-dd}", kj);
            dataBase db = new dataBase();
            string dat = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
            if (obj.search_date_of_left_day(kj, 0) == true)
            {
                // MessageBox.Show(kj + " Date is Found");
                clear_left_date();
                for (int num = 0; num < tableLayoutPanel1.ColumnCount ; num++)
                {
                    read_firstDay_from_DB(kj, num);
                }
            }
            else//if data is not avialable for date
            {
                // MessageBox.Show(kj + " Date Not Found");
                for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                {
                    for (int j = 0; j < tableLayoutPanel1.ColumnCount ; j++)
                    {
                        if (i == 0 || j == 0)
                        {
                            #region -----------------------add name for previous or next date available names----------------------------------------------------
                            if (i == 0 && j == 0)
                            {
                                string nowdt = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                                //this code for get future date to insert name only  if selected date is less than now date
                                if (Convert.ToDateTime(dat) < Convert.ToDateTime(nowdt))
                                {
                                    string ds = db.GetMinFutureDate(dat);
                                    if (ds != "")
                                    {
                                        for (int jj = 0; jj < tableLayoutPanel1.ColumnCount; jj++)
                                        {
                                            tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(jj, i));
                                        }

                                        read_firstDay_from_DB1(ds, 0);

                                    }
                                }
                                //this code for get past date to insert name only in selected date is greater than now date
                                else if ((Convert.ToDateTime(dat) > Convert.ToDateTime(nowdt)))
                                {
                                    string ds = db.GetMinPastDate(dat);
                                    if (ds != "")
                                    {
                                        for (int jj = 0; jj < tableLayoutPanel1.ColumnCount ; jj++)
                                        {
                                            tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(jj, i));
                                        }



                                        read_firstDay_from_DB1(ds, 0);

                                    }
                                }
                            }
                            #endregion end tiral and error
                        }
                        else
                        {

                            tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(j, i));
                            //MessageBox.Show("Deleting");
                        }
                    }
                }
            }
        }


        private void clear_left_date()
        {
            for (int i = 0; i < this.tableLayoutPanel1.RowCount; i++)
            {
                for (int j = 0; j < this.tableLayoutPanel1.ColumnCount ; j++)
                {
                    tableLayoutPanel1.Controls.Remove(tableLayoutPanel1.GetControlFromPosition(j, i));
                }
            }
        }

        //reading from database Left side
        public void read_firstDay_from_DB(string date, int number)
        {
            a = "";
            b = "";
            _c = "";
            d = "";
            e = "";
            f = "";
            g = "";
            h = "";
            i = "";
            j = "";
            k = "";
            l = "";

            string readed_data;
            obj.db_Connection(date, r);
            //  r = 0;
            // MessageBox.Show("Value of r = "+r);
            if (r == 12)
            { r = 0; }

            readed_data = obj.reading_left_date(date, number);
            string[] arr = readed_data.Split('@');

            //MessageBox.Show(readed_data + "Column =" + number);

            a = arr[0];
            b = arr[1];
            _c = arr[2];
            d = arr[3];
            e = arr[4];
            f = arr[5];
            g = arr[6];
            h = arr[7];
            i = arr[8];
            j = arr[9];
            k = arr[10];
            l = arr[11];
            //            MessageBox.Show(a+"@"+b+"@"+_c+"@"+d+"@"+e+"@"+f+"@"+g+"@"+h+"@"+i+"@"+j+"@"+k);

            for (int change_col = 0; change_col < cl; change_col++)
            {
                switch (change_col)
                {
                    case (0):
                        {
                            // pushing always 1st value from db to first column of table
                            cc = get_control(a);
                            if (cc.Name == "empty")
                            {
                                //MessageBox.Show("Empty box here at = "+change_col);
                            }
                            else
                            {
                                //            MessageBox.Show("Adding at 1 = " + a);
                                tableLayoutPanel1.Controls.Add(get_control(a), 0, r);

                            } break;
                        }
                    case 1:
                        {
                            cc = get_control(b);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;

                            }
                            else
                            {
                                //        MessageBox.Show("Adding at 2 = " + b);
                                tableLayoutPanel1.Controls.Add(get_control(b), 1, r);
                            } break;
                        }
                    case (2):
                        {
                            cc = get_control(_c);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //do nothing
                                //      MessageBox.Show("Adding at 3 empty");
                            }
                            else
                            {
                                //    MessageBox.Show("Adding at 3 = "+_c);
                                tableLayoutPanel1.Controls.Add(get_control(_c), 2, r);
                            } break;
                        }
                    case (3):
                        {
                            cc = get_control(d);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //do nothing
                                //                                MessageBox.Show("Adding at 4");
                            }
                            else
                            {
                                //                              MessageBox.Show("Adding at 4");
                                tableLayoutPanel1.Controls.Add(get_control(d), 3, r);
                            } break;
                        }
                    case (4):
                        {
                            cc = get_control(e);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                            MessageBox.Show("Adding at 5");
                                //do nothing
                            }
                            else
                            {
                                //                          MessageBox.Show("Adding at 5");
                                tableLayoutPanel1.Controls.Add(get_control(e), 4, r);
                            } break;
                        }
                    case (5):
                        {
                            cc = get_control(f);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                        MessageBox.Show("Adding at 6");
                                //do nothing
                            }
                            else
                            {
                                //                      MessageBox.Show("Adding at 6");
                                tableLayoutPanel1.Controls.Add(get_control(f), 5, r);
                            } break;
                        }
                    case (6):
                        {
                            cc = get_control(g);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                    MessageBox.Show("Adding at 7");
                                //do nothing
                            }
                            else
                            {
                                //                  MessageBox.Show("Adding at 7");
                                tableLayoutPanel1.Controls.Add(get_control(g), 6, r);
                            } break;
                        }
                    case (7):
                        {
                            cc = get_control(h);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                MessageBox.Show("Adding at 8");
                                //do nothing
                            }
                            else
                            {
                                //              MessageBox.Show("Adding at 8");
                                tableLayoutPanel1.Controls.Add(get_control(h), 7, r);
                            } break;
                        }
                    case (8):
                        {
                            cc = get_control(i);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //            MessageBox.Show("Adding at 9");
                                //do nothing
                            }
                            else
                            {
                                //          MessageBox.Show("Adding at 9");
                                tableLayoutPanel1.Controls.Add(get_control(i), 8, r);
                            } break;
                        }
                    case (9):
                        {
                            cc = get_control(j);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //        MessageBox.Show("Adding at 10");
                                //do nothing
                            }
                            else
                            {
                                //      MessageBox.Show("Adding at 10");
                                tableLayoutPanel1.Controls.Add(get_control(j), 9, r);
                            } break;
                        }
                    case (10):
                        {
                            cc = get_control(k);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                            }
                            else
                            {
                                //  MessageBox.Show("Adding at 11");
                                tableLayoutPanel1.Controls.Add(get_control(k), 10, r);
                            } break;
                        }
                    case (11):
                        {
                            cc = get_control(l);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                            }
                            else
                            {
                                //  MessageBox.Show("Adding at 11");
                                tableLayoutPanel1.Controls.Add(get_control(l), 11, r);
                            } break;
                        }
                }
            }
            r = r + 1;
        }

        private Control get_control(string a)
        {
            try
            {
                if (a.Length <= 0)
                {
                    //                MessageBox.Show("String Length = " + a.Length);
                    c = new Label();
                    c.Text = "E";
                    c.Name = "empty";
                }
                else if (a.Length >= 1 && a.Length < 19)
                {
                    // MessageBox.Show("String Length = " + a.Length);
                    c = new TextBox();
                    c.Height = 70;
                    c.Width = 55;
                    c.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    c.Text = a;
                    ((TextBox)c).BorderStyle = BorderStyle.None;
                    ((TextBox)c).Multiline = true;
                }
                else if (a.Length >= 19 && a.Length < 22)
                {
                    //                MessageBox.Show("String Length = " + a.Length);
                    c = new Label();
                    c.Height = 70;
                    c.Width = 50;
                    c.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    c.Text = a;
                }
                else if (a.Length >= 22)
                {
                    //              MessageBox.Show("String Length = " + a.Length);
                    c = label_generator(a);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return c;
        }

        private Panel label_generator(string a)
        {
            // MessageBox.Show(a);
            string[] arr = a.Split('!');
            Panel p1 = new Panel();

            //            p1.Location = new System.Drawing.Point(0, 114);
            p1.Name = "p1";
            int height = tableLayoutPanel1.Height / 6;
            int width = tableLayoutPanel1.Width / 11;
            p1.Size = new System.Drawing.Size(width, height);
            //            Console.WriteLine("P1.size ("+width+","+height+")");
            p1.TabIndex = 7;


            // label4.Enabled = false;
            textBox_3 = new TextBox();
            textBox_2 = new TextBox();
            textBox_1 = new TextBox();

            Label l1 = new Label();
            Label l2 = new Label();
            Label l3 = new Label();

            textBox_1.BackColor = Color.FromName(arr[0]);
            textBox_2.BackColor = Color.FromName(arr[0]);
            textBox_3.BackColor = Color.FromName(arr[0]);

            //p1.Location = new Point(20, 20);
            p1.Visible = true;
            p1.BackColor = Color.FromName(arr[0]);

            //  tableLayoutPanel1.Controls.Add(p1, yellow_change_column, yellow);
            p1.AllowDrop = true;
            p1.BorderStyle = System.Windows.Forms.BorderStyle.None;

            //
            // TextBox3 
            // 
            textBox_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox_3.Location = new System.Drawing.Point(-2, 22);
            textBox_3.Margin = new System.Windows.Forms.Padding(0);
            textBox_3.Multiline = true;
            textBox_3.Name = "textBox3";
            textBox_3.TextAlign = HorizontalAlignment.Left;
            textBox_3.Size = new System.Drawing.Size(p1.Width / 3, p1.Height /2);
            textBox_3.TabIndex = 2;
            textBox_3.Text = arr[3];
            textBox_3.BorderStyle = BorderStyle.None;

            textBox_3.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_3_KeyDown);

            // 
            // textBox2
            // 
            textBox_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox_2.Location = new System.Drawing.Point(0, 12);
            textBox_2.Margin = new System.Windows.Forms.Padding(0);
            textBox_2.Multiline = false;
            textBox_2.Name = "textBox2";
            textBox_2.TextAlign = HorizontalAlignment.Left;
            textBox_2.Size = new System.Drawing.Size(p1.Width / 3, p1.Height / 7);
            textBox_2.TabIndex = 1;
            textBox_2.Text = arr[2];
            textBox_2.BorderStyle = BorderStyle.None;

            textBox_2.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_2_KeyDown);

            // 
            // textBox1
            // 
            textBox_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            textBox_1.Location = new System.Drawing.Point(0, 0);
            textBox_1.Margin = new System.Windows.Forms.Padding(0);
            textBox_1.Multiline = false;
            textBox_1.Name = "textBox1";
            textBox_1.TextAlign = HorizontalAlignment.Left;
            textBox_1.ShortcutsEnabled = false;
            textBox_1.Size = new System.Drawing.Size(p1.Width/3, p1.Height / 5);
            textBox_1.TabIndex = 0;
            textBox_1.Text = arr[1];
            textBox_1.BorderStyle = BorderStyle.None;

            textBox_1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox_1_KeyDown);


            p1.Controls.Add(textBox_1);
            p1.Controls.Add(textBox_2);
            p1.Controls.Add(textBox_3);

            // Mouse Clicks
            p1.MouseClick += new System.Windows.Forms.MouseEventHandler(p1_yellow_MouseClick);
            p1.MouseDown += new System.Windows.Forms.MouseEventHandler(p1_MouseDown);
            p1.MouseMove += new System.Windows.Forms.MouseEventHandler(p1_yellow_MouseMove);
            p1.MouseUp += new System.Windows.Forms.MouseEventHandler(p1_MouseUp);

            //
            // Label1
            //
            l1.Size = new System.Drawing.Size(p1.Width / 3, p1.Height / 12);
            l1.BackColor = Color.FromName(arr[4]);
            l1.BorderStyle = BorderStyle.FixedSingle;
            l1.Location = new System.Drawing.Point(p1.Width / 2 + 13, p1.Height / 8);
            //l1.Text = "Label1";
            p1.Controls.Add(l1);
            l1.Visible = true;

            //
            // Label2
            //
            l2.Size = new System.Drawing.Size(p1.Width / 3, p1.Height / 12);
            l2.BackColor = Color.FromName(arr[5]);
            l2.BorderStyle = BorderStyle.FixedSingle;
            l2.Location = new System.Drawing.Point(p1.Width / 2 + 13,p1.Height / 5);
            //l2.Text = "Label2";
            p1.Controls.Add(l2);
            l2.Visible = true;


            //
            // Label3
            //
            l3.Size = new System.Drawing.Size(p1.Width / 3, p1.Height / 12);
            l3.BackColor = Color.FromName(arr[6]);
            l3.BorderStyle = BorderStyle.FixedSingle;
            l3.Location = new System.Drawing.Point(p1.Width / 2 + 13, p1.Height /4 +4);
            //l3.Text = "Label3";

            p1.Controls.Add(l3);

            l3.Visible = true;

            l1.MouseClick += readed_label1_MouseClick;
            l2.MouseClick += readed_label2_MouseClick;
            l3.MouseClick += readed_label3_MouseClick;


            return p1;
        }


        void p1_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 49, 68));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        public void textBox_1_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                /*textBox_1.SelectionStart = 0;
                textBox_1.SelectionLength = textBox_1.Text.Length;*/
                text.Select(0, 0);
                textBox_2.Focus();
            }
        }

        public void textBox_2_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                text.Select(0, 0);
                textBox_3.Focus();
            }
        }

        public void textBox_3_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = (TextBox)sender;
            if (e.KeyCode == Keys.Enter)
            {
                text.Select(textBox_3.SelectionStart, textBox_3.Text.Length);
                textBox_1.Focus();
            }
        }

        //Mouse Click Event for all Panels produced on run time
        private void p1_MouseClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs em = (MouseEventArgs)e;
            Control p = sender as Control;
            /*switch (em.Button)
            {
                case MouseButtons.Right:
                    {
                        MessageBox.Show();
                        Counter_for_MouseClicks++;                        
                         //   tableLayoutPanel1.Controls.Remove(p);
                            p.Dispose();
                            pink--;
                            p.Visible = false;
                            tableLayoutPanel1.Invalidate();
                       break; 
                    }
                default:
                    {
                        Console.Write("Do nothign");
                        break;
                    }
            }*/
            if (em.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Counter_for_MouseClicks++;
                //   tableLayoutPanel1.Controls.Remove(p);
                p.Dispose();
                pink--;
                p.Visible = false;
                tableLayoutPanel1.Invalidate();
            }
        }

        //Mouse Click Event for all Panels produced on run time
        private void p1_yellow_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        tableLayoutPanel1.Controls.Remove(p1);
                        p1.Visible = false;
                        p1.Hide();
                        pink--;
                        p1.Dispose();
                        break;
                    }             
                default:
                    {
                        Console.Write("Do nothign");
                        break;
                    }
            }
        }

        //MouseDown event handler for all your controls (on the tableLayoutPanel1)
        private void p1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control button = sender as Control;
                button.Parent = this;
                button.BringToFront();
                downPoint = e.Location;
                rownum = tableLayoutPanel1.GetRow(button);
                colnum = tableLayoutPanel1.GetColumn(button);

                for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
                {
                    for (int j = 0; j < tableLayoutPanel1.RowCount; j++)
                    {
                        Control cc = tableLayoutPanel1.GetControlFromPosition(i, j);
                        if (cc != null)
                        {
                            pos[kount] = Convert.ToString(tableLayoutPanel1.GetCellPosition(cc));
                            kount++;
                        }

                    }
                }
            }
        }

        private void p1_yellow_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Enabled = true;
            Control button = sender as Control;
            if (e.Button == MouseButtons.Left)
            {
                button.Left += e.X - downPoint.X;
                button.Top += e.Y - downPoint.Y;
                moved = true;
                tableLayoutPanel1.Invalidate();
            }
        }

        //MouseUp event handler for all your controls (on the tableLayoutPanel1)
        private void p1_MouseUp(object sender, MouseEventArgs e)
        {
            
            Control button = sender as Control;
            if (moved)
            {
                SetControl(button, e.Location);
                button.Parent = tableLayoutPanel1;
                tableLayoutPanel1.Controls.Add(button);
                moved = false;
                tableLayoutPanel1.Invalidate();
            }
            int c = tableLayoutPanel1.GetColumn(button);
            int r = tableLayoutPanel1.GetRow(button);

            for (int i = 0; i <= kount; i++)
            {
                if (Convert.ToString(tableLayoutPanel1.GetCellPosition(button)) == Convert.ToString(pos[i]))
                {
                    c = 0;
                }
            }
            


            if (c == 0 || r == 0 )
            {
                Control p = sender as Control;

                tableLayoutPanel1.SetColumn(p, colnum);
                tableLayoutPanel1.SetRow(p, rownum);

            }
        }

        //This is used to set the control on the tableLayoutPanel after releasing mouse
        private void SetControl(Control c, Point position)
        {
            Point localPoint = tableLayoutPanel1.PointToClient(c.PointToScreen(position));
            var keyValue = dict.FirstOrDefault(e => e.Value.Contains(localPoint));
            if (!keyValue.Equals(default(KeyValuePair<TableLayoutPanelCellPosition, Rectangle>)))
            {
                tableLayoutPanel1.SetCellPosition(c, keyValue.Key);
            }
        }


        public void readed_label1_MouseClick(object sender, MouseEventArgs e)
        {
            Control a = sender as Label;
            if (a.BackColor == Color.Yellow)
            {
                a.BackColor = Color.BlueViolet;
            }
            else if (a.BackColor == Color.BlueViolet)
            {
                a.BackColor = Color.Pink;
            }
            else
            {
                a.BackColor = Color.Yellow;
            }

        }

        public void readed_label2_MouseClick(object sender, MouseEventArgs e)
        {
            Control a = sender as Label;
            if (a.BackColor == Color.Yellow)
            {
                a.BackColor = Color.GreenYellow;
            }
            else if (a.BackColor == Color.GreenYellow)
            {
                a.BackColor = Color.Pink;
            }
            else
            {
                a.BackColor = Color.Yellow;
            }
        }

        public void readed_label3_MouseClick(object sender, MouseEventArgs e)
        {
            Control a = sender as Label;
            if (a.BackColor == Color.Yellow)
            {
                a.BackColor = Color.Red;
            }
            else if (a.BackColor == Color.Red)
            {
                a.BackColor = Color.Pink;
            }
            else
            {
                a.BackColor = Color.Yellow;
            }
        }

        //changing color of labels On click
        void label3_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            if (moved == true) { moved = false; }
            else
            {
                if (p1.BackColor == Color.Pink)
                {
                    p1.BackColor = Color.Red;
                }
                else if (p1.BackColor == Color.Red)
                {
                    p1.BackColor = Color.Pink;
                }
            }
        }

        void label2_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            if (moved == true) { moved = false; }
            else
            {
                if (p1.BackColor == Color.Pink)
                {
                    p1.BackColor = Color.GreenYellow;
                }
                else if (p1.BackColor == Color.GreenYellow)
                {
                    p1.BackColor = Color.Pink;
                }
            }
        }

        void label1_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            if (moved == true) { moved = false; }
            else
            {
                if (p1.BackColor == Color.Pink)
                {
                    p1.BackColor = Color.BlueViolet;
                }
                else if (p1.BackColor == Color.BlueViolet)
                {
                    p1.BackColor = Color.Pink;
                }
            }
        }

        //MouseMove event handler for all your controls (on the tableLayoutPanel1)
        private void p1_MouseMove(object sender, MouseEventArgs e)
        {
            Control button = sender as Control;
            if (e.Button == MouseButtons.Left)
            {
                button.Left += e.X - downPoint.X;
                button.Top += e.Y - downPoint.Y;
                moved = true;
                tableLayoutPanel1.Invalidate();
            }
        }

        private void p1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Control p = sender as Control;
                p.Visible = false;
                p.Dispose();
                tableLayoutPanel1.Controls.Remove(p);
                Console.Write("Panel name is " + p);
            }
        }


        void yellow_label1_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            if (moved == true) { moved = false; }
            else
            {
                if (p1.BackColor == Color.Yellow)
                {
                    p1.BackColor = Color.BlueViolet;
                }
                else if (p1.BackColor == Color.BlueViolet)
                {
                    p1.BackColor = Color.Yellow;
                }
            }
        }

        void yellow_label2_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            if (moved == true) { moved = false; }
            else
            {
                if (p1.BackColor == Color.Yellow)
                {
                    p1.BackColor = Color.GreenYellow;
                }
                else if (p1.BackColor == Color.GreenYellow)
                {
                    p1.BackColor = Color.Yellow;
                }
            }
        }

        void yellow_label3_MouseClick(object sender, MouseEventArgs e)
        {
            Control p1 = sender as Control;
            if (moved == true) { moved = false; }
            else
            {
                if (p1.BackColor == Color.Yellow)
                {
                    p1.BackColor = Color.Red;
                }
                else if (p1.BackColor == Color.Red)
                {
                    p1.BackColor = Color.Yellow;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            reading_firstDay_table();
            timer1.Enabled = false;
            this.Visible = false;
            Form1 f = new Form1(Convert.ToString(string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value)));
            f.Visible = true;
        }

        private void reading_10mints_delay(object sender, EventArgs e)
        {
            reading_firstDay_table();
            //reading_SecondDay_table();
            loading_page_Date_Checker(string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value));
            //loading_page_RightDate_Checkeer(string.Format("{0:yyyy-MM-dd}", dateTimePicker2.Value));
        }

        /*
         * Created By :- PriTesh D. Sortee
         * Created Date:- 26 Nov 2015
         * Purpose :- Ask User that he/she really  want to close the application
         */
        #region----------------------------------------Form Closing()------------------------------------------------------------------------------------
        private void SingleScheduler_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("This will close down the whole application. Confirm?", "Close Application", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MessageBox.Show("Do you Want To Save Changes. Confirm?", "Save Application Data", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    reading_firstDay_table();
                }
                else
                {

                }
            }
            else
            {
                e.Cancel = true;
            }

        }
        #endregion

        /*
         * Created By :- PriTesh D. Sortee
         * Created Date:- 27 Nov 2015
         * Purpose :- save details on datetime picker mouse down
         */
        #region-------------------------------------------datetimePicker1_MouseDown-----------------------------------------------------------------
        private void dateTimePicker1_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                reading_firstDay_table();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        /*
         * Created By :- PriTesh D. Sortee
         * Created Date:- 28 Nov 2015
         * Purpose :- DateTimePickerCloseUp
         */
        #region-----------------------------------------------------------------datetimepicker CloseUP()----------------------------------------
        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            try
            {
                string kj = string.Format("{0:yyyy-MM-dd}", dateTimePicker1.Value);
            loading_page_Date_Checker(kj);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(),"Exception",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        #endregion

        /*
         * Created By:- PriTesh D. Sortee
         * Created Date:- 28 Nov 2015
         * Purpose: To show name in first row if data is not available 
         */
        #region---------------------------------------------read_firstDay_from_DB1--------------------------------------
        public void read_firstDay_from_DB1(string date, int number)
        {
            a = "";
            b = "";
            _c = "";
            d = "";
            e = "";
            f = "";
            g = "";
            h = "";
            i = "";
            j = "";
            k = "";
            l = "";

            string readed_data;
            obj.db_Connection(date, 0);
            //  r = 0;
            // MessageBox.Show("Value of r = "+r);
            //if (r == 12)
            //{ r = 0; }

            readed_data = obj.reading_left_date(date, number);
            string[] arr = readed_data.Split('@');

            //MessageBox.Show(readed_data + "Column =" + number);

            a = arr[0];
            b = arr[1];
            _c = arr[2];
            d = arr[3];
            e = arr[4];
            f = arr[5];
            g = arr[6];
            h = arr[7];
            i = arr[8];
            j = arr[9];
            k = arr[10];
            l = arr[11];
            //            MessageBox.Show(a+"@"+b+"@"+_c+"@"+d+"@"+e+"@"+f+"@"+g+"@"+h+"@"+i+"@"+j+"@"+k);

            for (int change_col = 0; change_col < cl; change_col++)
            {
                switch (change_col)
                {
                    case (0):
                        {
                            // pushing always 1st value from db to first column of table
                            cc = get_control(a);
                            if (cc.Name == "empty")
                            {
                                //MessageBox.Show("Empty box here at = "+change_col);
                            }
                            else
                            {
                                //            MessageBox.Show("Adding at 1 = " + a);
                                tableLayoutPanel1.Controls.Add(get_control(a), 0, 0);

                            } break;
                        }
                    case 1:
                        {
                            cc = get_control(b);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;

                            }
                            else
                            {
                                //        MessageBox.Show("Adding at 2 = " + b);
                                tableLayoutPanel1.Controls.Add(get_control(b), 1, 0);
                            } break;
                        }
                    case (2):
                        {
                            cc = get_control(_c);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //do nothing
                                //      MessageBox.Show("Adding at 3 empty");
                            }
                            else
                            {
                                //    MessageBox.Show("Adding at 3 = "+_c);
                                tableLayoutPanel1.Controls.Add(get_control(_c), 2, 0);
                            } break;
                        }
                    case (3):
                        {
                            cc = get_control(d);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //do nothing
                                //                                MessageBox.Show("Adding at 4");
                            }
                            else
                            {
                                //                              MessageBox.Show("Adding at 4");
                                tableLayoutPanel1.Controls.Add(get_control(d), 3, 0);
                            } break;
                        }
                    case (4):
                        {
                            cc = get_control(e);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                            MessageBox.Show("Adding at 5");
                                //do nothing
                            }
                            else
                            {
                                //                          MessageBox.Show("Adding at 5");
                                tableLayoutPanel1.Controls.Add(get_control(e), 4, 0);
                            } break;
                        }
                    case (5):
                        {
                            cc = get_control(f);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                        MessageBox.Show("Adding at 6");
                                //do nothing
                            }
                            else
                            {
                                //                      MessageBox.Show("Adding at 6");
                                tableLayoutPanel1.Controls.Add(get_control(f), 5, 0);
                            } break;
                        }
                    case (6):
                        {
                            cc = get_control(g);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                    MessageBox.Show("Adding at 7");
                                //do nothing
                            }
                            else
                            {
                                //                  MessageBox.Show("Adding at 7");
                                tableLayoutPanel1.Controls.Add(get_control(g), 6, 0);
                            } break;
                        }
                    case (7):
                        {
                            cc = get_control(h);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //                MessageBox.Show("Adding at 8");
                                //do nothing
                            }
                            else
                            {
                                //              MessageBox.Show("Adding at 8");

                                tableLayoutPanel1.Controls.Add(get_control(h), 7, 0);
                            } break;
                        }
                    case (8):
                        {
                            cc = get_control(i);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //            MessageBox.Show("Adding at 9");
                                //do nothing
                            }
                            else
                            {
                                //          MessageBox.Show("Adding at 9");
                                tableLayoutPanel1.Controls.Add(get_control(i), 8, 0);
                            } break;
                        }
                    case (9):
                        {
                            cc = get_control(j);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                                //        MessageBox.Show("Adding at 10");
                                //do nothing
                            }
                            else
                            {
                                //      MessageBox.Show("Adding at 10");
                                tableLayoutPanel1.Controls.Add(get_control(j), 9, 0);
                            } break;
                        }
                    case (10):
                        {
                            cc = get_control(k);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                            }
                            else
                            {
                                //  MessageBox.Show("Adding at 11");
                                tableLayoutPanel1.Controls.Add(get_control(k), 10, 0);
                            } break;
                        }
                    case (11):
                        {
                            cc = get_control(l);
                            if (cc.Name == "empty")
                            {
                                cc.BackColor = Color.Red;
                            }
                            else
                            {
                                //  MessageBox.Show("Adding at 11");
                                tableLayoutPanel1.Controls.Add(get_control(l), 11, 0);
                            } break;
                        }
                }
            }

        }
        #endregion


        public void splash()
        {
            this.Hide();  
        }
        
        private void timerRe_Tick(object sender, EventArgs e)
        {
            
        }

    }
}
