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

namespace DisconnectedMimari2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=True ");
        // birden fazla şey açıp kapamak için MultipleActiveResultSets=True olması lazım datareader'a özel
        // bir bağlantıda sadece bir data reader çalışabilir ikinci bir datareader için öncekinin kapanması lazım
        // aynı anda iki data reader çalıştırmak için MultipleActiveResultSets=True yaptık

        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "select ProductName, UnitPrice, UnitsInStock from Products";
            
            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                // MultipleActiveResultSets=True eklemezse bu iki reader aynı anda çalışmıyor.
                SqlDataReader dr = cmd1.ExecuteReader();
                SqlDataReader dr2 = cmd2.ExecuteReader();

                MessageBox.Show("İşlem Başarılı");
            }
            else
            {
                conn.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories (CategoryName, Description) Values (@catName, @desc)";

            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            cmd2.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd2.Parameters.AddWithValue("@desc", txtDesc.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                // select cmd1 de olduğu için 
                SqlDataReader dr = cmd1.ExecuteReader();

                // 1.yol

                //DataTable dt = new DataTable();

                //if (dr.HasRows)
                //{
                //    // datatable doldurmak için load
                //    dt.Load(dr); // dr içindekileri dt ' ye yükledik
                //    // Load sa kendisi doluyordur, fill se başkasını dolduruyor
                //    dataGridView1.DataSource = dt;
                //    MessageBox.Show("İşlem Başarılı");
                //}

                // 2 yol

                dataGridView1.Columns.Add("CategoryName", "Kategoriler");
                dataGridView1.Columns.Add("Description", "Açıklamalar");

                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString());

                }
                          
            }
            else
            {
                conn.Close();
            }
            
        }
        SqlDataAdapter dap;

        private void btnInsertDisconn_Click(object sender, EventArgs e)
        {
            // 1 .yol

            //dap = new SqlDataAdapter("select CategoryName, Description from Categories", conn);   

            //SqlCommand cmd = new SqlCommand("Insert Categories (CategoryName, Description) Values (@catName, @desc)", conn);

            //dap.InsertCommand = cmd;

            //dap.InsertCommand.Parameters.AddWithValue("@catName", txtCatName.Text);
            //dap.InsertCommand.Parameters.AddWithValue("@desc", txtDesc.Text);

            //conn.Open();
            //dap.InsertCommand.ExecuteNonQuery();
            //MessageBox.Show("Kayıt ekleme başarılı");
            //conn.Close();

            //DataTable dt = new DataTable();
            //dap.Fill(dt);
            //dataGridView1.DataSource = dt;

            //2. yol

            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories (CategoryName, Description) Values (@catName, @desc)";

            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            cmd2.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd2.Parameters.AddWithValue("@desc", txtDesc.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                MessageBox.Show("İşlem başarılı");
            }
            else
            {
                conn.Close();
            }

            SqlDataAdapter dap = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            dap.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        List<string> lst = new List<string>();
        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories (CategoryName, Description) Values (@catName, @desc)";

            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            cmd2.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd2.Parameters.AddWithValue("@desc", txtDesc.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                SqlDataReader dr = cmd1.ExecuteReader();

                dataGridView1.Columns.Add("CategoryName", "Kategoriler");

                if (dr.HasRows)
                {
                    while(dr.Read())
                    {
                        // satır satır okuma yaptığı için buraya her geldiğinde ilk elemanmış gibi her kategori bilgisini list içine atar.
                        lst.Add(dr[0].ToString());
                    }

                    // datagrid view doldurma, kolona karşılık  gelmesi lazım
                    foreach(var item in lst)
                    {
                        dataGridView1.Rows.Add(item);
                    }
                    MessageBox.Show("İşlem Başarılı");
                }
            }
            else
            {
                conn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories (CategoryName, Description) Values (@catName, @desc)";

            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            cmd2.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd2.Parameters.AddWithValue("@desc", txtDesc.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                SqlDataReader dr = cmd1.ExecuteReader();

                dataGridView1.Columns.Add("CategoryName", "Kategoriler");
                dataGridView1.Columns.Add("Description", "Açıklamalar");

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString());
                    }
                    MessageBox.Show("İşlem Başarılı");
                }
            }
            else
            {
                conn.Close();
            }
        }
        List<Kategoriler> katList = new List<Kategoriler>();
        private void button4_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories (CategoryName, Description) Values (@catName, @desc)";

            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            cmd2.Parameters.AddWithValue("@catName", txtCatName.Text);
            cmd2.Parameters.AddWithValue("@desc", txtDesc.Text);

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                SqlDataReader dr = cmd1.ExecuteReader();

                //dataGridView1.Columns.Add("CategoryName", "Kategoriler");
                //dataGridView1.Columns.Add("Description", "Açıklamalar");
                

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Kategoriler k = new Kategoriler()
                        {
                            KatName = dr[0].ToString(),
                            KatAciklama = dr[1].ToString()
                        };
                        katList.Add(k);
                    }
                   
                    dataGridView1.DataSource = katList; 
                    // sorguda kat id gelmediği için hepsinin katidsi  0 görüyor 
                    dataGridView1.Columns[0].Visible = false;   // kat id 0 görünmesini istemediğimiz için kat id kolonunun visible'ını false yaptık.
                    // datagridview.datasoruce dan önce column visible satırını koyarsak Idex out off range hatası alırızç

                    MessageBox.Show("İşlem Başarılı");
                }
            }
            else
            {
                conn.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
