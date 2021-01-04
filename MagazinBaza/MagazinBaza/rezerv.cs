using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;                    // kutunbxaona baza bilan uchun
namespace MagazinBaza
{
    public partial class rezerv : Form
    {
        OleDbConnection connection = new OleDbConnection();  // ulash uchun 
        int posX;
        int posY;
        bool drag;

        public rezerv()
        {
            InitializeComponent();
           connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Project_manajer\MagazinBaza\MagazinBaza\bazacha.mdb";
            // baza kanpuyuterda joylashgan joyi .
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();                                     // dasturdan chiqish
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();                                         // bazani ochish 
                //dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;// bitta ustunini oq qil 
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.Gold;// bitta ustunni tiila rang qil 
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.InitialDirectory = "C:";                                   // c deiskdan joy och !!
                saveFileDialog1.Title = "Save as Excel File";                              // exsel fayl uchun 
                saveFileDialog1.FileName = "";                                             // savedan c no och
                saveFileDialog1.Filter = "Excel Files(2007)|*.xls|Excel Files(2016)|*.xlsx|Excel Files(2019)|*.xlsx";// 2007 ,2026,2029 yil xoxlaganiga quyish mumkin
                if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)                   // cancel bulmasa  
                {
                    Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application(); // bazani saqla bazadan
                    ExcelApp.Application.Workbooks.Add(Type.Missing);                      // exzel appga     

                    ExcelApp.Columns.ColumnWidth = 20;                                     // jadval 20 dan ol

                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)              // siklga ber   
                    {
                        ExcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;    // ustunni chiqar
                    }
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)                     // siklga ber
                    {
                        for (int j = 0; j < dataGridView1.Columns.Count; j++)              // ichki sikil bu ammalar jadvaldagi ustun va satrlarni massivga olib hammasini belgiladi
                        {
                            ExcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value; // nusxa olib quy
                        }
                    }
                    MessageBox.Show("Bazadan nusxa olindi!!");                             // bazadan nusxa olinganlik haqiada elon berish
                    ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog1.FileName.ToString());// nomiga olindi
                    ExcelApp.ActiveWorkbook.Saved = true;                                  // true bulsa
                    ExcelApp.Quit();                                                       // chiqib ket ...
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Eror");                                                   // agar xatolik bulsa eror chiqar 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // This code will Save Text Files ...
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "Noteped Text";
            saveFile.Filter = "Text Files (*.text)|*.txt|All files (*.*)|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFile.FileName))
                    for(int i=0; i<dataGridView1.Rows.Count+1;i++)
                    { for(int j=0; j<dataGridView1.Columns.Count; j++)
                      {
                             sw.WriteLine(dataGridView1.Text);
               
                      }
                    
                    }
                 }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
