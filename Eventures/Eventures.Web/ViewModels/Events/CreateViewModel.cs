namespace Eventures.Web.ViewModels.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [Display(Name = "Price Per Ticket")]
        public decimal PricePerTicket { get; set; }
    }
}