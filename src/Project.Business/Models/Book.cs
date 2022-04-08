using System;

namespace Project.Business.Models
{
    public class Book : Entity
    {
        public Guid SupplierId { get; set; }
        public string Title { get; set; }
        public string Resume { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public int Volume { get; set; }
        public Category Category { get; set; }
        public DateTime Publication { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public bool InStock { get; set; }

        /*EF Relation*/
        public Supplier Supplier { get; set; }
    }
}
