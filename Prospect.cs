using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Prospect
    {
        private int _numProspect;
        private string _nomProspect;
        private string _prenomProspect;
        private string _adresseProspect;
        private string _villeProspect;
        private string _cpProspect;
        private string _mailProspect;
        private string _telProspect;
        private Entreprise _leEntreprise;



        public Prospect(int numProspect, string nomProspect, string prenomProspect, string adresseProspect, string villeProspect, string cpProspect, string mailProspect, string telProspect, Entreprise leEntreprise)
        {
            _numProspect = numProspect;
            _nomProspect = nomProspect;
            _prenomProspect = prenomProspect;
            _adresseProspect = adresseProspect;
            _villeProspect = villeProspect;
            _cpProspect = cpProspect;
            _mailProspect = mailProspect;
            _telProspect = telProspect;
            _leEntreprise = leEntreprise;
        }


        public int NumProspect
        {
            get { return _numProspect; }
            set { _numProspect = value; }
        }

        public string NomProspect
        {
            get { return _nomProspect; }
            set { _nomProspect = value; }
        }

        public string PrenomProspect
        {
            get { return _prenomProspect; }
            set { _prenomProspect = value; }
        }

        public string AdresseProspect
        {
            get { return _adresseProspect; }
            set { _adresseProspect = value; }
        }

        public string VilleProspect
        {
            get { return _villeProspect; }
            set { _villeProspect = value; }
        }

        public string CpProspect
        {
            get { return _cpProspect; }
            set { _cpProspect = value; }
        }

        public string MailProspect
        {
            get { return _mailProspect; }
            set { _mailProspect = value; }
        }

        public string TelProspect
        {
            get { return _telProspect; }
            set { _telProspect = value; }
        }

        public Entreprise LeEntreprise
        {
            get { return _leEntreprise; }
            set { _leEntreprise = value; }
        }


        public override string ToString()
        {
            return LeEntreprise.NumSirenEntreprise.ToString() + " " + LeEntreprise.NomEntreprise.ToString();
        }
    }
}
