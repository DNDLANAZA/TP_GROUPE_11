using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Commande
    {
        public List<Vin> Vins { get; set; }
        public string RefCommande { get; set; }
        //public int Quantite {get; set;}
        public Client Client {get; set;}
        public DateTime DateCommande {get; set;}
        
        
        public Commande( int quantite, Client client, DateTime dateCommande, string refCommande)
        {
            Vins = new List<Vin>();
            //Quantite = quantite;
            Client = client;
            DateCommande = dateCommande;
            RefCommande = refCommande;
        }
        public Commande()
        {

        }
        public void AjouterVin(Vin vin)
        {
            Vins.Add(vin);
        }

        public double TotalCommande()
        {
            double montantTotal = 0;
            foreach (var vin in Vins)
            {
                montantTotal += vin.Prix;
            }

            return montantTotal;
        }

        public void AfficherCommande()
        {
            Console.WriteLine($" Ref commande: {RefCommande} || Client: {Client.Nom} || Nombre de vin commande: {Vins.Count} a la date de : {DateCommande}");
            Console.WriteLine(" La liste des vin present dans la commande ");
            foreach (var vin in Vins)
            {
                vin.AfficherVin();
            }
            Console.WriteLine($" Total de la commande: {TotalCommande()}$");
        }
    }
}
