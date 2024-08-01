using API.Rest.Example.Infrastructure.ViewModel.Product.Request;

namespace API.Rest.Example.Infrastructure.ViewModels.Product.Requests;

public class EditProductRequest : CreateProductRequest, IEditProductRequest
{
    /// <summary>
    /// Id of the target product.
    /// </summary>
    public int Id { get; set; }
}
