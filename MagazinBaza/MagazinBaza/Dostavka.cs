using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagazinBaza
{
    public partial class Dostavka : Form
    {
        OleDbConnection connection = new OleDbConnection();

        public Dostavka()
        {
            InitializeComponent();
           connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Project_manajer\MagazinBaza\MagazinBaza\bazacha.mdb";
          //  connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bazacha.mdb";
           
        }

        #region window control functions
        private void btn_exit_Click(object sender, EventArgs e)
        {
                this.Close();                                                          // dasturdan chiqib ketish
            }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;                          // minimezed funksiyasidan foydalanish 
        }
        #endregion

        #region click events
        private void btn_table_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();                                                      // bazani ochish
                //dataGridView1.RowsDefaultCellStyle.BackColor = Color.White; 
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                OleDbCommand comad = new OleDbCommand();                                // bazada ishlash uchun uzgaruvchi e'lon qilish
                comad.Connection = connection;                                          // bog'lash
                string quary = "Select * from baza";                                    // bazadagi bulimiga zapros berish
                comad.CommandText = quary;                                              // tastiqlash
                OleDbDataAdapter da = new OleDbDataAdapter(comad);                      // ustunlarni chiqarish
                DataTable dt = new DataTable();                                         // ustunlarni chiqarish 
                da.Fill(dt);                                                            // birlashtirish
                data_grid.DataSource = dt;                                              // chiqar
                connection.Close();                                                     // bazani yopish
            }
            catch (Exception)
            {
                // MessageBox.Show("eror");
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
          
            try
            {
              
                connection.Open();                                                     // bazani ochish 
                OleDbCommand commad = new OleDbCommand();                              // bazani bog'lash
                commad.Connection = connection;                                        // ulash
                commad.CommandText = "Select * from baza where kod='" + txt_qr_code.Text + "'"; // qrocode bir xil bulmasligi
                OleDbDataReader reader = commad.ExecuteReader();                       // uqish uchun 
                int k = 0;                                                             // uzgaruvchi
                while (reader.Read())                                                  // sikl 
                { 
                    k++;                                                               // sanash
                }
                connection.Close();                                                    // bazani yopish
                if (txt_product.Text == "" || txt_price.Text == "" || txt_trade.Text == "" || txt_firm.Text == "" || txt_qr_code.Text == "" || cmb_limit.Text == "" || txt_amount.Text == "")
                {
                    MessageBox.Show("Malumotlarni tuldiring!!");                       // bazani tuldirish haqida elon berish
                }
                double m, b, j, s;
                m = Convert.ToDouble(txt_price.Text);                                 // maxsulot narxi
                b = Convert.ToDouble(txt_amount.Text);                                // maxsulot soni 
                j = Convert.ToDouble(textBox4.Text);                                  // bazadagi summa
                s = m * b + j;                                                        // soni * narxini + bazadagi summa = s
                textBox3.Text = Convert.ToString(s);                                  // tex3 ga quy !!
               // else
                {
                    if (k > 0)                                                             // bazada malumot bor bulsa 
                    {
                        MessageBox.Show("malumot bazada bor");                             // elon qilish
                    }
                    else                                                                   // bazada malumot bulmasa malumot qushadi 
                    {
                        {

                           
                            connection.Open();                                             // bazani ochish
                            OleDbCommand com = new OleDbCommand();                         // uzgaruvchi elon qilish
                            com.Connection = connection;                                   // bog'lash 
                            string a = "Insert into baza(nom,narxi,sotmoq,firma,kod,muddat,soni,sana) values('" + txt_product.Text + "','" + txt_price.Text + "','" + txt_trade.Text + "','" + txt_firm.Text + "','" + txt_qr_code.Text + "','" + cmb_limit.Text + "','" + txt_amount.Text + "','" + DateTime.Now.ToString() + "')";
                            com.CommandText = a;                                           // bazaga kiritish
                            com.ExecuteNonQuery();                                         // tasdiqlash
                            MessageBox.Show("Bazaga malumot tushdi");                      // elon qilish xabarni 
                            txt_product.Text = "";                                         // textlarni tozalash ...
                            txt_price.Text = "";
                            txt_trade.Text = "";
                            txt_firm.Text = "";
                            txt_qr_code.Text = "";
                            cmb_limit.Text = "";
                            txt_amount.Text = "";                                                               
                            OleDbCommand olmoq = new OleDbCommand();                          // uzgaruvchi elon qilish
                            olmoq.Connection = connection;                                    // bog'lash baza bilan dasturni  
                            olmoq.CommandText = "Update summa set olmoq='" + textBox3.Text + "'where ID=1"; // uzgartirish
                            olmoq.ExecuteNonQuery();                                          // tastiqlash
                           // MessageBox.Show(com.CommandText);                               // elon qilish   
                            connection.Close();       
                        
                        }
                    }
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Eror");                                              // agar bazada xatolik bulsa eror chiqar
            }
        }
        #endregion

        private void btn_add_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button2.Visible = false;
            button1.Visible = false;
            btn_send.Visible = true;
            comboBox1.Visible = false;

            txt_product.Enabled = true;                                                    // text1 ga nomni chiqrar .... 
            txt_price.Enabled = true;
            txt_trade.Enabled = true;
            txt_firm.Enabled = true;
            txt_qr_code.Enabled = true;
            cmb_limit.Enabled = true;
            txt_amount.Visible = true;
            textBox2.Visible = false;
        }

        private void btn_simple_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button1.Visible = true;
            btn_send.Visible = false;
            comboBox1.Visible = true;
            button2.Visible = false;
            //==========================//
            txt_product.Enabled=false;                                                      // text1 ga nomni chiqrar .... 
            txt_price.Enabled=false;
            txt_trade.Enabled=false;
            txt_firm.Enabled=false;
            txt_qr_code.Enabled=false;
            cmb_limit.Enabled=false;
            txt_amount.Visible = false;
            textBox2.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bazaga oldingi maxsulot kelsa qushub quyish
            double a, s, b,k,j,m;                                   // uzgaruvchilar olingan
            a = Convert.ToDouble(txt_amount.Text);            // a degan uzgaruvchi text7
            b = Convert.ToDouble(textBox2.Text);              // b degan uzgaruvchi text6
            j = Convert.ToDouble(txt_price.Text);
            m = Convert.ToDouble(textBox4.Text);
            s = a + b;                                        // s=a+b;
            k = b * j + m ;                                   // k bunda firmadan olayotgan maxsulotlar tuplamini hisoblab beradi.
            textBox3.Text = Convert.ToString(k);
            txt_amount.Text = Convert.ToString(s);            // text6 ni textiga s uzlashtir
            try
            {
                connection.Open();                            // dasturni bazaga bog'lash
                OleDbCommand uzgartir = new OleDbCommand();   // uzgaruvchi elon qilish
                uzgartir.Connection = connection;             // ulash 
                uzgartir.CommandText = "Update baza set soni='" + txt_amount.Text + "'where ID=" + textBox1.Text + ""; // shart shunaqa bulsa
                uzgartir.ExecuteNonQuery();                   // uzgartir
               //  MessageBox.Show(uzgartir.CommandText);
                txt_product.Clear();                          // textBox larni tozalash.... 
                txt_price.Clear();
                txt_trade.Clear();
                txt_firm.Clear();
                txt_qr_code.Clear();
                cmb_limit.Text = "";
                txt_amount.Clear();
                textBox2.Text = "0";
                connection.Close();                           // bog'lash tugadi
            }
            catch (Exception)
            {
                 //MessageBox.Show("Eror");                   // shart bajarilmasa eror chiqar
            }
            //bu bulim magazin firmalardan maxsulot olganda obshe summani hisoblab ketaveradi ....
            try                                               // ish boshlash
            {
                connection.Open();                            // bazani ochish
                OleDbCommand com = new OleDbCommand();        // uzgaruvchi elon qilish
                com.Connection = connection;                  // bog'lash baza bilan dasturni  
                com.CommandText = "Update summa set olmoq='" + textBox3.Text + "'where ID=1"; // uzgartirish
                com.ExecuteNonQuery();                        // tastiqlash
                //MessageBox.Show(com.CommandText);           // elon qilish   
                connection.Close();                           // bazani yopish
            }
            catch (Exception)
            {
             //   MessageBox.Show("eror");                    // xato bulsa ekranga chiqar
            }
            try
            {
                connection.Open();                                                      // bazani ochish
                //dataGridView1.RowsDefaultCellStyle.BackColor = Color.White; 
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                OleDbCommand comad = new OleDbCommand();                                // bazada ishlash uchun uzgaruvchi e'lon qilish
                comad.Connection = connection;                                          // bog'lash
                string quary = "Select * from baza";                                    // bazadagi bulimiga zapros berish
                comad.CommandText = quary;                                              // tastiqlash
                OleDbDataAdapter da = new OleDbDataAdapter(comad);                      // ustunlarni chiqarish
                DataTable dt = new DataTable();                                         // ustunlarni chiqarish 
                da.Fill(dt);                                                            // birlashtirish
                data_grid.DataSource = dt;                                              // chiqar
                connection.Close();                                                     // bazani yopish
            }
            catch (Exception)
            {
                // MessageBox.Show("eror");
            }
            comboBox1.Items.Clear();
            try
            {
                connection.Open();                                // dasturni bazaga yulini 
                OleDbCommand chiqar = new OleDbCommand();         // uzgaruvchi
                chiqar.Connection = connection;                   // bog'lash  
                chiqar.CommandText = "Select * from baza";        // bazaga dan bulimini belgilash
                OleDbDataReader read = chiqar.ExecuteReader();    // read funksiyasi
                while (read.Read())                               // sikl operatori
                {
                    comboBox1.Items.Add(read["nom"].ToString());  // comoboxsga chiqaradigan malumot!!
                }
                connection.Close();                               // dastur bazadagi ishi tuxtadi
            }
            catch (Exception)
            {
                //MessageBox.Show("Eror");                        // agar shart bajarilmasa eror chiqar!!
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //comoboxsga malumotlarni chiqaradigan kod -->
            try
            {
                connection.Open();                                // dasturni bazaga yulini 
                OleDbCommand chiqar = new OleDbCommand();         // uzgaruvchi
                chiqar.Connection = connection;                   // bog'lash  
                chiqar.CommandText = "Select * from baza";        // bazaga dan bulimini belgilash
                OleDbDataReader read = chiqar.ExecuteReader();    // read funksiyasi
                while (read.Read())                               // sikl operatori
                {
                    comboBox1.Items.Add(read["nom"].ToString());  // comoboxsga chiqaradigan malumot!!
                }
                connection.Close();                               // dastur bazadagi ishi tuxtadi
            }
            catch (Exception)
            {
                //MessageBox.Show("Eror");                        // agar shart bajarilmasa eror chiqar!!
            }
            try                                                   // ishni boshlash 
            {
                connection.Open();                                // bazani ochish 
                OleDbCommand com = new OleDbCommand();            // uzgaruvchi elon qilish
                com.Connection = connection;                      // baza bilan dasturni bog'lash
                com.CommandText = "Select * from summa";          // bazadan summa bulimini top
                OleDbDataReader reader = com.ExecuteReader();     // o'qish uchun read funksiyasini chaqirish 
                while(reader.Read())                              // sikl berish
                { textBox4.Text = reader["olmoq"].ToString(); }   // text4 ga olmoqni bazadagi  narxini chiqar
                connection.Close();                               // bazani yop ...

            }
            catch (Exception)
            {
                MessageBox.Show("Eror");                           // agar xatoik bulsa eror deb elon ber
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comocboxga yoziladigan kod 
            try
            {
                connection.Open();                                                           // bazani dasturga  bog'lash 
                OleDbCommand yoz = new OleDbCommand();                                       // uzgaruvchi elon qilish 
                yoz.Connection = connection;                                                 // conect qilish
                yoz.CommandText = "select * from baza where nom='" + comboBox1.Text + "'";   // agar comobox1 da bazadagi nom bulsa 
               
                OleDbDataReader read = yoz.ExecuteReader();                                  // read uqish funksiya e'lon qilish
                while (read.Read())                                                          // sikl ochish
                {
                    txt_product.Text = read["nom"].ToString();                                       // text1 ga nomni chiqrar .... 
                    txt_price.Text = read["narxi"].ToString();
                    txt_trade.Text = read["sotmoq"].ToString();
                    txt_firm.Text = read["firma"].ToString();
                    txt_qr_code.Text = read["kod"].ToString();
                    cmb_limit.Text = read["muddat"].ToString();
                    txt_amount.Text = read["soni"].ToString();
                    textBox1.Text = read["ID"].ToString();
                }
                OleDbCommand commad = new OleDbCommand();
                commad.Connection = connection;
               
                connection.Close();                                                           // dastur b   ilan baza ishi tuxtadi

            }
            catch (Exception)
            {
               // MessageBox.Show("Eror");                                                      // shart bajarilmasa eror chiqar 
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            button3.Visible = false;
            button1.Visible = false;
            btn_send.Visible = false;
            comboBox1.Visible = true;
            button2.Visible = true;
            txt_product.Enabled = true;                                       // text1 ga nomni chiqrar .... 
            txt_price.Enabled = true;
            txt_trade.Enabled = true;
            txt_firm.Enabled = true;
            txt_qr_code.Enabled = true;
            cmb_limit.Enabled = true;
            txt_amount.Visible = true;
            textBox2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // malumumotlarni uzgardiradigan bulim 
            try
            {
                connection.Open();                            // bazani ochish 
                OleDbCommand uzgar = new OleDbCommand();      // bazada ish olib borish uchun uzgaruvchi elon qilish
                uzgar.Connection = connection;                // ulash
                uzgar.CommandText = "Update baza set nom='" + txt_product.Text + "',narxi='" + txt_price.Text + "',sotmoq='" + txt_trade.Text + "',firma='" + txt_firm.Text + "',kod='" + txt_qr_code.Text + "',muddat='" + cmb_limit.Text + "',soni='" + txt_amount.Text + "'Where ID=" + textBox1.Text + "";  // uzgartirish      
                uzgar.ExecuteNonQuery();                      // uzgartir
                MessageBox.Show("Malumot uzgardi!!");         // elon qilish
                txt_qr_code.Text = "";                        // textlarni tozalash....  
                textBox2.Text = "";
                txt_product.Text = "";
                txt_price.Text = "";
                txt_trade.Text = "";
                txt_firm.Text = "";
                cmb_limit.Text = "";
                txt_amount.Text = "";
                comboBox1.Text = "";
                connection.Close();                           // bazani yopish
            }
            catch (Exception)
            {
                //  MessageBox.Show("eror");                 // xatolik bulsa eror
            }
            try
            {
                connection.Open();                                                      // bazani ochish
                //dataGridView1.RowsDefaultCellStyle.BackColor = Color.White; 
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                OleDbCommand comad = new OleDbCommand();                                // bazada ishlash uchun uzgaruvchi e'lon qilish
                comad.Connection = connection;                                          // bog'lash
                string quary = "Select * from baza";                                    // bazadagi bulimiga zapros berish
                comad.CommandText = quary;                                              // tastiqlash
                OleDbDataAdapter da = new OleDbDataAdapter(comad);                      // ustunlarni chiqarish
                DataTable dt = new DataTable();                                         // ustunlarni chiqarish 
                da.Fill(dt);                                                            // birlashtirish
                data_grid.DataSource = dt;                                              // chiqar
                connection.Close();                                                     // bazani yopish
            }
            catch (Exception)
            {
                // MessageBox.Show("eror");
            }
            comboBox1.Items.Clear();
            try
            {
                connection.Open();                                // dasturni bazaga yulini 
                OleDbCommand chiqar = new OleDbCommand();         // uzgaruvchi
                chiqar.Connection = connection;                   // bog'lash  
                chiqar.CommandText = "Select * from baza";        // bazaga dan bulimini belgilash
                OleDbDataReader read = chiqar.ExecuteReader();    // read funksiyasi
                while (read.Read())                               // sikl operatori
                {
                    comboBox1.Items.Add(read["nom"].ToString());  // comoboxsga chiqaradigan malumot!!
                }
                connection.Close();                               // dastur bazadagi ishi tuxtadi
            }
            catch (Exception)
            {
                //MessageBox.Show("Eror");                        // agar shart bajarilmasa eror chiqar!!
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button1.Visible = false;
            btn_send.Visible = false;
            comboBox1.Visible = true;
            button2.Visible = false;
            txt_product.Enabled = true;
            txt_price.Enabled = true;
            txt_trade.Enabled = true;
            txt_firm.Enabled = true;
            txt_qr_code.Enabled = true;
            cmb_limit.Enabled = true;
            txt_amount.Visible = true;
            textBox2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //keraksiz malumotlaarni uchirish
            try
            {
                connection.Open();                              //bazani ochish
                OleDbCommand delete = new OleDbCommand();       //baza bilan ishlash uchun uzgaruvchi elon qilib olish
                delete.Connection = connection;                 // ulash 
                delete.CommandText = "Delete from baza where ID=" + textBox1.Text + ""; // malumotlar uchirish  
                delete.ExecuteNonQuery();                       // tastiqlash
                MessageBox.Show("malumotlar uchirildi!!");      // elon qilish
                txt_qr_code.Clear();                            // textlarni tozlash....
                textBox2.Clear();
                txt_product.Clear();
                txt_price.Clear();
                txt_trade.Clear();
                txt_firm.Clear();
                comboBox1.Text = "";
                txt_amount.Text = "";
                cmb_limit.Text = "";
                connection.Close();                             //bazani yopish
            }
            catch (Exception)
            {
                //MessageBox.Show("eror");                      // bog'lana olmasa eror chiqar!! 
            }
            try
            {
                connection.Open();                                                      // bazani ochish
                //dataGridView1.RowsDefaultCellStyle.BackColor = Color.White; 
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;
                OleDbCommand comad = new OleDbCommand();                                // bazada ishlash uchun uzgaruvchi e'lon qilish
                comad.Connection = connection;                                          // bog'lash
                string quary = "Select * from baza";                                    // bazadagi bulimiga zapros berish
                comad.CommandText = quary;                                              // tastiqlash
                OleDbDataAdapter da = new OleDbDataAdapter(comad);                      // ustunlarni chiqarish
                DataTable dt = new DataTable();                                         // ustunlarni chiqarish 
                da.Fill(dt);                                                            // birlashtirish
                data_grid.DataSource = dt;                                              // chiqar
                connection.Close();                                                     // bazani yopish
            }
            catch (Exception)
            {
                // MessageBox.Show("eror");
            }
            comboBox1.Items.Clear();
            try
            {
                connection.Open();                                // dasturni bazaga yulini 
                OleDbCommand chiqar = new OleDbCommand();         // uzgaruvchi
                chiqar.Connection = connection;                   // bog'lash  
                chiqar.CommandText = "Select * from baza";        // bazaga dan bulimini belgilash
                OleDbDataReader read = chiqar.ExecuteReader();    // read funksiyasi
                while (read.Read())                               // sikl operatori
                {
                    comboBox1.Items.Add(read["nom"].ToString());  // comoboxsga chiqaradigan malumot!!
                }
                connection.Close();                               // dastur bazadagi ishi tuxtadi
            }
            catch (Exception)
            {
                //MessageBox.Show("Eror");                        // agar shart bajarilmasa eror chiqar!!
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_base_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
