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
    public partial class Precent : Form
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Project_manajer\MagazinBaza\MagazinBaza\bazacha.mdb");
        int posX;
        int posY;
        bool drag;

        public Precent()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                posX = Cursor.Position.X - this.Left;
                posY = Cursor.Position.Y - this.Top;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                this.Top = System.Windows.Forms.Cursor.Position.Y - posY;
                this.Left = System.Windows.Forms.Cursor.Position.X - posX;
            }
            this.Cursor = Cursors.Default;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }

        private void Precent_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bazachaDataSet2.summa' table. You can move, or remove it, as needed.
            
            // TODO: This line of code loads data into the 'bazachaDataSet1.summa' table. You can move, or remove it, as needed.
//            this.summaTableAdapter.Fill(this.bazachaDataSet1.summa);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand commad = new OleDbCommand();
                commad.Connection = connection;
                string quary = "select * from summa";
                commad.CommandText = quary;
                OleDbDataReader reader = commad.ExecuteReader();
                while (reader.Read())
                {
                    chart1.Series["nur"].Points.AddXY(reader["sotmoq"].ToString(), reader["olmoq"].ToString());
                }
                connection.Close();
            }
            catch (Exception xa)
            { MessageBox.Show("eror" + xa); }
        }
    }
}
