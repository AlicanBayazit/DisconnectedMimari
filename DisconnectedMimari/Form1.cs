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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection("Server=DESKTOP-OEQJFLF\\MSSQLSERVER01;Database=Northwind;Trusted_Connection=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            // instance alabildiğim ve dispose methodu olan her nesne için, işim bittikten sonra ramden uzaklaştırmak istiyorsam using kullanılabilir
            // garbage collector'a randevu veriyor, işim bitince gel beni burdan al
            // birden fazla tablo ile alakalı işlem yaparken data set, bşrden fazla tablo alabiliyorsun
            // tek tablo ile alakalı işlem yaparken datatable, bir tablo alabiliyorsun
            SqlDataAdapter dap = new SqlDataAdapter("Select CategoryID, CategoryName from Categories", conn);
       
            DataTable dt = new DataTable();
            dap.Fill(dt); // dap'taki bilgiyi data table'a dolduruyoruz.

            // liste özelliği gösteren bütün nesnelerde data source özelliği vardır
            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "CategoryName"; // ön tarafta görünecek olan
            comboBox1.ValueMember = "CategoryID"; // arkada, cat name'e karşılık gelen cat is tuttuk

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("Select UnitPrice, ProductName from Products where CategoryID = @id", conn);
            dap.SelectCommand.Parameters.AddWithValue("@id", comboBox1.SelectedValue); // selected item desek cat name i alırdık, selecten value ile id aldık
            // dap'taki select'i seçmek için selectcommand kullandık

            DataSet ds = new DataSet(); // dataset en geniş kapsamlı olduğu için birden fazla tablo tuttuğu gibi tek bir tablo için de kullanılabilir.
            dap.Fill(ds); // dataset içini dap'tan gelen verilerle doldurduk.

            // dataset birden fazla tablo bulundurduğu için dt gibi eklemiyoruz, hangi tablo olduğunu belirtiyoruz.
            listBox1.DataSource = ds.Tables[0]; // ilk indexteki tabloyu alıyor, ilk indexte products
            //  listBox1.DataSource = ds.Tables["Products"]; // birden fazla tablo kullanıldığında bu şekilde isimle erişilebilir

            listBox1.DisplayMember = "ProductName"; // value
            listBox1.ValueMember = "UnitPrice"; // sadece product id yazma zorunluluğu yok, key
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) 
            {
                MessageBox.Show(listBox1.SelectedValue.ToString()); // listbox'ın value'sunda unitprice var, seçilen ürünün unitprice'ını getirir.
            }
        }
    }
}
