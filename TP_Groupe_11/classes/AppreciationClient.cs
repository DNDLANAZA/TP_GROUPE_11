using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class AppreciationClient
    {
        public float Longueur_en_bouche { get; set; }
        public float Odeur { get; set; }
        public string Commentaire { get; set; }
        public float Note { get; set; }
        public DateTime Date_appreciation { get; set; }

        public AppreciationClient(float longueur_en_bouche, float odeur, string commentaire, float note, DateTime date_appreciation)
        {
            Longueur_en_bouche = longueur_en_bouche;
            Odeur = odeur;
            Commentaire = commentaire;
            Note = note;
            Date_appreciation = date_appreciation;
        }

        public AppreciationClient()
        {
        }
    }
}
