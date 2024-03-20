using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Entreprise
    {
        private int _numSirenEntreprise;
        private string _nomEntreprise;
        private string _villeSiegeEntreprise;
        private string _cpSiegeEntreprise;
        private string _adresseSiegeEntreprise;
        private string _villeLocauxEntreprise;
        private string _cpLocauxEntreprise;
        private string _adresseLocauxEntreprise;



        public Entreprise(int numSirenEntreprise, string nomEntreprise, string villeSiegeEntreprise, string cpSiegeEntreprise, string adresseSiegeEntreprise, string villeLocauxEntreprise, string cpLocauxEntreprise, string adresseLocauxEntreprise)
        {
            _numSirenEntreprise = numSirenEntreprise;
            _nomEntreprise = nomEntreprise;
            _villeSiegeEntreprise = villeSiegeEntreprise;
            _cpSiegeEntreprise = cpSiegeEntreprise;
            _adresseSiegeEntreprise = adresseSiegeEntreprise;
            _villeLocauxEntreprise = villeLocauxEntreprise;
            _cpLocauxEntreprise = cpLocauxEntreprise;
            _adresseLocauxEntreprise = adresseLocauxEntreprise;
        }

        public int NumSirenEntreprise
        {
            get { return _numSirenEntreprise; }
            set { _numSirenEntreprise = value; }
        }

        public string NomEntreprise
        {
            get { return _nomEntreprise; }
            set { _nomEntreprise = value; }
        }

        public string VilleSiegeEntreprise
        {
            get { return _villeSiegeEntreprise; }
            set { _villeSiegeEntreprise = value; }
        }

        public string CpSiegeEntreprise
        {
            get { return _cpSiegeEntreprise; }
            set { _cpSiegeEntreprise = value; }
        }

        public string AdresseSiegeEntreprise
        {
            get { return _adresseSiegeEntreprise; }
            set { _adresseSiegeEntreprise = value; }
        }

        public string VilleLocauxEntreprise
        {
            get { return _villeLocauxEntreprise; }
            set { _villeLocauxEntreprise = value; }
        }

        public string CpLocauxEntreprise
        {
            get { return _cpLocauxEntreprise; }
            set { _cpLocauxEntreprise = value; }
        }

        public string AdresseLocauxEntreprise
        {
            get { return _adresseLocauxEntreprise; }
            set { _adresseLocauxEntreprise = value; }
        }


        public override string ToString()
        {
            return NumSirenEntreprise.ToString();
        }
    }
}
