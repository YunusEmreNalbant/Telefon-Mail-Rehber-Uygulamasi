using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //SQL baglantisi için eklenmesi gereken namespace.


namespace Rehber_Uygulaması
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=NALBANT\SQLEXPRESS;Initial Catalog=RehberUygulamasi;Integrated Security=True");
        
        void listele()
        {
            //datagridin içini veritabanındaki bilgilerle doldur. Veritabanını taşıma işlemi.

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Rehber", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void temizle ()
        {
            //temizle butonuna tıkladıgımız zaman textbox,maskedtextbox temizlensin.

            TxtAd.Text = "";
            TxtMail.Text = "";
            TxtSoyad.Text = "";
            MskTel.Text = "";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TxtID.Enabled = false;
            listele();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Rehber (AD,SOYAD,TELEFON,MAIL) values (@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel.Text);
            komut.Parameters.AddWithValue("@p4", TxtMail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Eklendi !");
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datagrid nesnesinde hücrelere çift tıklandığı zaman bilgiler sağdaki ad,soyad,mail,telefon kutucuklarına gönderilsin.

            int secilen = dataGridView1.SelectedCells[0].RowIndex; 

            TxtID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTel.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtMail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From Tbl_Rehber where ID=" + TxtID.Text, baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Silindi");
            listele();
            temizle();
            
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update Tbl_Rehber set AD=@p1, SOYAD=@p2, TELEFON=@p3, MAIL=@p4 where ID=@P5", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTel.Text);
            komut.Parameters.AddWithValue("@p4", TxtMail.Text);
            komut.Parameters.AddWithValue("@p5", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kişi Güncellendi !");
            listele();
            temizle();
        }
    }
}
