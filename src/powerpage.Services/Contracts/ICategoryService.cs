using powerpage.Entities;

namespace powerpage.Services.Contracts;

public interface ICategoryService
{
    void AddNewCategory(Category category);
    IList<Category> GetAllCategories();
}