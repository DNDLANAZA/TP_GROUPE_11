using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Client: Personne
    {
        public Client()
        {
        }

        public Client(string identifiant, string nom, string prenom, string email, int numero, Adresse adresse) : base(identifiant, nom, prenom, email, numero, adresse)
        {
        }



        public void AjouterAppreciation(AppreciationClient appreciationClient)
        {
            throw new NotImplementedException();
        }
    }
}
