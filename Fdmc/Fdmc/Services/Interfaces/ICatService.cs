namespace Fdmc.Services.Interfaces
{
    using ViewModels.CatViewModels;

    public interface ICatService
    {
        void AddCat(CreateCatViewModel catModel);

        bool IsCatExists(string name);

        DetailsCatViewModel GetCat(int id);
    }
}