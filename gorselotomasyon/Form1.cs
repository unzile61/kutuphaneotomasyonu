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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=gorselodev; Integrated Security=SSPI"); //veritabanı baglanti cumlecigi
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();//baglantının acık olup olmadıgı kontrol ediliyoruz
                SqlDataAdapter adp = new SqlDataAdapter("Select * from kitaplar where kitap_Adi LIKE '" + txtaranan.Text + "%'", baglanti);  //aranan kitap veritabanında var mı diye bakıyoruz
                adp.Fill(ds); // dataseti dolduruyoruz
                lstsonuc.DataSource = ds.Tables[0]; // dataseti listbox'ın datasource'na esitliyoruz
                lstsonuc.DisplayMember = "kitap_Adi"; //veritabanındaki alan ismini belirtiyoruz
                txtAdet.DataBindings.Clear(); //baglantı varsa baglantıyı temizliyoruz
                txtAdet.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "Adet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)); //yeni baglantı gerceklestiriyoruz
                txtDurum.DataBindings.Clear();
                txtDurum.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "mevcut_Durum", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

                txtKitapAdi.DataBindings.Clear();
                txtKitapAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "kitap_Adi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

                txtYazarAdi.DataBindings.Clear();
                txtYazarAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "yazar_Adi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

                baglanti.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void txtaranan_TextChanged(object sender, EventArgs e) //aranan texti
        {
            try
            {
                DataSet ds = new DataSet();
                if (baglanti.State == ConnectionState.Closed) baglanti.Open();
                SqlDataAdapter adp = new SqlDataAdapter("Select * from kitaplar where kitap_Adi LIKE '" + txtaranan.Text + "%'", baglanti); //veritabanında aranan kayıt var mı diye bakıyoruz
                adp.Fill(ds); //dataseti dolduruyoruz
                lstsonuc.DataSource = ds.Tables[0];
                lstsonuc.DisplayMember = "kitap_Adi";
                txtAdet.DataBindings.Clear(); //baglantı varsa baglantıyı temizliyoruz
                txtAdet.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "Adet", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)); //yeni baglantı gerceklestiriyoruz

                txtDurum.DataBindings.Clear();
                txtDurum.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "mevcut_Durum", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

                txtKitapAdi.DataBindings.Clear();
                txtKitapAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "kitap_Adi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

                txtYazarAdi.DataBindings.Clear();
                txtYazarAdi.DataBindings.Add(new System.Windows.Forms.Binding("text", ds.Tables[0], "yazar_Adi", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

                baglanti.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                baglanti.Close();
            }
        }

        private void btncikis_Click(object sender, EventArgs e) //cıkıs butonu ile formdan cıkıs yapıyoruz
        {
            Application.Exit();
        }

        private void btnAyarlar_Click(object sender, EventArgs e) //ayarlar butonu ile diger forma gecis yapıyoruz
        {
            this.Hide(); //anaformu gizle
            Form2 frmAyarlar = new Form2();
            frmAyarlar.Show();
        }


    }
}
