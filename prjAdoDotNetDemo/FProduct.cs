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

        private void button4_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            FrmProductEditor FRM = new FrmProductEditor();
            FRM.ShowDialog();
            //將dataGridView1.DataSource轉型為DT
            if (dataGridView1.DataSource is DataTable table)
            {
                DataRow row = table.NewRow();
                row["fName"] = FRM.Names;
                row["fCost"] = FRM.Cost;
                row["fQty"] = FRM.Qty;
                row["fPrice"] = FRM.Price;
                table.Rows.Add(row);
            }
            else { return; }
        }
    }
}
