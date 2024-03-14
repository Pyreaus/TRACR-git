using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Bristows.TRACR.API.Authentication;
using Bristows.TRACR.BLL.Authentication.Infrastructure;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.DTOs;
using Bristows.TRACR.Model.Models.POCOs;
using Bristows.TRACR.Model.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Bristows.TRACR.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public sealed partial class AuthController : ControllerBase
{
    #region [Infrastructure] 
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IAuthProvider _authProvider;   
    private readonly ILogger<AuthController> _logger;
    private static TE Ex<TE, T>(object? exc) where TE : Exception => throw Activator.CreateInstance(typeof(TE), $"Expected: {typeof(T)}", nameof(exc)) as TE ?? throw new Exception("Exception creation failed");
    private static TE Ex<TE>(object? exc = null) where TE : Exception => throw Activator.CreateInstance(typeof(TE), "untracked", nameof(exc)) as TE ?? throw new Exception("Exception creation failed");
    private static bool Ex(object? exc) => (exc == null) ? throw new ArgumentNullException(nameof(exc)) : exc.GetType() == typeof(Exception) ? throw (Exception)exc : true;
    public AuthController(IAuthProvider authProvider, ILogger<AuthController> logger, IMapper mapper, IUserService userService)
    {
        (_logger, _userService, _authProvider, _mapper) = (logger, userService ?? throw Ex<ArgumentNullException>(), authProvider, mapper);
    }
    #endregion

    /// <summary>
    /// POST: api/{version}/Auth/Register
    /// </summary>
    /// <response code="400"><see cref="User"/>not created</response>
    /// <response code="201"><see cref="User"/>created</response>
    /// <response code="500">internal server error</response>
    /// <param name="modeluest">RegisterUserReq DTO</param>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(CreatedAtActionResult))]
    [ActionName("Register"),HttpPost("[action]"),AllowAnonymous]
    public ActionResult<CreatedAtActionResult?> Register(
        [FromServices] IWebHostEnvironment env, [FromBody] RegisterUserReq model, CancellationToken ctk = default)
    {
        try
        {
            _authProvider.GenerateHash(ctk, out byte[] hash, out byte[] salt, model.Password);
            User user = _mapper.Map<RegisterUserReq, User>(model);
            (user.StoredHash, user.StoredSalt) = (hash, salt);
            if (Ex(user)) _userService.RegisterUser(user, ctk);
            return CreatedAtAction(nameof(Login), model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(Register));
            return (env != null && env.IsDevelopment()) ? StatusCode(StatusCodes.Status500InternalServerError, ex.Message) : StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

    /// <summary>    
    /// POST: api/{version}/Auth/Login
    /// </summary>
    /// <response code="200"><see cref="IEnumerable{Skill}"/> objects</response>
    /// <response code="204"><see cref="IEnumerable{Skill}"/> objects not found</response>
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DevUserViewModel))]
    [ActionName("Login"),HttpGet("[action]"),AllowAnonymous]
    public async Task<ActionResult<DevUserViewModel?>> Login(
        [FromServices] IWebHostEnvironment env, [FromBody] LoginReq model, CancellationToken ctk = default)
    {
        try
        {
            User? user = await _userService.GetUserAsync(model!.Email, ctk);
            if (Ex(user) && !_authProvider.VerifyHash(ctk, user!.StoredHash, user.StoredSalt, model.Password)) return Unauthorized();
            DevUserViewModel loginResponse = _mapper.Map<User, DevUserViewModel>(user!);
            (loginResponse.AuthToken, loginResponse.AuthTokenExpiration) = _authProvider.BuildToken(ctk, null!, user!);
            Ex(loginResponse);
            return Ok(loginResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(Login));
            return (env != null && env.IsDevelopment()) ? StatusCode(StatusCodes.Status500InternalServerError, ex.Message) : StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

    /// <summary>    
    /// GET: api/{version}/Auth/GetUser
    /// </summary>
    /// <response code="200"><see cref="IEnumerable{Skill}"/> objects</response>
    /// <response code="204"><see cref="IEnumerable{Skill}"/> objects not found</response>
    [ProducesResponseType(StatusCodes.Status404NotFound)]    // [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DevUserViewModel))]
    [ActionName("GetUser"),HttpGet("[action]/{email:string}"),Authorize]
    public async Task<ActionResult<DevUserViewModel?>> GetUser(
        [FromRoute] [EmailAddress] string email, [FromServices] IWebHostEnvironment env, CancellationToken ctk = default)
    {
        try
        {
            User? user = await _userService.GetUserAsync(email, ctk);
            DevUserViewModel userResponse = _mapper.Map<User, DevUserViewModel>(user!);
            Ex(userResponse);          //this logic is inferiror to normal null checks
            return Ok(userResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(GetUser));
            return (env != null && env.IsDevelopment()) ? StatusCode(StatusCodes.Status500InternalServerError, ex.Message) : StatusCode(StatusCodes.Status500InternalServerError, "Error processing request");
        }
    }

}
