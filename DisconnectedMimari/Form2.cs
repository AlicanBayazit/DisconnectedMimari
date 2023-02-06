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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True");
        private void Form2_Load(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("Select CustomerID, CompanyName from Customers;Select ShipperID, CompanyName from Shippers", conn);

            //dap.SelectCommand 
            //dap.InsertCommand = new SqlCommand("insert ....", conn);
            //dap.UpdateCommand
            //dap.DeleteCommand

   

            DataSet ds = new DataSet();
            dap.Fill(ds);
            ds.Tables[0].TableName = "Musteriler";
            ds.Tables[1].TableName = "Kargocular";

            #region Data Table ile tablolara isim vererek dataset'e ekleme
            //DataTable dtCustomers = new DataTable("Musteriler");
            //DataTable dtShippers = new DataTable("Kargocular");
            //ds.Tables[0].TableName = dtCustomers.TableName;
            //ds.Tables[1].TableName = dtShippers.TableName;
            #endregion

            //listBox1.DataSource = ds.Tables[0]; // yukarıda tablename diyip bu indekslere isim vermezsek burada index vererek kullanırız
            listBox1.DataSource = ds.Tables["Musteriler"];
            listBox1.DisplayMember = "CompanyName";
            listBox1.ValueMember = "CustomerID";
            
            comboBox1.DataSource = ds.Tables["Kargocular"];
            comboBox1.DisplayMember = "CompanyName";
            comboBox1.ValueMember = "ShipperID";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter dap = new SqlDataAdapter("Select OrderID, OrderDate from Orders where CustomerID = @cid and ShipVia = @sid",conn);

            SqlDataAdapter dap = new SqlDataAdapter("Select (CONVERT(nvarchar(20), OrderID) + ' >> ' +  CONVERT(nvarchar(11), OrderDate)) as Erp4 from Orders where CustomerID = @cid and ShipVia = @sid", conn);

            dap.SelectCommand.Parameters.AddWithValue("@cid", listBox1.SelectedValue);
            dap.SelectCommand.Parameters.AddWithValue("@sid", comboBox1.SelectedValue);

            DataTable dt = new DataTable();
            dap.Fill(dt);
            listBox2.DataSource = dt;
            //listBox2.DisplayMember = "OrderDate";
            // burdan sonra bir şey yapmıycağımız çin value member eklemedik gerekli olursa eklenir
            listBox2.DisplayMember = "Erp4";
        }
    }
}
