using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Groupe_11.interfaces;

namespace TP_Groupe_11.classes
{
    internal class Cave : IGestiondeCave
    {
        private const string caveFilePath = "cave.csv";
        public List<Vin> Vins { get; set; }

        public Cave()
        {
            Vins = LoadVinsFromCsv();
        }

        public void AjouterVin(Vin vin)
        {
            Vins.Add(vin);
            Console.WriteLine(" Le vin a été ajouté avec succès. ");
            SaveVinsToCsv();
        }

        public List<Vin> AfficherListeVin()
        {
            return Vins;
        }

        public Vin RecherVinAvecRef(string numRef)
        {
            return Vins.Find(V => V.NumRef == numRef);
        }

        public void MettreAjourVin(Vin vin)
        {
            Vin vinExiste = Vins.Find(V => V.NumRef == vin.NumRef);

            if (vinExiste != null)
            {
                vinExiste.Nom = vin.Nom;
                vinExiste.AnneeProduction = vin.AnneeProduction;
                vinExiste.Pourcentage_alcool = vin.Pourcentage_alcool;
                vinExiste.Origine = vin.Origine;
                vinExiste.Prix = vin.Prix;
                vinExiste.Disponibilitee = vin.Disponibilitee;
                vinExiste.Marque = vin.Marque;
                vinExiste.Sulphate = vin.Sulphate;
                vinExiste.Acid_citrique = vin.Acid_citrique;
                vinExiste.Acide_volatile = vin.Acide_volatile;
                vinExiste.Qualite = vin.Qualite;
                vinExiste.Cepages = vin.Cepages;
                vinExiste.Type_Vin = vin.Type_Vin;
                Console.WriteLine(" Le vin a été mis a jour avec succès. ");
                SaveVinsToCsv();
            }
            else
            {
                Console.WriteLine($" Le vin avec pour référence {vin.NumRef} n'existe pas!! ");
            }
        }

        public void SupprimerVin(string NumRef)
        {
            Vin vin = Vins.Find(V => V.NumRef == NumRef);

            if ( vin != null ) {
                Vins.Remove(vin);
                Console.WriteLine(" Le vin a été supprimer avec succès. ");
                SaveVinsToCsv();
            }
            else
            {
                Console.WriteLine($" Impossible de supprimer le vin avec pour référence {vin.NumRef} car il n'existe pas !! ");
            }
        }

        public void AugmenterQuantiteVin(string numRef, int quantite)
        {
            Vin vin = Vins.Find(V => V.NumRef == numRef);

            if ( vin != null )
            {
                vin.Quantite += quantite;
                Console.WriteLine(" La quantité du vin a été augmenter avec succes ");
                SaveVinsToCsv();
            }
        }

        public void RetirerQuantiteVin(string numRef, int quantite)
        {
            Vin vin = Vins.Find(V => V.NumRef==numRef);

            if ( vin != null )
            {
                if (vin.Quantite >= quantite)
                {
                    vin.Quantite -= quantite;
                    Console.WriteLine(" Sortie du vin effectuée avec succès. ");
                    SaveVinsToCsv();
                }
                else
                {
                    Console.WriteLine(" La quantité du vin n'est pas suffissante dans le stock, bien vouloir augmenter la quantité !!");
                }
            }
        }

        public void AfficherStock()
        {
            foreach (var vin in Vins)
            {
                vin.AfficherVin();
            }
        }
        private List<Vin> LoadVinsFromCsv()
        {
            if (File.Exists(caveFilePath))
            {
                using (StreamReader reader = new StreamReader(caveFilePath))
                using (CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csvReader.GetRecords<Vin>().ToList();
                }
            }
            else
            {
                return new List<Vin>();
            }
        }
        private void SaveVinsToCsv()
        {
            using (StreamWriter writer = new StreamWriter(caveFilePath))
            using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteRecords(Vins);
            }
        }
    }
}
