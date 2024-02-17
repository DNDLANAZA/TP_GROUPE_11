using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Groupe_11.interfaces;

namespace TP_Groupe_11.classes
{
    internal class Oenologue: Personne, IGestiondeCave
    {
        private string Domaine_expertise { get; set; }
        private int Experience {get; set;}

        public Oenologue(string identifiant, string nom, string prenom, string email, int numero, Adresse adresse, string domaine_expertise, int experience) : base(identifiant, nom, prenom, email, numero, adresse)
        {
            Domaine_expertise = domaine_expertise;
            Experience = experience;
        }

        public Oenologue()
        {
        }

        public Boolean ControleQualite(Vin vin)
        {
            throw new NotImplementedException();
        }

        public void DegusterVin()
        {
            throw new NotImplementedException();
        }

        public int NoterVin(Vin vin)
        {
            throw new NotImplementedException();
        }

        void IGestiondeCave.AjouterVin(Vin vin)
        {
            throw new NotImplementedException();
        }

        string IGestiondeCave.RecherVinAvecNom(string nom)
        {
            throw new NotImplementedException();
        }

        void IGestiondeCave.RetirerVin(Vin vin)
        {
            throw new NotImplementedException();
        }

        public void ConseillerVin(Vin vin)
        {
            throw new NotImplementedException();
        }
    }

}
