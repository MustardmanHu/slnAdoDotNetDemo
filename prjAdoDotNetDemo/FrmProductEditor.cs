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

        public bool isOkButtonClicked
        {
            get { return _isOkClick; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _isOkClick = true;
            //CProduct CP = new CProduct
            //{
            //    產品名稱 = txtName.Text.ToString(),
            //    庫存 = int.Parse(txtQty.Text),
            //    售價 = decimal.Parse(txtPrice.Text),
            //    成本 = decimal.Parse(txtCost.Text)
            //};
            Close();
        }
        CProduct _product = new CProduct();
        public CProduct product
        {
            get
            {
                _product.成本 = Convert.ToDecimal(txtCost.Text);
                _product.產品名稱 = txtName.Text;
                _product.庫存 = Convert.ToInt32(txtQty.Text);
                _product.售價 = Convert.ToDecimal(txtPrice.Text);
                return _product;
            }
            set
            {
                _product = value;
                txtPrice.Text = _product.售價.ToString();
                txtName.Text = _product.產品名稱;
                txtCost.Text = _product.成本.ToString();
                txtQty.Text = _product.庫存.ToString();
            }
        }
    }
}
