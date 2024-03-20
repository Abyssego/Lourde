using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class RendezVous
    {
        private int _numRendezVous;
        private string _nomRendezVous;
        private string _prenomRendezVous;
        private string _mailRendezVous;
        private string _telRendezVous;
        private int _numSirenRendezVous;
        private string _nomSocieteRendezVous;
        private string _villeRendezVous;
        private string _cpRendezVous;
        private string _adresseRendezVous;
        private string _dateRendezVous;
        private string _heureDebutRendezVous;
        private string _heureFinRendezVous;
        private string _butRendezVous;
        private string _descriptionRendezVous;
        private int _confirmationRendezVous;
        private Commerciaux _leCommerciaux;


        public RendezVous(int numRendezVous, string nomRendezVous, string prenomRendezVous, string mailRendezVous, string telRendezVous, int numSirenRendezVous, string nomSocieteRendezVous, string villeRendezVous, string cpRendezVous, string adresseRendezVous, string dateRendezVous, string heureDebutRendezVous, string heureFinRendezVous, string butRendezVous, string descriptionRendezVous, int confirmationRendezVous, Commerciaux lecommerciaux)
        {
            _numRendezVous = numRendezVous;
            _nomRendezVous = nomRendezVous;
            _prenomRendezVous = prenomRendezVous;
            _mailRendezVous = mailRendezVous;
            _telRendezVous = telRendezVous;
            _numSirenRendezVous = numSirenRendezVous;
            _nomSocieteRendezVous = nomSocieteRendezVous;
            _villeRendezVous = villeRendezVous;
            _cpRendezVous = cpRendezVous;
            _adresseRendezVous = adresseRendezVous;
            _dateRendezVous = dateRendezVous;
            _heureDebutRendezVous = heureDebutRendezVous;
            _heureFinRendezVous = heureFinRendezVous;
            _butRendezVous = butRendezVous;
            _descriptionRendezVous = descriptionRendezVous;
            _confirmationRendezVous = confirmationRendezVous;
            _leCommerciaux = lecommerciaux;
        }


        public int NumRendezVous
        {
            get { return _numRendezVous; }
            set { _numRendezVous = value; }
        }

        public string NomRendezVous
        {
            get { return _nomRendezVous; }
            set { _nomRendezVous = value; }
        }

        public string PrenomRendezVous
        {
            get { return _prenomRendezVous; }
            set { _prenomRendezVous = value; }
        }

        public string MailRendezVous
        {
            get { return _mailRendezVous; }
            set { _mailRendezVous = value; }
        }

        public string TelRendezVous
        {
            get { return _telRendezVous; }
            set { _telRendezVous = value; }
        }

        public int NumSirenRendezVous
        {
            get { return _numSirenRendezVous; }
            set { _numSirenRendezVous = value; }
        }

        public string NomSocieteRendezVous
        {
            get { return _nomSocieteRendezVous; }
            set { _nomSocieteRendezVous = value; }
        }

        public string VilleRendezVous
        {
            get { return _villeRendezVous; }
            set { _villeRendezVous = value; }
        }

        public string CpRendezVous
        {
            get { return _cpRendezVous; }
            set { _cpRendezVous = value; }
        }

        public string AdresseRendezVous
        {
            get { return _adresseRendezVous; }
            set { _adresseRendezVous = value; }
        }

        public string DateRendezVous
        {
            get { return _dateRendezVous; }
            set { _dateRendezVous = value; }
        }

        public string HeureDebutRendezVous
        {
            get { return _heureDebutRendezVous; }
            set { _heureDebutRendezVous = value; }
        }

        public string HeureFinRendezVous
        {
            get { return _heureFinRendezVous; }
            set { _heureFinRendezVous = value; }
        }

        public string ButRendezVous
        {
            get { return _butRendezVous; }
            set { _butRendezVous = value; }
        }

        public string DescriptionRendezVous
        {
            get { return _descriptionRendezVous; }
            set { _descriptionRendezVous = value; }
        }

        public int ConfirmationRendezVous
        {
            get { return _confirmationRendezVous; }
            set { _confirmationRendezVous = value; }
        }

        public Commerciaux LeCommerciaux
        {
            get { return _leCommerciaux; }
            set { _leCommerciaux = value; }
        }


        public override string ToString()
        {
            return NumRendezVous.ToString() + " " +  NomRendezVous.ToString() + " " + PrenomRendezVous.ToString();
        }

    }


}
