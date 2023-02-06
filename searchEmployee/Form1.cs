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

namespace searchEmployee
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True");

        private void btnGetir_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            label1.ForeColor = Color.Blue;

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select FirstName, LastName, City, Country from Employees", conn);


                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt; 
            }
            else
            {
                conn.Close();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            // 1 .YOL
            //SqlCommand cmd = new SqlCommand("select FirstName, LastName, City, Country from Employees where FirstName Like '%' + @name + '%' ", conn);
            //cmd.Parameters.AddWithValue("@name", txtName.Text);

            //if (conn.State == ConnectionState.Closed)
            //{
            //    conn.Open();

            //    SqlDataReader dr = cmd.ExecuteReader();
            //    DataTable dt = new DataTable();
            //    dt.Load(dr);
            //    dataGridView1.DataSource = dt;

            //}
            //else
            //{
            //    conn.Close();
            //}

            // 2 .YOL

            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            //DataPropertyName'e kolon ismi
            // filter where koşulu ekliyor
            // columns[0] diyip ilk kolon olan firstname'i seçtik

            //bs.Filter = string.Format("CONVERT("+dataGridView1.Columns[0].DataPropertyName+", System.String) LIKE '%"+txtName.Text+"%'");
            bs.Filter = string.Format("CONVERT({0}, System.String) LIKE '%{1}%'", dataGridView1.Columns[0].DataPropertyName, txtName.Text);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtName.Enabled = false;
            label1.ForeColor = Color.Red;
        }
    }
}
