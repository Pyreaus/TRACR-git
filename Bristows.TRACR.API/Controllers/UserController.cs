using System.Security.Claims;
using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.ViewModels;
using Bristows.TRACR.Model.Models.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Bristows.TRACR.Model.DTOs;

namespace Bristows.TRACR.API.Controllers;
[ApiController]
 // [Authorize(Policy="tracr-default",AuthenticationSchemes=NegotiateDefaults.AuthenticationScheme)]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[Route("api/v1/[controller]")]
public partial class UserController : ControllerBase
{
    #region [Infrastructure]
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly ClaimsPrincipal _claimsPrincipal;
    private static T NullArg<T>(T arg) => throw new ArgumentNullException(nameof(arg));
    private readonly string bnetUrl = "http://bnetsource/uploads/photos/";
    private readonly IUserService _userService;
    public UserController(ClaimsPrincipal claimsPrincipal, ILogger<UserController> logger, IUserService userService, IMapper mapper)
    {
        (_userService, _logger, _mapper, _claimsPrincipal) = (userService ?? NullArg<IUserService>(userService!), logger, mapper, claimsPrincipal);
    }
    #endregion

    /// <summary>
    /// GET: api/{version}/User/GetTraineesByReviewer/{pfid}
    /// </summary>
    /// <param name="pfid">trainee reviwer PFID</param>
    /// <response code="200">{trainee view objects}</response>
    /// <response code="404">missing trainee objects</response>
    [Authorize(Policy="tracr-reviewer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<TraineeViewModel>))]
    [ActionName("GetTraineesByReviewer"),HttpGet("[action]/{pfid:int}")]
    public async Task<ActionResult<IEnumerable<TraineeViewModel>?>> GetTraineesByReviewer([FromRoute] [ValidPfid] int pfid)
    {
        IEnumerable<PeopleFinderUser?> users = await _userService.GetPFUsersAsync();
        IEnumerable<Trainee?> trainees = await _userService.TraineesByReviewerAsync(pfid);
        IEnumerable<TraineeViewModel?> traineesVM = _mapper.Map<IEnumerable<Trainee?>,IEnumerable<TraineeViewModel>>(
            trainees.Where(
                trainee => users.Any(
                    user => user?.PFID.ToString() == trainee?.TRAINEE_PFID
                )
            ).OfType<Trainee>().ToList()!).OfType<TraineeViewModel>().ToList();
        foreach (PeopleFinderUser? user in users) user!.Photo = (bnetUrl + user.Photo?.ToString()) ?? "../../../assets/profilePic.png";
        foreach (TraineeViewModel? trainee in traineesVM) _mapper.Map(users.FirstOrDefault(user => trainee?.TRAINEE_PFID == user?.PFID.ToString())!, trainee);
        return (trainees.GetType() == typeof(List<Trainee>)) && traineesVM != null ? Ok(traineesVM) : StatusCode(404);
    }

    /// <summary>
    /// PUT: api/{version}/User/SetPair/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of trainee</param>
    /// <param name="addReq">AddModifyTraineeReq DTO</param>
    /// <response code="201">{ new trainee object }</response>
    /// <response code="400">object not created</response>
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(TraineeViewModel))]
    [Authorize(Policy="tracr-admin")]
    [ActionName("SetPair"),HttpPut("[action]/{pfid:int}")]
    public async Task<ActionResult<TraineeViewModel>?> SetPair([FromRoute] [ValidPfid] int pfid, [FromBody] AddModifyTraineeReq addReq)
    {
        if ((await _userService.GetPFUserAsync(pfid) is null)||(addReq is null)) return StatusCode(400);
        Trainee? currentTrainee = await _userService.GetTraineeByPfidAsync(pfid);
        _userService.SetPair(_mapper.Map(addReq, currentTrainee!));
        TraineeViewModel traineeVM = _mapper.Map<Trainee,TraineeViewModel>(currentTrainee!);
        return CreatedAtAction(nameof(GetTraineesByReviewer), new { pfid = currentTrainee?.REVIEWER_PFID }, traineeVM);
    }

