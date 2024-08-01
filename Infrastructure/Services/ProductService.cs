using API.Rest.Example.Data;
using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.DataManager;
using API.Rest.Example.Infrastructure.QueryBuilder;
using API.Rest.Example.Infrastructure.ViewModels.Product.Requests;
using Code.Models.Manager.Service;
using Core.Models.Manager.Interface;

namespace API.Rest.Example.Infrastructure.Service;

public class ProductService(ExampleContext db) : BaseService<Product, int>(db), IProductService
{
    private IProductQueryBuilder _query;
    private new readonly ExampleContext _db = db;
    private new ProductDataManager _dataManager = new();

    protected override IQueryBuilder<Product, int> StartQuery()
    {
        _query = new ProductQueryBuilder(_db.Products);
        return _query;
    }

    public async Task<Product> CreateAsync(IProduct values)
    {
        _dataManager.dataBaseObj = new Product();
        _dataManager.SetValues(values);

        await _db.AddAsync(_dataManager.dataBaseObj);
        await _db.SaveChangesAsync();

        return _dataManager.dataBaseObj;
    }

    public async Task<Product> EditAsync(IEditProductRequest request)
    {
        _dataManager.dataBaseObj = await _query.GetAsync(request.Id);
        _dataManager.SetValues(request);
        await _db.SaveChangesAsync(); 

        return _dataManager.dataBaseObj;
    }
}
