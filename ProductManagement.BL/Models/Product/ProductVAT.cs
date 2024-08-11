using System.ComponentModel.DataAnnotations;

namespace ProductManagement.BL.Models.Product
{
    public class ProductVAT
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPriceWithVAT{ get; set; }
    }
}