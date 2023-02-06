using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace searchEmployee
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DataTable dt;
        private void Form2_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Adi", typeof(string));
            dt.Columns.Add("Soyadi", typeof(string));
            dt.Columns.Add("Tarih", typeof(DateTime));

            dt.Rows.Add(1, "Ahmet", "Yılmaz", DateTime.Now);
            dt.Rows.Add(2, "Veli", "Koç", DateTime.Now);
            dt.Rows.Add(3, "Hatice", "Günay", DateTime.Now);
            dt.Rows.Add(4, "Sabri", "Sarıoğlu", DateTime.Now);

            dataGridView1.DataSource = dt;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(DataRow item in dt.Rows)
            {
                listBox1.Items.Add(string.Format("{0} -- {1} -- {2} -- {3}", item[0], item[1], item[2], item[3]));
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // tıklamaya göre ilgili row'un indeksini yakaladı, ilgi row indeksinden ilgili cells'ini yakaladı,
            // o cellsden ilgili row'un column'u yakaladı sonrada onu valuesunu 
            //listBox1.Items.Add(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()); // neye tıklarsan onu yakalar

            //listBox1.Items.Add(dataGridView1.SelectedCells[0].Value.ToString());

            //List<string> selectedRows = new List<string>();

            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    string currentRow = string.Empty;

            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        currentRow += String.Format("{0} ", cell.FormattedValue);
            //    }

            //    selectedRows.Add(currentRow);
            //}

            //for (int i = 0; i < selectedRows.Count; i++)
            //{
            //    this.listBox1.Items.Add(selectedRows[i]);


            //}

            //foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            //{
            //    listBox1.Items.Add(string.Format("{0} -- {1} -- {2} -- {3}", item.Cells[0].Value.ToString(),
            //    item.Cells[1].Value.ToString(),
            //    item.Cells[2].Value.ToString(), item.Cells[3].Value.ToString()));
            //}


            //foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            //{
            //    for(int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            //    {

            //    }
            //        listBox1.Items.Add(string.Format("{0} -- {1} -- {2} -- {3}", item.Cells[0].Value.ToString(), 
            //        item.Cells[1].Value.ToString(),
            //        item.Cells[2].Value.ToString(), item.Cells[3].Value.ToString()));
            //}

        }

    }
}
