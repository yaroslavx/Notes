using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.UpdateNote;

namespace Notes.WebApi.Models;

public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
            .ForMember(noteCommand => noteCommand.Id,
                opt => opt.MapFrom(notDto => notDto.Id))
            .ForMember(noteCommand => noteCommand.Title,
                opt => opt.MapFrom(notDto => notDto.Title))
            .ForMember(noteCommand => noteCommand.Details,
                opt => opt.MapFrom(notDto => notDto.Details));
    }
}