using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Produit
    {
        private int _numProduit;
        private string _nomProduit;
        private string _petiteDescriptionProduit;
        private string _longueDescriptionProduit;
        private string _prixProduit;
        private ProduitType _leProduitType;



        public Produit(int numProduit, string nomProduit, string petiteDescriptionProduit, string longueDescriptionProduit, string prixProduit, ProduitType leProduitType)
        {
            _numProduit = numProduit;
            _nomProduit = nomProduit;
            _petiteDescriptionProduit = petiteDescriptionProduit;
            _longueDescriptionProduit = longueDescriptionProduit;
            _prixProduit = prixProduit;
            _leProduitType = leProduitType;
        }

        public int NumProduit
        {
            get { return _numProduit; }
            set { _numProduit = value; }
        }

        public string NomProduit
        {
            get { return _nomProduit; }
            set { _nomProduit = value; }
        }

        public string PetiteDescriptionProduit
        {
            get { return _petiteDescriptionProduit; }
            set { _petiteDescriptionProduit = value; }
        }

        public string LongueDescriptionProduit
        {
            get { return _longueDescriptionProduit; }
            set { _longueDescriptionProduit = value; }
        }

        public string PrixProduit
        {
            get { return _prixProduit; }
            set { _prixProduit = value; }
        }

        public ProduitType LeProduitType
        {
            get { return _leProduitType; }
            set { _leProduitType = value; }
        }


        public override string ToString()
        {
            return NumProduit.ToString() + " " + NomProduit.ToString();
        }
    }
}
