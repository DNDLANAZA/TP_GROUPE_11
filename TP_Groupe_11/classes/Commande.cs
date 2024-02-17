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
        private List<Vin> Vin { get; set; }
        private int Quantite {get; set;}
        private Client Client {get; set;}
        private DateTime Date {get; set;}

        public Commande(List<Vin> vin, int quantite, Client client, DateTime date)
        {
            Vin = vin;
            Quantite = quantite;
            Client = client;
            Date = date;
        }
        public Commande()
        {

        }
        public float TotalCommande(int prix, int quantite)
        {
            throw new NotImplementedException();
        }

    }
}
