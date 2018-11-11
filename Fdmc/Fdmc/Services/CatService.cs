namespace Fdmc.Services
{
    using System.Linq;
    using Data;
    using Interfaces;
    using Models;
    using ViewModels.CatViewModels;

    public class CatService : ICatService
    {
        private readonly FdmcDbContext context;

        public CatService(FdmcDbContext context)
        {
            this.context = context;
        }

        public void AddCat(CreateCatViewModel catModel)
        {
            var cat = new Cat
            {
                Name = catModel.Name,
                Age = catModel.Age,
                Breed = catModel.Breed,
                Url = catModel.Url
            };

            this.context.Cats.Add(cat);
            this.context.SaveChanges();
        }

        public bool IsCatExists(string name)
        {
            return this.context.Cats.Any(c => c.Name == name);
        }

        public DetailsCatViewModel GetCat(int id)
        {
            var cat = this.context.Cats
                .Where(c => c.Id == id)
                .Select(c => new DetailsCatViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Age = c.Age,
                    Breed = c.Breed,
                    Url = c.Url
                })
                .FirstOrDefault();

            return cat;
        }
    }
}
