using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace prjAdoDotNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open() ;

            string sql = "INSERT INTO tCustomer( fName,fPhone,fEmail,fAddress,fPassword) VALUES ";
            //sql += " ('Marco','0923445667','marco@gmail.com','Taipei','1234')";
            sql += "(@Names,@Phone,@Email,@City,@PW)";
            //以下為SqlParameter範例
            SqlParameter SqlName= new SqlParameter("Names","MyName");
            //()內的參數1填入Key的名稱,2填入使用者自定義參數
            SqlParameter SqlPhone = new SqlParameter("Phone", "phoneNumber");
            SqlParameter SqlEmail = new SqlParameter("Email", "Email");
            SqlParameter SqlCity = new SqlParameter("City", "City");
            SqlParameter SqlPW = new SqlParameter("PW", "PW");
            //建立SqlParameter類別


            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add(SqlName);
            cmd.Parameters.Add(SqlPhone);
            cmd.Parameters.Add(SqlEmail);
            cmd.Parameters.Add(SqlCity);
            cmd.Parameters.Add(SqlPW);
            //在SQLCommand內加入Parameters物件
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("新增資料成功");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "dELETE FROM tCustomer WHERE fName='Marco'";
            cmd.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("刪除資料成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            string sql = "UPDATE tCustomer SET fPhone='0923445766',fEmail='marco@outlook.com' WHERE fName='Marco'";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("修改資料成功");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM tCustomer";

            SqlDataReader reader = cmd.ExecuteReader();

            string name = "沒有資料";
            if (reader.Read())
            {
                name = reader["fName"].ToString();
            }
            con.Close();
            MessageBox.Show("查詢結果："+name);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            (new FrmCustomerEditor()).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (new FProduct()).Show();
        }
    }
}
