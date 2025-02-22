using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListVm>
{
    private readonly INoteDbContext _context;
    private readonly IMapper _mapper;
    
    public GetNoteListQueryHandler(INoteDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        var notesQuery = await _context.Notes
            .Where(note => note.UserId == request.UserId)
            .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        return new NoteListVm { Notes = notesQuery };
            
    }
}