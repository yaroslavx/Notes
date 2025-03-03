using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;

namespace Notes.Tests.Notes.Queries;

[Collection("QueryCollection")]
public class GetNoteListQueryHandlerTests
{
    private readonly NotesDbContext Context;
    private readonly IMapper Mapper;

    public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }
    
    [Fact]
    public async Task GetNoteListQueryHandler_Success()
    {
        var handler = new GetNoteListQueryHandler(Context, Mapper);

        var result = await handler.Handle(
            new GetNoteListQuery()
            {
                UserId = NotesContextFactory.UserBId,
            }, CancellationToken.None);
        
        result.ShouldBeOfType<NoteListVm>();
        result.Notes.Count.ShouldBe(2);
    }
}