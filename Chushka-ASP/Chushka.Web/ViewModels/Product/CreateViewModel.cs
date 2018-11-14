namespace Chushka.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;

    public class CreateViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
