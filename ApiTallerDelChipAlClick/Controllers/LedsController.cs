using ApiTallerDelChipAlClick.DtoModels;
using ApiTallerDelChipAlClick.Services;
using ApiTallerDelChipAlClick.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiTallerDelChipAlClick.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class LedsController : ControllerBase
    {
        private IValidator<LedsInsertDto> _ledsInsertValidator;
        private IValidator<LedsUpdateDto> _ledsUpdateValidator;
        private ICommonService<LedsDto, LedsInsertDto, LedsUpdateDto> _ledsService;
        public LedsController([FromKeyedServices("LedsService")] ICommonService<LedsDto, LedsInsertDto, LedsUpdateDto> ledsServices,
            IValidator<LedsInsertDto> ledsInsertValidator,
            IValidator<LedsUpdateDto> ledsUpdateValidator) 
        {
            _ledsService = ledsServices;
            _ledsInsertValidator = ledsInsertValidator;
            _ledsUpdateValidator = ledsUpdateValidator; 
        }

        [HttpPost]
        public async Task<ActionResult<LedsDto>> Add(LedsInsertDto ledsInsertDto) 
        {
            var validationResult = await _ledsInsertValidator.ValidateAsync(ledsInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            if (!_ledsService.Validate(ledsInsertDto))
            {
                return BadRequest(_ledsService.Errors);
            }

            var LedsDto = await _ledsService.Add(ledsInsertDto);

            return Ok(LedsDto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<LedsDto>> Delete(int id)
        {
            var ledDto = await _ledsService.Delete(id);
            return ledDto == null ? NotFound() : Ok(ledDto);
        }
        [HttpGet]
        public async Task<IEnumerable<LedsDto>> Get() =>
            await _ledsService.Get();
        [HttpGet("{id}")]
        public async Task<ActionResult<LedsDto>> GetById(int id)
        {
            var ledsDto = await _ledsService.GetById(id);

            return ledsDto == null ? NotFound() : Ok(ledsDto);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<LedsDto>> Update(int id, LedsUpdateDto ledsUpdateDto)
        {
            var validationResult = await _ledsUpdateValidator.ValidateAsync(ledsUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var ledsDto = await _ledsService.Update(id, ledsUpdateDto);

            return ledsDto == null ? NotFound() : Ok(ledsDto);
        }
    }
}
