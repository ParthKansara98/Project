using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using MySql.Data.MySqlClient;
using Remote_Scheduller_Interface;


namespace Time_Table_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        // add server Mac Adress and replace it with localhost
        RemoteIR obj = (RemoteIR)Activator.GetObject(
                typeof(Remote_Scheduller_Interface.RemoteIR),
                "tcp://localhost:8089/RemoteScheduller");

        public static void Mian()
        {
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MessageBox.Show("Please Enter User Name and Password","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textBox1.Focus();
            }
            else if(textBox1.Text=="" && textBox2.Text!="")
            {
                MessageBox.Show("Please Enter User Name", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                textBox1.Focus();
            }
            else if (textBox1.Text != "" && textBox2.Text == "")
            {
                MessageBox.Show("Please Enter Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                textBox2.Focus();
            }


            else
            {
                try
                {
                    string user = textBox1.Text;
                    string pass = textBox2.Text;
                    if (obj.user_connection(user, pass))
                    {
                        this.Visible = false;
                        Form1 f = new Form1();
                        f.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Correct UserName And Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Text = null;
                        textBox2.Text = null;
                        textBox1.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Select(0, 0);
            textBox2.Select(0, 0);
        }

        private void TextBox1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox1.Select(0, 0);
                textBox2.Focus();
            }
        }

        private void textbox2_keyDown(object sender, KeyEventArgs ex)
        {
            if (ex.KeyCode == Keys.Enter)
            {
                textBox2.Select(0, 0);
                button2.Focus();
            }
        }
    }
}
