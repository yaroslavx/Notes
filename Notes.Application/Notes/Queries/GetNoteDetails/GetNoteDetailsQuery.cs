using MediatR;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails;

public class GetNoteDetailsQuery : IRequest<NoteDetailsVm>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}