using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjAdoDotNetDemo
{
    public partial class FrmKeyword : Form
    {
        bool _isOkClick = false;

        public FrmKeyword()
        {
            InitializeComponent();
        }

        public string keyword { 
            get { return txtKeyword.Text; }
            set { txtKeyword.Text=value; }
        }
        public bool isOkButtonClicked
        {
            get { return _isOkClick; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _isOkClick = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
