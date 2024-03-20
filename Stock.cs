using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class Stock
    {
        private int _numStock;
        private int _nombreStock;
        private Produit _leProduit;



        public Stock(int numStock, int nombreStock, Produit leProduit)
        {
            _numStock = numStock;
            _nombreStock = nombreStock;
            _leProduit = leProduit;
        }

        public int NumStock
        {
            get { return _numStock; }
            set { _numStock = value; }
        }

        public int NombreStock
        {
            get { return _nombreStock; }
            set { _nombreStock = value; }
        }

        public Produit LeProduit
        {
            get { return _leProduit; }
            set { _leProduit = value; }
        }


        public override string ToString()
        {
            return NumStock.ToString();
        }
    }
}
