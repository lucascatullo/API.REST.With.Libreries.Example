using API.Rest.Example.Data.Models;
using Core.Models.Manager.DataManager;

namespace API.Rest.Example.Infrastructure.DataManager;

public class ProductDataManager : BaseDataManager<Product, int>
{
    public void SetValues(IProduct values)
    {
        dataBaseObj.Description = values.Description;
        dataBaseObj.Price = values.Price;   
        dataBaseObj.Name = values.Name; 
    }
}
