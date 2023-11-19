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
public sealed partial class UserController : ControllerBase
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
    /// GET: api/{version}/User/GetReviewers
    /// </summary>
    /// <response code="200"><see cref="IEnumerable{UserViewModel}"/> objects</response>
    /// <response code="204"><see cref="IEnumerable{UserViewModel}"/> objects not found</response>
    [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<UserViewModel>))]
    [ActionName("GetReviewers"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<UserViewModel?>?>> GetReviewers()
    {
        IEnumerable<PeopleFinderUser?> reviewers = await _userService.GetReviewersAsync();
        IEnumerable<UserViewModel?> reviewersVM = _mapper.Map<IEnumerable<PeopleFinderUser?>,IEnumerable<UserViewModel>>(reviewers!);
        foreach(UserViewModel? rev in reviewersVM) 
        {
            rev!.Role = "Reviewer";
            rev!.Photo = (bnetUrl + rev!.Photo?.ToString()) ?? "../../../assets/profilePic.png";
        }
        return (reviewersVM != null) && (typeof(List<PeopleFinderUser>) == reviewers!.GetType()) ? Ok(reviewersVM) : StatusCode(204);
    }

    /// <summary>
    /// GET: api/{version}/User/GetTraineesByReviewer/{pfid}
    /// </summary>
    /// <param name="pfid">trainee reviwer PFID</param>
    /// <response code="500">internal error</response>
    /// <response code="404"><see cref="IEnumerable{TraineeViewModel}"/> objects not found</response>
    /// <response code="200"><see cref="IEnumerable{TraineeViewModel}"/> objects</response>
    [Authorize(Policy="tracr-reviewer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<TraineeViewModel>))]
    [ActionName("GetTraineesByReviewer"),HttpGet("[action]/{pfid:int}")]
    public async Task<ActionResult<IEnumerable<TraineeViewModel?>?>> GetTraineesByReviewer([FromRoute] [ValidPfid] int pfid)
    {
        IEnumerable<PeopleFinderUser?> users = await _userService.GetPFUsersAsync();
        IEnumerable<Trainee?> trainees = await _userService.TraineesByReviewerAsync(pfid);
        if ((users is null)||(trainees is null)) return StatusCode(404);
        IEnumerable<TraineeViewModel?> traineesVM = _mapper.Map<IEnumerable<Trainee?>,IEnumerable<TraineeViewModel>>(
            trainees.Where(
                trainee => users.Any(user => user?.PFID.ToString() == trainee?.TRAINEE_PFID)
            ).OfType<Trainee>().ToList()!).OfType<TraineeViewModel>().ToList();
        foreach (PeopleFinderUser? user in users) user!.Photo = (bnetUrl + user.Photo?.ToString()) ?? "../../../assets/profilePic.png";
        foreach (TraineeViewModel? trainee in traineesVM) _mapper.Map(users.FirstOrDefault(user => trainee?.TRAINEE_PFID == user?.PFID.ToString())!, trainee);
        return (trainees.GetType() == typeof(List<Trainee>)) && traineesVM != null ? Ok(traineesVM) : StatusCode(500);
    }

    /// <summary>
    /// PUT: api/{version}/User/SetPair/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of trainee</param>
    /// <param name="addReq">request DTO</param>
    /// <response code="500">internal error</response>
    /// <response code="400"><see cref="Trainee"/> not modified</response>
    /// <response code="201"><see cref="Trainee"/> modified</response>
    // [Authorize(Policy="tracr-admin")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(TraineeViewModel))]
    [ActionName("SetPair"),HttpPut("[action]/{pfid:int}")]
    public async Task<ActionResult<TraineeViewModel>?> SetPair([FromRoute] [ValidPfid] int pfid, [FromBody] AddModifyTraineeReq addReq)
    {
        Trainee? currentTrainee = await _userService.GetTraineeByPfidAsync(pfid);
        if ((currentTrainee is null)||(addReq is null)||(await _userService.GetPFUserAsync(pfid) is null)) return StatusCode(400);
        _userService.SetPair(_mapper.Map(addReq, currentTrainee!));
        TraineeViewModel traineeVM = _mapper.Map<Trainee,TraineeViewModel>(currentTrainee!);
        return traineeVM != null ? CreatedAtAction(nameof(GetTraineesByReviewer), new { pfid = currentTrainee?.REVIEWER_PFID }, traineeVM) : StatusCode(500);
    }

    /// <summary>
    /// POST: api/{version}/User/AssignTrainees/{pfid}
    /// </summary>
    /// <param name="addReq">request DTO</param>
    /// <param name="pfid">PFID of trainee</param>
    /// <response code="500">internal error</response>
    /// <response code="201"><see cref="TraineeViewModel"/> object</response>
    /// <response code="400"><see cref="TraineeViewModel"/> object not created</response>
    [Authorize(Policy="tracr-admin")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(TraineeViewModel))]
    [ActionName("AssignTrainees"),HttpPost("[action]/{pfid:int}")]
    public async Task<ActionResult<TraineeViewModel?>> AssignTrainees([FromRoute] [ValidPfid] int pfid, [FromBody] AddModifyTraineeReq addReq)
    {
        if ((await _userService.GetPFUserAsync(pfid) is null)||(addReq is null)) return StatusCode(400);
        Trainee? newTrainee = _mapper.Map<AddModifyTraineeReq,Trainee>(addReq!);
        newTrainee.TRAINEE_PFID = pfid.ToString();
        _userService.AssignTrainees(newTrainee);   
        TraineeViewModel traineeVM = _mapper.Map<Trainee,TraineeViewModel>(newTrainee!);
        return traineeVM != null ? CreatedAtAction(nameof(GetTraineesByReviewer), new { pfid = newTrainee?.REVIEWER_PFID }, traineeVM) : StatusCode(500);
    }

    /// <summary>
    /// PUT: api/{version}/User/EditTrainee/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of trainee</param>
    /// <param name="modifyReq">request DTO</param>
    /// <response code="200"><see cref="AddModifyTraineeReq"/> object modified</response>
    /// <response code="400"><see cref="Trainee"/> object not modified</response>
    // [Authorize(Policy="tracr-admin")]
    [Consumes(MediaTypeNames.Application.Json)]
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


    // /// <summary>
    // /// GET: api/{version}/User/GetReviewers
    // /// </summary>
    // /// <response code="200">{reviewer view objects}</response>
    // /// <response code="404">missing reviewer objects</response>
    //                                     // [Authorize(Policy="tracr-admin")]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<UserViewModel>))]
    // [ActionName("GetReviewers"),HttpGet("[action]")]
    // public async Task<ActionResult<IEnumerable<UserViewModel?>?>> GetReviewers()
    // {
    //     IEnumerable<PeopleFinderUser?> reviewers = await _userService.GetReviewersAsync();
    //     IEnumerable<UserViewModel?> reviewersVM = _mapper.Map<IEnumerable<PeopleFinderUser?>,IEnumerable<UserViewModel>>(reviewers!);
    //     foreach(UserViewModel? rev in reviewersVM) rev!.Role = "reviewer";
    //     return (reviewersVM != null) && (typeof(List<PeopleFinderUser>) == reviewers!.GetType()) ? Ok(reviewersVM) : StatusCode(204);
    // }
    
    /// <summary>
    /// GET: api/{version}/User/GetUserReviewer
    /// </summary>
    /// <param name="pfid">PFID of trainee</param>
    /// <response code="500">internal error</response>
    /// <response code="200"><see cref="UserViewModel"/> object</response>
    /// <response code="400"><see cref="UserViewModel"/> object not found</response>
    // [Authorize(Policy="tracr-trainee//tracr-reviewer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(UserViewModel))]
    [ActionName("GetUserReviewer"),HttpGet("[action]/{pfid:int}")]
    public async Task<ActionResult<UserViewModel?>> GetUserReviewer([FromRoute] [ValidPfid] int pfid)
    {
        Trainee? trainee = await _userService.GetTraineeByPfidAsync(pfid);
        PeopleFinderUser? reviewer = await _userService.ReviewerByTraineeAsync(pfid);
        if ((trainee is null)||(reviewer is null)) return StatusCode(400);
        UserViewModel userVM = _mapper.Map<PeopleFinderUser?,UserViewModel>(reviewer);
        userVM!.Role = "Reviewer";
        userVM!.Photo = (bnetUrl + userVM.Photo?.ToString()) ?? "../../../assets/profilePic.png";    
        return userVM != null ? Ok(userVM) : StatusCode(500);
    }

    /// <summary>
    /// GET: api/{version}/User/GetTrainees
    /// </summary>
    /// <response code="200"><see cref="IEnumerable{TraineeViewModel}"/> objects</response>
    /// <response code="404"><see cref="IEnumerable{TraineeViewModel}"/> objects not found</response>
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<TraineeViewModel>))]
    [ActionName("GetTrainees"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<TraineeViewModel?>?>> GetTrainees()
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
    /// <response code="200"><see cref="UserViewModel"/> objects</response>
    /// <response code="204"><see cref="UserViewModel"/> objects not found</response>
    // [Authorize(Policy="tracr-admin")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<UserViewModel>))]
    [ActionName("GetUsers"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<UserViewModel?>?>> GetUsers()
    {
        IEnumerable<PeopleFinderUser?> users = await _userService.GetPFUsersAsync();
        IEnumerable<UserViewModel?> usersVM = _mapper.Map<IEnumerable<PeopleFinderUser?>,IEnumerable<UserViewModel>>(users!);
        foreach (UserViewModel? user in usersVM) user!.Photo = (bnetUrl + user.Photo?.ToString()) ?? "../../../assets/profilePic.png";
        return (usersVM != null) && (typeof(List<PeopleFinderUser>) == users!.GetType()) ? Ok(usersVM) : StatusCode(204);
    }

    /// <summary>
    /// GET: api/{version}/User/GetUserType
    /// </summary>
    /// <response code="500">internal error</response>
    /// <response code="511">unauthorized client</response>
    /// <response code="200"><see cref="UserViewModel"/> object</response>
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
                    userVM!.Photo = (bnetUrl + userVM!.Photo?.ToString()) ?? "../../../assets/profilePic.png";
                    userVM!.Role = role ?? "Unauthorized";
                    return userVM != null ? Ok(userVM) : StatusCode(StatusCodes.Status511NetworkAuthenticationRequired);
                }
                return StatusCode(StatusCodes.Status511NetworkAuthenticationRequired);
            }
         }
        return env.IsDevelopment() ? throw new Exception() : StatusCode(StatusCodes.Status500InternalServerError);
    }




}
