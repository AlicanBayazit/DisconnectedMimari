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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True");
        BindingSource bs;
        private void btnSpEkleGetir_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select * from Categories", conn);
            conn.Open();
            bs = new BindingSource(); // geriye kalan yapman gereken ne kadar işlem varsa hepsini yapıyor
            SqlDataReader dr = cmd.ExecuteReader();
            bs.DataSource = dr;
            dataGridView1.DataSource = bs;
            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bs.MoveFirst(); // ilk kayda gidicek
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bs.MoveLast(); // son kayda gider
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.MovePrevious(); // önceki satıra doğru ilerle
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bs.MoveNext(); // sonraki satıra doğru ilerle        
        }
    }
}
