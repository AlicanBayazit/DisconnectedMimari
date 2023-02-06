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

namespace DisconnectedMimari
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlDataAdapter dap;
        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True");
        private void Form3_Load(object sender, EventArgs e)
        {
            dap = new SqlDataAdapter("Select CustomerID, CompanyName from Customers", conn); // dap nesnesine bir kere bir şey atamak gerekiyor
            // aşağıda dap.update şeklinde kullandığımızda çalışabilmesi için

            DataSet ds = new DataSet();
            dap.Fill(ds);
           
            listBox1.DataSource = ds.Tables[0]; 
            listBox1.DisplayMember = "CompanyName";
            listBox1.ValueMember = "CustomerID";
        }
       
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter dap = new SqlDataAdapter("Update Customers set CompanyName=@name where CustomerID = @id", conn);

            SqlCommand cmd = new SqlCommand("Update Customers set CompanyName=@name where CustomerID = @id", conn);

            dap.UpdateCommand = cmd;

            dap.UpdateCommand.Parameters.AddWithValue("@id", txtId.Text);
            dap.UpdateCommand.Parameters.AddWithValue("@name", txtCompanyName.Text);

            conn.Open();
            dap.UpdateCommand.ExecuteNonQuery();
            MessageBox.Show("İşlem Başarılı");
            conn.Close();            
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            txtId.Text = listBox1.SelectedValue.ToString();
            //1.yol
            //txtCompanyName.Text = listBox1.GetItemText(listBox1.SelectedItem); // display memberda gösterilen şeyler item

            //2.yol
            DataRowView drw = (DataRowView)listBox1.SelectedItem; // DataRowView o rowlardaki ilgili görünüm
            txtCompanyName.Text = drw["CompanyName"].ToString(); // drw içindeki company name bilgisi text box'a atıldı
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("Select CustomerID, CompanyName from Customers", conn);
            DataSet ds = new DataSet();

            dap.Fill(ds);

            listBox2.DataSource = ds.Tables[0];
            listBox2.DisplayMember = "CompanyName";
            listBox2.ValueMember = "CustomerID";

            listView1.Columns.Add("Id");
            listView1.Columns.Add("Name", 150);

            // Listbox 2 deki tüm kayıtları ekleme

            //foreach (DataRow item in ds.Tables[0].Rows)
            //{
            //    ListViewItem lvi = new ListViewItem();
            //    lvi.Text = item["CustomerID"].ToString();
            //    lvi.SubItems.Add(item["CompanyName"].ToString());
            //    listView1.Items.Add(lvi);

            //}
            
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            // Seçili list box 2 kaydını list view'a ekleme
            ListViewItem lvi = new ListViewItem();
            lvi.Text = listBox2.SelectedValue.ToString();
            DataRowView drw = (DataRowView)listBox2.SelectedItem; // DataRowView o rowlardaki ilgili görünüm
            lvi.SubItems.Add(drw["CompanyName"].ToString());
            listView1.Items.Add(lvi);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        //private void listBox3_DoubleClick(object sender, EventArgs e)
        //{
        //    listBox3.DataSource = listBox2.DataSource;
        //    listBox3.DisplayMember = listBox2.DisplayMember;
        //    listBox3.ValueMember = listBox2.ValueMember;
        //}
    }
}
