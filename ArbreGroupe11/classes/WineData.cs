using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbreGroupe11
{
    public class WineData
    {
        public double Alcohol { get; set; }
        public double Sulphates { get; set; }
        public double CitricAcid { get; set; }
        public double VolatileAcidity { get; set; }
        public int Quality { get; set; }

        public WineData(double alcohol, double sulphates, double citricAcid, double volatileAcidity, int quality)
        {
            Alcohol = alcohol;
            Sulphates = sulphates;
            CitricAcid = citricAcid;
            VolatileAcidity = volatileAcidity;
            Quality = quality;
        }
    }
}
