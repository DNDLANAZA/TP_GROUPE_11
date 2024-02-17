using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Performance
    {
        private Double Note { get; set; }
        private DateTime Date {get; set;}
        private Vin Vin {get; set;}

        public Performance()
        {
        }

        public Performance(double note, DateTime date, Vin vin)
        {
            Note = note;
            Date = date;
            Vin = vin;
        }

        public void Afficher()
        {
            
        }
    }
}
