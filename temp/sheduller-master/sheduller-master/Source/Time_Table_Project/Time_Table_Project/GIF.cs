using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_Table_Project
{
    public partial class GIF : Form
    {
        public GIF()
        {
            InitializeComponent();
        }

        private void Timer(object sender, EventArgs e)
        {
            this.Dispose();
            Form1 form = new Form1();
            form.Visible = true;
            //form.Show = true;
        }
    }
}
