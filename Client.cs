using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Client
    {

        private int _numClient;
        private string _nomClient;
        private string _prenomClient;
        private string _adresseClient;
        private string _villeClient;
        private string _cpClient;
        private string _mailClient;
        private string _telClient;
        private Entreprise _leEntreprise;



        public Client(int numclient, string nomclient, string prenomclient, string adresseclient, string villeclient, string cpclient, string mailclient, string telclient, Entreprise leentreprise)
        {
            _numClient = numclient;
            _nomClient = nomclient;
            _prenomClient = prenomclient;
            _adresseClient = adresseclient;
            _villeClient = villeclient;
            _cpClient = cpclient;
            _mailClient = mailclient;
            _telClient = telclient;
            _leEntreprise = leentreprise;
        }


        public int NumClient
        {
            get { return _numClient; }
            set { _numClient = value; }
        }

        public string NomClient
        {
            get { return _nomClient; }
            set { _nomClient = value; }
        }

        public string PrenomClient
        {
            get { return _prenomClient; }
            set { _prenomClient = value; }
        }

        public string AdresseClient
        {
            get { return _adresseClient; }
            set { _adresseClient = value; }
        }

        public string VilleClient
        {
            get { return _villeClient; }
            set { _villeClient = value; }
        }

        public string CpClient
        {
            get { return _cpClient; }
            set { _cpClient = value; }
        }

        public string MailClient
        {
            get { return _mailClient; }
            set { _mailClient = value; }
        }

        public string TelClient
        {
            get { return _telClient; }
            set { _telClient = value; }
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
