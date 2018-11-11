namespace Fdmc.ViewModels.CatViewModels
{
    using System.Collections.Generic;
    using Models;

    public class AllCatsViewModel
    {
        public ICollection<Cat> Cats { get; set; }
    }
}