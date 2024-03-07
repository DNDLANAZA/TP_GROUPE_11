using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Vin
    {
        // Accesseurs, mutatteurs et attributs de notre classe vin
        public string NumRef { get; set; }
        public string Nom { get; set; }
        public int AnneeProduction { get; set; }
        public double Pourcentage_alcool { get; set; }
        public string Origine { get; set; }
        public double Prix { get; set; }
        public int Quantite { get; set; }
        public bool Disponibilitee { get; set; }
        public string Marque { get; set; }
        public double Sulphate { get; set; }
        public double Acid_citrique { get; set; }
        public double Acide_volatile { get; set; }
        public int Qualite { get; set; }
        public List<Cepage> Cepages { get; set; }
        public Type_Vin Type_Vin { get; set; }
        public List<Performance> Performances { get; set; }
        public List<AppreciationClient> appreciationClients { get; set; }

        // Constructeur vide de notre classe vin
        public Vin()
        {
            
        }
        // Constructeur de notre classe vin
        public Vin(string numRef, string nom, int anneeProduction, double pourcentage_alcool, string origine, double prix, bool disponibilitee, string marque, int quantite, double sulphate, double acid_citrique, double acide_volatile, int qualite, Type_Vin type_Vin)
        {
            NumRef = numRef;
            Nom = nom;
            AnneeProduction = anneeProduction;
            Pourcentage_alcool = pourcentage_alcool;
            Origine = origine;
            Prix = prix;
            Disponibilitee = disponibilitee;
            Marque = marque;
            Quantite = quantite;
            Sulphate = sulphate;
            Acid_citrique = acid_citrique;
            Acide_volatile = acide_volatile;
            Qualite = qualite;
            Cepages = new List<Cepage>();
            Type_Vin = type_Vin;
            Performances = new List<Performance>();
        }

        // Méthode pour afficher un vin
        public void AfficherVin()
        {
           
            string cepagesStr = string.Empty;

            if (Cepages != null && Cepages.Count > 0)
            {
                cepagesStr = string.Join(", ", Cepages.Where(c => c != null));
            }
            
            Console.WriteLine($" Ref du vin: {NumRef} || Nom: {Nom} || Annee de production : {AnneeProduction} || Origine: {Origine} || Quantite: {Quantite}" +
                              $" Prix: {Prix} || Marque: {Marque} || Alcool: {Pourcentage_alcool} || Sulphate: {Sulphate} || Acide citrique: {Acid_citrique}" +
                              $" Acide_volatile: {Acide_volatile} || Cepage: {cepagesStr} || Type de vin: {Type_Vin} || Qualite: {Qualite} || Disponibilitee: {Disponibilitee} ");
            Console.WriteLine($"------------------- Fin de l'affichage du vin de reference: {NumRef} ");
            Console.WriteLine($"***********************************************************************************************************************************");
        }

        // Méthode pour ajouter une note de performance au vin
        public void AjouterPerformance(Performance performance) { 
            Performances.Add( performance );
        }

        // Méthode pour calculer la moyenne des notes de performance du vin
        public float CalculMoyennePerformance() {
            if ( Performances.Count == 0 ) { return 0; }
            float somme = 0;
            foreach (var performance in Performances)
            {
                somme += performance.Note;
            }
            return somme / Performances.Count;
        }

        // Méthode pour ajouter une appreciation au vin
        public void AjouterAppreciation(AppreciationClient appreciationClient)
        {
            appreciationClients.Add( appreciationClient );
        }

        // Méthode pour afficher toutes les appréciations du vin
         public void AfficherAppreciationVin()
        {
            Console.WriteLine($" Appréciations pour le vin {Nom} ");
            foreach (var appreciationVin in appreciationClients)
            {
                Console.WriteLine($" Longueur en bouche: {appreciationVin.Longueur_en_bouche} || Odeur: {appreciationVin.Odeur} || Commentaire: {appreciationVin.Commentaire}" +
                                  $" || Note: {appreciationVin.Note} || Date d'appreciation: {appreciationVin.Date_appreciation} ");
                
            }
            Console.WriteLine("****************** Fin d'affichage d'appreciation ******************");
        }
    }
}
