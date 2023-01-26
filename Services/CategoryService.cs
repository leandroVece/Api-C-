using EF.Models;

namespace API.Service;
public class CategoryService : ICategoryService
{
    TaskContext context;

    public CategoryService(TaskContext dbcontext)
    {
        context = dbcontext;
    }

    public IEnumerable<Category> Get()
    {
        return context.Categoties;
    }

    //Esta es la razon por la que no debemos usar palabras recervadas.
    public async System.Threading.Tasks.Task Post(Category category)
    {
        context.Add(category);
        await context.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task update(Guid id, Category category)
    {
        var categoryPresent = context.Categoties.Find(id);
        if (categoryPresent != null)
        {
            categoryPresent.Name = category.Name;
            categoryPresent.Description = category.Description;
            categoryPresent.Salubrity = category.Salubrity;

            await context.SaveChangesAsync();
        }
    }

    public async System.Threading.Tasks.Task Delete(Guid id)
    {
        var categoryPresent = context.Categoties.Find(id);
        if (categoryPresent != null)
        {
            context.Remove(categoryPresent);
            await context.SaveChangesAsync();
        }
    }



}

public interface ICategoryService
{
    IEnumerable<Category> Get();
    System.Threading.Tasks.Task Delete(Guid id);
    System.Threading.Tasks.Task update(Guid id, Category category);
    System.Threading.Tasks.Task Post(Category category);

}