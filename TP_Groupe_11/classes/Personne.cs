using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal abstract class Personne
    {
        public string Identifiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public int Numero { get; set; }

        public Adresse Adresse { get; set; }

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

        public abstract void AfficherInformation();

        public void RetournerVin()
        {
            throw new NotImplementedException();
        }
    }
}
