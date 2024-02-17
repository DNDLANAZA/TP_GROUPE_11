using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Vignoble : Terrain
    {
        private List<Cepage> Cepage {  get; set; }

        public Vignoble(string nom, ProprietaireVignoble proprietaire, float superficie, List<Cepage> cepage) : base(nom, proprietaire, superficie)
        {
            Cepage = cepage;
        }

        public Vignoble()
        {
            
        }


    }
}
