using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;

namespace Notes.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;

    public NoteController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    /// <summary>
    /// Gets the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note
    /// </remarks>
    /// <returns>Returns NoteListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery()
        {
            UserId = UserId,
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Gets the note by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note/142A9564-ABF9-47B8-8114-7928F1FA9A23
    /// </remarks>
    /// <param name="id"></param>
    /// <returns>Return NoteDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery()
        {
            Id = id,
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Creates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /note
    /// {
    ///     title: "note title"
    ///     details: "note details"
    /// }
    /// </remarks>
    /// <param name="createNoteDto">CreateNoteDto object</param>
    /// <returns>Return id (guid)</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteCommand createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var noteId = await Mediator.Send(command);
        return Ok(noteId);
    }

    /// <summary>
    /// Updates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /note
    /// {
    ///     title: "updated title"
    /// }
    /// </remarks>
    /// <param name="updateNoteDto">UpdateNoteDto object</param>
    /// <returns>Return NoContent</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteDetailsVm>> Update([FromBody] UpdateNoteCommand updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes the note by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /note/9C5D7C66-D270-4298-8C2B-97AC463D5710
    /// </remarks>
    /// <param name="id">Id of the note (guid)</param>
    /// <returns>Return NoContent</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpDelete("{id}")]    
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteNoteCommand() { Id = id, UserId = UserId });
        return NoContent();
    }
}