using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//FURKAN SOYLU B211200012 PROJE ÖDEVİ
namespace OyunProje
{
    public partial class OyunAlanı : Form
    {
        #region degiskenler
        bool solagit, sagagit;
        int toplanank = 0;
        int toplananf = 0;
        int toplanant = 0;
        int saniye = 75;
        int skor = 0;
        int hız = 12;
        int kacan = 0;
        int sürprizolay = 0;
        int ürüntoplam = 0;

        #endregion




        private readonly Oyun _oyun;
        
        
        Random randX = new Random();
        Random randY = new Random();
        

        public OyunAlanı()
        {
            
            InitializeComponent();
            _oyun = new Oyun(toplamyapılan,robotkolu,far,karoser,tekerlek,GameTimer,ClientSize,karosermiktar,farmiktar,tekerlekmiktar,label10,karosermiktar,farmiktar,tekerlekmiktar,gizemlikutu,durumlar,ürünadet,lbl_value,kacanlbl);
            //YÜKSEK SKORLAR
            lbl_value.Text = Properties.Settings.Default.h_skor;
        }
        //KLAVYE TUŞ AYARLAMALARI
        public void OyunAlanı_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Left)
            {
                solagit = true;
            }
            if (e.KeyCode==Keys.Right)
            {
                sagagit = true;
            }
            if (e.KeyCode==Keys.P)
            {
                robotkolu.Left = 0;
                hız = 0;
                timer1.Stop();
            }
            if (e.KeyCode==Keys.Enter)
            {
                robotkolu.Left += 12;
                hız +=12;
                timer1.Start();
                
            }
        }

        private void OyunAlanı_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                solagit = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                sagagit =false;
            }
            if (e.KeyCode == Keys.P)
            {
                robotkolu.Left = 0;
                hız = 0;
                timer1.Stop();
            }

        }

        // KALAN SÜRE
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Interval = 1000;
            saniye--;
            label10.Text = saniye.ToString();
            _oyun.hızarttır();

            // OYUNU BİTİRME
            if (saniye==0)
            {
                timer1.Stop();
                GameTimer.Stop();
                MessageBox.Show("Oyun bitti.");
            }
            
    
        }

      
        // ANA FONKSİYON 
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            
            kacanlbl.Text = kacan.ToString();

            // EN İYİ SKORLARI GÖRÜNTÜLEME

            int a = Int32.Parse(lbl_value.Text);
            if (skor>a)
            {
                lbl_value.Text = skor.ToString();
                Properties.Settings.Default.h_skor = lbl_value.Text;
                Properties.Settings.Default.Save();
            }
            // ROBOT KOLU AYARLAMALARI
            if (solagit == true && robotkolu.Left > 0)
            {
                robotkolu.Left -= 24;
                robotkolu.Image = Properties.Resources.pngwing_com__6_;
            }
            if (sagagit == true && robotkolu.Left + robotkolu.Width < this.ClientSize.Width)
            {
                robotkolu.Left += 24;
                robotkolu.Image = Properties.Resources.pngwing_com__6_;


            }
            // YUKARDAN AŞAĞI DÜŞME OLAYLARI
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "far" || (string)x.Tag == "teker" || (string)x.Tag == "karoser" || (string)x.Tag == "gizemlikutu")
                {

                    x.Top += hız;

                    // CİSİMLER ROBOTKOLUYLA ETKİLEŞİME GİRDİĞİNDE
                    if (far.Bounds.IntersectsWith(robotkolu.Bounds))
                    {

                        toplananf++;
                        farmiktar.Text = toplananf.ToString();
                    }
                    if (tekerlek.Bounds.IntersectsWith(robotkolu.Bounds))
                    {

                        toplanant++;
                        tekerlekmiktar.Text = toplanant.ToString();

                    }
                    if (karoser.Bounds.IntersectsWith(robotkolu.Bounds))
                    {

                        toplanank++;
                        karosermiktar.Text = toplanank.ToString();

                    }
                    if (gizemlikutu.Bounds.IntersectsWith(robotkolu.Bounds))
                    {
                        //GİZEMLİ KUTU OLAYLARI
                        Random random = new Random();
                        int olay = random.Next(1, 9);
                        _oyun.sürprizkutu();
                        
                    }



                    if (x.Top + x.Height > this.ClientSize.Height)
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        kacan++;

                    }

                    if (robotkolu.Bounds.IntersectsWith(x.Bounds))
                    {

                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, this.ClientSize.Width - x.Width);
                        //ÜRÜN OLUŞTURMA HESABI
                        if (toplananf >= 2 && toplanank >= 1 && toplanant >= 4)
                        {
                            ürüntoplam++;
                            ürünadet.Text = ürüntoplam.ToString();
                            toplananf = 0;
                            toplanank = 0;
                            toplanant = 0;
                            farmiktar.Text = "0";
                            tekerlekmiktar.Text = "0";
                            karosermiktar.Text = "0";
                            skor += 100;
                            toplamyapılan.Text = skor.ToString();

                        }


                    }

                }


            }


        }

       

    }
     
}
