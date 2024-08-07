﻿using API.Rest.Example.Data.Models;
using API.Rest.Example.Infrastructure.Mappers;
using API.Rest.Example.Infrastructure.Service;
using API.Rest.Example.Infrastructure.ViewModel.User.Request;
using API.Rest.Example.Infrastructure.ViewModels.User.Requests;
using Core.API.Request.Response.Handler;
using Core.API.Request.Response.Request;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tools.JWT.Helper.Helper;
using Tools.JWT.Helper.Model;

namespace API.Rest.Example.Controller;


[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController(IUserService userService, IUserMapper mapper, IJwtGenerator jwtGenerator) : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserService _userService = userService;
    private readonly IUserMapper _mapper = mapper;
    private readonly IJwtGenerator _jwtGenerator = jwtGenerator;

    /// <summary>
    /// Register a new user into the system
    /// </summary>
    /// <param name="request"></param>
    /// <response code="400">Invalid request. Check the reponse body</response>
    /// <returns>User object and a validation JWT token.</returns>
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<JsonResult> Create([FromBody] CreateUserRequest request)
    {
        var response = new BaseResponse();

        try
        {
            request.ModelIsValid(ModelState);
            User user = await _userService.CreateAsync(request);
            response.Success = true;
            response.Body = _mapper.BuildVM(user, _jwtGenerator.GenerateUserToken([.. (await _userService.GetRolesAsync(user))], user.Email!, user.Id));
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }

    /// <summary>
    /// Try to log in using user credentials
    /// </summary>
    /// <param name="request">Login data. (email and password)</param>
    /// <returns>User object and a validation JWT token.</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<JsonResult> LogIn([FromBody] LoginRequest request)
    {
        var response = new BaseResponse();

        try
        {
            User user = await _userService.LogInAsync(request.Email, request.Password);
            response.Success = true;
            response.Body = _mapper.BuildVM(user, _jwtGenerator.GenerateUserToken([.. (await _userService.GetRolesAsync(user))], user.Email!, user.Id));
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }


    /// <summary>
    /// Extract the user from the Claims
    /// </summary>
    /// <response code="401">Unauthorized</response>
    /// <returns>User object</returns>
    [HttpGet("get")]
    public async Task<JsonResult> Get()
    {
        var response = new BaseResponse();

        try
        {
            User user = await _userService.GetAsync(new JwtUser(User).Id);
            response.Success = true;
            response.Body = _mapper.BuildVM(user);
        }
        catch (Exception e)
        {
            response = ExceptionHandler.Handle<BaseResponse>(e);
        }
        return Json(response.FormatResponse(HttpContext));
    }

}
