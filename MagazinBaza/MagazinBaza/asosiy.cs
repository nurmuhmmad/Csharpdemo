using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;                                       // baza bilan bog'lnish uchun kutubxona

namespace MagazinBaza
{
    public partial class asosiy : Form
    {
      bool first;                                               // dasturda kankulyator uchun qilingan ish 
      string ID;                                                // bazadan maxsulotlar ID sini chiqarish uchun !!
      int qator = 0;                                            // datagridviewdagi qator nomeri, ya'ni xaridorning nechanchi mahsulotiligi
      int uchirilganlar=0;                                      // o'chirilganlarni sanab boradi
      string thisId;                                            // xaridorning sotib olayotgan mahsulotining Idsi, dataGridViewga yozish uchungina kerak
      decimal thisUmumiySumma = 0;                              // xaridorning umumiy to'lashi kerak bo'lgan summasi
        
       OleDbConnection connection = new OleDbConnection();      // connection degan uzgaruvchi elon qilib bazada shundan foydalanamiz!!
     
        Timer t = new Timer();                                  // t degan vaqt uzgaruvchisini oldik 
        public asosiy()
        {
            InitializeComponent();
            c = true;                                           // kankulyator dagi bull funksiyasi uchun ishlatilgan
            textBox4.Text = "";                                 // text4 ni tozala
            textBox9.Text = "0";                                // text9 ni textiga o bilan almashtir
            connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Project_manajer\MagazinBaza\MagazinBaza\bazacha.mdb";
            //connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bazacha.mdb";
            // bazani kanpiturdagi joylashgan joyi ... 
            //DataGridViewni boshlang'ich xossalari

            dataGridView1.ColumnCount = 7;
            dataGridView1.RowCount = 1;
            dataGridView1.Columns[0].HeaderText = "#";
            dataGridView1.Columns[1].HeaderText = "Код";
            dataGridView1.Columns[2].HeaderText = "Наименование";
            dataGridView1.Columns[3].HeaderText = "Цена";
            dataGridView1.Columns[4].HeaderText = "Количество";
            dataGridView1.Columns[5].HeaderText = "Сумма";
            dataGridView1.Columns[6].HeaderText = "id";
            dataGridView1.Columns[6].Visible = false;//id joylashgan ustunni ko'rsatmidigan qilamiz, oddiy odamlar ko'rishi shart emas
            dataGridView1.AllowUserToAddRows = false;
        
        }
        bool c;                                                 // bool degan uzgaruvchi kankulyator uchun ...
        private void button28_Click(object sender, EventArgs e)
        {
            password g = new password();                                     // dasturdan chiqish amali ...
            g.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
          
        }

        private void button21_Click_1(object sender, EventArgs e)
        {  // dostavka bulimaga utish kodi ...
            Dostavka a = new Dostavka();                         // yangi uzgaruvchi
            a.Show();                                            // amalning bajarilishi

        }