    /// <summary>
    /// GET: api/{version}/User/GetReviewers
    /// </summary>
    /// <response code="200">{reviewer view objects}</response>
    /// <response code="404">missing reviewer objects</response>
    [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<UserViewModel>))]
    [ActionName("GetReviewers"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<UserViewModel>?>> GetReviewers()
    {
        IEnumerable<PeopleFinderUser?> reviewers = await _userService.GetReviewersAsync();
        IEnumerable<UserViewModel?> reviewersVM = _mapper.Map<IEnumerable<PeopleFinderUser?>,IEnumerable<UserViewModel>>(reviewers!);
        foreach(UserViewModel? rev in reviewersVM) rev!.Role = "reviewer";
        return (reviewersVM != null) && (typeof(List<PeopleFinderUser>) == reviewers!.GetType()) ? Ok(reviewersVM) : StatusCode(204);
    }
                                    //------------------------------MAINTENANCE--------------------------------//
    /// <summary>
    /// PUT: api/{version}/User/AssignTrainees/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of trainee</param>
    /// <param name="addReq">AddModifyTraineeReq DTO</param>
    /// <response code="201">{ new trainee object }</response>
    /// <response code="400">object not created</response>
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(TraineeViewModel))]
    [Authorize(Policy="tracr-admin")]
    [ActionName("AssignTrainees"),HttpPost("[action]/{pfid:int}")]
    public async Task<ActionResult<TraineeViewModel>?> AssignTrainees([FromRoute] [ValidPfid] int pfid, [FromBody] AddModifyTraineeReq addReq)
    {
        if ((await _userService.GetPFUserAsync(pfid) is null)||(addReq is null)) return StatusCode(400);
        Trainee? currentTrainee = await _userService.GetTraineeByPfidAsync(pfid);
        _userService.AssignTrainees(_mapper.Map(addReq, currentTrainee!));
        TraineeViewModel traineeVM = _mapper.Map<Trainee,TraineeViewModel>(currentTrainee!);
        return CreatedAtAction(nameof(GetTraineesByReviewer), new { pfid = currentTrainee?.REVIEWER_PFID }, traineeVM);
    }
                                    //------------------------------MAINTENANCE--------------------------------//
    /// <summary>
    /// PUT: api/{version}/User/EditTrainee/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of trainee</param>
    /// <param name="modifyReq">AddModifyTraineeReq DTO</param>
    /// <response code="200">{AddModifyTraineeReq DTO}</response>
    /// <response code="400">object not modified</response>
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(TraineeViewModel))]
    [ActionName("EditTrainee"),HttpPut("[action]/{pfid:int}")]
    public async Task<ActionResult<TraineeViewModel?>> EditTrainee([FromRoute] [ValidPfid] int pfid, [FromBody] AddModifyTraineeReq modifyReq)
    {
        Trainee? trainee = await _userService.GetTraineeByPfidAsync(pfid);
        if ((trainee is null)||(modifyReq is null)) return StatusCode(400);
        _mapper.Map<AddModifyTraineeReq?,Trainee>(modifyReq, trainee);
        this._userService.UpdateTrainee(trainee);
        return StatusCode(200);
    }

    /// <summary>
    /// GET: api/{version}/User/GetTrainees
    /// </summary>
    /// <response code="200">{trainee view objects}</response>
    /// <response code="404">missing trainee objects</response>
                                                // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<TraineeViewModel>))]
    [ActionName("GetTrainees"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<TraineeViewModel>?>> GetTrainees()
    {
        IEnumerable<Trainee?> trainees = await _userService.GetTraineesAsync();
        IEnumerable<PeopleFinderUser?> users = await _userService.GetPFUsersAsync();
        IEnumerable<TraineeViewModel?> traineesVM = _mapper.Map<IEnumerable<Trainee?>,IEnumerable<TraineeViewModel>>(
            trainees.Where(
                trainee => users.Any(
                    user => user?.PFID.ToString() == trainee?.TRAINEE_PFID
                )
            ).OfType<Trainee>().ToList()!).OfType<TraineeViewModel>().ToList();
        foreach (PeopleFinderUser? user in users) user!.Photo = (bnetUrl + user.Photo?.ToString()) ?? "../../../assets/profilePic.png";
        foreach (TraineeViewModel? trainee in traineesVM) _mapper.Map(users.FirstOrDefault(user => trainee?.TRAINEE_PFID == user?.PFID.ToString())!, trainee);
        return (trainees.GetType() == typeof(List<Trainee>)) && traineesVM != null ? Ok(traineesVM) : StatusCode(404);
    }

    /// <summary>
    /// GET: api/{version}/User/GetUsers
    /// </summary>
    /// <response code="200">{user view objects}</response>
    /// <response code="404">missing user objects</response>
    [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<UserViewModel>))]
    [ActionName("GetUsers"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<UserViewModel>?>> GetUsers()
    {
        IEnumerable<PeopleFinderUser?> users = await _userService.GetPFUsersAsync();
        IEnumerable<UserViewModel?> usersVM = _mapper.Map<IEnumerable<PeopleFinderUser?>,IEnumerable<UserViewModel>>(users!);
        return (usersVM != null) && (typeof(List<PeopleFinderUser>) == users!.GetType()) ? Ok(usersVM) : StatusCode(204);
    }

    /// <summary>
    /// GET: api/{version}/User/GetUserType
    /// </summary>
    /// <response code="200">{user view objects}</response>
    /// <response code="511">unauthorized client</response>
    [ProducesResponseType(StatusCodes.Status511NetworkAuthenticationRequired)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(UserViewModel))]
    [ActionName("GetUserType"),HttpGet("[action]")]
    public async Task<ActionResult<UserViewModel?>> GetUserType([FromServices] IWebHostEnvironment webHostEnvironment)
    {
        IWebHostEnvironment env = webHostEnvironment ?? NullArg<IWebHostEnvironment>(webHostEnvironment!);
        if (_claimsPrincipal.Identity?.IsAuthenticated == true)
        {
            Claim? usernameClaim = _claimsPrincipal.FindFirst("DomainUsername");
            if (usernameClaim?.Value != null)
            {
                PeopleFinderUser? user = await _userService.GetByDomainAsync(usernameClaim.Value);
                if (user != null && user?.PFID != null)
                {
                    string? role = await _userService.GetRoleByPfidAsync((int)user.PFID);
                    UserViewModel? userVM = role != null ? _mapper.Map<PeopleFinderUser,UserViewModel>(user) : null;
                    userVM!.Role = role ?? "Unauthorized";
                    return userVM != null ? Ok(userVM) : StatusCode(StatusCodes.Status511NetworkAuthenticationRequired);
                }
                return StatusCode(StatusCodes.Status511NetworkAuthenticationRequired);
            }
         }
        return env.IsDevelopment() ? throw new Exception() : StatusCode(StatusCodes.Status500InternalServerError);
    }




}
