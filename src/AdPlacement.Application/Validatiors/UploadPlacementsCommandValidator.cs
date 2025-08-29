using FluentValidation;


namespace AdPlacements.Application.Validators;


public sealed class UploadPlacementsCommandValidator : AbstractValidator<AdPlacements.Application.Commands.UploadPlacementsCommand>
{
    public UploadPlacementsCommandValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content must not be empty");
    }
}