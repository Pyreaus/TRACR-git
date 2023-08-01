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
public partial class DiaryController : ControllerBase
{
    #region [Infrastructure]
    private readonly IMapper _mapper;
    private readonly ILogger<DiaryController> _logger;
    private readonly IDiaryService _diaryService;
    private static T NullArg<T>(T arg) => throw new ArgumentNullException(nameof(arg));
    public DiaryController(ILogger<DiaryController> logger, IMapper mapper, IDiaryService diaryService)
    {
        (_diaryService, _logger, _mapper) = (diaryService ?? NullArg<IDiaryService>(diaryService!), logger, mapper);
    }
    #endregion

    /// <summary>
    /// GET: api/{version}/Diary/GetSkills
    /// </summary>
    /// <response code="200">{skill view objects}</response>
    /// <response code="204">missing skill objects</response>
                                                                    // [Authorize(Policy="tracr-trainee//reviewer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<Skill>))]
    [ActionName("GetSkills"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<Skill?>?>> GetSkills()
    {
        IEnumerable<Skill?> skills = await _diaryService.GetSkills();
        return (skills != null) && (typeof(List<Skill>) == skills!.GetType()) ? Ok(skills) : StatusCode(204);
    }

    /// <summary>
    /// GET: api/{version}/Diary/GetDiariesPfid/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of diary objects</param>
    /// <response code="200">{diary view objects}</response>
    /// <response code="204">missing diary objects</response>
                                                                        // [Authorize(Policy="tracr-trainee//reviewer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<DiaryViewModel>))]
    [ActionName("GetDiariesPfid"),HttpGet("[action]/{pfid:int}")]
    public async Task<ActionResult<IEnumerable<DiaryViewModel?>?>> GetDiariesPfid([FromRoute] [ValidPfid] int pfid)
    {
        IEnumerable<Diary?> diaries = await _diaryService.GetDiariesAsync(pfid);
        IEnumerable<DiaryViewModel?> diaryVM = _mapper.Map<IEnumerable<Diary?>, IEnumerable<DiaryViewModel>>(diaries!);
        return (diaryVM != null) && (typeof(List<Diary>) == diaries!.GetType()) ? Ok(diaryVM) : StatusCode(204);
    }
    
    /// <summary>
    /// GET: api/{version}/Diary/GetTasksByDiaryId/{id}
    /// </summary>
    /// <param name="id">ID of diary object</param>
    /// <response code="200">{task view objects}</response>
    /// <response code="204">missing DiaryTask objects</response>
                                     // [Authorize(Policy="tracr-trainee//reviewer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<DiaryTaskViewModel>))]
    [ActionName("GetTasksByDiaryId"),HttpGet("[action]/{id:int}")]
    public async Task<ActionResult<IEnumerable<DiaryTaskViewModel?>?>> GetTasksByDiaryId([FromRoute] int id)
    {
        IEnumerable<DiaryTask?> diaryTasks = await _diaryService.DiaryTasksByDiaryIdAsync(id);
        IEnumerable<DiaryTaskViewModel?> diaryTasksVM = _mapper.Map<IEnumerable<DiaryTask?>, IEnumerable<DiaryTaskViewModel>>(diaryTasks!);
        return (diaryTasksVM != null) && (typeof(List<DiaryTask>) == diaryTasks!.GetType()) ? Ok(diaryTasksVM) : StatusCode(204);
    }

    /// <summary>
    /// POST: api/{version}/Diary/AddDiaryTask
    /// </summary>
    /// <param name="addReq">AddModifyDiaryTaskReq DTO</param>
    /// <response code="201">{ new diaryTask object }</response>
    /// <response code="400">object was not created</response>
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Policy="tracr-trainee")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(DiaryTaskViewModel))]
    [ActionName("AddDiaryTask"),HttpPost("[action]")]
    public ActionResult<DiaryTaskViewModel?> AddDiaryTask([FromBody] AddModifyDiaryTaskReq addReq)
    {
        if (addReq is null) return BadRequest(addReq);
        DiaryTask? newDiaryTask = _diaryService.CreateDiaryTask(_mapper.Map<AddModifyDiaryTaskReq,DiaryTask>(addReq));
        DiaryTaskViewModel diaryTaskVM = _mapper.Map<DiaryTask,DiaryTaskViewModel>(newDiaryTask!);
        return CreatedAtAction(nameof(GetTaskByTaskId), new { id = newDiaryTask?.DIARY_ID }, diaryTaskVM);
    }

    /// <summary>
    /// GET: api/{version}/Diary/GetTaskByTaskId/{id}
    /// </summary>
    /// <param name="id">ID of task object</param>
    /// <response code="200">{task view object}</response>
    /// <response code="204">missing DiaryTask object</response>
    [Authorize(Policy="tracr-trainee//reviewer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DiaryTaskViewModel))]
    [ActionName("GetTaskByTaskId"),HttpGet("[action]/{id:int}")]
    public async Task<ActionResult<DiaryTaskViewModel?>> GetTaskByTaskId([FromRoute] int id)
    {
        DiaryTask? diaryTask = await _diaryService.DiaryTaskByTaskIdAsync(id);
        DiaryTaskViewModel? diaryTaskVM = _mapper.Map<DiaryTask?, DiaryTaskViewModel>(diaryTask!);
        return (diaryTaskVM != null) && (typeof(DiaryTask) == diaryTask!.GetType()) ? Ok(diaryTaskVM) : StatusCode(204);
    }

    /// <summary>
    /// PUT: api/{version}/Diary/EditTaskByTaskId/{id}
    /// </summary>
    /// <param name="id">ID of task object</param>
    /// <param name="modifyReq">AddModifyDiaryTaskReq DTO</param>
    /// <response code="200">{task view object}</response>
    /// <response code="400">object not modified</response>
    [Authorize(Policy="tracr-trainee")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DiaryTaskViewModel))]
    [ActionName("EditTaskByTaskId"),HttpPut("[action]/{id:int}")]
    public async Task<ActionResult<DiaryTaskViewModel?>> EditTaskByTaskId([FromRoute] int id, [FromBody] AddModifyDiaryTaskReq modifyReq)
    {
        DiaryTask? diaryTask = await _diaryService.DiaryTaskByTaskIdAsync(id);
        if ((diaryTask is null)||(modifyReq is null)) return BadRequest(modifyReq);
        DiaryTaskViewModel? diaryTaskVM = _mapper.Map<DiaryTask?, DiaryTaskViewModel>(diaryTask);
        _mapper.Map(modifyReq, diaryTask);
        this._diaryService.UpdateDiaryTask(diaryTask!);
        return Ok(diaryTaskVM);
    }

    /// <summary>
    /// DELETE: api/{version}/Diary/DeleteTaskByTaskId/{id}
    /// </summary>
    /// <param name="id">ID of task object</param>
    /// <response code="204">invlaid id</response>
    /// <response code="200">object deleted</response>
    [Authorize(Policy="tracr-trainee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ActionName("DeleteTaskByTaskId"),HttpDelete("[action]/{id:int}")]
    public async Task<IActionResult> DeleteTaskByTaskId([FromRoute] int id)
    {
        if (await _diaryService.DiaryTaskByTaskIdAsync(id) is null) return StatusCode(204);
        DiaryTask? taskToDelete = await _diaryService.DiaryTaskByTaskIdAsync(id);
        _diaryService.DeleteDiaryTask(taskToDelete!);
        return Ok(200);
    }

    /// <summary>
    /// POST: api/{version}/Diary/AddDiary
    /// </summary>
    /// <param name="addReq">AddModifyDiaryReq DTO</param>
    /// <response code="201">{ new diary object }</response>
    /// <response code="400">object not created</response>
    [Authorize(Policy="tracr-trainee")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(DiaryViewModel))]
    [ActionName("AddDiary"),HttpPost("[action]")]
    public ActionResult<DiaryViewModel?> AddDiary([FromBody] AddModifyDiaryReq addReq)
    {
        if (addReq is null) return BadRequest(addReq);
        Diary? newDiary = _diaryService.CreateDiary(_mapper.Map<AddModifyDiaryReq,Diary>(addReq));
        DiaryViewModel diaryVM = _mapper.Map<Diary,DiaryViewModel>(newDiary!);
        return CreatedAtAction(nameof(GetDiariesPfid), new { pfid = newDiary?.PFID }, diaryVM);
    }

    /// <summary>
    /// PUT: api/{version}/Diary/EditDiaryPfid/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of diary object</param>
    /// <param name="modifyReq">AddModifyDiaryReq DTO</param>
    /// <response code="200">{diary view object}</response>
    /// <response code="400">object not modified</response>
    [Authorize(Policy="tracr-trainee")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DiaryViewModel))]
    [ActionName("EditDiaryPfid"),HttpPut("[action]/{pfid:int}")]
    public async Task<ActionResult<DiaryViewModel?>> EditDiaryPfid([FromRoute] [ValidPfid] int pfid, [FromBody] AddModifyDiaryReq modifyReq)
    {
        Diary? diary = await _diaryService.GetDiaryByPfidAsync(pfid);
        if ((diary is null)||(modifyReq is null)) return BadRequest(modifyReq);
        DiaryViewModel? diaryVM = _mapper.Map<Diary, DiaryViewModel>(diary!);
        _mapper.Map(modifyReq, diary);
        this._diaryService.UpdateDiary(diary!);
        return Ok(diaryVM);
    }

    /// <summary>
    /// DELETE: api/{version}/Diary/DeleteDiaryPfid/{pfid}
    /// </summary>
    /// <param name="pfid">PFID of diary object</param>
    /// <response code="204">invlaid pfid</response>
    /// <response code="200">object deleted</response>
    [Authorize(Policy="tracr-trainee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ActionName("DeleteDiaryPfid"),HttpDelete("[action]/{pfid:int}")]
    public async Task<IActionResult> DeleteDiaryPfid([FromRoute] [ValidPfid] int pfid)
    {
        if (await _diaryService.GetDiaryByPfidAsync(pfid) is null) return StatusCode(204);
        Diary? diaryToDelete = await _diaryService.GetDiaryByPfidAsync(pfid);
        _diaryService.DeleteDiary(diaryToDelete!);
        return Ok(200);
    }
}