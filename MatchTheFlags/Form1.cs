using MatchTheFlags.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchTheFlags
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        int sutun = 4, satir = 4;
        int adet, cift, gen, yuk, pay = 10;
        List<PictureBox> aciklar;
        int bulunanCiftAdet;
        int[] tumResimNolar;
        int[] kartlar;
        Button btnYenidenBaslat;

        public Form1()
        {
            InitializeComponent();
            OyunuBaslat();
        }

        private void OyunuBaslat()
        {
            panel1.Controls.Clear();

            #region Yeniden Başlat Butonunun Eklenip Gizlenmesi
            btnYenidenBaslat = new Button();
            btnYenidenBaslat.Text = "Yeniden Başlat";
            btnYenidenBaslat.Location = new Point(192, 233);
            btnYenidenBaslat.Size = new Size(120, 23);
            btnYenidenBaslat.Click += BtnYenidenBaslat_Click;
            btnYenidenBaslat.Visible = false;
            panel1.Controls.Add(btnYenidenBaslat);
            #endregion

            #region Oyun Değişkenlerinin Hesaplanması
            bulunanCiftAdet = 0;
            aciklar = new List<PictureBox>();
            adet = satir * sutun;
            cift = adet / 2;
            gen = (panel1.Width - (sutun - 1) * pay) / sutun;
            yuk = (panel1.Height - (satir - 1) * pay) / satir;
            #endregion

            #region Tüm Resim Noların Karışık Oluşturulması
            tumResimNolar = new int[263];
            for (int i = 0; i < 263; i++)
                tumResimNolar[i] = i;
            Karistir(tumResimNolar);
            #endregion

            #region Kartların Karışık Oluşturulması
            kartlar = new int[adet];
            Array.Copy(tumResimNolar, kartlar, cift);
            Array.Copy(tumResimNolar, 0, kartlar, cift, cift);
            Karistir(kartlar);
            #endregion

            #region Kartların Dizilmesi
            PictureBox pb;

            for (int i = 0; i < kartlar.Length; i++)
            {
                pb = new PictureBox();
                pb.Left = (i % sutun) * (gen + pay);
                pb.Top = (i / sutun) * (yuk + pay);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.BackColor = Color.Gray;
                pb.Tag = kartlar[i];
                pb.Width = gen;
                pb.Height = yuk;
                pb.Click += KartaTiklandiginda;
                panel1.Controls.Add(pb);
            }
            #endregion
        }

        private void BtnYenidenBaslat_Click(object sender, EventArgs e)
        {
            OyunuBaslat();
        }

        private void KartaTiklandiginda(object sender, EventArgs e)
        {
            PictureBox tiklanan = (PictureBox)sender;

            if (aciklar.Contains(tiklanan))
                return;

            if (aciklar.Count == 2)
            {
                AcikKartlariKapat();
            }

            KartiAc(tiklanan);

            if (aciklar.Count == 2 && (int)aciklar[0].Tag == (int)aciklar[1].Tag)
            {
                Refresh();
                Thread.Sleep(400);
                AciklariKapatGizle();
                bulunanCiftAdet++;

                if (bulunanCiftAdet == cift)
                {
                    MessageBox.Show("Oyun Bitti!");
                    btnYenidenBaslat.Show();
                }
            }
        }

        private void AciklariKapatGizle()
        {
            while (aciklar.Count > 0)
            {
                aciklar[0].Hide();
                KartiKapat(aciklar[0]);
            }
        }

        private void KartiAc(PictureBox kart)
        {
            int resimNo = (int)kart.Tag;
            kart.Image = (Image)Resources.ResourceManager.GetObject("_" + resimNo);
            aciklar.Add(kart);
        }

        private void AcikKartlariKapat()
        {
            while (aciklar.Count > 0)
            {
                KartiKapat(aciklar[0]);
            }
        }

        private void KartiKapat(PictureBox kart)
        {
            int resimNo = (int)kart.Tag;
            kart.Image = null;
            aciklar.Remove(kart);
        }

        private void Karistir(int[] dizi)
        {
            int yedek;
            int talihliIndeks;

            for (int i = 0; i < dizi.Length - 1; i++)
            {
                talihliIndeks = rnd.Next(i, dizi.Length);
                yedek = dizi[i];
                dizi[i] = dizi[talihliIndeks];
                dizi[talihliIndeks] = yedek;
            }
        }
    }
}
