namespace MyPerfume.Data.Models
{
    using System;

    using MyPerfume.Data.Common.Models;

    public class Sale : BaseDeletableModel<string>
    {
        public Sale()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ProductId { get; set; }

        public Product Product { get; set; }

        //public string UserId { get; set; }

        //public ApplicationUser User { get; set; }

        public decimal Discount { get; set; }

        public DateTime TimeOfSell { get; set; }
    }
}
