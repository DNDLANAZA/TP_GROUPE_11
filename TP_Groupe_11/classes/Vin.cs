using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Groupe_11.classes
{
    internal class Vin
    {
        private string NumRef { get; set; }
        private string Nom { get; set; }
        private int AnneeProduction { get; set; }
        private int Pourcentage_alcool { get; set; }
        private string Origine { get; set; }
        private Double Prix { get; set; }
        private Boolean Disponibilitee { get; set; }
        private string Marque { get; set; }
        private float Sulphate { get; set; }
        private float Acid_citrique { get; set; }
        private float Acide_volatile { get; set; }
        private Double Qualite { get; set; }
        private List<Cepage> Cepage { get; set; }
        private Type_Vin Type_Vin { get; set; }

        public Vin()
        {
            
        }

        public Vin(string numRef, string nom, int anneeProduction, int pourcentage_alcool, string origine, double prix, bool disponibilitee, string marque, float sulphate, float acid_citrique, float acide_volatile, double qualite, List<Cepage> cepage, Type_Vin type_Vin)
        {
            NumRef = numRef;
            Nom = nom;
            AnneeProduction = anneeProduction;
            Pourcentage_alcool = pourcentage_alcool;
            Origine = origine;
            Prix = prix;
            Disponibilitee = disponibilitee;
            Marque = marque;
            Sulphate = sulphate;
            Acid_citrique = acid_citrique;
            Acide_volatile = acide_volatile;
            Qualite = qualite;
            Cepage = cepage;
            Type_Vin = type_Vin;
        }

        public void Afficher()
        {
            throw new NotImplementedException();
        }
    }
}
