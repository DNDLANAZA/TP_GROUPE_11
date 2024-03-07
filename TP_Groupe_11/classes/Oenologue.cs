using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Groupe_11.interfaces;

namespace TP_Groupe_11.classes
{
    internal class Oenologue: Personne
    {
        private List<Client> clients;
        private List<ProprietaireVignoble> terrainOwners;

        // Accesseurs, mutatteurs et attributs de notre classe vin
        public string Domaine_expertise { get; set; }
        public int Experience {get; set;}

        public string Password { get; set;}
        // Constructeur vide de notre classe Oenologue
        public Oenologue()
        {
        }
        // Constructeur de notre classe Oenologue
        public Oenologue(string identifiant, string nom, string prenom, string email, int numero, Adresse adresse, string domaine_expertise, int experience, string password) : base(identifiant, nom, prenom, email, numero, adresse)
        {
            Domaine_expertise = domaine_expertise;
            Experience = experience;
            clients = new List<Client>();
            terrainOwners = new List<ProprietaireVignoble>();
            Password = password;
        }
        public void AddClient(Client client)
        {
            clients.Add(client);
        }
        public bool RemoveClient(string clientId)
        {
            Client client = clients.Find(c => c.Identifiant == clientId);
            if (client != null)
            {
                clients.Remove(client);
                return true;
            }
            return false;
        }
        public List<Client> GetAllClients()
        {
            return clients;
        }
        public void AddTerrainOwner(ProprietaireVignoble terrainOwner)
        {
            terrainOwners.Add(terrainOwner);
        }

        public bool RemoveTerrainOwner(string terrainOwnerId)
        {
            ProprietaireVignoble terrainOwner = terrainOwners.Find(to => to.Identifiant == terrainOwnerId);
            if (terrainOwner != null)
            {
                terrainOwners.Remove(terrainOwner);
                return true;
            }
            return false;
        }
        public List<ProprietaireVignoble> GetAllTerrainOwners()
        {
            return terrainOwners;
        }


        // Méthode pour afficher les informations de l'oenologue
        public override void AfficherInformation()
        {
            Console.WriteLine($" Identifiant: {Identifiant} || Nom: {Nom} || Prenom: {Prenom} || Adresse mail: {Email}" +
                              $" Numero: {Numero} || Rue: {Adresse.Rue} || Code Postale: {Adresse.Code_postale} || N° Appartement: {Adresse.Appartement}" +
                              $" Domaine d'expertise: {Domaine_expertise} || Experience: {Experience} ");
            Console.WriteLine($"------------------- Fin de l'affichage de l'oenologue {Nom}' '{Prenom} -----------------");
        }

        public bool ControleQualite(Vin vin)
        {
            if (vin.Qualite < 3)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Méthode pour deguster un vin
        public void DegusterVin(Vin vin)
        {
            Console.WriteLine($" L'oenologue {Nom} est entrain de deguster le vin {vin.Nom} de couleur {vin.Type_Vin} avec pour degre d'alcool {vin.Pourcentage_alcool} ");
            Console.WriteLine("********************** Fin de degustation *************************");
        }

        // Méthode pour noter les performances d'un vin
        public void NoterPerformanceVin(Vin vin, float note)
        {
            Performance performance = new Performance();
            vin.AjouterPerformance(performance );

        }

    }

}
