using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Data
{
    /// <summary>
    /// the main structures for use within the program
    /// </summary>
    public class Structures
    {
        /// <summary>
        /// the product class
        /// </summary>
        public class Product
        {
            /// <summary>
            /// the product code
            /// </summary>
            public String codProdus { get; set; } = String.Empty;
            /// <summary>
            /// the product name
            /// </summary>
            public String denumireProdus { get; set; } = String.Empty;
            /// <summary>
            /// the voted property
            /// </summary>
            public Boolean voted { get; set; } = false;
            /// <summary>
            /// the main votedCount for the object
            /// </summary>
            public Int32 votedCount { get; set; } = 0;
            /// <summary>
            /// the btnType used for altering the button
            /// </summary>
            public String btnType { get; set; } = "btn-primary";
            /// <summary>
            /// the main contructor with 2 parameters
            /// </summary>
            /// <param name="codp">the product code</param>
            /// <param name="denm">the product name</param>
            public Product(String codp, String denm) 
            {
                codProdus = codp;
                denumireProdus = denm;
            }
            /// <summary>
            /// the main constructor with 3 parameters
            /// </summary>
            /// <param name="codp">the product code</param>
            /// <param name="denm">the product name</param>
            /// <param name="votes">the product votes</param>
            public Product(String codp, String denm, Int32 votes)
            {
                codProdus = codp;
                denumireProdus = denm;
                votedCount = votes;
            }
        }
        /// <summary>
        /// the Products object class
        /// </summary>
        public class Products
        {
            /// <summary>
            /// the main product list
            /// </summary>
            public List<Product> productList = new List<Product>();

            /// <summary>
            /// this function is used to intialize the object from a retete list
            /// </summary>
            /// <param name="retete">A Retete Structure</param>
            public void initializeListFromRetete(Retete retete) 
            {
                //we will iterate the products
                foreach(Reteta reteta in retete.retete)
                {
                    //then we add a new product to the list
                    productList.Add(new Product(reteta.codp, reteta.denm));
                }
            }
            /// <summary>
            /// this function will initialize the products list from the counts list
            /// </summary>
            /// <param name="counts">the initial counts object</param>
            public void initializeListFromVoteCount(Counts counts)
            {
                //we will iterate the counts list
                foreach(Count count in counts.counts)
                {
                    //then we will add the new product to the list
                    productList.Add(new Product(count.codp, count.denm, count.votecount));
                }
            }
        }
    }
}
