using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityLinqToExpressions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KuzeyYeliDataContext ctx = new KuzeyYeliDataContext();
            dataGridView1.DataSource = ctx.Urunlers;
            //////////////////////
            cmbKategori.DataSource = ctx.Kategorilers;
            cmbKategori.DisplayMember = "KategoriAdi";//görünmesini istediğimiz propertyi seçtik
            cmbKategori.ValueMember = "KategoriID";//SELECTEDVALUEDA GÖSTERİLMESİNİ İSTEDİĞİMİZ DEĞERDİR
            cmbTedarikci.DataSource = ctx.Tedarikcilers;
            cmbTedarikci.DisplayMember = "SirketAdi";
            cmbTedarikci.ValueMember = "TedarikciID";

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            KuzeyYeliDataContext ctx=new KuzeyYeliDataContext();
            Urunler urn= new Urunler();
            urn.UrunAdi=txtUrunAdi.Text;
            urn.Fiyat = numFiyat.Value;
            urn.Stok = (short)numStok.Value;
            urn.KategoriID = (int)cmbKategori.SelectedValue;
            urn.TedarikciID=(int)cmbTedarikci.SelectedValue;

            ctx.Urunlers.InsertOnSubmit(urn);
            ctx.SubmitChanges();
            MessageBox.Show("Ürün Eklendi");
            dataGridView1.DataSource=ctx.Urunlers;//güncel listeyi yazdık.


        }

        private void btn_SİL_Click(object sender, EventArgs e)
        {
            int urunid = (int)dataGridView1.CurrentRow.Cells["UrunId"].Value;
            KuzeyYeliDataContext ctx= new KuzeyYeliDataContext();
            Urunler urn = ctx.Urunlers.SingleOrDefault(urun=>urun.UrunID==urunid);//lambda expression seçilip ürün seçilir.


            ctx.Urunlers.DeleteOnSubmit(urn);
            ctx.SubmitChanges();
            MessageBox.Show("Ürün silindi!");
            dataGridView1.DataSource= ctx.Urunlers;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            txtUrunAdi.Text = row.Cells["UrunAdi"].Value.ToString();
            txtUrunAdi.Tag = row.Cells["UrunId"].Value;

            cmbKategori.SelectedValue = row.Cells["KategoriID"].Value;
            cmbTedarikci.SelectedValue = row.Cells["TedarikciID"].Value;
            numFiyat.Value = (decimal)row.Cells["Fiyat"].Value;
            numStok.Value = (short)row.Cells["Stok"].Value;

        }

        private void btn_Guncelle_Click(object sender, EventArgs e)
        {
            int urunid = (int)txtUrunAdi.Tag;
            KuzeyYeliDataContext ctx = new KuzeyYeliDataContext();
            Urunler urn=ctx.Urunlers.SingleOrDefault(urun=>urun.UrunID==urunid);
            urn.UrunAdi = txtUrunAdi.Text;
            urn.Fiyat=numFiyat.Value;
            urn.Stok=(short)numStok.Value;
            urn.TedarikciID = (int)cmbTedarikci.SelectedValue;
            urn.KategoriID=(int)cmbKategori.SelectedValue;
            ctx.SubmitChanges();
            MessageBox.Show("Ürün Güncellendi!");
            dataGridView1.DataSource = ctx.Urunlers;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            KuzeyYeliDataContext ctx = new KuzeyYeliDataContext();
            dataGridView1.DataSource = ctx.Urunlers.Where(x => x.UrunAdi.Contains(textBox1.Text));
        }

        private void rdbUrunAdi_CheckedChanged(object sender, EventArgs e)
        {
            //KuzeyYeliDataContext ctx=new KuzeyYeliDataContext();
            //if (rbdUrunAdi.Checked)
            //    dataGridView1.DataSource = ctx.Urunlers.OrderBy(x => x.UrunAdi);
            //else if (RadioButton.Checked)
            //    dataGridView1.DataSource=ctx.Urunlers.OrderByDescending(x=> x.Fiyat);
            //else if(rbdStok.Checked)
            //    dataGridView1.DataSource=ctx.Urunlers.OrderByDescending(x=> x.Stok);
        }
    }
}
