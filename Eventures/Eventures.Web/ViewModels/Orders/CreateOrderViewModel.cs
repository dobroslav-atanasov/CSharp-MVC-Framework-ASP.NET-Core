namespace Eventures.Web.ViewModels.Orders
{
    using System.ComponentModel.DataAnnotations;
    
    public class CreateOrderViewModel
    {
        public int EventId { get; set; }

        public string CustomerName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Tickets")]
        public int Tickets { get; set; }
    }
}