using System.ComponentModel.DataAnnotations;

namespace shop.Models
{
    public class Basket
    {
        public Basket()
        {
            CartLines = new List<Product>();
        }
        public void RemoveAllFromCart()
        {
            CartLines.Clear();
        }
        public List<Product> CartLines { get; set; }

        public decimal FinalCost
        {
            get
            {
                decimal sum = 0;
                foreach (Product product in CartLines)
                {
                    sum += product.ProductCost;
                }
                return sum;
            }
        }
    }
}
