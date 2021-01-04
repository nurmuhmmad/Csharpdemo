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
namespace baza
{
    public partial class Form1 : Form
    {//Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\REWARD_1\Desktop\baza\baza\baza\bin\Debug\bazamuhim.mdb
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:bazamuhim.mdb");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string baza = "Insert into baza(fam,ism) values('"+textBox1.Text+"','"+textBox2.Text+"')";
                OleDbCommand commad = new OleDbCommand(baza,connection);
                commad.ExecuteNonQuery();
                MessageBox.Show("Malumot bazaga tushdi");
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
