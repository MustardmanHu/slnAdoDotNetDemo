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
        SqlCommandBuilder builder = new SqlCommandBuilder();
        SqlDataAdapter adapter;

        private int _position = -1;

        public int i
        {
            get;
            set;
        }

        public FProduct()
        {
            InitializeComponent();
        }
        new public void Refresh()
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-EKRVM5K;Initial Catalog=ClassUse;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };
            con.Open();
           
            adapter = new SqlDataAdapter("Use ClassUse Select * From tProduct ", con);
            builder.DataAdapter = adapter;
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
            if (dataGridView1.DataSource is DataTable table && FRM.product != null)
            {
                CProduct CP = FRM.product;
                DataRow row = table.NewRow();
                row["fID"] = CP.ID;
                row["fName"] = CP.產品名稱;
                row["fCost"] = CP.成本;
                row["fQty"] = CP.庫存;
                row["fPrice"] = CP.售價;
                table.Rows.Add(row);
            }
            else { return; }
            SetGridStyle();
        }

        private void FProduct_Load(object sender, EventArgs e)
        {
            Refresh();
            SetGridStyle();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            _position = e.RowIndex;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (_position < 0) { return; }
            DataTable table = dataGridView1.DataSource as DataTable;
            DataRow row = table.Rows[_position];
            CProduct CP = new CProduct();
            {
                CP.ID = (int)row["fID"];
                CP.產品名稱 = row["fName"].ToString();
                CP.售價 = (decimal)row["fPrice"];
                CP.庫存 = (int)row["fQty"];
                CP.成本 = (decimal)row["fCost"];
            }
            FrmProductEditor FRM = new FrmProductEditor
            {
                product = CP
            };
            FRM.ShowDialog();
            row["fID"] = FRM.product.ID;
            row["fName"] = FRM.product.產品名稱;
            row["fPrice"] = FRM.product.售價;
            row["fQty"] = FRM.product.庫存;
            row["fCost"] = FRM.product.成本;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable table = dataGridView1.DataSource as DataTable;
            DataRow row = table.Rows[_position];
            row.Delete();
        }

        private void FProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataTable table = dataGridView1.DataSource as DataTable;
            if(table == null)
            {
                
            }
            else { 
            if(table.Rows.Count>0)
                adapter.Update(table);
        }}

        private void button5_Click(object sender, EventArgs e)
        {
            FrmKeyword f = new FrmKeyword
            {
                Text = "關鍵字查詢作業"
            };
            f.ShowDialog();

            if (f.isOkButtonClicked)
            {
                DataTable table = dataGridView1.DataSource as DataTable;
                string CMT = $"fName LIKE '%{f.keyword}%'";
                CMT += $"OR fCost ='{f.keyword}'";
                CMT += $"OR fQty ='{f.keyword}'";
                CMT += $"OR fPrice ='{f.keyword}'";
                DataView dv = table.DefaultView;
                dv.RowFilter = CMT;
                dataGridView1.DataSource = dv;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable table = dataGridView1.DataSource as DataTable;
            if (table.Rows.Count > 0)
                adapter.Update(table);
        }
       
        public void SetGridStyle()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Width = 550;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 180;
            dataGridView1.Columns[4].Width = 180;
            dataGridView1.Columns[5].Width = 350;
            i = 1;
            foreach (DataGridViewRow Row in dataGridView1.Rows)
            {
                Row.DefaultCellStyle.BackColor = Color.AliceBlue;
                
                if (i % 3 == 0)
                { 
                    Row.DefaultCellStyle.BackColor = Color.DarkSalmon;
                }
                if (i % 3 == 1)
                {
                    Row.DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                }
                if (i % 3 == 2)
                {
                    Row.DefaultCellStyle.BackColor = Color.Cornsilk;
                }
                i++;
            }
        }
    }
}
