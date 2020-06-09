using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Data
{
    public class Structures
    {
        public class Product
        {
            public String codProdus { get; set; } = String.Empty;
            public String denumireProdus { get; set; } = String.Empty;
            public Boolean voted { get; set; } = false;
            public Int32 votedCount { get; set; } = 0;
            public String btnType { get; set; } = "btn-primary";

            public Product(String codp, String denm) 
            {
                codProdus = codp;
                denumireProdus = denm;
            }
            public Product(String codp, String denm, Int32 votes)
            {
                codProdus = codp;
                denumireProdus = denm;
                votedCount = votes;
            }
        }
        public class Products
        {
            public List<Product> productList = new List<Product>();

            public void initializeListFromRetete(Retete retete) 
            {
                foreach(Reteta reteta in retete.retete)
                {
                    productList.Add(new Product(reteta.codp, reteta.denm));
                }
            }
            public void initializeListFromVoteCount(Counts counts)
            {
                foreach(Count count in counts.counts)
                {
                    productList.Add(new Product(count.codp, count.denm, count.votecount));
                }
            }
        }
    }
}
