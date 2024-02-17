using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class ProprietaireVignoble : Personne
    {

        private List<Vignoble> Vignoble {  get; set; }

        public ProprietaireVignoble(string identifiant, string nom, string prenom, string email, int numero, Adresse adresse, List<Vignoble> vignoble) : base(identifiant, nom, prenom, email, numero, adresse)
        {
            this.Vignoble = vignoble;
        }

        public ProprietaireVignoble()
        {
            
        }
    }
}
