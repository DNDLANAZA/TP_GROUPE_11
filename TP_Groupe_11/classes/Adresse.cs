using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Adresse
    {
        //Creation de nos attributs

        public string Rue { get; set; }
        public string Code_postale { get; set; }
        public string Appartement { get; set; }

        //Creation du constructeur

        public Adresse(string rue, string code_postale, string appartement)
        {
            this.Rue = rue;
            this.Code_postale = code_postale;
            this.Appartement = appartement;
        }
    }
}
