using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Personne
    {
        private string Identifiant { get; set; }
        private string Nom { get; set; }
        private string Prenom { get; set; }
        private string Email { get; set; }
        private int Numero { get; set; }

        private Adresse Adresse { get; set; }

        public Personne(string identifiant, string nom, string prenom, string email, int numero, Adresse adresse)
        {
            Identifiant = identifiant;
            Nom = nom;
            Prenom = prenom;
            Email = email;
            Numero = numero;
            Adresse = adresse;
        }

        public Personne()
        {
        }

        public void AcheterVin(Vin vin, int quantite)
        {
            throw new NotImplementedException();
        }

        public void AfficherInformation()
        {
            throw new NotImplementedException();
        }

        public void RetournerVin()
        {
            throw new NotImplementedException();
        }
    }
}
