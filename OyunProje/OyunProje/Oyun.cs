using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace OyunProje
{
    public class Oyun : Ioyun
    {
        bool solagit, sagagit;
        int saniye = 75;
        int skor = 0;
        int hız = 8;
        int kacan = 0;
        int toplanank = 0;
        int toplananf = 0;
        int toplanant = 0;
        int sürprizolay = 0;
        int ürüntoplam = 0;
        Random randX = new Random();
        Random randY = new Random();

        #region Alanlar
        private Timer _gecenSureTimer = new Timer { Interval = 1000 };
        private TimeSpan _gecenSure;
        private Label toplamyapılan, kmiktar, farmiktar, tmiktar, zmn, karosermiktar, fmikar, tekerlekmiktar, durumlar, ürünadet, lblvalue,kacanlbl;
        private readonly Timer _oyunzamanı = new Timer { Interval = 1000 };
        private readonly Timer gametimer = new Timer { Interval = 1000 };
        private PictureBox robotkolu, far, tekerlek, karoser, gizemlikutu;
        private Size mekan;
        #endregion
        #region Olaylar
        public event EventHandler GecenSureDegisti;
        
        #endregion
        #region Özellikler
        public bool devamediyormu { get; private set; }

        public TimeSpan GecenSure
        {
            get => _gecenSure;
            private set
            {
                _gecenSure = value;
                GecenSureDegisti?.Invoke(this, EventArgs.Empty);
            }
        }

        public IEnumerable<Control> Controls { get; private set; }
        public object ClientSize { get; private set; }


        #endregion
        #region Metotlar
        public Oyun(Label toplamyapılan,PictureBox robotkolu, PictureBox far, PictureBox karoser, PictureBox tekerlek, Timer gametimer, Size mekan,Label kmiktar,
            Label farmiktar,Label tmiktar,Label zmn,Label karosermiktar,Label fmiktar,Label tekerlekmiktar,PictureBox gizemlikutu,Label durumlar,Label ürünadet,Label lblvalue,Label kacan)
        {
            _gecenSureTimer.Tick += gecenSureTimer_Tick;
            this.toplamyapılan = toplamyapılan;
            this.robotkolu = robotkolu;
            ClientSize = mekan;
            this.kmiktar = kmiktar;
            this.farmiktar = farmiktar;
            this.tmiktar = tmiktar;
            this.zmn = zmn;
            this.far = far;
            this.karoser = karoser;
            this.tekerlek = tekerlek;
            this.karosermiktar = karosermiktar;
            this.fmikar = fmikar;
            this.tekerlekmiktar = tekerlekmiktar;
            this.gizemlikutu = gizemlikutu;
            this.durumlar = durumlar;
            this.ürünadet = ürünadet;
            this.gametimer = gametimer;
            this.kacanlbl = kacanlbl;
            this.lblvalue = lblvalue;
            _oyunzamanı.Tick += gametimer_Tick;
            
            
            
            
            
           
            
        }

        private void gametimer_Tick(object sender, EventArgs e)
        {

            anafonk();
            
        }
        private void anafonk()
        {
            kacanlbl.Text = kacan.ToString();

            // EN İYİ SKORLARI GÖRÜNTÜLEME

            int a = Int32.Parse(lblvalue.Text);
            if (skor > a)
            {
                lblvalue.Text = skor.ToString();
                Properties.Settings.Default.h_skor = lblvalue.Text;
                Properties.Settings.Default.Save();
            }
            // ROBOT KOLU AYARLAMALARI
            if (solagit == true && robotkolu.Left > 0)
            {
                robotkolu.Left -= 24;
                robotkolu.Image = Properties.Resources.pngwing_com__6_;
            }
            if (sagagit == true && robotkolu.Left + robotkolu.Width < mekan.Width)
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
                        sürprizkutu();
                        
                    }



                    if (x.Top + x.Height > mekan.Height)
                    {
                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, mekan.Width - x.Width);
                        kacan++;

                    }

                    if (robotkolu.Bounds.IntersectsWith(x.Bounds))
                    {

                        x.Top = randY.Next(80, 300) * -1;
                        x.Left = randX.Next(5, mekan.Width - x.Width);
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

       

        private void gecenSureTimer_Tick(object sender, EventArgs e)
        {
            GecenSure += TimeSpan.FromSeconds(1);
        }
        public void baslat()
        {
            if (devamediyormu) return;
            {
                devamediyormu = true;
                _gecenSureTimer.Start();
 

            }
        }
        public void sürprizkutu()
        {
            Random random = new Random();
            int olay = random.Next(1, 9);

            switch (olay)
            {
                case 1:
                    sürprizolay = 1;
                    durumlar.Text = "Skora 50 eklendi.";
                    skor += 50;
                    toplamyapılan.Text = skor.ToString();
                    break;
                case 2:
                    sürprizolay = 2;
                    durumlar.Text = "Skordan 50 eksildi.";
                    skor -= 50;
                    toplamyapılan.Text = skor.ToString();
                    break;
                case 3:
                    sürprizolay = 3;
                    durumlar.Text = "Her parçaya bir tane eklendi.";
                    toplananf++;
                    toplanank++;
                    toplanant++;
                    farmiktar.Text = toplananf.ToString();
                    tekerlekmiktar.Text = toplanant.ToString();
                    karosermiktar.Text = toplanank.ToString();
                    break;
                case 4:
                    sürprizolay = 4;
                    durumlar.Text = "Her parçadan bir tane çıkarıldı.";
                    toplananf--;
                    toplanank--;
                    toplanant--;
                    farmiktar.Text = toplananf.ToString();
                    tekerlekmiktar.Text = toplanant.ToString();
                    karosermiktar.Text = toplanank.ToString();
                    break;
                case 5:
                    sürprizolay = 5;
                    saniye += 5;
                    durumlar.Text = "Saniyeye 10 eklendi.";
                    break;
                case 6:
                    sürprizolay = 6;
                    saniye -= 5;
                    durumlar.Text = "Saniyeden 10 çıkarıldı.";
                    break;
                case 7:
                    sürprizolay = 7;
                    ürüntoplam--;
                    ürünadet.Text = ürüntoplam.ToString();
                    durumlar.Text = "Bir ürün eksildi.";
                    break;
                case 8:
                    sürprizolay = 8;
                    ürüntoplam++;
                    ürünadet.Text = ürüntoplam.ToString();
                    durumlar.Text = "Bir ürün eklendi.";
                    break;
                default:
                    break;
            }


        }
        public void hızarttır()
        {

            if (zmn.Text == "55"||zmn.Text == "35" || zmn.Text == "15" )
            {
                robotkolu.Left += 10;
                gametimer.Interval -=10;
                durumlar.Text = "Oyun hızlandı";
            }

        }
 

        public void bitir()
        {
            if (!devamediyormu) return;
            {
                devamediyormu = false;
                _gecenSureTimer.Stop();
            }
        }
        
	

        public void duraklat()
        {
            throw new NotImplementedException();
        }
        public void restartgame()
        {
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "far" && (string)x.Tag == "teker" && (string)x.Tag == "karoser")
                {
                    x.Top = randY.Next(80, 300) * -1;
                    x.Left = randX.Next(5,mekan.Width - x.Width);
                }

            }

            robotkolu.Left = mekan.Width / 2;
            robotkolu.Image = Properties.Resources.pngwing_com__6_;

            skor = 0;
            hız = 8;
            kacan = 0;

            solagit = false;
            sagagit = false;

            gametimer.Start();

            
        }
        
       


        #endregion


    }
}
