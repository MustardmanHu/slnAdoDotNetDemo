using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace prjAdoDotNetDemo
{
    public partial class FrmCustomerEditor : Form
    {
        List<int> pksList = new List<int>();
        public FrmCustomerEditor(){InitializeComponent();}
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO tCustomer( ";
            sql += "fName,";
            sql += "fPhone,";
            sql += "fEmail,";
            sql += "fAddress,";
            sql += "fPassword";
            sql += " ) VALUES (";
            sql += "@Names"+",";
            sql += "@Phone"+",";
            sql += "@Email" + ",";
            sql += "@Adr" + ",";
            sql += "@PW" + ")";
            //防止SQL Injection,使用ParaMeters類別
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("Names", txtName.Text),

                new SqlParameter("Phone", txtPhone.Text),

                new SqlParameter("Email", txtEmail.Text),

                new SqlParameter("Adr", txtAddress.Text),

                new SqlParameter("PW", txtPassword.Text)
            };
            executeSql(sql, parameters);
            MessageBox.Show("新增資料成功");
        }
        private void executeSql(string sql, List<SqlParameter> parameters)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=DESKTOP-EKRVM5K;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM tCustomer WHERE fId=" + "@ID";
            
            List<SqlParameter> parameters = new List<SqlParameter>
            { new SqlParameter("ID", txtId.Text) };
            executeSql(sql,parameters);
            MessageBox.Show("刪除資料成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
            new SqlParameter("Names", txtName.Text),
            new SqlParameter("Phone", txtPhone.Text),
            new SqlParameter("Email", txtEmail.Text),
            new SqlParameter("Adr",txtAddress.Text),
            new SqlParameter("PW", txtPassword.Text),
            new SqlParameter("ID", txtId.Text)
            };
            string sql = "UPDATE tCustomer SET ";
            sql += " fName=" + "@Names"+",";
            sql += " fPhone='" +"@Phone"+",";
            sql += " fEmail='" + "@Email" + ",";
            sql += " fAddress='" +"@Adr"+ ",";
            sql += " fPassword='" + "@PW" + "";
            sql += " WHERE fId=" + "@ID";
            executeSql(sql, parameters);
            MessageBox.Show("修改資料成功");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-EKRVM5K;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };
            con.Open();
            SqlCommand cmd = new SqlCommand
            {
            Connection = con,
                CommandText = "SELECT * FROM tCustomer"
            };

            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();
            pksList.Clear();
            while (reader.Read())
            {
                pksList.Add((int)reader["fId"]);
                listBox1.Items.Add(reader["fName"].ToString());
            }
            con.Close();
        }

        private void displayBySql(string sql, List<SqlParameter> parameters)
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = @"Data Source=DESKTOP-EKRVM5K;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
            };
            con.Open();
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandText = sql
            };
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtId.Text = reader["fId"].ToString(); 
                txtName.Text = reader["fName"].ToString();
                txtPhone.Text = reader["fPhone"].ToString();
                txtEmail.Text = reader["fEmail"].ToString();
                txtAddress.Text = reader["fAddress"].ToString();
                txtPassword.Text = reader["fPassword"].ToString();
            }
            con.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            int i = listBox1.SelectedIndex;
            int pk = pksList[i];
            string CMText = "SELECT * FROM tCustomer WHERE@fID=";
            List<SqlParameter> parameters = new List<SqlParameter>
            { new SqlParameter("fID","pk")};
            displayBySql(CMText,parameters);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmKeyword f = new FrmKeyword
            {
                Text = "關鍵字查詢作業"
            };
            f.ShowDialog();

            if (f.isOkButtonClicked)
            {
                string sql = "SELECT * FROM tCustomer WHERE fName LIKE @k_keyword ";
                sql += " OR fPhone LIKE @k_keyword ";
                sql += " OR fEmail LIKE @k_keyword";
                sql += " OR fAddress LIKE @k_keyword ";
                List<SqlParameter> parameters = new List<SqlParameter>
            { new SqlParameter("k_keyword","%"+f.keyword+"%")};
                
                displayBySql(sql,  parameters);
            }
        }
    }
}
