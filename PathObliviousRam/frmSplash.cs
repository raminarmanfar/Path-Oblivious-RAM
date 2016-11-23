using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathObliviousRam
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            timer1.Interval = 60;
        }
    }
}
