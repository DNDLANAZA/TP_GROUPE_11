using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Client: Personne
    {
        public List<AppreciationClient> AppreciationClients { get; set; }
        public Client()
        {
        }

        public Client(string identifiant, string nom, string prenom, string email, int numero, Adresse adresse) : base(identifiant, nom, prenom, email, numero, adresse)
        {
            AppreciationClients = new List<AppreciationClient>();
        }

        public void AjouterAppreciationVin(Vin vin, float longueurBouche, float odeur, string commentaire, float note, DateTime dateAppreciation)
        {
            AppreciationClient appreciationClient = new AppreciationClient(longueurBouche, odeur, commentaire, note, dateAppreciation);
            vin.AjouterAppreciation(appreciationClient);
        }

        public override void AfficherInformation()
        {
            Console.WriteLine($" Identifiant: {Identifiant} || Nom: {Nom} || Prenom: {Prenom} || Adresse mail: {Email}" +
                              $" Numero: {Numero} || Rue: {Adresse.Rue} || Code Postale: {Adresse.Code_postale} || N° Appartement: {Adresse.Appartement}");
            Console.WriteLine($"------------------- Fin de l'affichage du client {Nom}' '{Prenom} -----------------");
        }
    }
}
