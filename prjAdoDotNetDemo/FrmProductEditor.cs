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
        CProduct _product = new CProduct();
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public bool isOkButtonClicked
        {
            get { return _isOkClick; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
                _isOkClick = true;
                Close();
           
            if(product==null)
            {
                MessageBox.Show("請確實輸入資料");
                return;
            }
        }
       
        public CProduct product
        {
            get
            {
                try
                {
                    _product.ID = Convert.ToInt32(txtID.Text);
                    _product.成本 = Convert.ToDecimal(txtCost.Text);
                    _product.產品名稱 = txtName.Text;
                    _product.庫存 = Convert.ToInt32(txtQty.Text);
                    _product.售價 = Convert.ToDecimal(txtPrice.Text);
                }
                catch (System.FormatException)
                {
                    return null ;
                 }
                return _product;
            }
            set
            {
                _product = value;
                txtID.Text = _product.ID.ToString();
                txtPrice.Text = _product.售價.ToString();
                txtName.Text = _product.產品名稱;
                txtCost.Text = _product.成本.ToString();
                txtQty.Text = _product.庫存.ToString();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
