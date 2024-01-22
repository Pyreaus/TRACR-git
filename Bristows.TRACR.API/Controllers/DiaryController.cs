using Bristows.TRACR.BLL.Services.Interfaces;
using Bristows.TRACR.Model.Models.Entities;
using Bristows.TRACR.Model.Models.ViewModels;
using Bristows.TRACR.Model.Models.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Bristows.TRACR.Model.DTOs;
using Bristows.TRACR.API.TESTDEV.DependancyInjection;
using Humanizer;

namespace Bristows.TRACR.API.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
// [Authorize(Policy="tracr-default",AuthenticationSchemes=NegotiateDefaults.AuthenticationScheme)]
[Route("api/v1/[controller]")]
public sealed partial class DiaryController : ControllerBase
{
    #region [Infrastructure]
    private readonly IMapper _mapper;
    private readonly IDiaryService _diaryService;
    private readonly ILogger<DiaryController> _logger;
    private static TE Ex<TE>() where TE : Exception => throw (TE)Activator.CreateInstance(typeof(TE), "untracked")!;
    private static TE Ex<TE, ExpectedType>(object? exc=null) where TE : Exception => throw (TE)Activator.CreateInstance(typeof(TE), $"Expected: {typeof(ExpectedType)}", nameof(exc))!;
    public DiaryController(ILogger<DiaryController> logger, IMapper mapper, IDiaryService diaryService)
    {
        (_diaryService, _logger, _mapper) = (diaryService ?? throw Ex<ArgumentNullException>(), logger, mapper);
    }
    #endregion

    /// <summary>
    /// GET: {{host}}/api/{{version}}/Diary/GetSkills
    /// </summary>
    /// <response code="200"><see cref="IEnumerable{Skill}"/>objects</response>
    /// <response code="204"><see cref="IEnumerable{Skill}"/>objects not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<Skill>))]
    [ActionName("GetSkills"),Authorize(Policy="trainee//reviewer"),HttpGet("[action]")]
    public async Task<ActionResult<IEnumerable<Skill?>?>> GetSkills()
    {
        IEnumerable<Skill?> skills = await _diaryService.GetSkills();
        return (skills != null) && (typeof(List<Skill>) == skills!.GetType()) ? Ok(skills) : StatusCode(204);
    }

    /// <summary>
    /// GET: {{host}}/api/{{version}}/Diary/GetDiariesPfid/[pfid]
    /// </summary>
    /// <param name="pfid">PFID of diary objects</param>
    /// <response code="200"><see cref="IEnumerable{DiaryViewModel}"/>objects</response>
    /// <response code="204"><see cref="IEnumerable{DiaryViewModel}"/>objects not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<DiaryViewModel>))]
    [ActionName("GetDiariesPfid"),Authorize(Policy="trainee//reviewer"),HttpGet("[action]/{pfid:int}")]
    public async Task<ActionResult<IEnumerable<DiaryViewModel?>?>> GetDiariesPfid([FromRoute] [ValidPfid] int pfid)
    {
        IEnumerable<Diary?> diaries = await _diaryService.GetDiariesAsync(pfid);
        IEnumerable<DiaryViewModel?> diaryVM = _mapper.Map<IEnumerable<Diary?>, IEnumerable<DiaryViewModel>>(diaries!);
        return (diaryVM != null) && (typeof(List<Diary>) == diaries!.GetType()) ? Ok(diaryVM) : StatusCode(204);
    }
    
    /// <summary>
    /// GET: {{host}}/api/{{version}}/Diary/GetTasksByDiaryId/[id]
    /// </summary>
    /// <param name="id">ID of diary object</param>
    /// <response code="200"><see cref="IEnumerable{DiaryTaskViewModel}"/>objects </response>
    /// <response code="204"><see cref="IEnumerable{DiaryTaskViewModel}"/>objects not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(IEnumerable<DiaryTaskViewModel>))]
    [ActionName("GetTasksByDiaryId"),Authorize(Policy="trainee//reviewer"),HttpGet("[action]/{id:int}")]
    public async Task<ActionResult<IEnumerable<DiaryTaskViewModel?>?>> GetTasksByDiaryId([FromRoute] int id)
    {
        IEnumerable<DiaryTask?> diaryTasks = await _diaryService.DiaryTasksByDiaryIdAsync(id);
        IEnumerable<DiaryTaskViewModel?> diaryTasksVM = _mapper.Map<IEnumerable<DiaryTask?>, IEnumerable<DiaryTaskViewModel>>(diaryTasks!);
        return (diaryTasksVM != null) && (typeof(List<DiaryTask>) == diaryTasks!.GetType()) ? Ok(diaryTasksVM) : StatusCode(204);
    }

