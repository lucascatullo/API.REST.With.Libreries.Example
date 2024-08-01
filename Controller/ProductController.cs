using API.Rest.Example.Infrastructure.Service;
using API.Rest.Example.Infrastructure.ViewModel.Product.Request;
using API.Rest.Example.Infrastructure.ViewModel.Product.Response;
using Azure.Core;
using Core.API.Request.Response.Handler;
using Core.API.Request.Response.Request;
using Core.API.Request.Response.Response;
using Core.Main.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Rest.Example.Controller;



[ApiController]
[Route("/api/product")]
public class ProductController(IProductService productService, IMapper mapper) : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IProductService _productService = productService;
    private readonly IMapper _mapper = mapper;



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


