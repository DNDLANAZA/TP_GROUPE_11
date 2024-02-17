using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Terrain
    {
        private string Nom { get; set; }
        private ProprietaireVignoble ProprietaireVignoble { get; set; }
        private float Superficie { get; set; }

        public Terrain(string nom, ProprietaireVignoble proprietaire, float superficie)
        {
            Nom = nom;
            ProprietaireVignoble = proprietaire;
            Superficie = superficie;
        }

        public Terrain()
        {
        }
    }
}
