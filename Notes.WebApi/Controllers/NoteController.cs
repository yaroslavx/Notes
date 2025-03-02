using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;

namespace Notes.WebApi.Controllers;

[Route("api/[controller]")]
public class NoteController : BaseController
{
    private readonly IMapper _mapper;

    public NoteController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<NoteListVm>> GetAll()
    {
        var query = new GetNoteListQuery()
        {
            UserId = UserId,
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
    {
        var query = new GetNoteDetailsQuery()
        {
            Id = id,
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteCommand createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = UserId;
        var noteId = await Mediator.Send(command);
        return Ok(noteId);
    }

    [HttpPut]    
    [Authorize]
    public async Task<ActionResult<NoteDetailsVm>> Update([FromBody] UpdateNoteCommand updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]    
    [Authorize]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteNoteCommand() { Id = id, UserId = UserId });
        return NoContent();
    }
}