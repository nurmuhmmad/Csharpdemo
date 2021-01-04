using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;                    // bazga bog'lash uchun kutubxona ...
namespace MagazinBaza
{
    public partial class trade_info : Form
    {
        OleDbConnection connection = new OleDbConnection();
        int posX;
        int posY;
        bool drag;

        public trade_info()
        {
            InitializeComponent();
        //    connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\jiddiy_project_magazin\magazinbaza\magazinbaza\bin\bazacha.mdb";
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Project_manajer\MagazinBaza\MagazinBaza\bazacha.mdb";
        
            // baza joylashgan joy ...
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();                 // dasturdan chiqb ketish
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();                                         // bazani ochish 
                OleDbCommand comad = new OleDbCommand();                   // uzgaruvchi elon qilish
                comad.Connection = connection;                             // bazanni bog'lash
                string quary = "Select * from baza";                       // bazadan baza degan bulimni chiqar!!
                comad.CommandText = quary;                                 // commad.comatext ga string uzgaruvchi ol
                OleDbDataAdapter da = new OleDbDataAdapter(comad);         // uzagruvchi
                DataTable dt = new DataTable();                            // ustun ,satr elon qilish
                da.Fill(dt);                                               // rozilik bog'lash
                dataGridView1.DataSource = dt;                             // daagirdveyivga chiqar
                connection.Close();                                        // bazada ishni tamomla ...
            }
            catch (Exception ex)
            {
                MessageBox.Show("eroor" + ex);                              // agar xato bulsa eror ber ...
            }
           //===============================================================================//
           
        //=====================================================================================//
            try
            {
                connection.Open();
                OleDbCommand commad = new OleDbCommand();
                commad.Connection = connection;
                commad.CommandText = "Select * from prodaj";
                OleDbDataAdapter adap = new OleDbDataAdapter(commad);
                DataTable funksiya = new DataTable();
               
                adap.Fill(funksiya);
                dataGridView2.DataSource = funksiya;
                connection.Close();

            }
            catch (Exception Ex)
            { MessageBox.Show("eror"+Ex); }
            //==============================================//
            //hisob kitob ammallari!!
         
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

           
        }

        private void olmoqtxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            try
            {
                connection.Open();
                OleDbCommand commad = new OleDbCommand();
                commad.Connection = connection;
                commad.CommandText = "select * from summa";
                OleDbDataReader reader = commad.ExecuteReader();
                while (reader.Read())
                {
                    sotmoqtxt.Text = reader["sotmoq"].ToString();
                  

                }
                connection.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("eror!!");
            }
        }
    }
}
