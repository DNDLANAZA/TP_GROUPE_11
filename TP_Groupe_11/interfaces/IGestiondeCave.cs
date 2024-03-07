using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_Groupe_11.classes;

namespace TP_Groupe_11.interfaces
{
    internal interface IGestiondeCave
    {
        void AjouterVin(Vin vin);

        List<Vin> AfficherListeVin();

        Vin RecherVinAvecRef(string NumRef);

        void MettreAjourVin(Vin vin);

        void SupprimerVin(string NumRef);

        void AugmenterQuantiteVin(string NumRef, int quantite);

        void RetirerQuantiteVin(string NumRef, int quantite);

        void AfficherStock();


    }
}
