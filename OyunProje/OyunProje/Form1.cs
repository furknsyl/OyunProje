using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OyunProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OyunAlanı oyunalani = new OyunAlanı();
            oyunalani.Show();
            Hide();

            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Robot kolunu hareket ettirmek için ok tuşlarını kullanınız. Oyuna başlamak için entere veya yukarıdaki butona basabilirsiniz.Oyunu durdurmak için P tuşuna basınız.",
                "Bilgi Ekranı",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En iyi skorlar oyun içinde görüntülenmektedir.");
        }
    }
}
