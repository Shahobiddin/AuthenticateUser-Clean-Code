using System.Collections.Generic;
using System.Linq;
using Functions.Task4.ThirdParty;

namespace Functions.Task4
{
    public class Order
    {
        public IList<IProduct> Products { get; set; }

        public double GetPriceOfAvailableProducts()
        {
            return CalculatePriceOfAvailableProducts();
        }

        private double CalculatePriceOfAvailableProducts()
        {
            return Products
                   .Where(w => w.IsAvailable())
                   .Select(p => p.GetProductPrice())
                   .Sum(s => s);
        }
    }
}
