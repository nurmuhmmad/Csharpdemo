using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace MagazinBaza
{
    public partial class bazadanix : Form
    {
        OleDbConnection connection = new OleDbConnection();
        public bazadanix()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Project_manajer\MagazinBaza\MagazinBaza\bazacha.mdb";
        }    

       
        public void rowsColor()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int val = Int32.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                if (val < 5)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    if (val == 0)
                    {
                        timer1.Start();
                    }
                }
                else if (val >= 5 && val < 10)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                }
                else if (val >= 10 && val < 50)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int val = Int32.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                    if (val == 0)
                    {
                        if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.Red)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        }
                        else if (dataGridView1.Rows[i].DefaultCellStyle.BackColor == Color.White)
                        {
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //   MessageBox.Show("eror");
            }
        }

        private void bazadanix_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand comad = new OleDbCommand();
                comad.Connection = connection;
                string quary = "Select * from baza";
                comad.CommandText = quary;
                OleDbDataAdapter da = new OleDbDataAdapter(comad);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                rowsColor();
                connection.Close();
                dataGridView1.ColumnCount =9 ;
                 dataGridView1.RowCount = 1;
                 dataGridView1.Columns[0].HeaderText = "ID";
                 dataGridView1.Columns[1].HeaderText = "Имя";
                 dataGridView1.Columns[2].HeaderText = "Цена";
                 dataGridView1.Columns[3].HeaderText = "Продажа";
                 dataGridView1.Columns[4].HeaderText = "Фирма";
                 dataGridView1.Columns[5].HeaderText = "штрих код";
                 dataGridView1.Columns[8].HeaderText = "штук";
                 dataGridView1.Columns[6].Visible = false;//id joylashgan ustunni ko'rsatmidigan qilamiz, oddiy odamlar ko'rishi shart emas
                 dataGridView1.AllowUserToAddRows = false;
        
            }
            catch (Exception)
            {
                //MessageBox.Show("eror");
            }
        }
    }
}
