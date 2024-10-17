using ApiTallerDelChipAlClick.DtoModels;
using FluentValidation;

namespace ApiTallerDelChipAlClick.Validators
{
    public class CommonModulesUpdateValidator : AbstractValidator<CommonModulesUpdateDto>
    {
        public CommonModulesUpdateValidator() 
        {
            RuleFor(x => x.ModuleName).NotEmpty().WithMessage("El nombre es obligatorio");
        }
    }
}
