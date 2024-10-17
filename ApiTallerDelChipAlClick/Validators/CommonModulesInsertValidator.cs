using ApiTallerDelChipAlClick.DtoModels;
using FluentValidation;

namespace ApiTallerDelChipAlClick.Validators
{
    public class CommonModulesInsertValidator : AbstractValidator<CommonModulesInsertDto>
    {
        public CommonModulesInsertValidator() 
        {
            RuleFor(x => x.ModuleName).NotEmpty().WithMessage("El nombre es obligatorio");
        }
    }
}
