using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Performance
    {
        public float Note { get; set; }
        public DateTime Date {get; set;}
        public Vin Vin {get; set;}

        public Performance()
        {
        }

        public Performance(float note, DateTime date, Vin vin)
        {
            Note = note;
            Date = date;
            Vin = vin;
        }

        public void EvaluerPerformance(Vin vin, int note, string commentaire) {

            Console.WriteLine($" J'attribut une note de {note} au vin {vin.Nom} suite a cette remarque : {commentaire}");

        }
        public void Afficher()
        {
            
        }
    }
}
