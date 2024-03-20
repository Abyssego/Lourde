using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lourde
{
    public class ProduitType
    {
        private string _typeProduit;

        public ProduitType(string typeProduit)
        {
            _typeProduit = typeProduit;
        }

        public string TypeProduit
        {
            get { return _typeProduit; }
            set { _typeProduit = value; }
        }

        public override string ToString()
        {
            return TypeProduit.ToString();
        }

    }
}
