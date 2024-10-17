using ApiTallerDelChipAlClick.DtoModels;
using FluentValidation;

namespace ApiTallerDelChipAlClick.Validators
{
    public class LedsInsertValidator : AbstractValidator<LedsInsertDto>
    {
        public LedsInsertValidator()
        {
            RuleFor(x => x.LedName).NotEmpty().WithMessage("El nombre es obligatorio");
        }
    }
}

