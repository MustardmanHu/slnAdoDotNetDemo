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
    public partial class FrmProductEditor : Form
    {
        bool _isOkClick = false;
        public FrmProductEditor()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public string Names { get; set; }
        public int Qty { get; set; } 
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public bool isOkButtonClicked
        {
            get { return _isOkClick; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _isOkClick = true;
            Names = txtName.Text.ToString();
            Qty = int.Parse(txtQty.Text);
            Price = decimal.Parse(txtPrice.Text);
            Cost = decimal.Parse(txtCost.Text);
            Close();
        }
    }
}
