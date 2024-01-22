using System.Net.Mime;
using Bristows.TRACR.API.AuthenticationTemplate;
using Bristows.TRACR.API.AuthenticationTemplate.Interfaces;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.DTOs;
using Bristows.TRACR.Model.Models.POCOs;
using Bristows.TRACR.Model.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bristows.TRACR.API.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("api/v1/[controller]")]
public sealed partial class AuthController : ControllerBase
{
    #region [Infrastructure] 
    private readonly IUserService _userService;
    private readonly IAuthProvider _authProvider;
    private readonly ILogger<AuthController> _logger;
    private static TE Ex<TE>() where TE : Exception => throw (TE)Activator.CreateInstance(typeof(TE), "untracked")!;
    private static TE Ex<TE, ExpectedType>(object? exc=null) where TE : Exception => throw (TE)Activator.CreateInstance(typeof(TE), $"Expected: {typeof(ExpectedType)}", nameof(exc))!;
    public AuthController(IAuthProvider authProvider, ILogger<AuthController> logger, IUserService userService)
    {
        (_logger, _userService, _authProvider) = (logger, userService, authProvider);
    }
    #endregion

    /// <summary>
    /// POST: api/{version}/Auth/Register
    /// </summary>
    /// <param name="request">RegisterUserReq DTO</param>
    /// <response code="500">internal server error</response>
    /// <response code="201"><see cref="User"/>created</response>
    /// <response code="400"><see cref="User"/>not created</response>
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(User))]
    [ActionName("Register"),HttpPost("[action]"),AllowAnonymous]
    public ActionResult<RegisterUserReq?> Register([FromServices] IWebHostEnvironment env, [FromBody] RegisterUserReq req)
    {
        if (req == null) return env == null ? throw Ex<NullReferenceException>() : env.IsDevelopment() ? throw Ex<ArgumentNullException>() : StatusCode(StatusCodes.Status400BadRequest);
        _authProvider.GenerateHash(out byte[] hash, out byte[] salt, req!.Password);
        User user = new(){StoredHash=hash, StoredSalt=salt, Password=req!.Password};            //use automapper
        return (user != null) ? CreatedAtAction(nameof(Login), null) : env.IsDevelopment() ? throw Ex<NullReferenceException, User>(user) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    /// <summary>    
    /// POST: api/{version}/Auth/Login
    /// </summary>
    /// <response code="200"><see cref="IEnumerable{Skill}"/> objects</response>
    /// <response code="204"><see cref="IEnumerable{Skill}"/> objects not found</response>
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(object))]  //replace 'object'
    [ActionName("Login"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<User?>?>> Login([FromServices] IWebHostEnvironment webHostEnvironment, LoginReq loginReq)
    {
        // IWebHostEnvironment env = webHostEnvironment ?? Ex(webHostEnvironment);
        // IEnumerable<Skill?> skills = await _diaryService.GetSkills();
        // return (skills != null) && (typeof(List<Skill>) == skills!.GetType()) ? Ok(skills) : StatusCode(204);
        // return env.IsDevelopment() ? throw new Exception() : StatusCode(StatusCodes.Status500InternalServerError)
        return null;
    }

}

// [AllowAnonymous]