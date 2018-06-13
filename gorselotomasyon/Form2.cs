using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace gorselotomasyon
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=gorselodev; Integrated Security=SSPI"); //veritabanı baglantı cümlecigi
        SqlCommand komut = new SqlCommand(); //kayıt ekleme,silme,guncelleme icin SqlCommand nesnesi olusturdum
        private void Form2_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            if (baglanti.State == ConnectionState.Closed) baglanti.Open(); //baglantının acık olup olmadıgı kontrol ediliyor
            SqlDataAdapter adp = new SqlDataAdapter("Select yazar_Adi, kitap_Adi, Adet, mevcut_Durum from kitaplar  ", baglanti); //kitaplar tablosundaki alanlar cekiliyor
            adp.Fill(ds);
            dgwkitap.DataSource = ds.Tables[0]; //datagriview'in ici dolduruluyor

            komut = new SqlCommand("Select Count(*) from kitaplar", baglanti); //kayıt sayısı hesaplanıyor
            lblKayitSayisi.Text = " Veritabanında " + (int)komut.ExecuteScalar() + " tane kayıt var"; //bulunan kayıt sayısı label ile gosteriliyor
            baglanti.Close(); //veritabanı baglantısı kapatılıyor
        }

        private void dgwkitap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int satirno, sutunno;

            satirno = e.RowIndex;
            sutunno = e.ColumnIndex;
            //datagridview'e kolonlar yerlestiriliyor
            txtYazarAdi.Text = dgwkitap.Rows[satirno].Cells[0].Value.ToString();
            txtKitapAdi.Text = dgwkitap.Rows[satirno].Cells[1].Value.ToString();
            txtAdedi.Text = dgwkitap.Rows[satirno].Cells[2].Value.ToString();
            txtDurum.Text = dgwkitap.Rows[satirno].Cells[3].Value.ToString();


        }

        private void btnEkle_Click(object sender, EventArgs e) //kayıt ekleme
        {
            if (string.IsNullOrEmpty(txtKitapAdi.Text)) //kitapAdi text'i bos girilirse kullanıcıya mesaj veriyor
            {
                MessageBox.Show("Lutfen Kitap Adı alanını doldurunuz");
                return;
            }
            else
            {
                try
                {
                    DataSet ds = new DataSet();
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                    ds.Clear();
                    SqlCommand komut = new SqlCommand("Insert into kitaplar( yazar_Adi,kitap_Adi,Adet,mevcut_Durum) values('" + txtYazarAdi.Text + "','" + txtKitapAdi.Text + "','" + txtAdedi.Text + "','" + txtDurum.Text + "')", baglanti); //veritabanına kayıt ekleme cümlecigi
                    komut.ExecuteNonQuery(); //etkilenen kayıt sayısı donderiliyor
                    komut = new SqlCommand("Select Count(*) from kitaplar", baglanti); //kayıt sayısı bulunuyor
                    lblKayitSayisi.Text = " Veritabanında " + (int)komut.ExecuteScalar() + " tane kayıt var"; //kayıt sayısı label'a aktarılıyor
                    baglanti.Close();
                    MessageBox.Show("Kayıt Eklendi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    baglanti.Close();
                }
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e) //kayıt guncelleme
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                ds.Clear();
                SqlCommand komut = new SqlCommand("Update kitaplar set yazar_Adi='" + txtYazarAdi.Text + "',kitap_Adi='" + txtKitapAdi.Text + "',Adet='" + txtAdedi.Text + "',mevcut_Durum='" + txtDurum.Text + "'where kitap_Adi='" + txtKitapAdi.Text + "'", baglanti); //kayıt guncelleme cumlecigi
                komut.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show("Kayıt Güncellendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void btnSil_Click(object sender, EventArgs e) //kayıt silme
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                ds.Clear();
                SqlCommand komut = new SqlCommand("Delete from kitaplar where kitap_Adi='" + txtKitapAdi.Text + "'", baglanti); //kayıt siliniyor
                komut.ExecuteNonQuery();
                komut = new SqlCommand("Select Count(*) from kitaplar", baglanti);
                lblKayitSayisi.Text = " Veritabanında " + (int)komut.ExecuteScalar() + " tane kayıt var";
                baglanti.Close();
                MessageBox.Show("Kayıt Silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void btnAnamenu_Click(object sender, EventArgs e) //ana forma gonderme
        {
            this.Hide(); //formu gizle
            Form1 frmAnaForm = new Form1();
            frmAnaForm.Show();
        }


    }
}
