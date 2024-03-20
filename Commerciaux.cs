using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Commerciaux
    {

        private int _numCommerciaux;
        private string _nomCommerciaux;
        private string _prenomCommerciaux;
        private string _adresseCommerciaux;
        private string _villeCommerciaux;
        private string _cpCommerciaux;
        private string _mailCommerciaux;
        private string _telCommerciaux;
        private Identification _leIdentification;



        public Commerciaux(int numCommerciaux, string nomCommerciaux, string prenomCommerciaux, string adresseCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux, Identification numpasswordcommerciaux)
        {
            _numCommerciaux = numCommerciaux;
            _nomCommerciaux = nomCommerciaux;
            _prenomCommerciaux = prenomCommerciaux;
            _adresseCommerciaux = adresseCommerciaux;
            _villeCommerciaux = villeCommerciaux;
            _cpCommerciaux = cpCommerciaux;
            _mailCommerciaux = mailCommerciaux;
            _telCommerciaux = telCommerciaux;
            _leIdentification = numpasswordcommerciaux;
        }

        public int NumCommerciaux
        {
            get { return _numCommerciaux; }
            set { _numCommerciaux = value; }
        }

        public string NomCommerciaux
        {
            get { return _nomCommerciaux; }
            set { _nomCommerciaux = value; }
        }

        public string PrenomCommerciaux
        {
            get { return _prenomCommerciaux; }
            set { _prenomCommerciaux = value; }
        }

        public string AdresseCommerciaux
        {
            get { return _adresseCommerciaux; }
            set { _adresseCommerciaux = value; }
        }

        public string VilleCommerciaux
        {
            get { return _villeCommerciaux; }
            set { _villeCommerciaux = value; }
        }

        public string CpCommerciaux
        {
            get { return _cpCommerciaux; }
            set { _cpCommerciaux = value; }
        }

        public string MailCommerciaux
        {
            get { return _mailCommerciaux; }
            set { _mailCommerciaux = value; }
        }

        public string TelCommerciaux
        {
            get { return _telCommerciaux; }
            set { _telCommerciaux = value; }
        }

        public Identification LeIdentification
        {
            get { return _leIdentification; }
            set { _leIdentification = value; }
        }

        public override string ToString()
        {
            return NumCommerciaux.ToString() + " " + NomCommerciaux.ToString() + " " + PrenomCommerciaux.ToString();
        }

    }
}
