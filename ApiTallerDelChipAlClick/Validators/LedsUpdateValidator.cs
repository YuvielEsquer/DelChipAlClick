using ApiTallerDelChipAlClick.DtoModels;
using FluentValidation;

namespace ApiTallerDelChipAlClick.Validators
{
    public class LedsUpdateValidator : AbstractValidator<LedsUpdateDto>
    {
        public LedsUpdateValidator()
        {
            RuleFor(x => x.LedName).NotEmpty().WithMessage("El nombre es obligatorio");
        }
    }
}
