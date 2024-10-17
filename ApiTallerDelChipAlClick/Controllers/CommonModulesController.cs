using ApiTallerDelChipAlClick.DtoModels;
using ApiTallerDelChipAlClick.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTallerDelChipAlClick.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class CommonModulesController : ControllerBase
    {
        private IValidator<CommonModulesInsertDto> _commonModulesInsertValidator;
        private IValidator<CommonModulesUpdateDto> _commonModulesUpdateValidator;
        private ICommonService<CommonModulesDto, CommonModulesInsertDto, CommonModulesUpdateDto> _commonModulesService;
        public CommonModulesController([FromKeyedServices("CommonModulesService")] ICommonService<CommonModulesDto, CommonModulesInsertDto, CommonModulesUpdateDto> commonModulesServices,
            IValidator<CommonModulesInsertDto> commonInsertValidator,
            IValidator<CommonModulesUpdateDto> commonUpdateValidator) 
        {
            _commonModulesService = commonModulesServices;
            _commonModulesInsertValidator = commonInsertValidator;
            _commonModulesUpdateValidator = commonUpdateValidator;
        }
        [HttpPost]
        public async Task<ActionResult<CommonModulesDto>> Add(CommonModulesInsertDto commonModulesInsertDto)
        {
            var validationResult = await _commonModulesInsertValidator.ValidateAsync(commonModulesInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if(!_commonModulesService.Validate(commonModulesInsertDto))
            {
                return BadRequest(_commonModulesService.Errors);
            }

            var CommonModulesDto = await _commonModulesService.Add(commonModulesInsertDto);

            return Ok(CommonModulesDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommonModulesDto>> Delete(int id)
        {
            var commonModulesDto = await _commonModulesService.Delete(id);
            return commonModulesDto == null ? NotFound() : Ok(commonModulesDto);
        }
        [HttpGet]
        public async Task<IEnumerable<CommonModulesDto>> Get() =>
            await _commonModulesService.Get();
        [HttpGet("{id}")]
        public async Task<ActionResult<CommonModulesDto>> GetById(int id)
        {
            var commonModulesDto = await _commonModulesService.GetById(id);

            return commonModulesDto == null ? NotFound() : Ok(commonModulesDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<CommonModulesDto>> Update(int id, CommonModulesUpdateDto commonModulesUpdateDto)
        {
            var validationResult = await _commonModulesUpdateValidator.ValidateAsync(commonModulesUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var modulesDto = await _commonModulesService.Update(id, commonModulesUpdateDto);

            return modulesDto == null ? NotFound() : Ok(modulesDto);
        }

    }
}
