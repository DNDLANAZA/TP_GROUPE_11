using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class AppreciationClient
    {
        private int Longueur_en_bouche { get; set; }
        private Boolean Odeur { get; set; }
        private string Commentaire { get; set; }
        private int Note { get; set; }
        private DateTime Date_appreciation { get; set; }

        public AppreciationClient(int longueur_en_bouche, bool odeur, string commentaire, int note, DateTime date_appreciation)
        {
            Longueur_en_bouche = longueur_en_bouche;
            this.Odeur = odeur;
            Commentaire = commentaire;
            Note = note;
            Date_appreciation = date_appreciation;
        }

        public AppreciationClient()
        {
        }
    }
}
