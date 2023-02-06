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

namespace ViewandStoredProcedure
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True");

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btnView_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select * from vw_MusterileriGetir", conn);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;

                // Data Table olmadan yapılış

                //dataGridView1.Columns.Add("CompanyName", "ŞirketAdı");
                //dataGridView1.Columns.Add("City", "Şehir");
                //dataGridView1.Columns.Add("Country", "Ülke");
                //dataGridView1.Columns.Add("Phone", "Telefon");
                //while (dr.Read())
                //{
                //    dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                //}

            }
            else
            {
                conn.Close();
            }

        }
        
        private void btnSP_Click(object sender, EventArgs e)
        {
            // sp_MusterileriGetir
            SqlCommand cmd = new SqlCommand("sp_MusterileriGetir", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                //DataTable dt = new DataTable();
                //dt.Load(dr);
                //dataGridView1.DataSource = dt;

                if(dr.HasRows) // varsa 
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    MessageBox.Show("Kategori başarıyla EKLENDİ");
                }
                else
                {
                    MessageBox.Show("Data Yok");
                }

            }
            else
            {
                conn.Close();
            }

        }

        private void btnSpEkleGetir_Click(object sender, EventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand("sp_KategoriEkle", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd.Parameters.AddWithValue("@desc", txtDesc.Text);
            SqlCommand cmd2 = new SqlCommand("select CategoryName, Description from Categories", conn);


            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd2.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;

            }
            else
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            SqlCommand cmd = new SqlCommand("sp_KategoriEkle", conn, tran);
            SqlCommand cmd2 = new SqlCommand("select CategoryName, Description from Categories", conn, tran);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd.Parameters.AddWithValue("@desc", txtDesc.Text);

            try
            {

                cmd.ExecuteNonQuery();

                SqlDataReader dr = cmd2.ExecuteReader();

                if (dr.HasRows)
                {

                    DataTable dt = new DataTable();
                    dt.Load(dr);

                    dataGridView1.DataSource = dt;
                    MessageBox.Show("Kategori başarıyla eklendi");
                    tran.Commit(); // buraya geldiğinde işlem başarılı olmuş olmalı çünkü mesaj ekrana yazılmış o yüzden commit edilebilir
                }

                else
                {
                    MessageBox.Show("Data yok");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Beklenmedik bir hatayla karşılaşıldı");
                tran.Rollback();
            }
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn2 = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True"))
            {
                if (conn2.State == ConnectionState.Closed)
                {
                    conn2.Open();
                    SqlTransaction tran = conn2.BeginTransaction();

                    SqlCommand cmd = new SqlCommand("sp_KategoriEkle", conn2, tran);
                    SqlCommand cmd2 = new SqlCommand("select CategoryName, Description from Categories", conn2, tran);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@catName", txtCatName.Text);
                    cmd.Parameters.AddWithValue("@desc", txtDesc.Text);

                    try
                    {

                        cmd.ExecuteNonQuery();

                        SqlDataReader dr = cmd2.ExecuteReader();

                        if (dr.HasRows)
                        {

                            DataTable dt = new DataTable();
                            dt.Load(dr);

                            dataGridView1.DataSource = dt;
                            MessageBox.Show("Kategori başarıyla eklendi");
                            tran.Commit();
                        }

                        else
                        {
                            MessageBox.Show("Data yok");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Beklenmedik bir hatayla karşılaşıldı");
                        tran.Rollback();
                    }
                }
                else
                {
                    conn2.Close();
                }
             
            }
        }
    }
}
