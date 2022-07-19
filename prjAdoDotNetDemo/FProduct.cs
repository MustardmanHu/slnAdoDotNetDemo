using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjAdoDotNetDemo
{
    public partial class FProduct : Form
    {
        public FProduct()
        {
            InitializeComponent();
        }
      new  public void  Refresh()
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-EKRVM5K;Initial Catalog=ClassUse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("Use ClassUse Select * From tProduct ", con);
            //連接SQL的命令字串
            DataSet DS = new DataSet();
            adapter.Fill(DS);
            con.Close();
            dataGridView1.DataSource = DS.Tables[0];
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmProductEditor FRM = new FrmProductEditor();
            FRM.ShowDialog();
            //將dataGridView1.DataSource轉型為DT
            if (dataGridView1.DataSource is DataTable table)
            {
                DataRow row = table.NewRow();
                row["fName"] = FRM.product.產品名稱;
                row["fCost"] = FRM.product.成本;
                row["fQty"] = FRM.product.庫存;
                row["fPrice"] = FRM.product.售價;
                table.Rows.Add(row);
            }
            else { return; }
        }

        private void FProduct_Load(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