        private void asosiy_Click(object sender, EventArgs e)
        {
         
        }
        private void t_Tick(object sender, EventArgs e)
        { 
            label7.Text = DateTime.Now.ToLongTimeString(); ; // kanpitur vaqtiga ulab quyish 
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void asosiy_Load(object sender, EventArgs e)
        {
           // t.Interval = 1000;                                    //in millsecond
            t.Tick += new EventHandler(this.t_Tick);              // lablel 18 ni harakatga keltirish
            t.Start();                                            // kanpitur vaqtini startga quyish...
        }

        private void hisob(object sender, EventArgs e)
        { 
            // cankulyator vasivasini bajarish uchun ishlatilgan kod ...
              try                                                    // try bu xato bulsa dasturdan chiqib ketma degani ..
            {
                Button b = sender as Button;                       // b degan uzgaruvchi bu qaysi button bosilsa shuni textini text9 yoz degani
          
                if (b.Text == "<-")                                // agar <- belgi bulsa
                {
                    if (textBox9.Text.Length > 0) textBox9.Text = textBox9.Text.Substring(0, textBox9.Text.Length - 1); // text9 dan bitta oxiridan qiymat olib tashla
                    if (textBox9.Text == "") textBox9.Text = "0";  // agar hech nima bulmasa 0 bilan almashtir
                }
                else if (b.Text == "X")                                // agar X belgi bulsa 
                {
                  //dataGridViewdagi qatorni olib tashlash:
                  //Umumiy summadan ayirish
                  thisUmumiySumma -= Convert.ToDecimal(dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells[5].Value.ToString());
                  dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                  uchirilganlar++;
                  label13.Text = thisUmumiySumma.ToString();
                }
                else                                               // unday bulmasa
                {
                    if (textBox9.Text == "0" || first)                      // text9 da 0 bulsa
                    {
                        textBox9.Text = b.Text;                    // text9 ga buttonlarni textini quy ..
                        first = false;
                    }
                    else
                        textBox9.Text += b.Text;                   // boshqa qiymatlar ham bulsa shuning orqasiga davom et..
                  //bazada mavjud sonini tekshirish, undan ko'p bo'sa omaslik uchun
                    connection.Open();                                                  // bazani ochish 
                    OleDbCommand commad = new OleDbCommand();                           // bazada ishlash uchun uzgaruvchi elon qilish
                    commad.Connection = connection;                                     // bog'lash baza bilan
                    commad.CommandText = "Select soni from baza where id=" + thisId;       // id bo'yicha
                    OleDbDataReader reader = commad.ExecuteReader();                    // read funksiyasini elon qilish 
                    reader.Read();
                    int bazadagi_soni = Int32.Parse(reader["soni"].ToString());
                    if (bazadagi_soni < Int32.Parse(textBox9.Text)) {
                      MessageBox.Show("Максимальное количество - " + bazadagi_soni);
                      textBox9.Text = bazadagi_soni.ToString();
                    }  
                  connection.Close();
                    
                }
              
            }
            catch (Exception ex)                                      // xato bulsa dasturdan chiqib ketma ...
            { MessageBox.Show(ex.Message.ToString()); }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {     // qrcode orqali uqiladigan bulim ...
           try                                                                       // xato bulsishini oldini olish
            {
                connection.Open();                                                   // bazani ochish 
                OleDbCommand commad = new OleDbCommand();                            // bazada ishlash uchun uzgaruvchi elon qilish
                commad.Connection = connection;                                      // bog'lash baza bilan
                commad.CommandText = "Select * from baza where kod='" + textBox3.Text + "'"; // text3 da qrcod bulsa
                OleDbDataReader reader = commad.ExecuteReader();                     // read funksiyasini elon qilish 
                while (reader.Read())                                                // sikl orqali bazani tekshirish
                {
                    textBox9.Text = "1";                                             // text9 ga 1 quy
                    first = true;                                                    // first bul funksiyasi true qilib quyish
                    summatxt.Text = reader["sotmoq"].ToString();                     // text6 ga ham bazadagi narx ustinidagi qiymatni chiqar
                    firmatxt.Text = reader["firma"].ToString();                      // text7 ga  bazadagi firma ustinidagi qiymatni chiqar  
                    sroktxt.Text = reader["muddat"].ToString();                      // text8 ga  bazadagi muddat ustinidagi qiymatni chiqar
                    nomtxt.Text = reader["nom"].ToString();                          // text5 ga  bazadagi nom ustinidagi qiymatni chiqar
                    thisId = reader["id"].ToString();
                    textBox9_TextChanged(sender, e);                                 // hisob kitobni ishga tushurvoradi
                }

                connection.Close();                                                  // baza bilan ish tugadi ...
            }
            catch (Exception)                                                        // agar xoto bulsa dasturdan chiqma 
            { }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            textBox9.Clear();                                                        // text9 ni tozalash
            textBox4.Clear();                                                        // text4 ni tozalash
            nomtxt.Text="";                                                        // text5 ni tozalash 
            summatxt.Text="";                                                        // text6 ni tozalash
            firmatxt.Text = ""; ;                                                        // text7 ni tozalash
            textBox3.Clear();                                                        // text3 ni tozalash
            sroktxt.Text = ""; ;                                                        // text8 ni tozalash
            label13.Text = ""; ;                                                        // text2 ni tozalash
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();                                                    // bazani ochish
                string quary = "select nom from baza where nom like \"%" + textBox1.Text + "%\"";// text1 da qrcode qiymati bulsa 
                OleDbCommand commad = new OleDbCommand(quary, connection);            // baza bilan ishlashda uzgaruvchi elon qilish
                OleDbDataReader reader = commad.ExecuteReader();                      // uqish uchun read funksiyasi
                listBox1.Items.Clear();                                               // listboxsni tozala
                while (reader.Read())                                                 // sikl ichida bazani uqi 
                {
                    listBox1.Items.Add(reader.GetString(0));                          // bazagadagi nomni chiqar listboxga
                }
                connection.Close();                                                   // bazani yopish
                if (listBox1.Items.Count > 0)                                         // text1 ga suz yozaversak listboxsdagi qiymati kamayib boraversin ...
                {
                    listBox1.SelectedIndex = 0;                                       // textda bazadagi malumot bulmasa listboxni textiga 0 quy 
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Bazaga ulana olmayapti!!");                          // xatolik yuz bersa elon qilish
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        { 

            textBox9.Text = "1";
            first = true;
            //==============================================================//

            //==== barcha kerakli ma'lumotlar bittada olinadi!!
            try
            {
                connection.Open();                                                        // bazani ochish 
                string nomi = "Select id, nom, sotmoq, firma, muddat from baza where nom like \"" + listBox1.Items[listBox1.SelectedIndex] + "\"";//hammasini belgilash
                OleDbCommand newsqlcmd = new OleDbCommand(nomi, connection);              // bog'lash va uzgaruvchi elon qilish
                OleDbDataReader newsqlrdr = newsqlcmd.ExecuteReader();                    // uqish uchun uzgaruvchi elon qilish
                while (newsqlrdr.Read())                                                  // sikl berish
                { 
                    textBox4.Text=newsqlrdr["sotmoq"].ToString();                         // tex4 ga sotmoq ni chiqar
                    summatxt.Text = newsqlrdr["sotmoq"].ToString();                       // tex6 ga sotmoq ni chiqar  
                    nomtxt.Text = newsqlrdr["nom"].ToString();                          // tex5 ga nom ni chiqar
                    firmatxt.Text = newsqlrdr["firma"].ToString();                        // tex6 ga firma ni chiqar
                    sroktxt.Text = newsqlrdr["muddat"].ToString();                       // tex8 ga muddat ni chiqar
                    thisId = newsqlrdr["id"].ToString();                                  // // tex4 ga ID ni chiqar
                }
                connection.Close();                                                       // bazani yopish
            }
            catch (Exception)                                                             // baza xato bulsa dasturdan chiqib ketma
            {
                MessageBox.Show("eror");                                                  // eror deb elon qil!!
            }


        }

        private void button23_Click(object sender, EventArgs e)
        {
                                          
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try                                                    // try bu xato bulsa dasturdan chiqib ketma degani ..
            {
                button46.Enabled = true;
                Button b = sender as Button;                       // b degan uzgaruvchi bu qaysi button bosilsa shuni textini text9 yoz degani
                if (b.Text == "+")                                 // + bosganda datagridviewga qo'shib qo'yadi
                {
                  dataGridView1.RowCount = qator + 1-uchirilganlar;
                  //yangi qator qo'shiladi, pastdagilar ustunlarga kerakli ma'lumotlarni yozib chiqish
                  dataGridView1.Rows[qator - uchirilganlar].Cells[0].Value = (qator + 1).ToString();
                  dataGridView1.Rows[qator - uchirilganlar].Cells[1].Value = textBox3.Text;
                  dataGridView1.Rows[qator - uchirilganlar].Cells[2].Value = nomtxt.Text;
                  dataGridView1.Rows[qator - uchirilganlar].Cells[3].Value = summatxt.Text;  
                  dataGridView1.Rows[qator - uchirilganlar].Cells[4].Value = textBox9.Text;
                  dataGridView1.Rows[qator - uchirilganlar].Cells[5].Value = textBox4.Text;
                  dataGridView1.Rows[qator - uchirilganlar].Cells[6].Value = thisId;   
                  thisUmumiySumma += Decimal.Parse(textBox4.Text);
                  textBox9.Text = "0";
                  textBox4.Text = "0";
                  textBox3.Text = "";
                  qator++;
                  firmatxt.Text = "";
                  sroktxt.Text = "";
                  nomtxt.Text = "";
                  label13.Text = thisUmumiySumma.ToString();
                  summatxt.Text = "";
                    
                }

                
                else if (b.Text == "<-")                          // agar <- belgi bulsa
                {
                    if (textBox9.Text.Length > 0) textBox9.Text = textBox9.Text.Substring(0, textBox9.Text.Length - 1); // text9 dan bitta oxiridan qiymat olib tashla
                    if (textBox9.Text == "") textBox9.Text = "0";  // agar hech nima bulmasa 0 bilan almashtir
                }
                else                                               // unday bulmasa
                {
                    if (textBox9.Text == "0")                      // text9 da 0 bulsa
                        textBox9.Text = b.Text;                    // text9 ga buttonlarni textini quy ..
                    else
                        textBox9.Text += b.Text;                   // boshqa qiymatlar ham bulsa shuning orqasiga davom et..
                }
            }
            catch (Exception)                                      // xato bulsa dasturdan chiqib ketma ...
            { }
        }

        private void hisob2barobar(object sender, EventArgs e)
        {
            double baza_summa, hozir_summa, natija_summa;         // uzgaruvchilarni elon qilish
            baza_summa = Convert.ToDouble(textBox10.Text);        // bazadagi sotmoqdagi summa ...
            hozir_summa = Convert.ToDouble(label13.Text);        // hozir sotilgan maxsulotlarni umumiy summasi 
            natija_summa = baza_summa + hozir_summa;              // ikkovini qushish
            textBox10.Text = Convert.ToString(natija_summa); ;    // tex10 ga yozib quy!!
            try                                                   // try bu xato bulsa dasturdan chiqib ketma degani ..
            {
                Button b = sender as Button;                       // b degan uzgaruvchi bu qaysi button bosilsa shuni textini text9 yoz degani
                if (b.Text == "=")                                 // agar textga = bulsa
                {
                  textBox9.Text = "";                              //tozalash
                  textBox4.Text = "";                              // text4 ni tozala
                  // text4 ni tozala
                  nomtxt.Text = "";
                  summatxt.Text = "";
                  firmatxt.Text = "";
                  sroktxt.Text = "";;
                  //sotish jarayoni
                  //bazadan sonini ayirib tashlidi
                  thisId = "";
                  thisUmumiySumma = 0;
                  connection.Open();
                  for (int i = 0; i < dataGridView1.RowCount; i++)
                  {
                    //MessageBox.Show(dr.Cells[6].Value.ToString());
                     string ayiriladigansoni = dataGridView1.Rows[i].Cells[4].Value.ToString();
                     string ayiriladiganId = dataGridView1.Rows[i].Cells[6].Value.ToString();
                     string ayiriladiganSql = "update baza set soni=soni-"+ayiriladigansoni + " where id="+ayiriladiganId;
                     OleDbCommand ayirishCmd = new OleDbCommand(ayiriladiganSql, connection);
                     ayirishCmd.ExecuteNonQuery();
                  }
                  connection.Close();
                  
                 // MessageBox.Show("Bazadan ayirildi");
                }
            }
            catch (Exception ex)                                      // xato bulsa dasturdan chiqib ketma ...
            { MessageBox.Show(ex.Message.ToString ());}
          //===============================================================================//
            rtReceit.Clear();
             // string ayiriladigansoni = dataGridView1.Rows[i].Cells[4].Value.ToString();
             // string narxi=dataGridView1.Rows[]
            rtReceit.AppendText(Environment.NewLine);
            rtReceit.AppendText("----- Mine Market------ "+Environment.NewLine);
            rtReceit.AppendText("_ _ _ _ _ _ _ _ _ _ _ _ _ _  " + Environment.NewLine);
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string nom = dataGridView1.Rows[i].Cells[2].Value.ToString();
                string son = dataGridView1.Rows[i].Cells[4].Value.ToString();
                string naxrxi = dataGridView1.Rows[i].Cells[3].Value.ToString();
                rtReceit.AppendText(nom+"\t\t | \t"+son +" ШТ."+Environment.NewLine);
                rtReceit.AppendText("сумма:\t\t\t\t"+naxrxi+Environment.NewLine);
            }
            rtReceit.AppendText("_ _ _ _ _ _ _ _ _ _ _ _ _ _" + Environment.NewLine + Environment.NewLine); 
            rtReceit.AppendText("Summa:"+"\t\t | \t"+hozir_summa+Environment.NewLine);
            rtReceit.AppendText("_ _ _ _ _ _ _ _ _ _ _ _ _ _ " + Environment.NewLine + Environment.NewLine);
            rtReceit.AppendText("Time :"+"\t\t | \t" + label7.Text+Environment.NewLine);
            rtReceit.AppendText("_ _ _ _ _ _ _ _ _ _ _ _ _ _ " + Environment.NewLine + Environment.NewLine);
            rtReceit.AppendText("\t"+"Xaridingiz uchun raxmat!! " + Environment.NewLine);
           //==================================================================================//
          
            //==============================================================================//
            try                                             // bazada ishlash uchun ...
            {
                connection.Open();                          // bazani ochish
                OleDbCommand commad = new OleDbCommand();   // uzgaruvchi olish
                commad.Connection = connection;             // bog'lash
                commad.CommandText = "Update summa set sotmoq='" + textBox10.Text + "'where ID=1";// uzgartirish ...
                commad.ExecuteNonQuery();                   // tastiqlash
               // MessageBox.Show(commad.CommandText);      // elon qilish 
                connection.Close();                         // bazani yopish
            }
            catch (Exception)                               // xato bulsa dasturdan chiqib ketma
            {
                MessageBox.Show("Eror");                    // elon qil ulana olmasa
            }
            try
            {
                connection.Open();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string nom = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    richTextBox1.AppendText(nom+" ");
                }
                OleDbCommand commad = new OleDbCommand();
                commad.Connection = connection;
                commad.CommandText = "Insert into prodaj(nom,narx,vaqt) values('" + richTextBox1.Text + "','" + label13.Text + "','" + DateTime.Now +"')";
                commad.ExecuteNonQuery();
                richTextBox1.Text = "";
               // MessageBox.Show("Bazaga olingan maxsulotlar tushdi!!");
                connection.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("eror!!");
            }
            // tozalash ishlari:
            for (int i = 0; i < dataGridView1.RowCount; i++)
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    dataGridView1.Rows[i].Cells[j].Value = "";

            //hammasini boshlang'ich qiymatlarga keltirvoladi
            dataGridView1.RowCount = 1;
            qator = 0;
            uchirilganlar = 0;
            label13.Text = "";      
        }


        private void button26_Click(object sender, EventArgs e)
        {
            rezerv rez = new rezerv();
            rez.ShowDialog();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Precent pre = new Precent();
            pre.ShowDialog();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            trade_info ti = new trade_info();
            ti.ShowDialog();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            my_space ms = new my_space();
            ms.ShowDialog();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
          try {
              textBox4.Text = (Int32.Parse(textBox9.Text) * Int32.Parse(summatxt.Text)).ToString(); 
             }
          catch(Exception ex){}
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {         
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        { // olingan maxsulotlarni umumiy summasini hisoblab quyish funksiyasi
            try                                          // qatolik bulmaslik uchun
            {
                connection.Open();                       // bazani ochish
                OleDbCommand com=new OleDbCommand();     // uzgaruvchi elon qilish
                com.Connection = connection;             // bog'lash
                com.CommandText = "Select * from summa"; // Bazadan summa degan bulimni uqi
                OleDbDataReader reader = com.ExecuteReader();// uqish uchun uzgaruvchi elon qilish
                while (reader.Read())                    // sikl berish
                { 
                 textBox10.Text=reader["sotmoq"].ToString(); // tex10 ga qiymatini chiqar
                }                                          
                connection.Close();                      // bazani yopish

            }
            catch (Exception)
            {
                MessageBox.Show("Summani hiboblashda eror"); // elon qilish                     
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            bazadanix a = new bazadanix();
            a.ShowDialog();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(rtReceit.Text, new Font("Arial", 14, FontStyle.Regular), Brushes.Black, 120, 120);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            rtReceit.Clear();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            // This code Will Open Text Files
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                rtReceit.LoadFile(openfile.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            // This code will Save Text Files ...
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Noteped Text";
            saveFile.Filter = "Text Files (*.text)|*.txt|All files (*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName))
                    sw.WriteLine(rtReceit.Text);
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            rtReceit.Cut();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            rtReceit.Copy();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            rtReceit.Paste();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