    /// <summary>
    /// POST: {{host}}/api/{{version}}/Diary/AddDiaryTask
    /// </summary>
    /// <param name="request">AddModifyDiaryTaskReq DTO</param>
    /// <response code="400"><see cref="DiaryTask"/>object not created</response>
    /// <response code="201"><see cref="DiaryTaskViewModel"/>object</response>
    [ValidateAntiForgeryToken]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(DiaryTaskViewModel))]
    [ActionName("AddDiaryTask"),Authorize(Policy="trainee"),HttpPost("[action]")]
    public ActionResult<DiaryTaskViewModel?> AddDiaryTask([FromBody] AddModifyDiaryTaskReq request)
    {
        if (request is null) return BadRequest(request);
        DiaryTask? newDiaryTask = _diaryService.CreateDiaryTask(_mapper.Map<AddModifyDiaryTaskReq,DiaryTask>(request));
        DiaryTaskViewModel diaryTaskVM = _mapper.Map<DiaryTask,DiaryTaskViewModel>(newDiaryTask!);
        return CreatedAtAction(nameof(GetTaskByTaskId), new { id = newDiaryTask?.DIARY_ID }, diaryTaskVM);
    }

    /// <summary>
    /// GET: {{host}}/api/{{version}}/Diary/GetTaskByTaskId/[id]
    /// </summary>
    /// <param name="id">ID of task object</param>
    /// <response code="200"><see cref="DiaryTaskViewModel"/>object</response>
    /// <response code="204"><see cref="DiaryTaskViewModel"/>object not found</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DiaryTaskViewModel))]
    [ActionName("GetTaskByTaskId"),Authorize(Policy="trainee//reviewer"),HttpGet("[action]/{id:int}")]
    public async Task<ActionResult<DiaryTaskViewModel?>> GetTaskByTaskId([FromRoute] int id)
    {
        DiaryTask? diaryTask = await _diaryService.DiaryTaskByTaskIdAsync(id);
        DiaryTaskViewModel? diaryTaskVM = _mapper.Map<DiaryTask?, DiaryTaskViewModel>(diaryTask!);
        return (diaryTaskVM != null) && (typeof(DiaryTask) == diaryTask!.GetType()) ? Ok(diaryTaskVM) : StatusCode(204);
    }

    /// <summary>
    /// PUT: {{host}}/api/{{version}}/Diary/EditTaskByTaskId/[id]
    /// </summary>
    /// <param name="id">ID of task object</param>
    /// <param name="request">AddModifyDiaryTaskReq DTO</param>
    /// <response code="400"><see cref="DiaryTask"/>object not modified</response>
    /// <response code="200"><see cref="DiaryTaskViewModel"/>object</response>
    [ValidateAntiForgeryToken]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DiaryTaskViewModel))]
    [ActionName("EditTaskByTaskId"),Authorize(Policy="trainee"),HttpPut("[action]/{id:int}")]
    public async Task<ActionResult<DiaryTaskViewModel?>> EditTaskByTaskId([FromRoute] int id, [FromBody] AddModifyDiaryTaskReq request)
    {
        DiaryTask? diaryTask = await _diaryService.DiaryTaskByTaskIdAsync(id);
        if ((diaryTask is null)||(request is null)) return BadRequest(request);
        DiaryTaskViewModel? diaryTaskVM = _mapper.Map<DiaryTask?, DiaryTaskViewModel>(diaryTask);
        _mapper.Map(request, diaryTask);
        this._diaryService.UpdateDiaryTask(diaryTask!);
        return Ok(diaryTaskVM);
    }

    /// <summary>
    /// DELETE: {{host}}/api/{{version}}/Diary/DeleteTaskByTaskId/[id]
    /// </summary>
    /// <param name="id">ID of task object</param>
    /// <response code="200"><see cref="DiaryTask"/>object deleted</response>
    /// <response code="204"><see cref="DiaryTask"/>object not deleted</response>
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ActionName("DeleteTaskByTaskId"),Authorize(Policy="trainee"),HttpDelete("[action]/{id:int}")]
    public async Task<IActionResult> DeleteTaskByTaskId([FromRoute] int id)
    {
        if (await _diaryService.DiaryTaskByTaskIdAsync(id) is null) return StatusCode(204);
        DiaryTask? taskToDelete = await _diaryService.DiaryTaskByTaskIdAsync(id);
        _diaryService.DeleteDiaryTask(taskToDelete!);
        return Ok(200);
    }

    /// <summary>
    /// POST: {{host}}/api/{{version}}/Diary/AddDiary
    /// </summary>
    /// <param name="request">AddModifyDiaryReq DTO</param>
    /// <response code="400"><see cref="Diary"/>object not created</response>
    /// <response code="201"><see cref="DiaryViewModel"/>object</response>
    [ValidateAntiForgeryToken]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created,Type=typeof(DiaryViewModel))]
    [ActionName("AddDiary"),Authorize(Policy="trainee"),HttpPost("[action]")]
    public ActionResult<DiaryViewModel?> AddDiary([FromBody] AddModifyDiaryReq request)
    {
        if (request is null) return BadRequest(request);
        Diary? newDiary = _diaryService.CreateDiary(_mapper.Map<AddModifyDiaryReq,Diary>(request));
        DiaryViewModel diaryVM = _mapper.Map<Diary,DiaryViewModel>(newDiary!);
        return CreatedAtAction(nameof(GetDiariesPfid), new { pfid = newDiary?.PFID }, diaryVM);
    }

    /// <summary>
    /// PUT: {{host}}/api/{{version}}/Diary/EditDiaryById/[pfid]
    /// </summary>
    /// <param name="id">DiaryId of diary object</param>
    /// <param name="request">AddModifyDiaryReq DTO</param>
    /// <response code="400"><see cref="Diary"/>object not modified</response>
    /// <response code="200"><see cref="DiaryViewModel"/>object</response>
    [ValidateAntiForgeryToken] 
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK,Type=typeof(DiaryViewModel))]
    [ActionName("EditDiaryById"),Authorize(Policy="trainee//reviewer"),HttpPut("[action]/{id:int}")]
    public async Task<ActionResult<DiaryViewModel?>> EditDiaryById([FromRoute] int id, [FromBody] AddModifyDiaryReq request)
    {
        Diary? diary = await _diaryService.GetDiaryByDiaryIdAsync(id);
        if ((diary is null)||(request is null)) return BadRequest(request);
        DiaryViewModel? diaryVM = _mapper.Map<Diary, DiaryViewModel>(diary!);
        _mapper.Map(request, diary);
        diary.SIGNED_OFF_TIMESTAMP = (diary.SIGNED_OFF_BY is null)||(diary.SIGNED_OFF_BY is "") ? null : DateTime.Now;
        _diaryService.UpdateDiary(diary!);
        return Ok(diaryVM);
    }

    /// <summary>
    /// DELETE: {{host}}/api/{{version}}/Diary/DeleteDiaryPfid/[pfid]
    /// </summary>
    /// <param name="pfid">PFID of diary object</param>
    /// <response code="204">invlaid pfid</response>
    /// <response code="200">object deleted</response>
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ActionName("DeleteDiaryPfid"),Authorize(Policy="trainee"),HttpDelete("[action]/{pfid:int}")]
    public async Task<IActionResult> DeleteDiaryPfid([FromRoute] [ValidPfid] int pfid)
    {
        if (await _diaryService.GetDiaryByPfidAsync(pfid) is null) return StatusCode(204);
        Diary? diaryToDelete = await _diaryService.GetDiaryByPfidAsync(pfid);
        _diaryService.DeleteDiary(diaryToDelete!);
        return Ok(200);
    }
}