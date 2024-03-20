using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Facture
    {
        private int _numFacture;
        private string _dateFacture;
        private int _qteFacture;
        private Produit _leProduit;
        private Client _leClient;



        public Facture(int numFacture, string dateFacture, int qteFacture, Produit leProduit, Client leClient)
        {
            _numFacture = numFacture;
            _dateFacture = dateFacture;
            _qteFacture = qteFacture;
            _leProduit = leProduit;
            _leClient = leClient;
        }

        public int NumFacture
        {
            get { return _numFacture; }
            set { _numFacture = value; }
        }

        public string DateFacture
        {
            get { return _dateFacture; }
            set { _dateFacture = value; }
        }

        public int QteFacture
        {
            get { return _qteFacture; }
            set { _qteFacture = value; }
        }

        public Produit LeProduit
        {
            get { return _leProduit; }
            set { _leProduit = value; }
        }

        public Client LeClient
        {
            get { return _leClient; }
            set { _leClient = value; }
        }

        public override string ToString()
        {
            return LeProduit.NomProduit.ToString();
        }
    }
}
