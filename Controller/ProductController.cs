using API.Rest.Example.Infrastructure.Service;
using API.Rest.Example.Infrastructure.ViewModel.Product.Request;
using API.Rest.Example.Infrastructure.ViewModel.Product.Response;
using API.Rest.Example.Infrastructure.ViewModels.Product.Requests;
using Core.API.Request.Response.Handler;
using Core.API.Request.Response.Request;
using Core.API.Request.Response.Response;
using Core.Main.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Rest.Example.Controller;

[ApiController]
[Route("/api/product")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController(IProductService productService, IMapper mapper) : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IProductService _productService = productService;
    private readonly IMapper _mapper = mapper;


    /// <summary>
    /// Creates a new product
    /// </summary>
    /// <param name="request">New product values</param>
    /// <response code="400">Invalid request. Check the reponse body</response>
    /// <response code="401">Unauthorized.</response>
    /// <returns>Product object</returns>
    [HttpPost("create")]
    public async Task<JsonResult> Create([FromBody] CreateProductRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var product = await _productService.CreateAsync(request);
            response.Success = true; 
            response.Body = _mapper.CopyAndReturn<CreateProductResponse>(product);
        }
        catch(Exception e) 
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }


    /// <summary>
    /// Edits an existing product.
    /// </summary>
    /// <param name="request">New product values</param>
    /// <response code="400">Invalid request. Check the reponse body</response>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Product was not found.</response>
    /// <returns>Product object.</returns>
    [HttpPatch("edit")]
    public async Task<JsonResult> Edit([FromBody] EditProductRequest request)
    {
        var response = new BaseResponse();

        try
        {
            var product = await _productService.EditAsync(request);
            response.Success = true;
            response.Body = _mapper.CopyAndReturn<CreateProductResponse>(product);
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }


    /// <summary>
    /// Query for one product.
    /// </summary>
    /// <param name="id">Id of the product</param>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Product was not found.</response>
    /// <returns>Product object.</returns>
    [HttpGet("get/{id}")]
    public async Task<JsonResult> Get(int id)
    {
        var response = new BaseResponse();

        try
        {
            var product = await _productService.GetAsync(id);
            response.Success = true;
            response.Body = _mapper.CopyAndReturn<CreateProductResponse>(product);
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }

    /// <summary>
    /// Logical delete one product.
    /// </summary>
    /// <param name="id">Id of the product</param>
    /// <response code="401">Unauthorized.</response>
    /// <response code="404">Product was not found.</response>
    /// <returns>Base response with empty body</returns>
    [HttpDelete("delete/{id}")]
    public async Task<JsonResult> Delete(int id)
    {
        var response = new BaseResponse();

        try
        {
            var success = await _productService.LogicalDeleteAsync(id);
            response.Success = success;
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }


    /// <summary>
    /// Query for a page of products.
    /// </summary>
    /// <param name="pageNum">Number of the page</param>
    /// <param name="pageSize">Size of the page</param>
    /// <response code="401">Unauthorized.</response>
    /// <returns>A list of products</returns>
    [HttpGet] 
    public async Task<JsonResult> GetAll(int pageNum, int pageSize)
    {
        var response = new PaginatedResponse();

        try
        {
            var page = await _productService.GetAll(pageNum, pageSize);
            response.Success = true;
            response.HasNextPage = page.HasNextPage;
            response.Body = _mapper.CopyForAll<CreateProductResponse>(page.Items);
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<PaginatedResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }

}


