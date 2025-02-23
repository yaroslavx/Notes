using FluentValidation;

namespace Notes.Application.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(udpateNoteCommand => udpateNoteCommand.Id).NotEqual(Guid.Empty);
        RuleFor(udpateNoteCommand => udpateNoteCommand.Title).NotEmpty().MaximumLength(250);
        RuleFor(udpateNoteCommand => udpateNoteCommand.UserId).NotEqual(Guid.Empty);
    }
}