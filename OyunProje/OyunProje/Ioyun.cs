using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// FURKAN SOYLU B211200012
namespace OyunProje
{
    interface Ioyun 
    {
        event EventHandler GecenSureDegisti;
        bool devamediyormu { get; }
        TimeSpan GecenSure { get; }
        void baslat();
        void bitir();
        void duraklat();
      

            
    }
}
