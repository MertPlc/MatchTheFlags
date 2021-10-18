using MatchTheFlags.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        public Form1()
        {
            InitializeComponent();
            OyunuBaslat();
        }

        private void OyunuBaslat()
        {
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

        private void KartaTiklandiginda(object sender, EventArgs e)
        {
            
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
