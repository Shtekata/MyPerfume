namespace MyPerfume.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using MyPerfume.Data.Common.Models;

    public class Sale : BaseDeletableModel<string>
    {
        public Sale()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public decimal Discount { get; set; }

        public DateTime TimeOfSell { get; set; }
    }
}
