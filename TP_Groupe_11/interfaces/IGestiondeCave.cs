﻿using System;
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

        void RetirerVin(Vin vin);

        string RecherVinAvecNom(string nom);
    }
}
