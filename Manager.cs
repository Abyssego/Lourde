using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Manager
    {
        private int _numManager;
        private string _nomManager;
        private string _prenomManager;
        private string _adresseManager;
        private string _villeManager;
        private string _cpManager;
        private string _mailManager;
        private string _telManager;
        private Identification _leIdentification;



        public Manager(int numManager, string nomManager, string prenomManager, string adresseManager, string villeManager, string cpManager, string mailManager, string telManager, Identification leIdentification)
        {
            _numManager = numManager;
            _nomManager = nomManager;
            _prenomManager = prenomManager;
            _adresseManager = adresseManager;
            _villeManager = villeManager;
            _cpManager = cpManager;
            _mailManager = mailManager;
            _telManager = telManager;
            _leIdentification = leIdentification;
        }


        public int NumManager
        {
            get { return _numManager; }
            set { _numManager = value; }
        }

        public string NomProspect
        {
            get { return _nomManager; }
            set { _nomManager = value; }
        }

        public string PrenomManager
        {
            get { return _prenomManager; }
            set { _prenomManager = value; }
        }

        public string AdresseManager
        {
            get { return _adresseManager; }
            set { _adresseManager = value; }
        }

        public string VilleManager
        {
            get { return _villeManager; }
            set { _villeManager = value; }
        }

        public string CpManager
        {
            get { return _cpManager; }
            set { _cpManager = value; }
        }

        public string MailManager
        {
            get { return _mailManager; }
            set { _mailManager = value; }
        }

        public string TelManager
        {
            get { return _telManager; }
            set { _telManager = value; }
        }

        public Identification NumPasswordManager
        {
            get { return _leIdentification; }
            set { _leIdentification = value; }
        }


        public override string ToString()
        {
            return NumManager.ToString();
        }
    }
}
